/*
 * 创建人：盖鹏军
 * 时间：2022年4月1日10点30分
 * 描述：判断是否为配置资源命令
 */
using dingdingsuccess.Log4;
using System;
using System.Configuration;

namespace dingdingsuccess.BobotHandler
{
    /// <summary>
    /// 管理员发送消息，配置会议室资源命令
    /// </summary>
    public class BasicConfigurationHandler : DialogueHandler
    {
        string authurl;
        string url;

        /// <summary>
        /// 判断用户发送内容是否和资源匹配
        /// </summary>
        /// <param name="ddID"></param>
        /// <param name="content"></param>
        public override void HandleRequest(string ddID, string content)
        {
            foreach (var item in userResourceEntity.data.resourcesTree)//遍历资源
            {
                LoggerHelper.Info("遍历资源：判断是否是会议室资源");
                if (item.name == "会议室")
                {
                    authurl = item.url;

                    LoggerHelper.Info("用户功能实现职责链的第一层资源信息：" + authurl + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数" + LoggerHelper.GetLineNum());

                    foreach (var textd in item.child)
                    {
                        if (content.Contains(textd.name))
                        {

                            authurl = ConfigurationManager.ConnectionStrings["header"].ConnectionString + authurl + textd.url;

                            LoggerHelper.Info("用户功能实现职责链的接口地址：" + authurl + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                            string basicUrl = ConfigurationManager.ConnectionStrings["BasicConfiguration"].ConnectionString;
                            url = String.Format(authurl, ddID, basicUrl);

                            LoggerHelper.Info("用户功能实现职责链的资源信息：" + url + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                            httpHelper.HttpPost(url);

                        }
                        else
                        {
                            LoggerHelper.Info("管理员会议室设置配置资源判断未通过" + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                            successor.HandleRequest(ddID, content);
                        }

                    }
                }



            }

        }
    }
}