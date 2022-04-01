/*
 * 创建人：王梦杰
 * 时间：2021年12月5日11:01:39
 * 描述：调用钉钉获取审批实例接口
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace KmsService.DingDingInterface
{
    /// <summary>
    /// 审批ID获取类
    /// </summary>
    public class ApproveID
    {
        /// <summary>
        /// 获取审批实例ID
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>审批ID</returns>
        public string GetApproveID(DateTime startTime, DateTime endTime)
        {
            string result = null;

            //审批流唯一码
            string processCode = "PROC-GTHKCO8W-Q9TRGUB0Q4OTTLZBUNGW1-XS358GCJ-1";
            //把时间转换为时间戳
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long start = (startTime.Ticks - time.Ticks) / 10000;
            long end = (endTime.Ticks - time.Ticks) / 10000;
            //string start = Convert.ToInt32((Convert.ToDateTime(startTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
            //string end = Convert.ToInt32((Convert.ToDateTime(endTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
            //获取访问token
            AccessToken accessToken = new AccessToken();
            string token = accessToken.GetAccessToken();
            //post请求url
            string url = string.Format("https://oapi.dingtalk.com/topapi/processinstance/listids?access_token={0}", token);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //添加参数
            var json_rep = new
            {
                process_code = processCode,
                start_time = start,
                end_time = end
            };
            string jsonData = JsonConvert.SerializeObject(json_rep);
            HttpHelper helper = new HttpHelper();
            //将返回结果转换成josn串形式
            JToken json = JToken.Parse(helper.HttpPost(url, jsonData));
            //获取josn串里result对应的值
            result = json["result"].ToString();
            //截取字符串
            string[] intercept = result.Split('"');
            List<string> list = new List<string>();
            for (int i = 0; i < intercept.Length; i++)
            {
                list.Insert(0, intercept[i]);
            }
            //获取审批实例ID
            string approveID = list[1].Trim();
            return approveID;
        }
    }
}