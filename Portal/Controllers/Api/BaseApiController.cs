using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Portal.Filter;

namespace Portal.Controllers.Api
{
    [RequestAuthorize]
    public class BaseApiController : ApiController
    {
    }
}
