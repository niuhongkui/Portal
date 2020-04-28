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
    public class PriceDAL
    {
        private DB _db = new DB();

        public Page<PriceEx> List(BaseParm parm)
        {
            var page = new Page<PriceEx>(parm);
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append(@"SELECT p.Code,p.Name,p1.Name AS PName,p1.Code PCode,IFNULL(p2.Price,0) Price,IFNULL(p2.MemberPrice,0) MemberPrice,p1.TypeCode,p1.TypeName,p2.ID,p2.CreateDate,p1.ID ProductID ,p.ID UnitID FROM 
              productunit p LEFT JOIN product p1 ON 1=1
              LEFT JOIN price p2 ON p.Code=p2.UnitCode AND p1.ID=p2.ProductID");
            strSql.Where(" p1.ID=@0", parm.Id);
            page.rows = _db.Fetch<PriceEx>(strSql);
            return page;
        }

        public bool Save(List<productprice> list)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                foreach (var p in list)
                {
                    if (string.IsNullOrEmpty(p.ID))
                    {
                        p.ID = Guid.NewGuid().ToString();
                        _db.Insert(p);
                    }
                    else
                    {
                        _db.Update(p);
                    }
                }
                scope.Complete();
                return true;
            }
        }
    }
}
