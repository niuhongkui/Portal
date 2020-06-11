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

        public ApiMessage<string> Save(PriceEx n, CurrentUser user)
        {
            var res = new ApiMessage<string>();
            var p = new productprice();
            p.ID = n.ID;
            p.Price = n.Price;
            p.MemberPrice = n.MemberPrice;
            p.LimitNum = n.LimitNum;
            p.ProductID = n.ProductID;
            p.UnitID = n.UnitID;
            p.UnitName = n.UnitID;
            p.StaffID = user.Id;
            p.StaffName = user.UserName;
            p.CreateDate = DateTime.Now;
            _dal.Save(p);
            return res;
        }

        public ApiMessage<string> Del(PriceEx p)
        {
            var api = new ApiMessage<string>();
            if (string.IsNullOrEmpty(p.ID)) {
                api.Success = false;
                api.Msg = "该商品不存在价格";
                return api;
            }
            var old = new oldprice();
            old.ID = Guid.NewGuid().ToString();
            old.PriceID = p.ID;
            old.ProductID = p.ProductID;
            old.Price = p.Price;
            old.MemberPrice = p.MemberPrice;
            old.LimitNum = p.LimitNum;
            old.StaffName = p.StaffName;
            old.ProductName = p.Name;
            old.CreatDate = DateTime.Now;
            var msg = _dal.Del(old);
            api.Success = msg;
            api.Msg = msg ? "操作成功" : "操作失败";
            return api;
        }

        public ApiMessage<PriceEx> Get(string id,string proid)
        {
            var model = _dal.Get(id,proid);
            var api = new ApiMessage<PriceEx>();
            api.Data = model;
            return api;
        }
    }
}
