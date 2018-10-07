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
using Portal.Filter;
using Portal.Models;

namespace Portal.Controllers.Api
{
    public class BankApiController : BaseApiController
    {
        private readonly BankBLL _bankBll = new BankBLL();

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Page<BankVModel> List([FromBody] BaseParm parm)
        {
            var list = _bankBll.List(parm);
            var vmList = new Page<BankVModel>()
            {
                PageIndex = list.PageIndex,
                PageSize = list.PageSize,
                total = list.total
            };
            foreach (var item in list.rows)
            {
                var vm = new BankVModel(item);
                vmList.rows.Add(vm);
            }
            return vmList;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<string> Delete(string id)
        {
            return _bankBll.Delete(id);
        }


        [HttpGet]
        //[AllowAnonymous]
        public string test(string id)
        {
            return id;
        }
    }
}
