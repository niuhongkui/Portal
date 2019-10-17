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
    public class SwiperBLL
    {
        private SwiperDAL dal = new SwiperDAL();
        public ApiMessage<bool> Edit(swiper model)
        {
            if (string.IsNullOrEmpty(model.ProductID))
            {
                model.ProductID = "";
                model.ProductName = "";
            }
            if (string.IsNullOrEmpty(model.ID))
            {
                model.ID = Guid.NewGuid().ToString();
                model.CreateDate = DateTime.Now;
                model.Insert();
            }
            else
            {
                model.CreateDate = DateTime.Now;
                model.Update();
            }
            return new ApiMessage<bool>() { Data = true };
        }

        public ApiMessage<bool> Delete(string id)
        {
            return dal.Delete(id);
        }

        public Page<swiper> List(BaseParm parm)
        {
            return dal.List(parm);
        }
        public ApiMessage<swiper> Get(string id)
        {
            var api= dal.Get(id);
            api.Success = !string.IsNullOrEmpty(api?.Data?.ID);
            return api;
        }
    }
}
