/*
 * 创建人：盖鹏军
 * 时间：2022年1月1日10点30分
 * 描述：用于注册钉钉卡片回调方法
 */
using Newtonsoft.Json;

namespace dingdingsuccess
{
    /// <summary>
    /// 注册卡片回调类
    /// </summary>
    public class CallBack
    {
        /// <summary>
        /// 注册回调地址
        /// </summary>
        /// <param name="callbackURL">需要注册的回调地址</param>
        /// <returns>返回json</returns>
        public string BackMessage(string callbackURL)
        {

          
            //注册回调的钉钉接口
            string dingdingurl = string.Format("https://oapi.dingtalk.com/topapi/im/chat/scencegroup/interactivecard/callback/register?access_token={0}", AccessToken.GetAccessToken());
            var data = new
            {
                //需要注册的回调地址
                callback_url = callbackURL,
                //是否更新已经存在的回调地址
                forceUpdate = true,
                //回调地址名称一个企业下不可重名，不同的H5应用生成的token也不可，会被覆盖掉，所以注册的时候名称一定要不同
                callbackRouteKey = "T-CardMessage"
                
            };
            string json = JsonConvert.SerializeObject(data);
            HttpHelper httpHelper = new HttpHelper();
            string text = httpHelper.HttpPost(dingdingurl, json);

            return text;
        }
    }
}