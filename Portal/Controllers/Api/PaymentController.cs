using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using BLL;
using Model;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Model.Entity;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Aop.Api.Util;
using System.Web;
using System.Configuration;

namespace Portal.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class PaymentController : BaseApiController
    {
        #region 参数
        private string RSA2 = "RSA2";
        private string CHARSET = "utf-8";
        // 这个是沙箱环境的网关，真实环境需要改一下
        private string URL = "https://openapi.alipay.com/gateway.do";
        private string APPID = ConfigurationManager.AppSettings["Ali_APPID"];
        private string APP_PRIVATE_KEY = ConfigurationManager.AppSettings["APP_PRIVATE_KEY"];
        private string ALIPAY_PUBLIC_KEY = ConfigurationManager.AppSettings["ALIPAY_PUBLIC_KEY"];
        private string Ali_PUBLIC_KEY = ConfigurationManager.AppSettings["Ali_PUBLIC_KEY"];
        #endregion

        private PaymentBLL bll = new PaymentBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<string> Alipay(PayModel pm)
        {
            var api = new ApiMessage<string>();
            IAopClient client = new DefaultAopClient(URL, APPID, APP_PRIVATE_KEY, "json", "1.0", RSA2, ALIPAY_PUBLIC_KEY, CHARSET, false);
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            var res = bll.CkeckData(pm, UserInfo);
            if (!res.Success)
            {
                api.Success = false;
                api.Msg = res.Msg;
                return api;
            }
            var oData = res.Data;
            model.Body = oData.Body;
            model.Subject = oData.Subject;
            model.TotalAmount = oData.TotalAmount.ToString();
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = oData.OutTradeNo;


            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            request.SetBizModel(model);
            request.SetNotifyUrl(ConfigHelper.WebSiteUrl+"/api/payment/updateorder/alipay");
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            api.Data = response.Body;
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiMessage<object> Wxpay(PayModel pm)
        {
            var api = new ApiMessage<object>();
            var res = bll.CkeckData(pm, UserInfo);
            if (!res.Success)
            {
                api.Success = false;
                api.Msg = res.Msg;
                return api;
            }
            var oData = res.Data;            
            oData.IP = HttpContext.Current.Request.UserHostAddress;
            api = bll.CkeckWxData(oData, UserInfo);           
            return api;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        [HttpPost]
        [AllowAnonymous]
        public string UpdateOrder(string type)
        {
            if (type == "alipay")
            {
                NameValueCollection collection = HttpContext.Current.Request.Form;
                String[] requestItem = HttpContext.Current.Request.Form.AllKeys;
                IDictionary<string, string> sArray = new Dictionary<string, string>();

                for (int i = 0; i < requestItem.Length; i++)
                {
                    sArray.Add(requestItem[i], collection[requestItem[i]]);
                }

                LogHelper.WriteLog("支付返回：" + JsonConvert.SerializeObject(sArray), LogHelper.LogType.Info);

                var success = AlipaySignature.RSACheckV1(sArray, Ali_PUBLIC_KEY, CHARSET, "RSA2", false);
                if (!success)
                    return "fail";

                var orderno = sArray["out_trade_no"];
                var trade_no = sArray["trade_no"];
                var sign = bll.UpdateOrder(orderno, trade_no);

                return sign.Success ? "success" : "fail";
            }
            else
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                HttpRequestBase request = context.Request;//定义传统request对象           
                var verifyResult = "fail";
                var requestXml = WeiXinUtil.GetRequestXmlData(request);
                var dic = WeiXinUtil.FromXml(requestXml);

                LogHelper.WriteLog("支付返回：" + JsonConvert.SerializeObject(dic), LogHelper.LogType.Info);

                var returnCode = WeiXinUtil.GetValueFromDic<string>(dic, "return_code");

                if (!string.IsNullOrEmpty(returnCode) && returnCode == "SUCCESS")//通讯成功
                {
                    var result = WeiXinUtil.WePayNotifyValidation(dic);
                    if (result)
                    {
                        var transactionid = WeiXinUtil.GetValueFromDic<string>(dic, "transaction_id");

                        if (!string.IsNullOrEmpty(transactionid))
                        {
                            var queryXml = WeiXinUtil.BuildQueryRequest(transactionid, dic);
                            var queryResult = WeiXinUtil.Post("https://api.mch.weixin.qq.com/pay/orderquery", queryXml);
                            var queryReturnDic = WeiXinUtil.FromXml(queryResult);

                            if (WeiXinUtil.ValidatonQueryResult(queryReturnDic))//查询成功
                            {
                                verifyResult = "success";
                                var status = WeiXinUtil.GetValueFromDic<string>(dic, "result_code");

                                if (!string.IsNullOrEmpty(status) && status == "SUCCESS")
                                {
                                    //var order = new Order()
                                    //{
                                    //    OrderNumber = WeiXinUtil.GetValueFromDic<string>(dic, "out_trade_no"),
                                    //    TransactionId = transactionid,
                                    //    ProductPrice = WeiXinUtil.GetValueFromDic<decimal>(dic, "total_fee") / 100,
                                    //    TradeType = WeiXinUtil.GetValueFromDic<string>(dic, "trade_type"),
                                    //    BankType = WeiXinUtil.GetValueFromDic<string>(dic, "bank_type"),
                                    //    PayDate = DateTime.Parse(WeiXinUtil.GetValueFromDic<string>(dic, "time_end")),
                                    //    StatusId = 1,
                                    //    IsPresent = false,
                                    //    AddDate = DateTime.Now,
                                    //    IsDelete = false
                                    //};
                                    //CURD.Add(order, ConfigHelper.WriteDB);
                                    var orderNo = WeiXinUtil.GetValueFromDic<string>(dic, "out_trade_no");
                                    var sign = bll.UpdateOrder(orderNo, transactionid);

                                    WeiXinUtil.BuildReturnXml("OK", "成功");
                                }
                            }
                            else
                                WeiXinUtil.BuildReturnXml("FAIL", "订单查询失败");
                        }
                        else
                            WeiXinUtil.BuildReturnXml("FAIL", "支付结果中微信订单号不存在");
                    }
                    else
                        WeiXinUtil.BuildReturnXml("FAIL", "签名失败");
                }
                else
                {
                    string returnmsg;
                    dic.TryGetValue("return_msg", out returnmsg);
                    throw new Exception("异步通知错误：" + returnmsg);
                }

                return verifyResult;
            }


        }
    }
}
