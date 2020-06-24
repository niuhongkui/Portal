using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace DAL
{
    public class UserInfoDAL
    {
        private DB _db = new DB();

        public ApiMessage<userinfo> LoginOn(userinfo user)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select * from userinfo f  ");
            strSql.Where("f.UserCode=@0  and f.IsActive=1", user.UserCode);
            strSql.Where("f.PassWord=@0 ", user.PassWord);
            var loginUser = _db.Fetch<userinfo>(strSql).FirstOrDefault();
            if (string.IsNullOrEmpty(loginUser?.ID))
            {
                return new ApiMessage<userinfo> { MsgCode = "400", Msg = "用户不存在或密码有误", Success = false };
            }
            return new ApiMessage<userinfo> { Data = loginUser };
        }

        public ApiMessage<userinfo> LoginByToken(string userCode)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select * from userinfo f  ");
            strSql.Where("f.UserCode=@0  and f.IsActive=1", userCode);
            var loginUser = _db.Fetch<userinfo>(strSql).FirstOrDefault();
            if (string.IsNullOrEmpty(loginUser?.ID))
            {
                return new ApiMessage<userinfo> { MsgCode = "400", Msg = "用户不存在或密码有误", Success = false };
            }
            return new ApiMessage<userinfo> { Data = loginUser };
        }

        public ApiMessage<string> Add(userinfo user)
        {
            var row = user.Insert();
            return new ApiMessage<string> { Msg = "保存成功" };
        }

        public ApiMessage<userinfo> GetByPhone(string strPhone)
        {
            var model = userinfo.FirstOrDefault(" where UserCode=@0", strPhone);
            return model == null ? new ApiMessage<userinfo> { Success = false } : new ApiMessage<userinfo>() { Data = model };
        }

        public ApiMessage<string> Edit(userinfo user)
        {
            var row = user.Update();
            return row > 0 ? new ApiMessage<string> { Msg = "保存成功" } : new ApiMessage<string> { Success = false, Msg = "保存失败" };
        }
        public ApiMessage<userinfo> Get(string id)
        {
            var api = new ApiMessage<userinfo> { Data = userinfo.FirstOrDefault(" where id=@0", id) };
            return api;
        }

        public Page<userinfo> List(BaseParm parm)
        {
            var page = new Page<userinfo>(parm);
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
            var list = userinfo.Page(parm.page, parm.rows, strSql.ToString(), parm);
            page.rows = list.Items;
            page.total =(int)list.TotalItems;

            return page;
        }

        public ApiMessage<string> Delete(string id)
        {
            var rows = userinfo.Delete("where id=@0", id);
            if (rows > 0)
                return new ApiMessage<string> { Msg = "删除成功" };
            else
                return new ApiMessage<string> { Success = false, Msg = "删除失败" };
        }


    }
}
