using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;

namespace Portal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductController : BaseController
    {
        private readonly ProductBLL _proBll = new ProductBLL();
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
        public ActionResult Edit(string id)
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
            parm.Id = UserInfo.StationId;
            var json = _proBll.List(parm);
            return Json(json);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Get(string id)
        {
            var json = _proBll.Get(id);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}