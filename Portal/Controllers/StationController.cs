using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class StationController : BaseController
    {
        // GET: Station
        public ActionResult Index()
        {
            return View();
        }
    }
}