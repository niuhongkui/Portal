using BLL;
using Common;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Portal.Controllers.Api
{
    public class LoginOnController : ApiController
    {
        private OperatorBLL _bll = new OperatorBLL();
       
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<Operator> LoginOn([FromBody]Operator user)
        {
            return null;
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<Operator> LoginOut([FromBody]Operator user)
        {
            return null;
        }
    }
}
