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
    }
}
