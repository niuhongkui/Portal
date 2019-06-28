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
        public ActionResult Edit()
        {
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
    }
}