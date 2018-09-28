using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {

            ViewBag.Auth = Encrypt.MD5(UserInfo.Id + "_" + UserInfo.UserType);
            return View();
        }

        public ActionResult Default()
        {
            return View();
        }


        public ActionResult Error() {
            return View();
        }

        
    }
}