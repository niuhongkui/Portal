using Common;
using Model;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BankDAL
    {
        private DB _db = new DB();
        public int Add(Bank b)
        {
            var id = b.Insert();
            b.Operator.PKId = id.ToString();
            b.Operator.Insert();
            return (int)id;
        }

        public Page<Bank> List(BaseParm parm)
        {
            var page = new Page<Bank>();
            var strSql = PetaPoco.Sql.Builder;
            var strSqlCount = PetaPoco.Sql.Builder;
            strSql.Append("select * from bank b left join operator o on o.PKId=b.Id");
            strSqlCount.Append("select count(1) from bank b left join operator o on o.PKId=b.Id");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                strSql.Where("b.Name=@0", parm.Name);
                strSqlCount.Where("b.Name=@0", parm.Name);
            }
            page.rows = _db.Fetch<Bank, Operator>(strSql).Skip(parm.Rows * parm.Index).Take(parm.Rows).ToList();
            page.PageSize = parm.Rows;
            page.PageIndex = parm.Index;
            page.total = _db.FirstOrDefault<int>(strSqlCount);
            return page;
        }

    }
}
