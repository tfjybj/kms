  
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 定时任务
{

    /*
     功能：给用户推送周报表的接口
     */
    public class PushWeeklyReport
    {
        public void message(Rootobject rootobject)
        {
            
            string url =  "http://d-msg.dmsd.tech:8002/dingmessage/send/simpleCardMsg";//调用的message接口
            
            string json = JsonConvert.SerializeObject(rootobject);//var类型强转为json串类型
            HttpHelper helper = new HttpHelper();
            string result = helper.HttpPost(url, json);//执行message接口
        }
    }
}