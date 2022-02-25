/*
 * 创建人：王梦杰
 * 时间：2021年12月7日11:01:39
 * 描述：调用钉钉获取accesstoken接口
 */
using Newtonsoft.Json.Linq;
using dingdingsuccess.Log4;
using dingdingsuccess.LimitBLL;
namespace dingdingsuccess
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
        public static string GetAccessToken()
        {

            //设置要执行的url地址
            string url = string.Format( "https://oapi.dingtalk.com/gettoken?appkey={0}&appsecret={1}",GetConfig.GetConfigValue("basicData", "Appkey"), GetConfig.GetConfigValue("basicData", "AppSecret"));

            //执行url
            HttpHelper helper = new HttpHelper();
            //接收返回的json串
            JToken json = JToken.Parse(helper.HttpGet(url));
            //将json串中的指定值取出
            string accesstoken = json["access_token"].ToString();
            LoggerHelper.Info("获取访问token:"+accesstoken);
            //返回结果
            return accesstoken;
        }


        /// <summary>
        /// 获取机器人token
        /// </summary>
        /// <returns>返回机器人生成的token用于发送互动卡片</returns>
        public static string GetRobotAccessToken()
        {

            //设置要执行的url地址
            string url = string.Format( "https://oapi.dingtalk.com/gettoken?appkey={0}&appsecret={1}", GetConfig.GetConfigValue("basicData", "Robotkey"), GetConfig.GetConfigValue("basicData", "RobotSecret"));            
            //执行url
            HttpHelper helper = new HttpHelper();
            //接收返回的json串
            JToken json = JToken.Parse(helper.HttpGet(url));
            //将json串中的指定值取出
            string accesstoken = json["access_token"].ToString();

            //返回结果
            return accesstoken;
        }



    }
}