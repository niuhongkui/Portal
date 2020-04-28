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

        public ApiMessage<string> Save(List<PriceEx> list,CurrentUser user)
        {
            var res=new ApiMessage<string>();
            var prices=new List<productprice>();
            list.ForEach(n =>
            {
                var p=new productprice();
                p.ID = n.ID;
                p.CreateDate=DateTime.Now;
                p.Price = n.Price;
                p.ProductID = n.ProductID;
                p.UnitID = n.ID;
                p.UnitName = n.Name;
                p.StaffID = user.Id;
                p.StaffName = user.UserName;
                p.MemberPrice = n.MemberPrice;
                p.CreateDate = n.CreateDate;
                prices.Add(p);
            });
            _dal.Save(prices);
            return res;
        }
    }
}
