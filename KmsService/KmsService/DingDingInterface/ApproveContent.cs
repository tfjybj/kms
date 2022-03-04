/*
 * 创建人：王梦杰
 * 时间：2021年12月6日11:01:39
 * 描述：调用钉钉获取审批实例详情接口
 */

using Newtonsoft.Json;
using KmsService.DingDingModel;
using KmsService.Log4;

namespace KmsService.DingDingInterface
{
    public class ApproveContent
    {
        /// <summary>
        /// 获取审批实例详情
        /// </summary>
        /// <param name="processInstance">OA审批ID</param>
        /// <returns></returns>
        public ApproveContentTask GetApproveContent(string processInstance)
        {
            AccessToken accessToken = new AccessToken();
            string token = accessToken.GetAccessToken();
            //ApproveID approveID = new ApproveID();
            //string processInstance  = approveID.GetApproveID(startTime,endTime);
            string url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/get?access_token={0}", token);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "POST";

            var json_rep = new
            {
                process_instance_id = processInstance
            };
            string jsonData = JsonConvert.SerializeObject(json_rep);
            HttpHelper helper = new HttpHelper();
            string result = helper.HttpPost(url, jsonData);
            LoggerHelper.Info("调用钉钉获取审批实例详情接口的结果：" + result);
            ApproveContentTask model = JsonConvert.DeserializeObject<ApproveContentTask>(result);
            //string roomName = model.process_instance.form_component_values[0].value;
            //string operation_result = model.process_instance.result;
            return model;
        }
    }
}