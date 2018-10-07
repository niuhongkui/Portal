using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class BankOrderController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: BankOrder
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Summary()
        {
            return View();
        }
        
    }
}