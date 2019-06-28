using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }
    }
}