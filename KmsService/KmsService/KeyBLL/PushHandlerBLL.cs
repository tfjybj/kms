/*
 * 创建人：王梦杰
 * 创建时间：2022年1月7日10:27:45
 * 描述：职责链父类
 */

namespace KmsService.KeyBLL
{
    /// <summary>
    /// PushHandlerBLL类，定义一个处理请求的接口
    /// </summary>
    public abstract class PushHandlerBLL
    {
        protected PushHandlerBLL successor;

        /// <summary>
        /// 设置上下级
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(PushHandlerBLL successor)
        {
            this.successor = successor;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="Participants">参会人数</param>
        /// <returns>会议室名称</returns>
        public abstract string HandleRequest(int participants);
    }
}