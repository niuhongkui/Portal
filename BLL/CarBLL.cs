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
    }
}
