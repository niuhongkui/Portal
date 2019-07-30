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
using System.IO;
using System.Web;

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
            if (api.Success)
            {
                var key = Encrypt.MD5(UserInfo.Id + "_用户");
                var currentUser = (CurrentUser)CacheHelper.GetCache(key);
                currentUser.UserName = user.UserName;
                currentUser.ImageUrl = user.ImgUrl;
                CacheHelper.SetCache(key, currentUser, new TimeSpan(0, 30, 0));
            }
            return api;
        }

        [HttpPost]
        public ApiMessage<string> UploadImg()
        {
            var request = System.Web.HttpContext.Current.Request;
            var res = new ApiMessage<string>();
            var files = request.Files;
            string[] limitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
            if (files.Count > 0)
            {
                var file = files[0];
                var name = file.FileName;
                //获取后缀名
                string namejpg = Path.GetExtension(name).ToUpper();
                //判断是否符合要求
                if (!limitPictureType.Contains(namejpg) && file.ContentLength > 0)
                {
                    res.Success = false;
                    res.Msg = "图片格式错误";
                    return res;
                }
                var tempPath = $"/images/" + UserInfo.UserCode + "/";
                //获取上传的路径
                var path = System.Web.Hosting.HostingEnvironment.MapPath(tempPath);
                //生成一个新的文件名
                var gid = Guid.NewGuid().ToString();
                var newname = gid + namejpg;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //上传
                file.SaveAs(path + newname);
                res.Data = tempPath + newname;
                //更新数据库 
                var user = new UserInfoEx();
                user.UserName = UserInfo.UserName;
                user.ImgUrl = res.Data;
                EditUser(user);

                return res;
            }

            return new ApiMessage<string>() { Success = false, Msg = "上传失败" };
        }
    }
}
