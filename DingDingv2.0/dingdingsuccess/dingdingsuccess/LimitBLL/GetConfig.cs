using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace dingdingsuccess.LimitBLL
{
    public class GetConfig
    {
        public static string GetConfigValue(string sectionName, string key)
        {
            //去dingID这个配置节找相应的dingID
            NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            return nameValueCollection.Get(key);
        }
    }
}