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
        /// 获得默认收货地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<address> GetDef(string id)
        {
            return bll.GetDefault(UserInfo.Id,id);
        }
        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<bool> Save(address model)
        {
            model.UserID = UserInfo.Id;
            model.CreateDate = DateTime.Now;
            return bll.Save(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<bool> Del(string id)
        {
            return bll.Del(id);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        public Page<address> GetList(BaseParm parm)
        {
            parm.Id = UserInfo.Id;
            return bll.GetList(parm);
        }
    }
}
