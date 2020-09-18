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
    /// <summary>
    /// 
    /// </summary>
    public class ProductController : BaseApiController
    {
        private readonly ProductTypeBLL _typeBll = new ProductTypeBLL();
        private readonly ProductBLL _proBll = new ProductBLL();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<object> GetCate()
        {
            var list = new List<object>();
            var last = _typeBll.List(new BaseParm { rows = 1000, page = 1 }).rows.OrderBy(n => n.OrderByNo).ThenBy(n=>n.CreateDate);
            foreach (var item in last)
            {
                list.Add(new { id = item.ID, name = item.Name });
            }
            var res = new ApiMessage<object>();
            res.Data = list;
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<object> GetLastCate(string id)
        {
            var list = new List<object>();
            var top = _typeBll.GetCategory().rows;
            var last = _typeBll.List(new BaseParm()).rows;
            foreach (var item in top)
            {
                var node = new { id = item.EnumValue, name = item.EnumName, child = new List<object>() };
                //var temp = last.Where(n => n.TopCategoryID == item.EnumValue.ToString()).ToList();
                //foreach (var t in temp)
                //{
                //    node.child.Add(new { id = t.ID, pid = t.TopCategoryID, name = t.Name, picture = t.ImgUrl });
                //}
                list.Add(node);
            }
            var res = new ApiMessage<object>();
            res.Data = list;
            return res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiMessage<List<Goods>> GetGoods(BaseParm parm)
        {
            var res = _proBll.GetGoods(parm);
            return res;
        }
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<List<Goods>> GetLikeGoods(string id)
        {
            var api = new ApiMessage<List<Goods>>()
            {
                Data = new List<Goods>()
            };
            if (string.IsNullOrEmpty(UserInfo?.Id))
                return api;
            api = _proBll.GetLikeGoods(UserInfo?.Id);
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<Good> GetGood(string id)
        {

            var res = _proBll.GetGood(new BaseParm { Id = id });
            if (UserInfo?.IsMember == 0)
            {
                res.Data.SpecList.ForEach(n => { n.MPrice = n.Price; });
            }
            return res;
        }
        /// <summary>
        /// 指定商铺下的所有商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<List<StoreGood>> GetAllGood()
        {
            var userId = UserInfo?.Id;
            return _proBll.GetAllGood(userId);
        }

    }
}
