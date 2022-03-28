using dingdingsuccess.Log4;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace dingdingsuccess.BobotHandler
{
    public class BasicConfigurationHandler : DialogueHandler
    {
        string authurl;
        string url;

        public override void HandleRequest(string ddID, string content)
        {
            foreach (var item in userResourceEntity.data.resourcesTree)//遍历资源
            {
                if (item.name == "会议室")
                {
                    authurl = item.url;

                    LoggerHelper.Info("用户功能实现职责链的第一层资源信息：" + authurl + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数" + LoggerHelper.GetLineNum());

                    foreach (var textd in item.child)
                    {
                        if (content.Contains(textd.name))
                        {

                            authurl= ConfigurationManager.ConnectionStrings["header"].ConnectionString + authurl + textd.url;

                            LoggerHelper.Info("用户功能实现职责链的接口地址：" + authurl + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                            url = string.Format(authurl, ddID, "http://192.168.60.140/" );

                            LoggerHelper.Info("用户功能实现职责链的资源信息：" + url + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                            httpHelper.HttpPost(url);

                        }

                    }
                }


                
            }

        }
    }
}