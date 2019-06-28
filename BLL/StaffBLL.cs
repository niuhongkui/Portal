using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model;
using Model.Entity;

namespace BLL
{
    public class StaffBLL
    {
        private StaffDAL _dal = new StaffDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userType">1系统管理员，2银行人员，3押运人员</param>
        /// <returns></returns>
        public ApiMessage<staff> LoginOn(staff user)
        {
            return _dal.LoginOn(user);
        }
    }
}
