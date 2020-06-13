using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Common;
using Model;
using Model.Entity;

namespace Portal.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class InStoreController : BaseController
    {
        private readonly InStoreBLL _proBll = new InStoreBLL();
        // GET: Product
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
        public ActionResult Detail()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public JsonResult List(BaseParm parm)
        {
            var json = _proBll.List(parm);
            return Json(json);
        }

        public JsonResult Save(instore model)
        {
            model.StaffID = UserInfo.Id;
            model.StaffName = UserInfo.UserName;
            model.CreateDate = DateTime.Now;

            var res = _proBll.Save(model);
            return Json(res);

        }

        

        

        
    }
}