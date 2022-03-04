/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：钉钉创建群返回值实体
 */

namespace dingdingsuccess
{
    /// <summary>
    /// 钉钉创建群返回值
    /// </summary>
    public class CreateGroupReturnEntity
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string chatid { get; set; }
        public string openConversationId { get; set; }
        public int conversationTag { get; set; }
        
    }

}