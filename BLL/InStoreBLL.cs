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
    public class InStoreBLL
    {
        private InStoreDAL _dal = new InStoreDAL();
        public Page<instore> List(BaseParm parm)
        {
            return _dal.List(parm);
        }

        public ApiMessage<string> Save(instore model) {

            return _dal.Save(model);
        }
    }
}
