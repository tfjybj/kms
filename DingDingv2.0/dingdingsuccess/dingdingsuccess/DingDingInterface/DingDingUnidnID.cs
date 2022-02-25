using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dingdingsuccess.Log4;
namespace dingdingsuccess
{
    public class DingDingUnidnID
    {
        /// <summary>
        /// 根据unionid获取用户ID
        /// </summary>
        /// <param name="code">根据unionid获取用户ID</param>
        /// <returns>用户ID</returns>
        public string unionid(string code)
        {

            string url = string.Format("https://oapi.dingtalk.com/topapi/user/getbyunionid?access_token={0}", AccessToken.GetAccessToken());


            var json_rep = new
            {
                unionid = code
            };
            string jsonData = JsonConvert.SerializeObject(json_rep);
            HttpHelper helper = new HttpHelper();
            string result = helper.HttpPost(url, jsonData);

            UserEntity model = JsonConvert.DeserializeObject<UserEntity>(result);



            LoggerHelper.Info("根据unionid获取用户ID日志:" + result);

            return model.result.userid;
        }
    }
}