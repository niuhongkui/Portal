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
        private  readonly  PriceDAL _priceDal=new PriceDAL();

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
            var res=new ApiMessage<Good>();
            var good=new Good();
            var p= _dal.Get(parm.Id).Data;
            if (string.IsNullOrEmpty(p?.ID))
            {
                res.Success = false;
                res.Msg = "商品不存在";
                return res;
            }
            good.ID = p.ID;
            good.Title = p.Name;
            //good.ImgList.Add(new Img() {Url = p.ImgUrl});
            //good.ImgList.Add(new Img() { Url = p.ImgUrl2 });
            //good.ImgList.Add(new Img() { Url = p.ImgUrl3 });
            good.Detail = p.Detail;
            //good.Sales = p.Sales;

            var prices = _priceDal.List(parm).rows;
            foreach (var ex in prices)
            {
                var s =new Spec();
                s.ID = ex.UnitID;
                s.Name = ex.Name;
                s.MPrice = ex.MemberPrice;
                s.Price = ex.Price;
                good.SpecList.Add(s);
            }
            res.Data = good;

            return res;
        }
    }
}
