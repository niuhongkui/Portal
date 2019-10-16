using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model.Entity;
using Newtonsoft.Json;

namespace BLL
{
    public class OrderBLL
    {
        private OrderDAL dal=new OrderDAL();

        public ApiMessage<List<CartEx>> GetPrice(List<PriceEx> p)
        {
            var api=new ApiMessage<List<CartEx>>() {Data = new List<CartEx>()};
            var list = dal.GetPrice(p);
            if (list.Count != p.Count)
            {
                api.Success = false;
                api.Msg = "商品数据异常";
            }
            else
            {
                list.ForEach(n =>
                {
                    var firstOrDefault = p.FirstOrDefault(m => n.ProductID == m.ProductID && m.UnitID == n.UnitID);
                    if (firstOrDefault != null)
                        n.Amount = firstOrDefault.Amount;
                });
                api.Success = true;
                api.Data = list;
            }
            return api;
        }

        public ApiMessage<object> Save(OrderData data)
        {
            var cartIds = data.Detail.Select(n => n.ID).ToList();
            cartIds = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(cartIds));
            data.Detail.ForEach(n =>
            {
                n.ID = Guid.NewGuid().ToString();
                n.OrderNo = data.OrderNo;
                n.PMoney = n.PPrice * n.Amount;
                n.Money = n.Price * n.Amount;
            });
            data.Amount = data.Detail.Sum(n => n.Amount);
            data.Money = data.Detail.Sum(n => n.Money);
            data.PMoney = data.Detail.Sum(n => n.PMoney);
            var res= dal.Save(data);
            if (res.Success&&cartIds.Any(n=>!string.IsNullOrEmpty(n)))
            {
                var cDal=new CartDAL();
                cDal.Delete(cartIds);
            }
            return res;
        }

        public Page<OrderData> GetList(BaseParm parm)
        {
            return dal.List(parm);
        }
    }
}
