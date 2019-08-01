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
        private readonly ProductTypeBLL _typeBll = new ProductTypeBLL();
        private readonly ProductBLL _proBll = new ProductBLL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Page<producttype> GetTypeList(BaseParm parm)
        {
            return _typeBll.List(parm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Page<product> GetList(BaseParm parm)
        {
            return _proBll.List(parm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<object> GetCate()
        {
            var list = new List<object>();
            var top= _typeBll.GetCategory().rows;
            var last = _typeBll.List(new BaseParm()).rows;
            foreach (var item in top)
            {
                list.Add(new { id=item.EnumValue,name=item.EnumName});
            }
            foreach (var item in last)
            {
                list.Add(new {id=item.ID,pid=item.TopCategoryID,name=item.Name });
            }
            var res = new ApiMessage<object>();
            res.Data = list;
            return res;
        }
    }
}
