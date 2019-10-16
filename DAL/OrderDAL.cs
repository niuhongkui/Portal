using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;
using Model.Entity;
using Newtonsoft.Json;

namespace DAL
{
    public class OrderDAL
    {
        private DB _db = new DB();

        public List<CartEx> GetPrice(List<PriceEx> p)
        {
            var list = _db.Query<CartEx>(
                @"SELECT p1.ProductID,p1.Price,p1.MemberPrice MPrice,p1.UnitName,p2.ID UnitID,p.Name ProductName,0 Amount,p.ImgUrl  FROM product p
                      LEFT JOIN price p1 ON p.ID=p1.ProductID
                      LEFT JOIN productunit p2 ON p1.UnitCode = p2.Code
                      WHERE p.ID in (@0) AND p2.ID in (@1) AND p.IsActive=1", p.Select(n => n.ProductID), p.Select(n => n.UnitID)).ToList();
            return list;
        }

        public ApiMessage<object> Save(OrderData model)
        {
            var api = new ApiMessage<object>();
            api.Success = false;
            using (var tran = new PetaPoco.Transaction(_db))
            {
                order om = model;
                om.Insert();

                foreach (var od in model.Detail)
                {
                    var node = JsonConvert.DeserializeObject<orderdetail>(JsonConvert.SerializeObject(od));
                    node.Insert();
                }
                tran.Complete();
                api.Success = true;
                api.Data = new { om.OrderNo, om.Money };
            }
            return api;
        }


        public Page<OrderData> List(BaseParm parm)
        {
            var page = new Page<OrderData>(parm);
            var strSql = new StringBuilder();
            strSql.Append("select * from  `order` where 1=1");
            strSql.Append(" And UserID=@Id");
            if (parm.Type != "全部")
            {
                strSql.Append(" AND State = @Type");
            }
            strSql.Append(" ORDER BY CreateDate DESC");
            var list = _db.Query<OrderData>(strSql.ToString(), parm)
                .Take(parm.rows)
                .Skip(parm.index * parm.rows)
                .ToList();
            if (list.Any())
            {
                var details = _db.Query<OrderDetail>(@"select o.*,p.ImgUrl from orderdetail o 
                                LEFT JOIN product p ON p.ID=o.ProductID
                                where o.orderno in (@0)", list.Select(n => n.OrderNo)).ToList();
                list.ForEach(n =>
                {
                    n.Detail = details.Where(m => m.OrderNo == n.OrderNo).ToList();
                });
            }

            page.rows = list;
            return page;
        }
    }
}
