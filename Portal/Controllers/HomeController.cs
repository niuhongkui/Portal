﻿using System;
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