using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Common;
using Model;

namespace BLL
{
    public class SysOptionBLL
    {
        private SysOptionDAL dal = new SysOptionDAL();

        public ApiMessage<List<sysoption>> GetOptByType(string t)
        {
            var api = new ApiMessage<List<sysoption>>();
            api.Data = dal.GetOptByType(t);
            return api;
        }
    }
}
