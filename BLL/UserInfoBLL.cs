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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<userinfo> LoginByToken(userinfo user)
        {
            var msgData = _dal.LoginByToken(user.UserCode);
            if (!msgData.Success)
                return msgData;
            var key = Encrypt.MD5(msgData.Data.ID + "_用户");
            return key == user.PassWord ? msgData : new ApiMessage<userinfo>() { Success = false, Msg = "用户不存在" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApiMessage<string> VerifyCode(string strPhone)
        {
            var api = new ApiMessage<string>() { Msg = "发送成功" };
            var gCode = CacheHelper.GetCache("GCode_" + strPhone);
            if (gCode != null)
            {
                api.Success = false;
                api.Msg = "已经发送过验证码,不能重复获取";
                return api;
            }
            gCode = CacheHelper.GetCache("VCode_" + strPhone);
            if (gCode == null)
            {
                var random = new Random().Next(100000, 999999);
                CacheHelper.SetCache("VCode_" + strPhone, random.ToString(), new TimeSpan(0, 10, 0));
                Message.Send(strPhone, random.ToString());
            }
            else
            {
                Message.Send(strPhone, gCode.ToString());
            }
            CacheHelper.SetCache("GCode_" + strPhone, strPhone, DateTime.Now.AddMinutes(3));
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApiMessage<string> Re_VerifyCode(string strPhone)
        {
            var api = new ApiMessage<string>();
            if (!_dal.GetByPhone(strPhone).Success) return VerifyCode(strPhone);
            api.Success = false;
            api.Msg = "手机号已注册";
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<string> Registered(UserInfoEx user)
        {
            var api = new ApiMessage<string>();

            var gCode = CacheHelper.GetCache("VCode_" + user.UserCode);
            if (_dal.GetByPhone(user.UserCode).Success)
            {
                api.Success = false;
                api.Msg = "手机号已注册";
                return api;
            }
            if (gCode == null)
            {
                api.Success = false;
                api.Msg = "验证码已过期";
                return api;
            }
            if (gCode.ToString() != user.VerifyCode)
            {
                api.Success = false;
                api.Msg = "验证码有误";
                return api;
            }
            var dbUser = new userinfo
            {
                ID = Guid.NewGuid().ToString(),
                IsActive = 1,
                IsMember = 0,
                PassWord = Encrypt.MD5(user.PassWord),
                CreateDate = DateTime.Now,
                Phone = user.UserCode,
                UserCode = user.UserCode,
                ImgUrl = "",
                UserName = "用户_" + new Random().Next(100000, 999999)
            };
            api = _dal.Add(dbUser);
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<string> EditPassWord(UserInfoEx user)
        {
            var api = new ApiMessage<string>();
            var gCode = CacheHelper.GetCache("VCode_" + user.UserCode);
            if (gCode == null)
            {
                api.Success = false;
                api.Msg = "验证码已过期";
                return api;
            }
            if (gCode.ToString() != user.VerifyCode)
            {
                api.Success = false;
                api.Msg = "验证码有误";
                return api;
            }
            var dbModel = _dal.GetByPhone(user.UserCode);
            if (!dbModel.Success)
            {
                api.Success = false;
                api.Msg = "账号不存在";
                return api;
            }
            var dbUser = dbModel.Data;
            dbUser.PassWord = Encrypt.MD5(user.PassWord);
            return _dal.Edit(dbUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<string> EditUser(UserInfoEx user)
        {
            var dbUser = _dal.Get(user.ID).Data;
            dbUser.UserName = user.UserName;
            dbUser.ImgUrl = user.ImgUrl;
            var api = _dal.Edit(dbUser);
            return api;
        }

        public Page<userinfo> List(BaseParm parm)
        {
            return _dal.List(parm);
        }

        public ApiMessage<userinfo> Get(string id)
        {

            return _dal.Get(id);
        }

        public ApiMessage<string> Save(userinfo user)
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

        public ApiMessage<string> Delete(string id)
        {
            return _dal.Delete(id);
        }
    }
}
