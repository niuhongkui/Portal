using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Common;

namespace Portal.Controllers
{
    public class SwiperController : BaseController
    {
        private SwiperBLL bll = new SwiperBLL();
        // GET: Swiper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public JsonResult Save(swiper model)
        {
            //var res=
            return Json(bll.Edit(model));
        }

        public JsonResult List(BaseParm parm)
        {
            var list= bll.List(parm);
            return Json(list);
        }

        public JsonResult Get(string id)
        {
            var res = bll.Get(id);
            
            return Json(res,JsonRequestBehavior.AllowGet);
        }
    }
}