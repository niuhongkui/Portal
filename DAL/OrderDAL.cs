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
    }
}
