using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Common
{
    public class WeiXinUtil
    {
        public class WxPayModel
        {
            public string appid { set; get; }
            public string partnerid { set; get; }
            public string prepayid { set; get; }
            public string package { set; get; }
            public string noncestr { set; get; }
            public string timestamp { set; get; }
            public string sign { set; get; }
        }
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns></returns>
        public static string BuildRequest(string trade_no, string detail, decimal fee, string ip)
        {
            var dicParam = CreateParam(trade_no, detail, fee, ip);

            var signString = CreateURLParamString(dicParam);
            var preString = signString + "&key=" + ConfigHelper.APIKey;
            var sign = Sign(preString, "utf-8").ToUpper();
            dicParam.Add("sign", sign);

            return BuildForm(dicParam);
        }



        /// <summary>
        /// Signs the specified prestr.
        /// </summary>
        /// <param name="prestr">The prestr.</param>
        /// <param name="_input_charset">The input charset.</param>
        /// <returns></returns>
        private static string Sign(string prestr, string _input_charset)
        {
            var sb = new StringBuilder(32);
            MD5 md5 = new MD5CryptoServiceProvider();
            var t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr));
            foreach (var t1 in t)
            {
                sb.Append(t1.ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <returns></returns>
        private static SortedDictionary<string, string> CreateParam(string trade_no, string detail, decimal fee, string ip)
        {
            const string amount = "1";
            double dubamount;
            double.TryParse(amount, out dubamount);
            var notify_url =ConfigHelper.WebSiteUrl+ "/api/payment/updateorder/wxpay"; //支付完成后的回调处理页面

            var dic = new SortedDictionary<string, string>
        {
            {"appid", ConfigHelper.AppID},//账号ID
            {"mch_id", ConfigHelper.MchID},//商户号
            {"nonce_str", Guid.NewGuid().ToString().Replace("-", "")},//随机字符串
            {"body", detail}, //商品描述
            {"out_trade_no", trade_no},//商户订单号
            {"total_fee", (fee*100).ToString()},//总金额
            {"spbill_create_ip",ip},//终端IP
            {"notify_url", notify_url},//通知地址
            {"trade_type", "APP"}//交易类型
        };

            return dic;
        }

        /// <summary>
        /// Creates the URL parameter string.
        /// </summary>
        /// <param name="dicArray">The dic array.</param>
        /// <returns></returns>
        private static string CreateURLParamString(SortedDictionary<string, string> dicArray)
        {
            var prestr = new StringBuilder();
            foreach (var temp in dicArray.OrderBy(o => o.Key))
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            var nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// Builds the form.
        /// </summary>
        /// <param name="dicParam">The dic parameter.</param>
        /// <returns></returns>
        private static string BuildForm(SortedDictionary<string, string> dicParam)
        {
            var sbXML = new StringBuilder();
            sbXML.Append("<xml>");
            foreach (var temp in dicParam)
            {
                sbXML.Append("<" + temp.Key + ">" + temp.Value + "</" + temp.Key + ">");
            }

            sbXML.Append("</xml>");
            return sbXML.ToString();
        }

        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        /// <exception cref="Exception">将空的xml串转换为WxPayData不合法!</exception>
        public static SortedDictionary<string, string> FromXml(string xml)
        {
            var sortDic = new SortedDictionary<string, string>();
            if (string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            var nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                var xe = (XmlElement)xn;

                if (!sortDic.ContainsKey(xe.Name))
                    sortDic.Add(xe.Name, xe.InnerText);
            }
            return sortDic;
        }

        /// <summary>
        /// Posts the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        /// <exception cref="Exception">POST请求错误" + e</exception>
        public static string Post(string url, string content, string contentType = "application/x-www-form-urlencoded")
        {
            string result;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    var stringContent = new StringContent(content, Encoding.UTF8);
                    var response = client.PostAsync(url, stringContent).Result;
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                throw new Exception("POST请求错误" + e);
            }
            return result;
        }

        /// <summary>
        /// Gets the value from dic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic">The dic.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T GetValueFromDic<T>(IDictionary<string, string> dic, string key)
        {
            string val;
            dic.TryGetValue(key, out val);

            var returnVal = default(T);
            if (val != null)
                returnVal = (T)Convert.ChangeType(val, typeof(T));

            return returnVal;
        }

        /// <summary>
        /// Builds the application pay.
        /// </summary>
        /// <param name="prepayid">The prepayid.</param>
        /// <returns></returns>
        public static string BuildAppPay(string prepayid)
        {
            var dicParam = CreateWapAndAppPayParam(prepayid);
            var signString = CreateURLParamString(dicParam);
            var preString = signString + "&key=" + ConfigHelper.APIKey;

            var sign = Sign(preString, "utf-8").ToUpper();
            dicParam.Add("sign", sign);

            return JsonConvert.SerializeObject(
                new
                {
                    appid = dicParam["appid"],
                    partnerid = dicParam["partnerid"],
                    prepayid = dicParam["prepayid"],
                    package = dicParam["package"],
                    noncestr = dicParam["noncestr"],
                    timestamp = dicParam["timestamp"],
                    sign = dicParam["sign"]
                });
        }

        /// <summary>
        /// Creates the wap and application pay parameter.
        /// </summary>
        /// <param name="prepayId">The prepay identifier.</param>
        /// <returns></returns>
        private static SortedDictionary<string, string> CreateWapAndAppPayParam(string prepayId)
        {
            var dic = new SortedDictionary<string, string>
        {
            {"appid", ConfigHelper.AppID},//公众账号ID
            {"partnerid", ConfigHelper.MchID},//商户号
            {"prepayid", prepayId},//预支付交易会话ID
            {"package", "Sign=WXPay"},//扩展字段
            {"noncestr", Guid.NewGuid().ToString().Replace("-", "")},//随机字符串
            {
                "timestamp",
                (Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds)).ToString()
            }//时间戳
        };

            return dic;
        }

        /// <summary>
        /// Validatons the query result.
        /// </summary>
        /// <param name="dic">The dic.</param>
        /// <returns></returns>
        public static bool ValidatonQueryResult(SortedDictionary<string, string> dic)
        {
            var result = false;

            if (dic.ContainsKey("return_code") && dic.ContainsKey("return_code"))
            {
                if (dic["return_code"] == "SUCCESS" && dic["result_code"] == "SUCCESS")
                    result = true;
            }

            if (result) return true;

            var sb = new StringBuilder();
            foreach (var item in dic.Keys)
            {
                sb.Append(item + ":" + dic[item] + "|");
            }

            return false;
        }

        /// <summary>
        /// Gets the request XML data.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string GetRequestXmlData(HttpRequestBase request)
        {
            var stream = request.InputStream;
            int count;
            var buffer = new byte[1024];
            var builder = new StringBuilder();
            while ((count = stream.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            stream.Flush();
            stream.Close();

            return builder.ToString();
        }

        /// <summary>
        /// Wes the pay notify validation.
        /// </summary>
        /// <param name="dic">The dic.</param>
        /// <returns></returns>
        public static bool WePayNotifyValidation(SortedDictionary<string, string> dic)
        {
            var sign = GetValueFromDic<string>(dic, "sign");
            if (dic.ContainsKey("sign"))
            {
                dic.Remove("sign");
            }

            var tradeType = GetValueFromDic<string>(dic, "trade_type");
            var preString = CreateURLParamString(dic);

            if (string.IsNullOrEmpty(tradeType))
            {
                var preSignString = preString + "&key=" + ConfigHelper.APIKey;
                var signString = Sign(preSignString, "utf-8").ToUpper();
                return signString == sign;
            }
            else
                return false;
        }

        /// <summary>
        /// Builds the query request.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="dic">The dic.</param>
        /// <returns></returns>
        public static string BuildQueryRequest(string transactionId, SortedDictionary<string, string> dic)
        {
            var dicParam = CreateQueryParam(transactionId);
            var signString = CreateURLParamString(dicParam);
            var key = ConfigHelper.APIKey;
            var preString = signString + "&key=" + key;
            var sign = Sign(preString, "utf-8").ToUpper();
            dicParam.Add("sign", sign);

            return BuildForm(dicParam);
        }

        /// <summary>
        /// Creates the query parameter.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        private static SortedDictionary<string, string> CreateQueryParam(string transactionId)
        {
            var dic = new SortedDictionary<string, string>
        {
            {"appid", ConfigHelper.AppID},//公众账号ID
            {"mch_id", ConfigHelper.MchID},//商户号
            {"nonce_str", Guid.NewGuid().ToString().Replace("-", "")},//随机字符串
            {"transaction_id", transactionId}//微信订单号
        };
            return dic;
        }

        /// <summary>
        /// Builds the return XML.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="returnMsg">The return MSG.</param>
        /// <returns></returns>
        public static string BuildReturnXml(string code, string returnMsg)
        {
            return
                $"<xml><return_code><![CDATA[{code}]]></return_code><return_msg><![CDATA[{returnMsg}]]></return_msg></xml>";
        }
    }
}
