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
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT p.*,p1.`Code` ,p1.`Name` ,p1.TypeID,p1.TypeName  FROM product p1  LEFT JOIN  productprice p on p.ProductID =p1.ID ");
            var strSqlCount = new StringBuilder();
            strSqlCount.Append("SELECT count(1)  FROM product p1  LEFT JOIN  productprice p on p.ProductID =p1.ID  WHERE p1.IsActive=1");
            strSql.Append(" WHERE p1.IsActive=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND p1.Name like @Name");
                strSqlCount.Append(" AND p1.Name like @Name");
            }
            if (!string.IsNullOrEmpty(parm.Type))
            {
                strSql.Append(" AND p1.TypeName like @Type");
                strSqlCount.Append(" AND p1.TypeName like @Type");
            }
            page.rows = _db.Fetch<PriceEx>(strSql.ToString(),parm)
               .Take(parm.rows)
               .Skip(parm.index * parm.rows)
               .ToList();
            page.total =
                _db.FirstOrDefault<int>(strSqlCount.ToString(), parm);
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
