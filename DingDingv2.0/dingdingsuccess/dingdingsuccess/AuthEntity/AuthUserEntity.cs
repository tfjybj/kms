/*
 * 创建人：盖鹏军
 * 时间：2022年4月1日10点29分
 * 描述：接收权限用户实体
 */
namespace dingdingsuccess.AuthEntity
{
    /// <summary>
    /// 权限用户实体
    /// </summary>
    public class AuthUserEntity
    {
        public string code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public int count { get; set; }
    }

    public class Data
    {
        public bool isSelect { get; set; }
        public string id { get; set; }
        public string userCode { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string tenantId { get; set; }
    }

}