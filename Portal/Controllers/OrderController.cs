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
    public class OrderController : BaseController
    {
        private OrderBLL bll=new OrderBLL();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public JsonResult List(BaseParm parm)
        {
            var list = bll.ListByPage(parm);
            return Json(list);
        }

        public JsonResult GetOrder(string id)
        {
            var list = bll.GetOrder(id);
            return Json(list);
        }

        public JsonResult PickUp(string id)
        {
            var res = bll.PickUp(id);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
    }
}