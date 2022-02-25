using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess
{

    //public class DingMessageModel
    //{
    //    public string[] dingIds { get; set; }
    //    public string groupName { get; set; }
    //    public string messageContent { get; set; }
    //    public string messageTitle { get; set; }
    //    public string messageUrl { get; set; }
    //    public string picUrl { get; set; }
    //    public string sender { get; set; }
    //}

    /// <summary>
    /// 发送消息到企业群model
    /// </summary>
    public class DingMessageEntity
    {
        /// <summary>
        /// 群ID
        /// </summary>
        public string chatid { get; set; }
       /// <summary>
       /// 消息类型
       /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public Link link { get; set; }


    }
    public class Link
    {
        /// <summary>
        /// URL链接
        /// </summary>
        public string messageUrl { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        ///消息标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string text { get; set; }
    }




}