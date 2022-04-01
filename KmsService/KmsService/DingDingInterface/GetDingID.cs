/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：获取dingID操作类
 */
using Newtonsoft.Json;

namespace KmsService.DingDingInterface
{
    /// <summary>
    /// 获取dingID
    /// </summary>
    public class GetDingID
    {
        /// <summary>
        /// 根据unionid获取用户ID
        /// </summary>
        /// <param name="code">根据unionid获取用户ID</param>
        /// <returns>用户ID</returns>
        public string unionid(string code)
        {
            AccessToken accessToken = new AccessToken();
            string url = string.Format("https://oapi.dingtalk.com/topapi/user/getbyunionid?access_token={0}", accessToken.GetAccessToken());

            var json_rep = new
            {
                unionid = code
            };
            string jsonData = JsonConvert.SerializeObject(json_rep);
            HttpHelper helper = new HttpHelper();
            string result = helper.HttpPost(url, jsonData);

            UserEntity model = JsonConvert.DeserializeObject<UserEntity>(result);

            return model.result.userid;
        }
    }
}