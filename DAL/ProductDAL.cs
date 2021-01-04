using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;
using Model.Entity;

namespace DAL
{
    public class ProductDAL
    {
        private DB _db = new DB();

        public Page<product> List(BaseParm parm)
        {

            var page = new Page<product>(parm);
            var strSql = new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND Name like @Name");
            }
            var list = product.Page(parm.page, parm.rows, strSql.ToString(), parm);
            page.rows = list.Items;
            page.total = (int)list.TotalItems;

            return page;
        }

        public ApiMessage<string> Delete(string id)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                try
                {
                    productimg.Delete("where productid=@0", id);
                    instore.Delete("where productid=@0", id);
                    store.Delete("where productid=@0", id);
                    var rows = product.Delete(" where id=@0", id);
                    var msg = new ApiMessage<string>();
                    if (rows > 0)
                    {
                        msg.Msg = "删除成功";
                        msg.Success = true;
                    }
                    else
                    {
                        msg.Msg = "删除失败";
                        msg.Success = false;
                    }
                    scope.Complete();
                    return msg;
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    throw e;
                }
            }
        }

        public ApiMessage<string> Edit(ProductEx t)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                try
                {
                    if (string.IsNullOrEmpty(t.ID))
                    {
                        t.ID = Guid.NewGuid().ToString();
                        t.Code = CodeNo.Get(CodeType.Product);
                        t.Insert();

                        for (int i = 0; i < t.Imgs.Count; i++)
                        {
                            var item = t.Imgs[i];
                            var img = new productimg
                            {
                                ID = Guid.NewGuid().ToString(),
                                ProductID = t.ID,
                                RowNO = i,
                                Url = item.url
                            };
                            img.Insert();
                        }
                    }
                    else
                    {
                        t.Update();
                        productimg.Delete("where ProductId=@0", t.ID);
                        for (int i = 0; i < t.Imgs.Count; i++)
                        {
                            var item = t.Imgs[i];
                            var img = new productimg
                            {
                                ID = Guid.NewGuid().ToString(),
                                ProductID = t.ID,
                                RowNO = i,
                                Url = item.url
                            };
                            img.Insert();
                        }
                    }
                    scope.Complete();
                    return new ApiMessage<string>();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    throw e;
                }

            }
        }

        public ApiMessage<ProductEx> Get(string id)
        {
            var json = new ApiMessage<ProductEx>();
            var m = _db.FirstOrDefault<ProductEx>(" where id=@0", id);
            if (m == null)
            {
                json.Success = false;
                json.Msg = "不存在";
                return json;
            }
            var imgs = productimg.Fetch("where ProductID=@0", m.ID).ToList();
            m.Imgs = imgs?.Select(n => new ProImg { url = n.Url })?.ToList();
            json.Data = m;
            return json;
        }

        public Page<productunit> GetUnit()
        {
            var page = new Page<productunit>();
            var list = productunit.Fetch(" where isactive=1");
            page.rows = list;
            page.total = list.Count;
            return page;
        }

        public ApiMessage<List<StoreGood>> GetGoods(BaseParm parm)
        {
            var page = new ApiMessage<List<StoreGood>>();
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT p.ID,p.Name,p.TypeID,p1.Url,p2.Price,p2.MemberPrice,p2.LimitNum,p2.UnitName,p2.UnitID,s.Amount ,s.OutAmount
                  FROM product p
                  LEFT JOIN(SELECT * FROM productimg n WHERE n.RowNO = 0)  p1 ON p.ID = p1.ProductID
                  LEFT JOIN productprice p2 ON p.ID = p2.ProductID
                  INNER JOIN store s ON p.ID = s.ProductID AND p2.UnitID = s.UnitID
                  LEFT JOIN producttype  p3 ON p.TypeID = p3.ID
                  WHERE p.IsActive = 1 AND s.Amount>0 ");
            if (!string.IsNullOrEmpty(parm.Name)) {
                strSql.Append(" And p.name like  CONCAT('%',@name,'%')");
            }
            if (parm.Type == "2")
            {
                if (parm.Code == "1")
                    strSql.Append(" ORDER BY p2.Price Desc");
                else
                    strSql.Append(" ORDER BY p2.Price ");
            }
            else if (parm.Type == "1")
            {
                strSql.Append(" ORDER BY s.OutAmount desc");
            }
            else
            {
                strSql.Append(" ORDER BY p3.OrderByNo, p3.CreateDate, p.OrderByNo ");
            }
            strSql.Append("  LIMIT @m,@n");
            var list =
                _db.Fetch<StoreGood>(strSql.ToString(), new
                {
                    name = parm.Name,
                    m = (parm.page - 1) * parm.rows,
                    n = parm.rows
                });
            page.Data = list;
            return page;
        }

        public ApiMessage<List<Goods>> GetLikeGoods(string userId)
        {
            var ids = _db.Query<string>(@"SELECT o.ProductID FROM  orderdetail o 
                          LEFT JOIN `order` o1 ON o.OrderNo = o1.OrderNo 
                          WHERE o1.UserID=@0
                          GROUP BY o.ProductID ORDER BY SUM(o.Amount) DESC", userId);
            var page = new ApiMessage<List<Goods>>();
            var list =
                _db.Fetch<Goods>(
                    @"SELECT  p.ID,p.`Name` title,p.ImgUrl image,p.ImgUrl2 image2,p.ImgUrl3 image3,p.Sales sales,MIN(p1.Price) price 
                    FROM product p 
                    LEFT JOIN price p1 ON p.ID = p1.ProductID
                    WHERE p1.Price>0 AND p.IsActive=1 AND p.ID IN (@0)
                    GROUP BY p.ID LIMIT 0,10", ids);
            page.Data = list;
            return page;
        }

        /// <summary>
        /// 指定商铺下的所有商品
        /// </summary>
        /// <returns></returns>
        public ApiMessage<List<StoreGood>> GetAllGood(BaseParm parm)
        {

            var api = new ApiMessage<List<StoreGood>>();
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT p.ID,p.Name,p.TypeID,p1.Url,p2.Price,p2.MemberPrice,p2.LimitNum,p2.UnitName,p2.UnitID,s.Amount 
                  FROM product p
                  LEFT JOIN(SELECT * FROM productimg n WHERE n.RowNO = 0)  p1 ON p.ID = p1.ProductID
                  LEFT JOIN productprice p2 ON p.ID = p2.ProductID
                  INNER JOIN store s ON p.ID = s.ProductID AND p2.UnitID = s.UnitID
                  LEFT JOIN producttype  p3 ON p.TypeID=p3.ID
                  WHERE p.IsActive = 1    ORDER BY p3.OrderByNo, p3.CreateDate, p.OrderByNo 
                ");
            api.Data = _db.Fetch<StoreGood>(strSql.ToString()).ToList();

            return api;
        }
        public ApiMessage<List<StoreGood>> GetAllGood2(BaseParm parm)
        {

            var api = new ApiMessage<List<StoreGood>>();
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT p.ID,p.Name,p.TypeID,p1.Url,p2.Price,p2.MemberPrice,p2.LimitNum,p2.UnitName,p2.UnitID,s.Amount 
                  FROM product p
                  LEFT JOIN(SELECT * FROM productimg n WHERE n.RowNO = 0)  p1 ON p.ID = p1.ProductID
                  LEFT JOIN productprice p2 ON p.ID = p2.ProductID
                  INNER JOIN store s ON p.ID = s.ProductID AND p2.UnitID = s.UnitID
                  LEFT JOIN producttype  p3 ON p.TypeID=p3.ID
                  WHERE p.IsActive = 1  AND p.TypeID=@Type  ORDER BY p3.OrderByNo, p3.CreateDate, p.OrderByNo  LIMIT @m ,@n
                ");
            api.Data = _db.Fetch<StoreGood>(strSql.ToString(), new
            {
                parm.Type,
                m = (parm.page) * parm.rows,
                n = parm.rows
            }).ToList();

            return api;
        }
    }
}
