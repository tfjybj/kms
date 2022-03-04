/*
 * 创建人：王梦杰
 * 创建时间：2022年1月7日10:25:36
 * 描述：根据人数推送会议室
 */

using System;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 根据人数推送会议室
    /// </summary>
    public class PushRoomBLL
    {
        /// <summary>
        /// 根据参会人数推送会议室名称
        /// </summary>
        /// <param name="participants">参会人数</param>
        /// <returns>会议室名称</returns>
        public string PushRoom(int participants)
        {
            string roomName = null;
            try
            {
                ConcretePushTwentyRoomBLL concretePushTwenty = new ConcretePushTwentyRoomBLL();
                ConcretePushFourtyRoomBLL concretePushFourty = new ConcretePushFourtyRoomBLL();
                ConcretePushLargeFourtyBLL largeFourtyBLL = new ConcretePushLargeFourtyBLL();
                //设置上下级
                concretePushTwenty.SetSuccessor(concretePushFourty);
                concretePushFourty.SetSuccessor(largeFourtyBLL);
                //返回结果
                roomName = concretePushTwenty.HandleRequest(participants);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用根据参会人数推送相应会议室方法的错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }
            return roomName;
        }
    }
}