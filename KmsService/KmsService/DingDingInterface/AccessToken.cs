/*
 * 创建人：王梦杰
 * 时间：2021年12月7日11:01:39
 * 描述：调用钉钉获取accesstoken接口
 */

using Newtonsoft.Json.Linq;
using System.Configuration;
namespace KmsService.DingDingInterface
{
    /// <summary>
    /// 获取访问token
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 获取访问token
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            string appKey = ConfigurationManager.ConnectionStrings["appkey"].ConnectionString;
            string appSecret = ConfigurationManager.ConnectionStrings["appsecret"].ConnectionString;
            //设置要执行的url地址
            string url = string.Format("https://oapi.dingtalk.com/gettoken?appkey={0}&appsecret={1}",appKey,appSecret);
            //执行url
            HttpHelper helper = new HttpHelper();
            //接收返回的json串
            JToken json = JToken.Parse(helper.HttpGet(url));
            //将json串中的指定值取出
            string accesstoken = json["access_token"].ToString();
            //LoggerHelper.Error("调用钉钉接口获取的访问token："+accesstoken);
            //返回结果
            return accesstoken;
        }
    }
}