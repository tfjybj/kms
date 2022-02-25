// This file is auto-generated, don't edit it. Thanks.

/*
 * 创建人：盖鹏军
 * 时间：2022年1月6日14点58分
 * 描述：给用户发送互动卡片消息
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dingdingsuccess.Log4;
using Tea;
using Tea.Utils;


namespace dingdingsuccess
{
    /// <summary>
    /// 发送卡片消息
    /// </summary>
    public class SampleCardMessage
    {

        /**
         * 使用 Token 初始化账号Client
         * @return Client
         * @throws Exception
         */
        public static AlibabaCloud.SDK.Dingtalkim_1_0.Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config();
            config.Protocol = "https";
            config.RegionId = "central";
            return new AlibabaCloud.SDK.Dingtalkim_1_0.Client(config);
        }

        /// <summary>
        /// 发送卡片消息
        /// </summary>
        /// <param name="cardDataCardParamMap">卡片内容字典</param>
        public static void SendCardMessage(Dictionary<string, string> cardDataCardParamMap)
        {
            AlibabaCloud.SDK.Dingtalkim_1_0.Client client = CreateClient();
            AlibabaCloud.SDK.Dingtalkim_1_0.Models.SendInteractiveCardHeaders sendInteractiveCardHeaders = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.SendInteractiveCardHeaders();
            sendInteractiveCardHeaders.XAcsDingtalkAccessToken = AccessToken.GetRobotAccessToken();
            try
            {

                AlibabaCloud.SDK.Dingtalkim_1_0.Models.SendInteractiveCardRequest.SendInteractiveCardRequestCardData cardData = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.SendInteractiveCardRequest.SendInteractiveCardRequestCardData
                {
                    CardParamMap = cardDataCardParamMap,
                };
                AlibabaCloud.SDK.Dingtalkim_1_0.Models.SendInteractiveCardRequest sendInteractiveCardRequest = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.SendInteractiveCardRequest
                {
                    //卡片消息的模板ID
                    CardTemplateId = cardDataCardParamMap["CardTemplateId"].ToString(),
                    //用户ID
                    ReceiverUserIdList = new List<string>
                {
                  cardDataCardParamMap[ "userid"].ToString()
                },
                    //外部唯一标识一条卡片消息的字段
                    OutTrackId = cardDataCardParamMap["outTrackId"].ToString(),
                    //
                    ConversationType = 0,
                    //卡片消息使用到的回调函数
                    CallbackRouteKey = cardDataCardParamMap["CallbackRouteKey"].ToString(),
                    CardData = cardData,
                };

                
                client.SendInteractiveCardWithOptions(sendInteractiveCardRequest, sendInteractiveCardHeaders, new AlibabaCloud.TeaUtil.Models.RuntimeOptions());
            }
            catch (TeaException err)
            {
                if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
                {
                    // err 中含有 code 和 message 属性，可帮助开发定位问题
                    LoggerHelper.Error("发送卡片错误信息：" + "编码" + err.Code + "," + err.Message);
                }
            }
            catch (Exception _err)
            {
                LoggerHelper.Error("发送消息卡片接口错误日志："+_err.Message);
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


        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns>时间戳</returns>
        private static string GetTimeStamp()
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((DateTime.Now - dateStart).TotalSeconds);
            return timeStamp.ToString();
        }

    }
}