using Common;
using DAL;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OperatorBLL
    {
        private OperatorDAL _dal = new OperatorDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userType">1系统管理员，2银行人员，3押运人员</param>
        /// <returns></returns>
        public ApiMessage<Operator> LoginOn(Operator user, int userType)
        {
            user.UserType = userType.ToString();
            return _dal.LoginOn(user);
        }
    }
}
