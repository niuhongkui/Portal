using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigHelper
    {
        
        public static string APIKey = ConfigurationManager.AppSettings["wxapikey"];
        public static string MchID = ConfigurationManager.AppSettings["wxmchid"];

        public static string WebSiteUrl = ConfigurationManager.AppSettings["return_url"];

        public static string AppID = ConfigurationManager.AppSettings["wxappid"];
    }
}
