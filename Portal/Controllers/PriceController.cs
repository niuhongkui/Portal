using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;
using Model.Entity;
using Newtonsoft.Json;

namespace Portal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PriceController : BaseController
    {
        private readonly PriceBLL _bll = new PriceBLL();
        // GET: Price
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Index(string name)
        {
            ViewBag.Name = name;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proid"></param>
        /// <returns></returns>
        public ActionResult Detail(string id,string proid)
        {
            ViewBag.Id = id;
            ViewBag.proId = proid;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public JsonResult List(BaseParm parm)
        {
            var list = _bll.List(parm);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Save(PriceEx model)
        {
            var res = _bll.Save(model, UserInfo);
            return Json(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="px"></param>
        /// <returns></returns>
        public JsonResult Del(PriceEx px)
        {
            px.StaffID = UserInfo.Id;
            px.StaffName = UserInfo.UserName;
            var res = _bll.Del(px);
            return Json(res);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proid"></param>
        /// <returns></returns>
        public JsonResult Get(string id, string proid)
        {
            var res = _bll.Get(id,proid);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}