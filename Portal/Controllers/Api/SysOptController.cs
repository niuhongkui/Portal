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
    public class SysOptController : BaseApiController
    {
        private SysOptionBLL bll = new SysOptionBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<List<sysoption>> GetMember(string id)
        {
            return bll.GetOptByType("member");
        }

        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<string> Version(string id)
        {
            return bll.GetVersion();
        }

    }
}
