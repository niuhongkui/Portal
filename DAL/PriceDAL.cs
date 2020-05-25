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
            strSql.Append(@"SELECT p.*,p1.`Code` ,p1.`Name` ,p1.TypeID,p1.TypeName  FROM productprice p INNER JOIN product p1 on p.ProductID =p1.ID WHERE p1.IsActive=1");
            //strSql.Where(" p1.ID=@0", parm.Id);
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
