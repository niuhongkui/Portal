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
    public class ProductTypeController : BaseController
    {
        private readonly ProductTypeBLL _typeBll = new ProductTypeBLL();
        // GET: ProductType
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
        public JsonResult GetType(BaseParm parm)
        {
            parm.Id = UserInfo.StationId;
            var json= _typeBll.List(parm);
            return Json(json);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(string id)
        {
            var json = _typeBll.Delete(id);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public JsonResult EditJson(producttype parm)
        {
            parm.CreateDate=DateTime.Now;
            parm.StaffID = UserInfo.Id;
            parm.StaffName = UserInfo.UserName;
            parm.StationID = UserInfo.StationId;
            var json = _typeBll.Edit(parm);
            return Json(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Get(string id)
        {
            var json = _typeBll.Get(id);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCategory()
        {
            return Json(_typeBll.GetCategory());
        }
    }
}