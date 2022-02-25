using dingdingsuccess.AuthEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dingdingsuccess.KmsServiceReference;
namespace dingdingsuccess.BobotHandler
{
    public abstract class Handler
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
        protected static ServiceClient serviceClient=new ServiceClient();
        

        protected Handler successor;
        
        /// <summary>
        /// 设置职责链中的继任者
        /// </summary>
        /// <param name="successor"></param>
        public void SetSuccessor(Handler successor)
        {
           this.successor = successor;
        }

        public abstract void HandleRequest(string ddID,string content);



    }
}