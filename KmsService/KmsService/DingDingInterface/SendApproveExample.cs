/*
 * 创建人：王梦杰
 * 时间：2021年12月7日11:01:39
 * 描述：调用钉钉发起审批接口
 */

using Newtonsoft.Json;
using System.Net;
using KmsService.DingDingModel;
using KmsService.Log4;

namespace KmsService.DingDingInterface
{
    public class SendApproveExample
    {
        /// <summary>
        /// 发送审批实例
        /// </summary>
        /// <param name="sendApproveModel">审批模板实体</param>
        public SendApproveRe_valueModel SendApprove(SendApproveModel sendApproveModel)
        {
            //获取访问token
            AccessToken accessToken = new AccessToken();
            string token = accessToken.GetAccessToken();
            //审批流唯一码
            string processCode = "PROC-GTHKCO8W-Q9TRGUB0Q4OTTLZBUNGW1-XS358GCJ-1";
            //url地址
            string url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/create?access_token={0}", token);

            //定义一个web请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求类型为POST
            request.Method = "POST";
            //设置参数
            SendApproveModel model = sendApproveModel;
            model.process_code = processCode; //审批流唯一码
            //将返回的json串转换为string
            string str = JsonConvert.SerializeObject(model);
            HttpHelper httpHelper = new HttpHelper();
            //执行url
            string result = httpHelper.HttpPost(url, str);
            //将返回值反序列化，保存审批ID、发起人ID
            SendApproveRe_valueModel valueModel = JsonConvert.DeserializeObject<SendApproveRe_valueModel>(result);
            //获取发起人ID
            valueModel.user_id = model.originator_user_id;
            LoggerHelper.Info("调用钉钉发送审批实例结果" + valueModel.errmsg);
            return valueModel;
        }
    }
}