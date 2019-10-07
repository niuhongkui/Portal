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
    public class UserInfoController : BaseController
    {
        private readonly UserInfoBLL _bll = new UserInfoBLL();
        // GET: UserInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Get(string id)
        {
            var res = _bll.Get(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult List(BaseParm parm)
        {
            var json = _bll.List(parm);
            return Json(json);
        }

        public JsonResult Save(userinfo user)
        {
            if (string.IsNullOrEmpty(user.ImgUrl))
            {
                user.ImgUrl = "";
            }
            var json = _bll.Save(user);

            return Json(json);
        }
        public JsonResult Delete(string id)
        {
            var json = _bll.Delete(id);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}