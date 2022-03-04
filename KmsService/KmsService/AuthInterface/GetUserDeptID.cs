/*
 * 创建人：王梦杰
 * 时间：2021年12月13日08:06:48
 * 描述：调用权限接口获取部门ID
 */

using System.Collections.Generic;
using KmsService.DingDingModel;

namespace KmsService.AuthInterface
{
    public class GetUserDeptID
    {
        /// <summary>
        /// 获取部门ID
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        public string GetDeptID(string phoneNumber)
        {
            //实例化获取人员信息的接口
            GetUserCode getUser = new GetUserCode();
            //调用获取人员信息的方法
            UserCodeModel userCode = getUser.GetDingID(phoneNumber);
            //权限接口获取部门ID的url
            string url = string.Format("http://d-authtemp.dmsd.tech:9002/rbac/user/queryOrgInfoByUserId/{0}", userCode.UserID);
            HttpHelper helper = new HttpHelper();

            //JToken json = JToken.Parse(helper.HttpGet(url));
            //接收执行结果
            string result = helper.HttpGet(url);
            //字符分割
            string[] intercept = result.Split('"');
            List<string> list = new List<string>();
            for (int i = 0; i < intercept.Length; i++)
            {
                list.Insert(0, intercept[i]);
            }
            //拿出部门ID
            string deptID = list[57].Trim();
            return deptID;
        }
    }
}