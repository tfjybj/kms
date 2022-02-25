using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dingdingsuccess.Log4;
namespace dingdingsuccess
{
    /// <summary>
    /// 发送群链接消息
    /// </summary>
    public class SendDingGroupMessage
    {
        public void SendMessage(string code,string CalendarEventId,string userid)
        {
            string url = string.Format("https://oapi.dingtalk.com/chat/send?access_token={0}", AccessToken.GetAccessToken());
            string texturl= string.Format("http://d-kms.dmsd.tech/CallRoom.aspx?{0}.{1}", CalendarEventId,userid);
            DingMessageEntity model = new DingMessageEntity();
            Link link = new Link();
            model.chatid = code;
            model.msgtype = "link";
            link.messageUrl = texturl;
            link.text = "请点击预约会议室";
            link.title = "可以看看有没有您想要的会议室";
            link.picUrl = "@lADOADmaWMzazQKA";
            model.link = link;
            //model.groupName = "会议室预约";
            //model.messageTitle = "可以看看有没有您想要的会议室";
            //model.messageContent = "请点击预约会议室";
            string json= JsonConvert.SerializeObject(model);
            HttpHelper helper = new HttpHelper();
            string message= helper.HttpPost(url,json);
            //打印日志
            LoggerHelper.Info("发送消息日志："+message);
        }
    }
}