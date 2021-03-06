﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;
using Model.Entity;
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;

namespace Portal.Controllers.Api
{
    public class OrderController : BaseApiController
    {
        private FavoriteBLL fbll = new FavoriteBLL();
        private OrderBLL obll = new OrderBLL();

        [HttpGet]
        public ApiMessage<bool> IsFavorite(string id)
        {
            var model = new favorite();
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
            var list = fbll.CartList(id);
            if (UserInfo?.IsMember >0)
            {
                list.Data.ForEach(n =>
                {
                    n.OPrice = n.Price - n.MPrice;
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
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<List<CartEx>> GetPrice(Order order)
        {
            var list = obll.GetPrice(order.GoodsData);
            if (UserInfo?.IsMember >0)
            {
                list.Data.ForEach(n =>
                {
                    n.OPrice = n.Price - n.MPrice;
                    n.Price = n.MPrice;
                });
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<object> Save(OrderData data)
        {
            data.UserName = UserInfo.UserName;
            data.CreateDate = DateTime.Now;
            data.UserID = UserInfo.Id;
            data.State = "待付款";
            data.ID = Guid.NewGuid().ToString();
            data.OrderNo = CodeNo.Get(CodeType.Order);
            data.IsMember = UserInfo.IsMember;
            data.Address = data.Address ?? "";
            return obll.Save(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Page<OrderData> GetOrders(BaseParm parm)
        {
            parm.Id = UserInfo.Id;
            return obll.GetList(parm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<bool> DelOrder(string id)
        {
            return obll.DelOrder(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<bool> CloseOrder(string id)
        {
            return obll.CloseOrder(id);
        }
    }
}