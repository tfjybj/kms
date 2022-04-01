/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：获取unionID操作类
 */
using Newtonsoft.Json;
using KmsService.DingDingModel;
using KmsService.Log4;

namespace KmsService.DingDingInterface
{
    /// <summary>
    /// 获取unionID
    /// </summary>
    public class GetUnionID
    {
        /// <summary>
        /// 根据用户的userID获取用户信息
        /// </summary>
        /// <param name="userID">钉钉ID</param>
        /// <returns>unionID实体</returns>
        public GetUnionIDModel GetDingDingUnionID(string userID)
        {
            //获取访问token
            AccessToken access = new AccessToken();
            string token = access.GetAccessToken();
            //获取钉钉unionID的url
            string url = string.Format("https://oapi.dingtalk.com/topapi/v2/user/get?access_token={0}", token);

            HttpHelper helper = new HttpHelper();

            //GetUnionIDModel model = new GetUnionIDModel();

            //定义参数
            var json_rep = new
            {
                language = "zh_CN",
                userid = userID
            };
            //将参数转换为string类型
            string jsonData = JsonConvert.SerializeObject(json_rep);
            //执行url
            string result = helper.HttpPost(url, jsonData);
            LoggerHelper.Info("调用钉钉根据用户的userID获取用户信息的结果：" + result);
            //将返回值进行反序列化
            GetUnionIDModel userModel = JsonConvert.DeserializeObject<GetUnionIDModel>(result);

            return userModel;
        }
    }
}