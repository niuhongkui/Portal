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


        //[AllowAnonymous]
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<string> Delete(string id)
        {
            return _bankBll.Delete(id);
        }
        /// <summary>
        /// 明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<BankVModel> Detail(string id)
        {
            var data = _bankBll.Detail(id);
            var api = new ApiMessage<BankVModel>();
            api.Data = new BankVModel(data.Data);
            return api;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<string> Save(BankVModel parm)
        {
            return _bankBll.Save(parm.ToModel());
        }

        /// <summary>
        /// 选银行
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<object> BankKeyValue()
        {
            var list = _bankBll.KeyValue();
            List<object> kv = new List<object>();
            list.ForEach(n=> {
                kv.Add(new { n.Id, n.Name });
            });
            return kv;
        }
    }
}
