using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace DAL
{
    public class StaffDAL
    {
        private DB _db = new DB();

        public ApiMessage<staff> LoginOn(staff user)
        {
            var strSql = PetaPoco.Sql.Builder;
            strSql.Append("select * from staff f left join station s on s.ID=f.StationID ");
            strSql.Where("f.UserCode=@0  and f.IsActive=1", user.UserCode);
            strSql.Where("f.PassWord=@0 and s.IsActive=1", user.PassWord);
            var loginUser = _db.Fetch<staff>(strSql).FirstOrDefault();
            if (string.IsNullOrEmpty(loginUser?.ID))
            {
                return new ApiMessage<staff> { MsgCode = "400", Msg = "用户不存在或密码有误", Success = false };
            }
            return new ApiMessage<staff> { Data = loginUser };
        }
    }


}
