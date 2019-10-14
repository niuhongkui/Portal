using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

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

        public ApiMessage<bool> Edit(swiper model)
        {
            //var res=
            return bll.Edit(model);
        }

        public ApiMessage<swiper> List(swiper model)
        {
            return bll.List(model);
        }
    }
}