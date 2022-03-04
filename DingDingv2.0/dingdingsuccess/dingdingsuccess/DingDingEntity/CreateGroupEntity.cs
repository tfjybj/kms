/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：创建钉钉群
 */

namespace dingdingsuccess
{
    /// <summary>
    /// 创建钉钉群
    /// </summary>
    public class CreateGroupEntity
    {
        public string name { get; set; }
        public string owner { get; set; }
        public string[] useridlist { get; set; }
    }

}