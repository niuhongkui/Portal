using BLL;
using Common;
using Model;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Portal.Controllers.Api
{
    public class BankApiController : ApiController
    {
        private readonly BankBLL _bankBll = new BankBLL();

        [HttpPost]
        public Page<Bank> List(BaseParm parm)
        {
            return _bankBll.List(parm);
        }

        public string test(string id)
        {
            return id;
        }
    }
}
