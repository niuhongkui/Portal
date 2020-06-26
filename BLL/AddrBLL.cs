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
        public ApiMessage<address> GetDefault(string userid)
        {
            var res= dal.GetDefault(userid);
            var api = new ApiMessage<address>();
            api.Data = res;
            return api;
        }
    }
}
