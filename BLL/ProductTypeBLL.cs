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
        private ProductTypeDAL _dal = new ProductTypeDAL();

        public Page<producttype> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
    }
}
