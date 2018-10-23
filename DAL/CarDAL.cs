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
            strSql.Where("b.IsDelete='0' and o.usertype=2");
            strSqlCount.Where("b.IsDelete='0' and o.usertype=2");
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

        public ApiMessage<Car> Detail(string id)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select * from Car b left join operator o on o.PKId=b.Id");
            strSql.Where("b.IsDelete='0' and o.usertype=2  AND b.Id=@0", id);
            var b = _db.Fetch<Car, Operator>(strSql);
            var api = new ApiMessage<Car>();
            api.Data = b.FirstOrDefault();
            return api;

        }

        public ApiMessage<string> Delete(string id)
        {
            ApiMessage<string> msg = new ApiMessage<string>();
            var obj = Car.FirstOrDefault("where Id=@0", id);
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

        public ApiMessage<string> Save(Car b, int type = 0)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                car copy = b;
                if (type == 0)
                {
                    _db.Insert(copy);
                    _db.Insert(b.Operator);
                }
                else
                {
                    copy.Update();
                    b.Operator.Update();
                }

                scope.Complete();
            }
            return new ApiMessage<string>();
        }

    }
}
