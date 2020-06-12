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
        public JsonResult Save(ProductEx model)
        {
            model.CreateDate=DateTime.Now;
            model.StaffID = UserInfo.Id;
            model.StaffName = UserInfo.UserName;

            var json = _proBll.Edit(model);
            return Json(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(string id)
        {
            var json = _proBll.Delete(id);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}