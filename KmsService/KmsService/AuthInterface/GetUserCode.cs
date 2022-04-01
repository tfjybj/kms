/*
 * 创建人：王梦杰
 * 时间：2021年12月7日11:55:25
 * 描述：调用权限接口获取用户的dingid
 */

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using KmsService.DingDingModel;

namespace KmsService.AuthInterface
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    public class GetUserCode
    {
        /// <summary>
        /// 调用权限接口获取用户的dingid
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        public UserCodeModel GetDingID(string phoneNumber)
        {
            //权限接口获取用户信息的url
            string url = string.Format("http://d-authtemp.dmsd.tech:9002/rbac/user/usercode/{0}", phoneNumber);
            HttpHelper helper = new HttpHelper();
            //执行url
            helper.HttpGet(url);
            //把返回值转换成json串的形式
            JToken json = JToken.Parse(helper.HttpGet(url));
            //取出json串里data对应的值
            string result = json["data"].ToString();
            //进行字符分割
            string[] intercept = result.Split('"');
            List<string> list = new List<string>();
            for (int i = 0; i < intercept.Length; i++)
            {
                list.Insert(0, intercept[i]);
            }
            //取出dingid和userid
            UserCodeModel userCode = new UserCodeModel();
            userCode.DingID = list[35].Trim();
            userCode.UserID = list[39].Trim();

            return userCode;
        }
    }
}