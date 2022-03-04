using Newtonsoft.Json;
using KmsService.DingDingModel;
using KmsService.Log4;

namespace KmsService.DingDingInterface
{
    public class ApproverMessage
    {
        /// <summary>
        /// 发送群消息链接接口
        /// </summary>
        /// <param name="model"></param>
        /// <param name="link">具体消息内容实体类</param>
        public void SendMessage(DingMessageModel model, Link link)
        {
            AccessToken access = new AccessToken();
            string url = string.Format("https://oapi.dingtalk.com/chat/send?access_token={0}", access.GetAccessToken());
            //string texturl = textUrl;
            //string texturl = string.Format("http://d-kms.dmsd.tech/CallRoom.aspx?CalendarEventId={0}", CalendarEventId);
            //DingMessageModel model = new DingMessageModel();
            //Link link = new Link();
            //model.chatid = code;
            //model.msgtype = "link";
            //link.messageUrl = textUrl;
            //link.text = "请点击预约会议室";
            //link.title = "可以看看有没有您想要的会议室";
            //link.picUrl = "@lADOADmaWMzazQKA";
            //model.groupName = "会议室预约";
            //model.messageTitle = "可以看看有没有您想要的会议室";
            //model.messageContent = "请点击预约会议室";
            model.link = link;
            //model.link = link;
            string json = JsonConvert.SerializeObject(model);
            HttpHelper helper = new HttpHelper();
            string message = helper.HttpPost(url, json);
            //打印日志
            LoggerHelper.Info("调用钉钉发送群消息链接接口的结果：" + message);
        }
    }
}