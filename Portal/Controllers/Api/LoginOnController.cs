using BLL;
using Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Portal.Models;

namespace Portal.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginOnController : BaseApiController
    {
        private OperatorBLL _bll = new OperatorBLL();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<object> LoginOn(Operator user)
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
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<string> LoginOut()
        {
            var key = Encrypt.MD5(UserInfo.Id + "_" + UserInfo.UserType);
            var userInfo = CacheHelper.GetCache(key);
            if (userInfo == null) return new ApiMessage<string>() { Success = false, Msg = "用户没有登录" };
            CacheHelper.RemoveCache(key);
            return new ApiMessage<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiMessage<string> GetCode(string id)
        {
            var api = new ApiMessage<string>();
            api.Data = DateCode.GetCode(id);
            return api;
        }
    }
}
