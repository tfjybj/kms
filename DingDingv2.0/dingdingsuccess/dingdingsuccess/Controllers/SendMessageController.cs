using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dingdingsuccess.CardMessageBLL;
using dingdingsuccess.Log4;
using dingdingsuccess.DingDingInterface;
namespace dingdingsuccess.Controllers
{
    /// <summary>
    /// 调用发送卡片消息接口
    /// </summary>
    public class SendMessageController : ApiController
    {
        SendCardMessage sendCardMessage=new SendCardMessage();
        /// <summary>
        /// 发送钉钉领取钥匙卡片消息
        /// </summary>
        /// <param name="roomname">会议室名称</param>
        /// <param name="calendarid">日程ID</param>
        /// <param name="userid">用户ID</param>
        [HttpGet]
        public string SendReceiveCard(string roomName, string calendarID,string userID)
        {

            string result = null;
            try
            {

                result = sendCardMessage.SendGetKey(roomName, calendarID, userID);                
                return result;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送钉钉领取钥匙卡片消息：" + e.Message + "  具体信息：" + e.StackTrace);
                return result;
            }

        }

        /// <summary>
        /// 发送归还钥匙卡片消息
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户ID</param>
        [HttpGet]
        public string SendReturnKey(string roomName, string calendarID, string userID)
        {
            string result = null;
            try
            {

                //发送归还钥匙卡片消息
                sendCardMessage.SendReturnKey(roomName, calendarID, userID);
                result = "0";
                return result;
            }
            catch (Exception)
            {
                result = "1111";
                return result;
            }

        }


        /// <summary>
        /// 更新钉钉领取钥匙卡片消息
        /// </summary>
        /// <param name="roomname">会议室名称</param>
        /// <param name="outTrackId">卡片唯一标识</param>
        [HttpGet]
        public string UpdateReceiveCard(string roomName,string OutTrackId)
        {

            string result = null;
            try
            {

                sendCardMessage.UpdateGetKey(roomName, OutTrackId);
                result = "0";
                return result;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("更新钉钉领取钥匙卡片消息错误信息：" +e.Message + "  具体信息：" + e.StackTrace);
                result = "1111";
                return result;
            }

        }


        /// <summary>
        /// 发送询问值班卡片
        /// </summary>
        /// <param name="userID">用户钉钉ID</param>
        /// <returns></returns>
        [HttpGet]
        public string SendInquiryCard(string userID, string content)
        {
            string result = null;
            try
            {
                result = sendCardMessage.SendInquiryCard(userID, content);
                return result;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送文本类型方法错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
                return result;

            }

        }


        /// <summary>
        /// 发送机器人文本消息
        /// </summary>
        /// <param name="userID">钉钉ID</param>
        /// <param name="content">文本内容</param>
        /// <returns></returns>
        [HttpPost]
        public string SendRotbotText(string userID, string content)
        {
            try
            {
                RobotSendText.RobotSendMessage(userID, content);
                return "0";
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送文本类型方法错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
                return "1111";
            }
        }
    }
}