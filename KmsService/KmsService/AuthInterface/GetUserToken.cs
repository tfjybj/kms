using Newtonsoft.Json;
using KmsService.DingDingModel;
using System.Configuration;
namespace KmsService.AuthInterface
{
    /// <summary>
    /// 获取用户token
    /// </summary>
    public class GetUserToken
    {
        /// <summary>
        /// 获取用户的token值
        /// </summary>
        /// <param name="phonenumber"></param>
        /// <returns></returns>
        public UserTokenModel GetToken(string phoneNumber)
        {
            //URL地址
            string url = ConfigurationManager.ConnectionStrings["authURL"].ConnectionString;

            HttpHelper helper = new HttpHelper();
            var json_rep = new
            {
                password = phoneNumber,
                userCode = phoneNumber
            };
            string jsonData = JsonConvert.SerializeObject(json_rep);
            //实体接受权限接口返回的值
            UserTokenModel userToken = JsonConvert.DeserializeObject<UserTokenModel>(helper.HttpPost(url, jsonData));

            return userToken;
        }
    }
}