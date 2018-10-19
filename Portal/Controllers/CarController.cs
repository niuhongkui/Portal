﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}