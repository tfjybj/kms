/*
 * 创建人：盖鹏军
 * 时间：2022年4月1日10点30分
 * 描述：机器人对话职责链父类
 */
using dingdingsuccess.AuthEntity;
using dingdingsuccess.KmsServiceReference;
namespace dingdingsuccess.BobotHandler
{
    /// <summary>
    /// 机器人对话职责链
    /// </summary>
    public abstract class DialogueHandler
    {
        /// <summary>
        /// 接收权限返回值实体类
        /// </summary>
        protected static AuthUserResourceEntity userResourceEntity;
        protected static AuthUserEntity userEntity;
        /// <summary>
        /// 发送接口请求工具类
        /// </summary>
        protected static HttpHelper httpHelper = new HttpHelper();
        protected static ServiceClient serviceClient = new ServiceClient();


        protected DialogueHandler successor;

        /// <summary>
        /// 设置职责链中的继任者
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(DialogueHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(string ddID, string content);



    }
}