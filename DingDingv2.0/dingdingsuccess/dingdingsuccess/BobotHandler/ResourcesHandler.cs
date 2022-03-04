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
                    LoggerHelper.Info("用户功能实现职责链的第一层资源信息：" + authurl+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                    foreach (var textd in item.child)
                    {
                        LoggerHelper.Info("用户功能实现职责链的第二层层资源名称信息：" + textd.name+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                        if (content.Contains(textd.name))
                        {
                            string roomName = serviceClient.GetRoom(content, ddID);
                            LoggerHelper.Info("用户功能实现职责链的参数信息：" + userEntity.data.name+"、"+roomName+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                            if (roomName != null)
                            {
                                authurl = ConfigurationManager.ConnectionStrings["header"].ConnectionString + authurl + textd.url;
                                LoggerHelper.Info("用户功能实现职责链接口地址：" + authurl + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                                url = string.Format(authurl, ddID,"请到研发中心领取钥匙："+ roomName, userEntity.data.name);
                                LoggerHelper.Info("用户功能实现职责链的资源信息：" + url+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                                httpHelper.HttpGet(url);
                            }
                            
                        }
                        else
                        {
                            LoggerHelper.Info("管理员领取钥匙资源判断未通过"+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                        }

                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Error("用户功能实现职责链的错误信息：" + e.Message + "\n具体信息：" + e.StackTrace);
                
            }

        }
    }
}