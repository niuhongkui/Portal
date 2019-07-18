using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model;
using Model.Entity;

namespace BLL
{
    public class PriceBLL
    {
        private readonly PriceDAL _dal = new PriceDAL();

        public Page<PriceEx> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
    }
}
