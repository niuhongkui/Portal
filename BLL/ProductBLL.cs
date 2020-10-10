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
    public class ProductBLL
    {
        private readonly ProductDAL _dal = new ProductDAL();
        private readonly PriceDAL _priceDal = new PriceDAL();

        public Page<product> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
        public ApiMessage<string> Delete(string id)
        {
            return _dal.Delete(id);
        }
        public ApiMessage<string> Edit(ProductEx parm)
        {
            return _dal.Edit(parm);
        }
        public ApiMessage<ProductEx> Get(string id)
        {
            return _dal.Get(id);
        }


        public Page<productunit> GetUnit()
        {
            return _dal.GetUnit();
        }

        public ApiMessage<List<Goods>> GetGoods(BaseParm parm)
        {
            return _dal.GetGoods(parm);
        }

        public ApiMessage<List<Goods>> GetLikeGoods(string userId)
        {
            return _dal.GetLikeGoods(userId);
        }

        public ApiMessage<Good> GetGood(BaseParm parm)
        {
            var res = new ApiMessage<Good>();
            var good = new Good();
            var p = _dal.Get(parm.Id).Data;
            if (string.IsNullOrEmpty(p?.ID))
            {
                res.Success = false;
                res.Msg = "商品不存在";
                return res;
            }
            good.ID = p.ID;
            good.Name = p.Name;
            good.ImgList = p.Imgs.Select(n=>new Img { Url=n.url}).ToList();
            good.Detail = p.Detail;
            parm.page = 1;
            parm.rows = 1000;
            var prices = _priceDal.List(parm).rows;
            foreach (var ex in prices)
            {
                var s = new Spec();
                s.ID = ex.UnitID;
                s.Name = ex.Name;
                s.MPrice = ex.MemberPrice;
                s.Price = ex.Price;
                s.StoreAmount = ex.Amount;
                good.SpecList.Add(s);
            }
            res.Data = good;

            return res;
        }
        /// <summary>
        /// 指定商铺下的所有商品
        /// </summary>
        /// <returns></returns>
        public ApiMessage<List<StoreGood>> GetAllGood(string userId, BaseParm parm)
        {
            var res = _dal.GetAllGood(parm);
            if (!string.IsNullOrEmpty(userId))
            {
                var cartList = new CartDAL().List(userId);
                res.Data.ForEach(n =>
                {
                    var node = cartList.Data.FirstOrDefault(m => n.ID == m.ProductID);
                    if (node != null)
                    {
                        n.SelectAmount = node.Amount;
                    }
                });
            }
            return res;
        }
        public ApiMessage<List<StoreGood>> GetAllGood2(string userId,BaseParm parm)
        {
            var res= _dal.GetAllGood2(parm);
            if (!string.IsNullOrEmpty(userId)) {
                var cartList = new CartDAL().List(userId);
                res.Data.ForEach(n =>
                {
                    var node = cartList.Data.FirstOrDefault(m => n.ID == m.ProductID);
                    if (node != null)
                    {
                        n.SelectAmount = node.Amount;
                    }
                });
            }
            return res;
        }
    }
}
