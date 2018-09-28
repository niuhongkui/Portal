using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Entity;
using Common;
using Newtonsoft.Json;

namespace Portal.Controllers
{
    public class LoginController : BaseController
    {
        private readonly OperatorBLL _bll;
        public LoginController()
        {
            _bll = new OperatorBLL();
            IsChecked = false;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginOn(Operator ope)
        {
            ope.PassWord = Encrypt.MD5(ope.PassWord);

            var user = _bll.LoginOn(ope);
            if (user.Success)
            {
                var currentUser = ToUser(user.Data);
                var strUser = JsonConvert.SerializeObject(currentUser);
                //页面session
                Session["user"] = strUser;
                //webapi登录验证用
                CacheHelper.SetCache(Encrypt.MD5(currentUser.UserName+"_"+currentUser.UserType), currentUser);
                return Redirect("/home/index");
            }
            else
            {
                return Redirect("/home/index");
            }


        }

        public ActionResult LoginOut()
        {
            Session["user"] = null;
            return View("Login/index");
        }

        private CurrentUser ToUser(Operator admin)
        {
            CurrentUser user = new CurrentUser();
            user.PassWord = admin.PassWord;
            user.UserName = admin.UserName;
            user.Name = admin.Name;
            user.UserType = admin.UserType;
            user.Phone = admin.Phone;
            user.Id = admin.Id;
            //foreach (var n in admin.Roles)
            //{
            //    foreach (var l in n.Limits)
            //    {
            //        var limit = new Limit();
            //        limit.ClassName = l.ClassName;
            //        limit.Url = l.Url;
            //        limit.Name = l.Name;
            //        limit.PId = l.PId;
            //        limit.Level = l.Level;
            //        limit.Id = l.Id;

            //        user.Limits.Add(limit);
            //    }
            //}
            return user;
        }
    }


}