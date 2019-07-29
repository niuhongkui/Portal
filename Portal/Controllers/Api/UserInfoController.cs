using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;
using Model.Entity;
using Portal.Filter;
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
            outData.Data = new
            {
                Token = key,
                currentUser.UserName,
                currentUser.UserCode,
                currentUser.ImageUrl,
                currentUser.Id,
                currentUser.Phone
            };
            CacheHelper.SetCache(key, currentUser, new TimeSpan(0, 30, 0));
            return outData;
        }

        /// <summary>
        /// 登录 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<object> LoginByToken(userinfo user)
        {
            var userData = _bll.LoginByToken(user);
            var outData = new ApiMessage<object>
            {
                Success = userData.Success,
                Msg = userData.Msg,
                MsgCode = userData.MsgCode
            };
            if (!userData.Success) return outData;
            var currentUser = UserVModel.FormatUser(userData.Data);
            var key = Encrypt.MD5(currentUser.Id + "_" + currentUser.UserType);
            outData.Data = new
            {
                Token = key,
                currentUser.UserName,
                currentUser.UserCode,
                currentUser.ImageUrl,
                currentUser.Id,
                currentUser.Phone
            };
            CacheHelper.SetCache(key, currentUser, new TimeSpan(0, 30, 0));
            return outData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<string> VerifyCode(string strPhone)
        {
            var api = _bll.VerifyCode(strPhone);
            return api;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<string> Registered(UserInfoEx user)
        {
            var api = _bll.Registered(user);
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPhone"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<string> Re_VerifyCode(string strPhone)
        {
            var api = _bll.Re_VerifyCode(strPhone);
            return api;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<string> EditPassWord(UserInfoEx user)
        {
            var api = _bll.EditPassWord(user);
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<string> EditUser(UserInfoEx user)
        {
            user.ID = UserInfo.Id;
            user.UserCode = UserInfo.UserCode;
            var api = _bll.EditUser(user);
            return api;
        }

        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<string> UploadImg(UserInfoEx user)
        {
            var api = _bll.EditPassWord(user);
            return api;
        }
    }
}
