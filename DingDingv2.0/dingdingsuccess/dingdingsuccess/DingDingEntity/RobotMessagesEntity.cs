using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess.DingDingEntity
{

    public class AtUsersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string dingtalkId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string staffId { get; set; }
    }

    public class Text
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string content { get; set; }
    }

    public class RobotMessagesEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string conversationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AtUsersItem> atUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chatbotCorpId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chatbotUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msgId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string senderNick { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isAdmin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string senderStaffId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sessionWebhookExpiredTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int createAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string senderCorpId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string conversationType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string senderId { get; set; }
        /// <summary>
        /// 机器人测试-TEST
        /// </summary>
        public string conversationTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isInAtList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sessionWebhook { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Text text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msgtype { get; set; }
    }


}