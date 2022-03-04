/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：根据unionid获取用户ID实体
 */

namespace dingdingsuccess
{
    /// <summary>
    /// 根据unionid获取用户ID
    /// </summary>
    public class UserEntity
    {

        public string errcode { get; set; }
        public string errmsg { get; set; }
        public Result result { get; set; }
        public string request_id { get; set; }
       
        
        public class Result
        {
            public string contact_type { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public string userid { get; set; }
        }
    }



}