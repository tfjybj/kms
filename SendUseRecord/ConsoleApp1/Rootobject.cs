/*
 * 创建人：王梦杰
 * 创建时间：2022年1月5日08:55:31
 * 描述：卡片消息实体
 */

namespace 定时任务
{

    /// <summary>
    /// 卡片消息实体
    /// </summary>
    public class Rootobject
    {
        /// <summary>
        /// dingdingID
        /// </summary>
        public string[] dingIds { get; set; }
        /// <summary>
        /// 群名
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string messageContent { get; set; }

        /// <summary>
        /// 消息单独标题
        /// </summary>
        public string messageSingleTitle { get; set; }
        /// <summary>
        /// 消息单独url
        /// </summary>
        public string messageSingleUrl { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string messageTitle { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public string sender { get; set; }
    }

}
