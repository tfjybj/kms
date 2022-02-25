using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using dingdingsuccess.KmsServiceReference;
using dingdingsuccess.Log4;
namespace dingdingsuccess.BobotHandler
{
    public class ResourcesHandler : DialogueHandler
    {
        
        /// <summary>
        /// 判断用户发送内容是否和资源相匹配
        /// </summary>
        /// <param name="ddID"></param>
        /// <param name="content"></param>
        public override void HandleRequest(string ddID, string content)
        {
            string authurl;
            string url;
            #region 循环权限返回值中的具体资源，进行资源名称和消息内容判断是否匹配
            //foreach (var item in userResourceEntity.data.resourcesTree)
            //{
            //    if (content.Contains(item.name))
            //    {
            //        if (item.name.Contains("无参"))
            //        {
            //            httpHelper.HttpPost(item.url);
            //        }
            //        else
            //        {

            //        }

            //    }

            //}
            #endregion

            try
            {
                foreach (var item in userResourceEntity.data.resourcesTree)
                {
                    authurl = item.url;
                    LoggerHelper.Info("用户功能实现职责链的第一层资源信息：" + authurl);
                    foreach (var textd in item.child)
                    {
                        LoggerHelper.Info("用户功能实现职责链的第二层层资源名称信息：" + textd.name);
                        if (content.Contains(textd.name))
                        {
                            if (serviceClient.GetRoom(content)!=null)
                            {
                                authurl = ConfigurationManager.ConnectionStrings["header"].ConnectionString + authurl + textd.url;
                                url = string.Format(authurl, ddID, "请到研发中心领取钥匙：" + serviceClient.GetRoom(content), userEntity.data.name);
                                LoggerHelper.Info("用户功能实现职责链的资源信息：" + url);
                                httpHelper.HttpGet(url);
                            }
                            

                        }

                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Error("用户功能实现职责链的错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
                throw;
            }

        }
    }
}