using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;
using Model.Entity;

namespace Portal.Controllers.Api
{
    public class OrderController : BaseApiController
    {
       
        [HttpGet]
        public ApiMessage<string> IsFavorite(string id)
        {
            return null;
        }
    }
}