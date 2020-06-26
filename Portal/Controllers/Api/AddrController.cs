using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Portal.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class AddrController : BaseApiController
    {
        private AddrBLL bll = new AddrBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<address> GetDef()
        {
            return bll.GetDefault(UserInfo.Id);
        }
    }
}
