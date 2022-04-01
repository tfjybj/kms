using dingdingsuccess.AuthEntity;
using dingdingsuccess.Log4;
using Newtonsoft.Json;
using System;
using System.Configuration;
namespace dingdingsuccess.BobotHandler
{
    public class GradeHandler : DialogueHandler
    {
        /// <summary>
        /// 判断用户是否存在于本产品项目下
        /// </summary>
        /// <param name="ddID"></param>
        /// <param name="content"></param>
        public override void HandleRequest(string ddID, string content)
        {
            //string userUrl;
            string userResourceUrl = null;



            try
            {
                //根据用户手机号获取用户权限系统中的信息，参数设置到配置文件中
                userResourceUrl = string.Format(ConfigurationManager.ConnectionStrings["userResource"].ConnectionString, userEntity.data.userCode, ConfigurationManager.ConnectionStrings["productId"].ConnectionString);
                userResourceEntity = JsonConvert.DeserializeObject<AuthUserResourceEntity>(httpHelper.HttpPost(userResourceUrl));

                if ("用户不存在" == userResourceEntity.message)
                {
                    throw new Exception("用户没有权限");

                }
                else
                {
                    LoggerHelper.Info("判断用权限职责链的信息：" + userResourceEntity.data.productEnglishName);

                    successor.HandleRequest(ddID, content);
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Error("判断用权限职责链的错误信息：" + e.Message + "\n具体信息：" + e.StackTrace + "\n具体参数信息：" + userResourceUrl);

            }


        }
    }
}