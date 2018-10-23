using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model.Entity;

namespace BLL
{
   public  class CarBLL
    {
        private readonly CarDAL _carDal = new CarDAL();
        public Page<Car> List(BaseParm parm)
        {
            return _carDal.List(parm);
        }

        public ApiMessage<Car> Detail(string id)
        {
            return _carDal.Detail(id);
        }

        public ApiMessage<string> Delete(string id)
        {
            return _carDal.Delete(id);
        }

        public ApiMessage<string> Save(Car b)
        {
            var type = 0;
            if (string.IsNullOrEmpty(b.Id))
            {
                b.Id = Guid.NewGuid().ToString();
                b.Operator.PKId = b.Id;
                b.UpdateDate = DateTime.Now;
                b.CreateDate = DateTime.Now;


                b.Operator.Id = Guid.NewGuid().ToString();
                b.Admin = b.Operator.Id;
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

            if (b.Operator.PassWord.Split('-').Length != 16)
            {
                b.Operator.PassWord = Encrypt.MD5(b.Operator.PassWord);
            }
            return _carDal.Save(b, type);
        }
    }
}
