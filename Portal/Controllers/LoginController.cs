using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Entity;
using Common;
using Model;
using Newtonsoft.Json;
using Portal.Models;

namespace Portal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : BaseController
    {
        private readonly StaffBLL _bll;
        /// <summary>
        /// 
        /// </summary>
        public LoginController()
        {
            _bll = new StaffBLL();
            IsChecked = false;
        }

        /// <summary>
        /// view页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="ope"></param>
        /// <returns></returns>
        public ActionResult LoginOn(staff ope)
        {
            ope.PassWord = Encrypt.MD5(ope.PassWord);

            var user = _bll.LoginOn(ope);
            if (user.Success)
            {
                var currentUser = UserVModel.FormatUser(user.Data);
                var strUser = JsonConvert.SerializeObject(currentUser);
                //页面session
                Session["user"] = strUser;
                //webapi登录验证用
                CacheHelper.SetCache(Encrypt.MD5(currentUser.Id + "_" + currentUser.UserType), currentUser);
                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Login/Index?msg=" + user.Msg);
            }


        }

        /// <summary>
        /// 退出接口
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            Session["user"] = null;
            CacheHelper.RemoveCache(Encrypt.MD5(UserInfo.Id + "_" + UserInfo.UserType));
            return View("index");
        }


    }


}