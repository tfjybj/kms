
/*
 * 创建人：王梦杰
 * 时间：2022年1月5日08:19:23
 * 描述：每周给用户发送周报
 */
using Newtonsoft.Json;


namespace 定时任务
{

    /*
     功能：给用户推送周报表的接口
     */
    public class PushWeeklyReport
    {
        /// <summary>
        /// 发送卡片消息
        /// </summary>
        /// <param name="rootobject">消息实体</param>
        public void Message(Rootobject rootobject)
        {
            //发送卡片消息的url
            string url = "http://d-msg.dmsd.tech:8002/dingmessage/send/simpleCardMsg";
            
            
            string json = JsonConvert.SerializeObject(rootobject);//var类型强转为json串类型
            HttpHelper helper = new HttpHelper();
            string result = helper.HttpPost(url, json);//执行message接口
        }
    }
}