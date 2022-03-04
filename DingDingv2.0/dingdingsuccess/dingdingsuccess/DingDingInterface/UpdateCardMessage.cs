// This file is auto-generated, don't edit it. Thanks.
/*
 * 创建人：盖鹏军
 * 时间：2022年1月6日14点58分
 * 描述：更新互动卡片消息
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using dingdingsuccess.Log4;
using Tea;
using Tea.Utils;
using dingdingsuccess.DingDingEntity;

namespace dingdingsuccess
{
    /// <summary>
    /// 更新互动卡片消息
    /// </summary>
    public class UpdateCardMessage
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
        /// 更新卡片信息
        /// </summary>
        /// <param name="cardDataCardParamMap">传入更新卡片所需参数</param>
        public static void UdateCard(Dictionary<string ,string> cardDataCardParamMap)
        {
            AlibabaCloud.SDK.Dingtalkim_1_0.Client client = CreateClient();
            AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardHeaders updateInteractiveCardHeaders = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardHeaders();
            updateInteractiveCardHeaders.XAcsDingtalkAccessToken = AccessToken.GetRobotAccessToken();




            //Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
            //{
            //    {"title",callBackMessageEntity.title },
            //    {"date",callBackMessageEntity.date},
            //    {"usagetime",callBackMessageEntity.usagetime },
            //    {"used", state}
            //};
            AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions cardOptions = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions
            {
                UpdateCardDataByKey = false,
                UpdatePrivateDataByKey = false,
            };
            AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardData cardData = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardData
            {
                CardParamMap = cardDataCardParamMap,

            };
            AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardRequest updateInteractiveCardRequest = new AlibabaCloud.SDK.Dingtalkim_1_0.Models.UpdateInteractiveCardRequest
            {
                OutTrackId = cardDataCardParamMap["outTrackId"].ToString(),
                CardData = cardData,               
                UserIdType = 1,
                CardOptions = cardOptions

            };
            try
            {
                LoggerHelper.Info("更新卡片信息：" +updateInteractiveCardRequest.OutTrackId+ cardDataCardParamMap.Values+ "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                client.UpdateInteractiveCardWithOptions(updateInteractiveCardRequest, updateInteractiveCardHeaders, new AlibabaCloud.TeaUtil.Models.RuntimeOptions());
            }
            catch (TeaException err)
            {
                if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
                {
                    // err 中含有 code 和 message 属性，可帮助开发定位问题
                    LoggerHelper.Error("更新卡片错误信息："+"编码"+err.Code+","+err.Message+"\n堆栈信息"+err.StackTrace+"、"+err.Source);
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