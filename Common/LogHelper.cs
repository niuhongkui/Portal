using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]
namespace Common
{
    public class LogHelper
    {
        private static ILog _iLog;
        /// <summary>
        /// 接口
        /// </summary>
        private static ILog Log
        {
            get
            {
                if (_iLog!= null)
                {
                    return _iLog;
                }
                _iLog = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

                return _iLog;
            }
        }


        public static void WriteLog(string msg,LogType type)
        {
            switch (type)
            {
                case LogType.Debug:
                    Log.Debug(msg);
                    break;
                case LogType.Info:
                    Log.Info(msg);
                    break;
                case LogType.Warn:
                    Log.Warn(msg);
                    break;
                case LogType.Error:
                    Log.Error(msg);
                    break;
                case LogType.Fatal:
                    Log.Fatal(msg);
                    break;
            }
        }

        public enum LogType
        {
            /// <summary>
            /// 调试
            /// </summary>
            Debug = 0,
            /// <summary>
            /// 信息
            /// </summary>
            Info = 1,
            /// <summary>
            /// 警告
            /// </summary>
            Warn = 2,
            /// <summary>
            /// 一般错误
            /// </summary>
            Error = 3,
            /// <summary>
            /// 致命错误
            /// </summary>
            Fatal = 4
        }
    }

}
