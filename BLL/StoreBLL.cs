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
    public class StoreBLL
    {
        private StoreDAL _dal = new StoreDAL();
        public Page<store> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
    }
}
