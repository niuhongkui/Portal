using Common;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AddrBLL
    {
        AddrDAL dal = new AddrDAL();
        public ApiMessage<address> GetDefault(string userid,string id)
        {
            var res = dal.GetDefault(userid,id);
            var api = new ApiMessage<address>();
            api.Data = res;
            return api;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiMessage<bool> Save(address model)
        {
            var sign = dal.Save(model);
            var res = new ApiMessage<bool>();
            if (!sign)
            {
                res.Msg = "操作失败";
                res.Success = false;
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiMessage<bool> Del(string id)
        {
            var sign = dal.Del(id);
            var res = new ApiMessage<bool>();
            if (!sign)
            {
                res.Msg = "操作失败";
                res.Success = false;
            }
            return res;
        }
        public Page<address> GetList(BaseParm parm)
        {
            return dal.GetList(parm);
        }
    }
}
