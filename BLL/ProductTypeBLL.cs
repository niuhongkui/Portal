using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model;

namespace BLL
{
    public class ProductTypeBLL
    {
        private readonly ProductTypeDAL _dal = new ProductTypeDAL();

        public Page<producttype> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
        public ApiMessage<string> Delete(string id)
        {
            return _dal.Delete(id);
        }
        public ApiMessage<string> Edit(producttype parm)
        {
            return _dal.Edit(parm);
        }
        public ApiMessage<producttype> Get(string id)
        {
            return _dal.Get(id);
        }
    }
}
