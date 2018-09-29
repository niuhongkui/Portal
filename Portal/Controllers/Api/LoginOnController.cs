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
        public ApiMessage<string> LoginOn(Operator user)
        {
            user.PassWord = Encrypt.MD5(user.PassWord);
            var userData = _bll.LoginOn(user);
            var outData = new ApiMessage<string>
            {
                Success = userData.Success,
                Msg = userData.Msg,
                MsgCode = userData.MsgCode
            };
            if (!userData.Success) return outData;
            var currentUser = UserVModel.FormatUser(userData.Data);
            outData.Data = Encrypt.MD5(currentUser.Id + "_" + currentUser.UserType);
            CacheHelper.SetCache(outData.Data, currentUser,new TimeSpan(0,0,30));
            return outData;
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<string> LoginOut()
        {
            var key= Encrypt.MD5(UserInfo.Id + "_" + UserInfo.UserType);
            var userInfo= CacheHelper.GetCache(key);
            if (userInfo == null) return new ApiMessage<string>() {Success = false, Msg = "用户没有登录"};
            CacheHelper.RemoveCache(key);
            return new ApiMessage<string>() ;
        }
    }
}
