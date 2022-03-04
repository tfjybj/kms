/*
 *时间：2021年12月10日 14点47分
 *创建人：盖鹏军
 *接口功能：调用钉钉接口通过免登码获取用户信息
 */

using Newtonsoft.Json;
using KmsService.DingDingModel;

namespace KmsService.DingDingInterface
{
    /// <summary>
    /// 调用钉钉接口通过免登码获取用户信息
    /// </summary>
    public class DingUserInfo
    {
        /// <summary>
        /// 通过免登授权码获取用户信息
        /// </summary>
        /// <param name="code">免登授权码</param>
        /// <returns>返回用户信息</returns>
        public DingUserInfoModel UserInfo(string code)
        {
            AccessToken accessToken = new AccessToken();
            string url = string.Format("https://oapi.dingtalk.com/topapi/v2/user/getuserinfo?access_token={0}", accessToken.GetAccessToken());//
            HttpHelper helper = new HttpHelper();
            var josn = new
            {
                code = code
            };

            //将body参数进行序列化
            string jsonData = JsonConvert.SerializeObject(josn);

            //获取请求到的用户信息，进行反序列化
            DingUserInfoModel model = JsonConvert.DeserializeObject<DingUserInfoModel>(helper.HttpPost(url, jsonData));//进行post请求
            return model;
        }
    }
}