/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：基本数据配置表D层操作类
 */
using Newtonsoft.Json;
using KmsService.DingDingModel;
using KmsService.Log4;

namespace KmsService.DingDingInterface
{
    /// <summary>
    /// 自动同意审批
    /// </summary>
    public class AgreeRequest
    {
        public string AgreeRequestInfo(AgreeRequestModel model, Request agreeRequest)
        {
            //获取访问token
            AccessToken access = new AccessToken();
            string token = access.GetAccessToken();
            string url = string.Format("https://oapi.dingtalk.com/topapi/process/instance/execute?access_token={0}", token);
            model.request = agreeRequest;
            string jsonData = JsonConvert.SerializeObject(model);
            HttpHelper httpHelper = new HttpHelper();
            //执行url
            string result = httpHelper.HttpPost(url, jsonData);
            LoggerHelper.Info("调用钉钉自动同意审批接口的结果：" + result);
            return result;
        }
    }
}