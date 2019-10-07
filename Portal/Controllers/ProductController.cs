using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;
using Model;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUnit()
        {
            var json = _proBll.GetUnit();
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult Save(product model)
        {
            model.CreateDate=DateTime.Now;
            var json = _proBll.Edit(model);
            return Json(json);
        }
    }
}