using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace Portal.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected CurrentUser UserInfo
        {
            get
            {
                var user = Session["user"];
                return user == null ? null : JsonConvert.DeserializeObject<CurrentUser>(user.ToString());
            }
        }

        public bool IsChecked { set; get; }
        public BaseController()
        {
            IsChecked = true;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!IsChecked)
                return;

            if (UserInfo == null)
                filterContext.Result = new RedirectResult("/login/index");
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Controller.ViewBag.User = UserInfo;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            LogHelper.WriteLog(filterContext.Exception.Message, LogHelper.LogType.Error);
            //设置为true阻止golbal里面的错误执行
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("/home/error");
        }
    }
}