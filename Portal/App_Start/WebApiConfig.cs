using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Cors;
using System.Configuration;

namespace Portal
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            var allowOrigins = ConfigurationManager.AppSettings["cors:allowOrigins"];
            var allowHeaders = ConfigurationManager.AppSettings["cors:allowHeaders"];
            var allowMethods = ConfigurationManager.AppSettings["cors:allowMethods"];
            // Web API 配置和服务
            config.EnableCors(new EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods));
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
