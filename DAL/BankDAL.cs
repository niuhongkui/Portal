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
            strSql.Where("b.IsDelete='0'");
            strSqlCount.Where("b.IsDelete='0'");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                strSql.Where("b.Name LIKE  concat('%',@0,'%')", parm.Name);
                strSqlCount.Where("b.Name LIKE  concat('%',@0,'%')", parm.Name);
            }
            page.rows = _db.Fetch<Bank, Operator>(strSql).Skip(parm.rows * (parm.page - 1)).Take(parm.rows).ToList();
            page.PageSize = parm.rows;
            page.PageIndex = parm.page;
            page.total = _db.FirstOrDefault<int>(strSqlCount);
            return page;
        }

        public ApiMessage<string> Delete(string id)
        {
            ApiMessage<string> msg = new ApiMessage<string>();
            var obj = Bank.FirstOrDefault("where Id=@0", id);
            if (obj != null)
            {
                obj.IsDelete = true;
                var row = obj.Update();
                if (row > 0)
                {
                    msg.Success = true;
                    msg.Msg = "删除成功";
                }
                else
                {
                    msg.Success = false;
                    msg.Msg = "删除失败";
                }
            }
            else
            {
                msg.Success = false;
                msg.Msg = "数据不存在";
            }

            return msg;

        }


        public ApiMessage<Bank> Detail(string id)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select * from bank b left join operator o on o.PKId=b.Id");
            strSql.Where("b.IsDelete='0' AND b.Id=@0", id);
            var b = _db.Fetch<Bank, Operator>(strSql);
            var api = new ApiMessage<Bank>();
            api.Data = b.FirstOrDefault();
            return api;

        }
    }
}
