/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：钉钉回调返回值实体
 */

namespace dingdingsuccess
{
    /// <summary>
    /// 钉钉回调返回值类
    /// </summary>
    public class DingDingBackEntity
    {
        public string msg_signature { get; set; }
        public string encrypt { get; set; }
        public string timeStamp { get; set; }
        public string nonce { get; set; }
    }
}