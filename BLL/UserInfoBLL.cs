using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model;

namespace BLL
{
    public class UserInfoBLL
    {
        private readonly UserInfoDAL _dal = new UserInfoDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<userinfo> LoginOn(userinfo user)
        {
            return _dal.LoginOn(user);
        }
    }
}
