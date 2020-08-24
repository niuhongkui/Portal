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

        public void ToPay() {
            IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", "app_id", "merchant_private_key", "json", "1.0", "RSA2", "alipay_public_key", "GBK", false);
            AlipayTradePayRequest request = new AlipayTradePayRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"20150320010101001\"," +
            "\"scene\":\"bar_code\"," +
            "\"auth_code\":\"28763443825664394\"," +
            "\"product_code\":\"FACE_TO_FACE_PAYMENT\"," +
            "\"subject\":\"Iphone6 16G\"," +
            "\"buyer_id\":\"2088202954065786\"," +
            "\"seller_id\":\"2088102146225135\"," +
            "\"total_amount\":88.88," +
            "\"trans_currency\":\"USD\"," +
            "\"settle_currency\":\"USD\"," +
            "\"discountable_amount\":8.88," +
            "\"body\":\"Iphone6 16G\"," +
            "      \"goods_detail\":[{" +
            "        \"goods_id\":\"apple-01\"," +
            "\"goods_name\":\"ipad\"," +
            "\"quantity\":1," +
            "\"price\":2000," +
            "\"goods_category\":\"34543238\"," +
            "\"categories_tree\":\"124868003|126232002|126252004\"," +
            "\"body\":\"特价手机\"," +
            "\"show_url\":\"http://www.alipay.com/xxx.jpg\"" +
            "        }]," +
            "\"operator_id\":\"yx_001\"," +
            "\"store_id\":\"NJ_001\"," +
            "\"terminal_id\":\"NJ_T_001\"," +
            "\"extend_params\":{" +
            "\"sys_service_provider_id\":\"2088511833207846\"," +
            "\"industry_reflux_info\":\"{\\\\\\\"scene_code\\\\\\\":\\\\\\\"metro_tradeorder\\\\\\\",\\\\\\\"channel\\\\\\\":\\\\\\\"xxxx\\\\\\\",\\\\\\\"scene_data\\\\\\\":{\\\\\\\"asset_name\\\\\\\":\\\\\\\"ALIPAY\\\\\\\"}}\"," +
            "\"card_type\":\"S0JP0000\"" +
            "    }," +
            "\"timeout_express\":\"90m\"," +
            "\"auth_confirm_mode\":\"COMPLETE：转交易支付完成结束预授权;NOT_COMPLETE：转交易支付完成不结束预授权\"," +
            "\"terminal_params\":\"{\\\"key\\\":\\\"value\\\"}\"," +
            "\"promo_params\":{" +
            "\"actual_order_time\":\"2018-09-25 22:47:33\"" +
            "    }," +
            "\"advance_payment_type\":\"ENJOY_PAY_V2\"," +
            "      \"query_options\":[" +
            "        \"voucher_detail_list\"" +
            "      ]," +
            "\"request_org_pid\":\"2088201916734621\"," +
            "\"is_async_pay\":false" +
            "  }";
            AlipayTradePayResponse response = client.Execute(request);
            Console.WriteLine(response.Body);
        }

    }
}