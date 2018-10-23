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
        public Page<CarVModel> List([FromBody] BaseParm parm)
        {
            var list = _carBll.List(parm);
            var vmList = new Page<CarVModel>()
            {
                PageIndex = list.PageIndex,
                PageSize = list.PageSize,
                total = list.total
            };
            foreach (var item in list.rows)
            {
                var vm = new CarVModel(item);
                vmList.rows.Add(vm);
            }
            return vmList;
        }

        /// <summary>
        /// 明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<CarVModel> Detail(string id)
        {
            var data = _carBll.Detail(id);
            var api = new ApiMessage<CarVModel>();
            api.Data = new CarVModel(data.Data);
            return api;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<string> Delete(string id)
        {
            return _carBll.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<string> Save(CarVModel parm)
        {
            return _carBll.Save(parm.ToModel());
        }
    }
}
