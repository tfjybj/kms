using dingdingsuccess.AuthEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using dingdingsuccess.Log4;
namespace dingdingsuccess.BobotHandler
{
    public class PhoneHandler:DialogueHandler
    {

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="ddID"></param>
        /// <param name="content"></param>
        public override void HandleRequest(string ddID, string content)
        {
            string userUrl;
            try
            {
                //根据钉钉ID获取用户手机号
                userUrl = string.Format(ConfigurationManager.ConnectionStrings["getUserByDingid"].ConnectionString, ddID);
                httpHelper.HttpGet(userUrl);
                userEntity = JsonConvert.DeserializeObject<AuthUserEntity>(httpHelper.HttpGet(userUrl));
                if (userEntity.message != "查询为空!")
                {
                    LoggerHelper.Info("判断用户是否存在职责链的信息："+userEntity.data.phone);
                    successor.HandleRequest(ddID, content);
                }
                else
                {
                    throw new Exception("用户不存在");
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Error("判断用户是否存在职责链的错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
                
            }


        }
    }
}