using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;
using Portal.Models;

namespace Portal.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoController : BaseApiController
    {
        private UserInfoBLL _bll = new UserInfoBLL();

        /// <summary>
        /// 登录 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<object> LoginOn(userinfo user)
        {
            user.PassWord = Encrypt.MD5(user.PassWord);
            var userData = _bll.LoginOn(user);
            var outData = new ApiMessage<object>
            {
                Success = userData.Success,
                Msg = userData.Msg,
                MsgCode = userData.MsgCode
            };
            if (!userData.Success) return outData;
            var currentUser = UserVModel.FormatUser(userData.Data);
            var key = Encrypt.MD5(currentUser.Id + "_" + currentUser.UserType);
            outData.Data = new { token = key, UserName = currentUser.UserName, UserType = currentUser.UserType };
            CacheHelper.SetCache(key, currentUser, new TimeSpan(0, 0, 30));
            return outData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApiMessage<string> VerifyCode(string strPhone)
        {
            var api=new ApiMessage<string>();


            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<string> Registered(userinfo user)
        {
            var api = new ApiMessage<string>();


            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiMessage<string> EditPassWord(userinfo user)
        {
            var api = new ApiMessage<string>();


            return api;
        }

    }
}
