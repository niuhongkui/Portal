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
        private FavoriteBLL fdal=new FavoriteBLL();

        [HttpGet]
        public ApiMessage<bool> IsFavorite(string id)
        {
            var model=new favorite();
            model.ProductID = id;
            model.UserID = UserInfo.Id;
            return fdal.Exists(model);
        }

        [HttpPost]
        public ApiMessage<bool> AddOrDel(FavoriteEx parm)
        {
            parm.UserID = UserInfo.Id;
            parm.UserName = UserInfo.UserName;
            return fdal.AddOrDel(parm, parm.type);
        }

        [HttpPost]
        public ApiMessage<bool> AddCart(cart parm)
        {
            parm.UserID = UserInfo.Id;
            parm.UserName = UserInfo.UserName;
            return fdal.CartAddOrDel(parm, 1);
        }
    }
}