using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;
using Model;
using Model.Entity;

namespace Portal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class InStoreController : BaseController
    {
        private readonly InStoreBLL _proBll = new InStoreBLL();
        // GET: Product
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            ViewBag.Id = id;
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public JsonResult List(BaseParm parm)
        {
            var json = _proBll.List(parm);
            return Json(json);
        }
        


        

        

        
    }
}