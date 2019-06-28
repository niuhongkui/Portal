using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;

namespace Portal.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductController : BaseApiController
    {
        private readonly ProductTypeBLL _typeBll =new ProductTypeBLL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Page<producttype> GetType(BaseParm parm)
        {
            return _typeBll.List(parm);
        }
    }
}
