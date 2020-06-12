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
    public class StaffDAL
    {
        private DB _db = new DB();

        public ApiMessage<staff> LoginOn(staff user)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select f.* from staff f left join station s on s.ID=f.StationID ");
            strSql.Where("f.UserCode=@0  and f.IsActive=1", user.UserCode);
            strSql.Where("f.PassWord=@0 and s.IsActive=1", user.PassWord);
            var loginUser = _db.Fetch<staff>(strSql).FirstOrDefault();
            if (string.IsNullOrEmpty(loginUser?.ID))
            {
                return new ApiMessage<staff> { MsgCode = "400", Msg = "用户不存在或密码有误", Success = false };
            }
            return new ApiMessage<staff> { Data = loginUser };
        }

        public Page<staff> List(BaseParm parm)
        {
            var page = new Page<staff>(parm);
            var strSql = new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND UserName like @Name");
            }
            if (!string.IsNullOrEmpty(parm.Code))
            {
                parm.Code = "%" + parm.Code + "%";
                strSql.Append(" AND UserCode like @Code");
            }
            page.rows = staff.Fetch(strSql.ToString(), parm)
                    .Take(parm.rows)
                    .Skip(parm.index * parm.rows)
                    .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from staff " + strSql, parm);

            return page;

            return null;
        }

        public staff Get(string id)
        {
            return staff.FirstOrDefault("where id=@0", id);
        }

        public ApiMessage<string> Add(staff user) {
            user.Insert();
            return new ApiMessage<string>();

        }

        public ApiMessage<string> Edit(staff user)
        {
            var sign = user.Update();
            var api = new ApiMessage<string>();
            if (sign == 0) {
                api.Success = false;
                api.Msg = "操作失败";
            }
            return api;
        }
    }



}
