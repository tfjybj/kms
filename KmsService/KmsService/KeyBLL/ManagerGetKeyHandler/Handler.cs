/*
 * 创建人：盖鹏军
 * 创建时间：2022年1月24日14点14分
 * 描述：管理员领取钥匙职责链
 */

namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    public abstract class Handler
    {
        protected Handler successor;

        /// <summary>
        /// 开锁抽象方法
        /// </summary>
        /// <param name="roomID">会议室ID</param>
        public abstract void OpenLock(string roomID);
    }
}