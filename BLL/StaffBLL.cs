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
        /// <param name="userType">1系统管理员</param>
        /// <returns></returns>
        public ApiMessage<staff> LoginOn(staff user)
        {
            return _dal.LoginOn(user);
        }

        public Page<staff> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
        public ApiMessage<staff> Get(string id)
        {
            var api = new ApiMessage<staff>();
            var model= _dal.Get(id);
            if (model == null) {
                api.Success = false;
                api.Msg = "该账户不存在";
            }
            else
            {
                api.Data = model;
            }

            return api;
        }

        public ApiMessage<string>Save(staff user)
        {
            if (string.IsNullOrEmpty(user.ID))
            {
                user.ID = Guid.NewGuid().ToString();
                user.CreateDate = DateTime.Now;
                user.Phone = "";
                user.PassWord = Encrypt.MD5(user.PassWord);
                return _dal.Add(user);
            }
            else
            {
                if (user.PassWord.Split('-').Length < 5)
                {
                    user.PassWord = Encrypt.MD5(user.PassWord);
                }
                return _dal.Edit(user);
            }
        }
    }
}
