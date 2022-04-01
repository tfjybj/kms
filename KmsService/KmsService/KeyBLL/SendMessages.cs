/*
 * 创建人：王梦杰
 * 创建日期：2022年3月12日19:45:39
 * 描述：给用户发消息的方法
 */
using System.Configuration;
using KmsService.Log4;
namespace KmsService.KeyBLL
{
    /*

     功能：给用户发消息的方法，
     */
    public class SendMessages
    {
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