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
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
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
            var list = _bll.List(parm);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult Save(List<PriceEx> list)
        {
            var res = _bll.Save(list,UserInfo);
            return Json(res);
        }
    }
}