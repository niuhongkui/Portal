using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entity;
using Common;

namespace DAL
{
    public class OperatorDAL
    {
        private DB _db = new DB();

        public ApiMessage<Operator> LoginOn(Operator user)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select * from Operator ");
            strSql.Where("UserName=@0  and IsDelete=0", user.UserName);
            strSql.Where("PassWord=@0 ",  user.PassWord);
            var LoginUser = _db.Fetch<Operator>(strSql).FirstOrDefault();
            if (string.IsNullOrEmpty(LoginUser?.Id))
            {
                return new ApiMessage<Operator> { MsgCode = "400", Msg = "用户不存在或密码有误", Success = false };
            }
            return new ApiMessage<Operator> { Data = LoginUser };
        }
    }
}
