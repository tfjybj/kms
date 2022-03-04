using Newtonsoft.Json;

namespace KmsService.DingDingInterface
{
    /*
     功能：给用户推送周报表的接口
     */

    public class PushWeeklyReport
    {
        public void message(string ddID)
        {
            string url = "http://d-msg.dmsd.tech:8002/dingmessage/send/simpleCardMsg";

            var mes = new
            {
                dingIds = ddID,
                groupName = "",
                messageContent = "请查收您一周的会议使用情况",
                messageSingleTitle = "",
                messageSingleUrl = "http://localhost:53727/DataSummary.aspx" + ddID,
                messageTitle = "一周的会议使用情况",
                sender = "",
            };
            string json = JsonConvert.SerializeObject(mes);//var类型强转为json串类型
            HttpHelper helper = new HttpHelper();
            helper.HttpPost(url, json);//执行message接口
        }
    }
}