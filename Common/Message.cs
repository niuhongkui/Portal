using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;
using System.Configuration;

namespace Common
{
    public class Message
    {
        // 短信应用 SDK AppID
        static int appid = Convert.ToInt32(ConfigurationManager.AppSettings["msg_appid"]);
        // 短信应用 SDK AppKey
        static string appkey = ConfigurationManager.AppSettings["msg_appkey"];

        static string logoname = ConfigurationManager.AppSettings["msg_logoname"];

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="strCode"></param>
        public static ApiMessage<string> Send(string strPhone, string strCode)
        {
            var msg = new ApiMessage<string>();
            try
            {
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                var result = ssender.send(0, "86", strPhone, "【" + logoname + "】您的验证码是: " + strCode, "", "");
                if (result.result == 0)
                    return msg;
                msg.Success = false;
                msg.Msg = result.errMsg;
                return msg;
            }
            catch (JSONException e)
            {
                msg.Success = false;
                msg.Msg = e.Message;
                return msg;
            }
            catch (HTTPException e)
            {
                msg.Success = false;
                msg.Msg = e.Message;
                return msg;
            }
            catch (Exception e)
            {
                msg.Success = false;
                msg.Msg = e.Message;
                return msg;
            }
        }

        public static ApiMessage<string> Get(string strPhone)
        {
            return null;
        }
    }
}
