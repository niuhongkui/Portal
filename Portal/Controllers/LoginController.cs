using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoginOn()
        {
            return Json(null);
        }

        public JsonResult LoginOut()
        {
            return null;
        }
    }
}