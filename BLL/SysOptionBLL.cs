using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Common;
using Model;
using System.Configuration;

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

        public ApiMessage<string> GetVersion()
        {
            var api = new ApiMessage<string>();
            var ver = ConfigurationManager.AppSettings["version"];
            api.Data = string.IsNullOrEmpty(ver) ? "V1.0" : ver;
            return api;
        }
    }
}
