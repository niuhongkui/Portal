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

        public ApiMessage<string> Delete(string id)
        {
            return _bankDal.Delete(id);
        }

        public ApiMessage<Bank> Detail(string id)
        {
            return _bankDal.Detail(id);
        }

        public ApiMessage<string> Save(Bank b)
        {
            var type = 0;
            if (string.IsNullOrEmpty(b.Id))
            {
                b.Id = Guid.NewGuid().ToString();
                b.Operator.PKId = b.Id;
                b.UpdateDate = DateTime.Now;
                b.CreateDate = DateTime.Now;
                b.Admin = b.Operator.Id;

                b.Operator.Id = Guid.NewGuid().ToString();
                b.Operator.CreateDate = DateTime.Now;
                b.Operator.IsDelete = false;
                b.Operator.UpdateDate = DateTime.Now;
                type = 0;
            }
            else
            {
                type = 1;
                b.UpdateDate = DateTime.Now;
                b.Operator.UpdateDate = DateTime.Now;
            }
            return _bankDal.Save(b,type);
        }
    }
}
