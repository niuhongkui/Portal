using Common;
using DAL;
using Model;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BankBLL
    {
        private readonly BankDAL _bankDal = new BankDAL();
        public Page<Bank> List(BaseParm parm)
        {
            return _bankDal.List(parm);
        }
    }
}
