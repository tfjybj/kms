using System.Collections.Generic;
using System.Configuration;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.Log4;
namespace KmsService.KeyBLL
{
    /*

     功能：给用户发消息的方法，
     */
    public class SendMessages
    {
        public void SendMessageUser()
        {
            PersonReportDAL prd = new PersonReportDAL();
            PushWeeklyReport pwr = new PushWeeklyReport();
            List<string> dd = prd.AllddID();//获取查到的钉id

            foreach (var item in dd)
            {
                pwr.message(item.ToString());//调用message接口，把获取的id传入
            }
        }

        /// <summary>
        /// 机器人发送文本消息
        /// </summary>
        /// <param name="dingID">用户钉钉ID</param>
        /// <param name="contetText">文本消息内容</param>
        public void SendBotbotText(string dingID,string contetText)
        {
            try
            {
                HttpHelper httpHelper = new HttpHelper();
                string url = string.Format("?userID={0}&content={1}", dingID, contetText);
                string result= httpHelper.HttpPost(ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + url);
                if (result==null)
                {
                    throw new System.Exception("消息发送失败");
                }
            }
            catch (System.Exception e)
            {

                LoggerHelper.Error("发送机器人消息错误信息：" + e.Message + "\n具体信息：" + e.StackTrace + "\n参数信息ID、内容：" + dingID + "、" + contetText);
            }

        }

    }
}