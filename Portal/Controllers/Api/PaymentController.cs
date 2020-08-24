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
        private string APPID = "2021001182691857";
        private string APP_PRIVATE_KEY = "MIIEogIBAAKCAQEAi7gw0BbVoNWJU3RfB7I8AqUK2XOfnWbq68eCW5qU8MNWHImPlz7ABnkHdeK+OMzaeGC6HCSPIgomUSNUZkx6JyFO232Akvs/7nhNVHfmgqbTZmvSunmpUpfjaoU9UI/9m4yFDkl7wM4nP2YC4eqlxWi4CbhI2Yp2rsfOxjkd32Kg+66CtI4a+nPY7Wl22UC0BuLH2Gm+pdVI34ZH/lPNsoDlAR/12JXu5KPjMOt2azz+BZUIzEetqvmnZubKsN2vGuUmwA7izbMr00MR0+xjUIyGLJZSCbrhoHiyJqJQlQrhlkz4AWIxTX1FEsionLmKQJj+WAIsVLHYWh7ksg/UOwIDAQABAoIBAHQlme5iDRS2boJBqv3q3JgWOv6pb3aZp5B5OZSM3GOI/nyanhNMxrSax+jnpNny8WpAfnYGrjXN2ix5AiBIUwNUXQl5Ovj0hCDpQN+HDMhvhi0OVQ9PM8LFIPfb6yydhmVYWwOVcprExTuewaasOUHx24u49lZhFGgXEX0W3g5j5FH5FYme9aP46+Fz424DrnKNdGIpaGjqyTYEGxIzwSTIJL8HbrUMWRKvNUbITjljps2vM3xU52BDDNxZM9eo8dV0sLITqNAtjwy+D+EfjKDN/oSglnIApwaf9SrUsszjBLy6J4B3z3MaSwy2ghCpIRnrn78YTmag4KeQ5h1eUQECgYEAxZYW2rBKU6fkklxUAAa6H0Flga1LCkCDs/HIcc6O/OxEPfMfbF94ZbvkLioPwyLF19zHiAJrQ33nld/5VCo65eNPbKT+bJRIanr9Uztd+AVPp0vXsFZJKsHJoa30HPqRuoJMlO2AqGMr83h/VqZYVCSdkb3iE32Yu/vYDEY1g0MCgYEAtQaSlW8BGCGL9+BA0nZRN6o3i92Kd9yx9QJt/JNkfVsOhTA6g6AVbL6M/vqwmLw9Qb7XRX1cIln7OedIz2OKToZr+KBrYftcUtoNhelOX5BIE3575E0pRSh7NRw7yIjaW3t1lAwj7Bs3Y50i1PlCYLlbUCHeeTsFOmbqYWnzz6kCgYBSPKbkfI1jBjUXScBYhnQ/AAwMjiD2cmWeppqD67INyRSaKC/C/nVw+mP9ZtpKoJVxw910WXVlkOirs53ljvIWrqZnFMEkVg9R6kC3vLTevu8pNWLfbPplBmUymuFIkm3HD+Zp8fQjoaswWc2+Ndv7oYXHnB2VeSfxzd9dNHefWwKBgBEJhVI7GEdFIP87Q96K9CnhA0lOmHGfe8Arcl6LILILl9pBJ8CrmFibtnlo7qXxUXKWm3wWyE0TWumgMuIR5DpvvEmyD2kxVwcVLqCjzJSB58at6Msb1/6CSNY+ygGdn69sdyxv2BGonhXp/BU/QFgKGeX4yg9u1pcgiH6SkjfBAoGAFz1TL6Gz/Wwu0Aa7b71T/gfOwHWepdKuv9pe0WatFf5cDWh8sj2B6NpSC0Qy+pYhn4XwXT3f2ooVIQF4QVVyZzxfuDpEHSPCf1eS4Gw1SsHhvdew4YsUSbDJL+SRLPSKs1QdHiu4xDjg1DRaW5r0fS3sMD0iM8hysCGI+IENq5g=";
        private string ALIPAY_PUBLIC_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAi7gw0BbVoNWJU3RfB7I8AqUK2XOfnWbq68eCW5qU8MNWHImPlz7ABnkHdeK+OMzaeGC6HCSPIgomUSNUZkx6JyFO232Akvs/7nhNVHfmgqbTZmvSunmpUpfjaoU9UI/9m4yFDkl7wM4nP2YC4eqlxWi4CbhI2Yp2rsfOxjkd32Kg+66CtI4a+nPY7Wl22UC0BuLH2Gm+pdVI34ZH/lPNsoDlAR/12JXu5KPjMOt2azz+BZUIzEetqvmnZubKsN2vGuUmwA7izbMr00MR0+xjUIyGLJZSCbrhoHiyJqJQlQrhlkz4AWIxTX1FEsionLmKQJj+WAIsVLHYWh7ksg/UOwIDAQBA";
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
            request.SetNotifyUrl("http://49.232.204.122/api/payment/updateorder/alipay");
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            api.Data = response.Body;
            return api;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        [HttpPost]
        [AllowAnonymous]
        public string UpdateOrder(object obj)
        {
            NameValueCollection collection = HttpContext.Current.Request.Form;
            String[] requestItem = HttpContext.Current.Request.Form.AllKeys;
            IDictionary<string, string> sArray = new Dictionary<string, string>();
            for (int i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], collection[requestItem[i]]);
            }
         
            LogHelper.WriteLog("支付返回：" + JsonConvert.SerializeObject(sArray), LogHelper.LogType.Info);

            var success = AlipaySignature.RSACheckV1(sArray, ALIPAY_PUBLIC_KEY, CHARSET, "RSA2", false);
            if (!success)
                return "fail";

            var orderno = sArray["out_trade_no"];
            var sign = bll.UpdateOrder(orderno);

            return sign.Success ? "success" : "fail";

        }
    }
}
