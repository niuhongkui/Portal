using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using Newtonsoft.Json;
using Portal.Filter;

namespace Portal.Controllers.Api
{
    [RequestAuthorize]
    public class BaseApiController : ApiController
    {
        protected CurrentUser UserInfo
        {
            get
            {
                var auth = Request.Headers.Authorization?.Parameter;
                var user= (CurrentUser)CacheHelper.GetCache(auth);
                return user;
            }
        }
    }
}
