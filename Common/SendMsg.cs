using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Common
{
    public class SendMsg
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile">自己要验证收短信的手机号</param>
        /// <param name="code">验证码</param>
        public static string  Send(string mobile,string code)
        {
            string appkey =ConfigurationManager.AppSettings["msgAppKey"];//自己在腾讯云上申请的App Key
            var appId= ConfigurationManager.AppSettings["msgAppId"];//sdkappid=自己在腾讯云上申请的SDK AppID
            var sign= ConfigurationManager.AppSettings["msgSign"];//自己审核通过的短信签名
            var tpl_id= ConfigurationManager.AppSettings["msgtpl_id"];//自己审核通过的短信模版id

            string random = SendTools.GenerateRandomCode(10);
            string time = SendTools.GetTimeStamp(10).ToString();
            string sig = SendTools.Sha256($"appkey={appkey}&random={random}&time={time}&mobile={mobile}");
            var postData = new SendCode
            {
                Ext = "",
                Extend = "",
                Params = new string[] { code, "30" },
                Sig = sig,
                Sign = sign,
                Tel = new Phone { Mobile = mobile,Nationcode = "86"/*国家标识*/ },
                Time = time,
                Tpl_id = tpl_id
            };
            string url = $"https://yun.tim.qq.com/v5/tlssmssvr/sendsms?sdkappid={appId}&random={random}";
            string postDataStr = JsonConvert.SerializeObject(postData).ToLower();
            LogHelper.WriteLog("sendmsg postData"+postDataStr, LogHelper.LogType.Info);
            string result = SendTools.HttpPost(url, postDataStr);
            LogHelper.WriteLog("sendmsg result" + result, LogHelper.LogType.Info);
            return result;
        }
    }
    /// <summary>
    /// 参数类
    /// </summary>
    public class SendCode
    {
        public string Ext { get; set; }
        public string Extend { get; set; }
        public string[] Params { get; set; }
        public string Sig { get; set; }
        public string Sign { get; set; }
        public Phone Tel { get; set; }
        public string Time { get; set; }
        public string Tpl_id { get; set; }
    }
    /// <summary>
    /// 电话参数
    /// </summary>
    public class Phone
    {
        public string Mobile { get; set; }
        public string Nationcode { get; set; }
    }
    public static class SendTools
    {
        /// <summary>
        /// 返回指定个数的随机数
        /// </summary>
        /// <param name="length">个数</param>
        /// <returns>随机数</returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
        /// <summary>
        /// 获取时间戳格式
        /// </summary>
        /// <param name="flg">多少位的时间戳</param>
        /// <returns>时间戳</returns>
        public static long GetTimeStamp(int flg)
        {
            long time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            switch (flg)
            {
                case 10:
                    time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                    break;
                case 13:
                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
                    time = (DateTime.Now.Ticks - startTime.Ticks) / 10000;
                    break;
            }
            return time;
        }
        /// <summary>
        /// Sha256加密算法
        /// </summary>
        /// <param name="data">加密的内容</param>
        /// <returns>加密后的数据</returns>
        public static string Sha256(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString().ToLower();           
        }
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postdata">参数</param>
        /// <returns>返回内容</returns>
        public static string HttpPost(string url, string postdata)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Referer = null;
            req.AllowAutoRedirect = true;
            req.Accept = "*/*";

            byte[] data = Encoding.UTF8.GetBytes(postdata);
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
            }
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
