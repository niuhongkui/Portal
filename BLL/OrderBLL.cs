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
    }
}
