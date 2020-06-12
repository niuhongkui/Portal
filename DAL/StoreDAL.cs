using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StoreDAL
    {
        private DB _db = new DB();

        public Page<store> List(BaseParm parm) {
            var page = new Page<store>(parm);
            var strSql = new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND ProductName like @Name");
            }
            page.rows = store.Fetch(strSql.ToString(), parm)
                    .Take(parm.rows)
                    .Skip(parm.index * parm.rows)
                    .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from store " + strSql, parm);
            return page;
        }
    }
}
