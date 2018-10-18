using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Portal.Models;

namespace Portal.Controllers.Api
{
    public class CarApiController : BaseApiController
    {
        private readonly CarBLL _carBll = new CarBLL();

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Page<BankVModel> List([FromBody] BaseParm parm)
        {
            var list = _carBll.List(parm);
            var vmList = new Page<BankVModel>()
            {
                PageIndex = list.PageIndex,
                PageSize = list.PageSize,
                total = list.total
            };
            foreach (var item in list.rows)
            {
                var vm = new BankVModel(null);
                vmList.rows.Add(vm);
            }
            return vmList;
        }
    }
}
