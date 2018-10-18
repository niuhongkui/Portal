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
    public class CarDAL
    {
        private DB _db = new DB();
        public Page<Car> List(BaseParm parm)
        {
            var page = new Page<Car>();
            var strSql = PetaPoco.Sql.Builder;
            var strSqlCount = PetaPoco.Sql.Builder;
            strSql.Append("select * from car b left join operator o on o.PKId=b.Id");
            strSqlCount.Append("select count(1) from car b left join operator o on o.PKId=b.Id");
            strSql.Where("b.IsDelete='0'");
            strSqlCount.Where("b.IsDelete='0'");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                strSql.Where("b.Name LIKE  concat('%',@0,'%')", parm.Name);
                strSqlCount.Where("b.Name LIKE  concat('%',@0,'%')", parm.Name);
            }
            page.rows = _db.Fetch<Car, Operator>(strSql).Skip(parm.rows * (parm.page - 1)).Take(parm.rows).ToList();
            page.PageSize = parm.rows;
            page.PageIndex = parm.page;
            page.total = _db.FirstOrDefault<int>(strSqlCount);
            return page;
        }
    }
}
