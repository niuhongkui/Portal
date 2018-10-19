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
    }
}
