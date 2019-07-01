using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;

namespace Portal.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductBLL _typeBll = new ProductBLL();
        // GET: Product
        public ActionResult Index()
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
            var json = _typeBll.List(parm);
            return Json(json);
        }
    }
}