﻿using System;
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
            page.rows = product.Fetch(strSql.ToString(), parm)
                .Take(parm.rows)
                .Skip(parm.index * parm.rows)
                .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from product " + strSql, parm);

            return page;
        }

        public ApiMessage<string> Delete(string id)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                try
                {
                    var rows = product.Delete(" where id=@0", id);
                    productimg.Delete("where productid=@0",id);
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

                        foreach (var item in t.Imgs)
                        {
                           var img=new productimg {
                                ID=Guid.NewGuid().ToString(),
                                ProductID=t.ID,
                                RowNO=0,
                                Url=item.url
                            };
                            img.Insert();
                        }
                    }
                    else
                    {
                        t.Update();
                        productimg.Delete("where ProductId=@0", t.ID);
                        foreach (var item in t.Imgs)
                        {
                            var img = new productimg
                            {
                                ID = Guid.NewGuid().ToString(),
                                ProductID = t.ID,
                                RowNO = 0,
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
            var imgs = productimg.Query("where ProductID=@0", m.ID);
            foreach (var item in imgs)
            {
                m.Imgs.Add(new ProImg { name = "", url = item.Url });
            }
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

        public ApiMessage<List<Goods>> GetGoods(BaseParm parm)
        {
            var page = new ApiMessage<List<Goods>>();
            var list =
                _db.Fetch<Goods>(
                    @"SELECT  p.ID,p.`Name` title,p.ImgUrl image,p.ImgUrl2 image2,p.ImgUrl3 image3,p.Sales sales,MIN(p1.Price) price,p.TypeCode 
                    FROM product p 
                    LEFT JOIN price p1 ON p.ID = p1.ProductID
                    LEFT JOIN producttype t on t.`Code`=p.TypeCode
                    WHERE p1.Price>0 AND p.IsActive=1  AND t.ID=@id
                    GROUP BY p.ID LIMIT @m,@n", new
                    {
                        id = parm.Id,
                        m = parm.index * parm.rows - parm.rows,
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
    }
}
