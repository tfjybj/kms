/*
 * 创建人：王梦杰
 * 创建时间：2021年12月21日10:30:55
 * 描述：还钥匙基本逻辑代码
 */

using System;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    public class ReturnKeyBLL
    {
        /// <summary>
        /// 还钥匙逻辑
        /// </summary>
        /// <param name="approveID">审批实例ID</param>
        /// <param name="roomInfo">房间信息实体</param>
        public void ReturnKey(string calendarID)
        {
            try
            {
                //订阅MQTTServer服务
                MQTTServer.Instance();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用动态归还钥匙方法错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }
        }
    }
}