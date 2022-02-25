// This file is auto-generated, don't edit it. Thanks.

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dingdingsuccess.Log4;
using Tea;
using Tea.Utils;

namespace dingdingsuccess.DingDingInterface
{
    public class RobotSendText
    {

        /**
         * 使用 Token 初始化账号Client
         * @return Client
         * @throws Exception
         */
        public static AlibabaCloud.SDK.Dingtalkrobot_1_0.Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config();
            config.Protocol = "https";
            config.RegionId = "central";
            return new AlibabaCloud.SDK.Dingtalkrobot_1_0.Client(config);
        }

        /// <summary>
        /// 机器人发送文本消息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="content">文本内容</param>
        public static void RobotSendMessage(string userID, string content)
        {
            AlibabaCloud.SDK.Dingtalkrobot_1_0.Client client = CreateClient();
            AlibabaCloud.SDK.Dingtalkrobot_1_0.Models.BatchSendOTOHeaders batchSendOTOHeaders = new AlibabaCloud.SDK.Dingtalkrobot_1_0.Models.BatchSendOTOHeaders();
            batchSendOTOHeaders.XAcsDingtalkAccessToken = AccessToken.GetRobotAccessToken();
            var textContent = new
            {
                content = content
            };
            string text = JsonConvert.SerializeObject(textContent);
            AlibabaCloud.SDK.Dingtalkrobot_1_0.Models.BatchSendOTORequest batchSendOTORequest = new AlibabaCloud.SDK.Dingtalkrobot_1_0.Models.BatchSendOTORequest
            {
                RobotCode = "dingefydqbitushwpwyd",
                UserIds = new List<string>
                {
                    userID
                },
                MsgKey = "officialTextMsg",
                MsgParam = text
            };
            try
            {
                client.BatchSendOTOWithOptions(batchSendOTORequest, batchSendOTOHeaders, new AlibabaCloud.TeaUtil.Models.RuntimeOptions());
            }
            catch (TeaException err)
            {
                if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
                {
                    // err 中含有 code 和 message 属性，可帮助开发定位问题
                    LoggerHelper.Error("发送文本类型方法错误信息：" + err.Code + "--" + err.Message);
                }
            }
            catch (Exception _err)
            {
                TeaException err = new TeaException(new Dictionary<string, object>
                {
                    { "message", _err.Message }
                });
                if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
                {
                    // err 中含有 code 和 message 属性，可帮助开发定位问题
                }
            }
        }


    }
}