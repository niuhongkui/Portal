﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemController : BaseController
    {
        // GET: System
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
       
    }
}