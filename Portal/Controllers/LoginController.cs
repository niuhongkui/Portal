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
    public class LoginController : BaseController
    {
        private readonly StaffBLL _bll;
        public LoginController()
        {
            _bll = new StaffBLL();
            IsChecked = false;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

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
                CacheHelper.SetCache(Encrypt.MD5(currentUser.Id + "_"+currentUser.UserType), currentUser);
                return Redirect("/home/index");
            }
            else
            {
                return Redirect("/login/index");
            }


        }

        public ActionResult LoginOut()
        {
            Session["user"] = null;
            CacheHelper.RemoveCache(Encrypt.MD5(UserInfo.Id + "_" + UserInfo.UserType));
            return View("index");
        }

        
    }


}