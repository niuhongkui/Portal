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
    public class StoreController : BaseController
    {
        private readonly StoreBLL _bll = new StoreBLL();
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
        /// <param name="parm"></param>
        /// <returns></returns>
        public JsonResult List(BaseParm parm)
        {
            var json = _bll.List(parm);
            return Json(json);
        }
      
    }
}