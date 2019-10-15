using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;
using Model.Entity;

namespace Portal.Controllers.Api
{
    public class OrderController : BaseApiController
    {
        private FavoriteBLL fbll=new FavoriteBLL();
        private OrderBLL obll=new OrderBLL();

        [HttpGet]
        public ApiMessage<bool> IsFavorite(string id)
        {
            var model=new favorite();
            model.ProductID = id;
            model.UserID = UserInfo.Id;
            return fbll.Exists(model);
        }

        [HttpPost]
        public ApiMessage<bool> AddOrDel(FavoriteEx parm)
        {
            parm.UserID = UserInfo.Id;
            parm.UserName = UserInfo.UserName;
            return fbll.AddOrDel(parm, parm.type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<bool> AddCart(cart parm)
        {
            parm.UserID = UserInfo.Id;
            parm.UserName = UserInfo.UserName;
            return fbll.CartAdd(parm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<List<CartEx>> CartList(string id)
        {
            id = UserInfo.Id;
            var list= fbll.CartList(id);
            if (UserInfo?.IsMember == 1)
            {
                list.Data.ForEach(n =>
                {
                    n.OPrice = n.Price;
                    n.Price = n.MPrice;
                });
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiMessage<bool> CartDel(string id)
        {
            var t = 0;
            if (id == "all")
            {
                id = UserInfo.Id;
                t = 1;
            }
            var api = fbll.CartDel(id, t);
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<List<CartEx>> GetPrice(List<PriceEx> p)
        {
            return obll.GetPrice(p);
        }
    }
}