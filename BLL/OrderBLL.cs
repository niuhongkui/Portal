using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model;
using Model.Entity;
using Newtonsoft.Json;

namespace BLL
{
    public class OrderBLL
    {
        private OrderDAL dal = new OrderDAL();

        public ApiMessage<List<CartEx>> GetPrice(List<PriceEx> p)
        {
            var api = new ApiMessage<List<CartEx>>() { Data = new List<CartEx>() };
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
            data.Detail.ForEach(n =>
            {
                n.ID = Guid.NewGuid().ToString();
                n.OrderNo = data.OrderNo;
                n.PMoney = n.PPrice * n.Amount;
                n.Money = n.Price * n.Amount;
            });
            data.Amount = data.Detail.Sum(n => n.Amount);
            data.Money = data.Detail.Sum(n => n.Money) + data.SendMoney;
            data.PMoney = data.Detail.Sum(n => n.PMoney) + data.SendMoney;
            var res = dal.Save(data);
            return res;
        }

        public Page<OrderData> GetList(BaseParm parm)
        {
            return dal.List(parm);
        }
        public Page<order> ListByPage(BaseParm parm)
        {
            return dal.ListByPage(parm);
        }

        public Page<orderdetail> GetOrder(string orderNo)
        {
            return dal.GetOrder(orderNo);
        }

        public ApiMessage<string> PickUp(string orderNo)
        {
            return dal.PickUp(orderNo);
        }
        public ApiMessage<bool> DelOrder(string orderNo)
        {
            return dal.DelOrder(orderNo);
        }
        public ApiMessage<bool> CloseOrder(string orderNo)
        {
            return dal.CloseOrder(orderNo);
        }

        public ApiMessage<string> UpdateState(string orderNo)
        {
            return dal.UpdateState(orderNo,"");
        }
    }
}
