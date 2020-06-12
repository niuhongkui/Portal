using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class StationController : BaseController
    {

        private readonly StaffBLL _bll = new StaffBLL();
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
        public ActionResult Detail(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Addr()
        {
            return View();
        }

        public JsonResult List(BaseParm parm)
        {
            var json = _bll.List(parm);
            return Json(json);
        }
        public JsonResult Get(string id)
        {
            var res = _bll.Get(id);
            return Json(res,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(staff user)
        {
            var res = _bll.Save(user);
            return Json(res); ;

        }
        public JsonResult Delete(string id)
        {
            return null;
        }
    }
}