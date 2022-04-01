﻿/*
 * 创建人：王梦杰
 * 时间：2021年12月5日11:01:39
 * 描述：一个执行post请求方法，一个执行get请求方法
 */

using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace KmsService
{
    /// <summary>
    /// http请求帮助类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 无参数的执行post请求方法
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns>返回执行结果</returns>
        public string HttpPost(string url)
        {
            //创建http请求
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            //参数类型，这里是json类型
            //还有别的类型如"application/x-www-form-urlencoded"
            httpWebRequest.ContentType = "application/json";
            //参数数据长度
            httpWebRequest.ContentLength = 0;
            //设置请求类型
            httpWebRequest.Method = "POST";
            //设置超时时间
            httpWebRequest.Timeout = 20000;

            System.Net.ServicePointManager.Expect100Continue = false;
            //将参数写入请求地址中
            
            //发送请求
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //读取返回数据
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
            string responseContent = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();
            httpWebRequest.Abort();
            return responseContent;
        }



        /// <summary>
        /// 执行post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数</param>
        /// <returns>返回执行结果</returns>
        public string HttpPost(string url, string data)
        {
            //创建http请求
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            //字符串转换为字节码
            byte[] bs = Encoding.UTF8.GetBytes(data);
            //参数类型，这里是json类型
            //还有别的类型如"application/x-www-form-urlencoded"
            httpWebRequest.ContentType = "application/json";
            //参数数据长度
            httpWebRequest.ContentLength = bs.Length;
            //设置请求类型
            httpWebRequest.Method = "POST";
            //设置超时时间
            httpWebRequest.Timeout = 20000;
            //httpWebRequest.ServicePoint.Expect100Continue = false;
            System.Net.ServicePointManager.Expect100Continue = false;
            //将参数写入请求地址中
            httpWebRequest.GetRequestStream().Write(bs, 0, bs.Length);
            //发送请求
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //读取返回数据
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
            string responseContent = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();
            httpWebRequest.Abort();
            return responseContent;
        }

        /// <summary>
        /// 执行post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数</param>
        /// <returns>返回执行结果</returns>
        public string HttpPost(string url, string data, string token)
        {
            //创建http请求
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            //字符串转换为字节码
            byte[] bs = Encoding.UTF8.GetBytes(data);

            httpWebRequest.Headers["Authorization"] = token;
            //参数类型，这里是json类型
            //还有别的类型如"application/x-www-form-urlencoded"
            httpWebRequest.ContentType = "application/json";
            //参数数据长度
            httpWebRequest.ContentLength = bs.Length;
            //设置请求类型
            httpWebRequest.Method = "POST";
            //设置超时时间
            httpWebRequest.Timeout = 20000;
            //httpWebRequest.ServicePoint.Expect100Continue = false;
            System.Net.ServicePointManager.Expect100Continue = false;
            //将参数写入请求地址中
            httpWebRequest.GetRequestStream().Write(bs, 0, bs.Length);
            //发送请求
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            //读取返回数据
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
            string responseContent = streamReader.ReadToEnd();
            streamReader.Close();
            httpWebResponse.Close();
            httpWebRequest.Abort();
            return responseContent;
        }

        /// <summary>
        /// 执行get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns>返回执行结果</returns>
        public string HttpGet(string url)
        {
            //创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求方法
            request.Method = "Get";
            //设置超时时间
            request.Timeout = 20000;
            //发送请求
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //读取返回数据
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    JToken jToken = JToken.Parse(result);
                    request.Abort();

                    return result;
                }
            }
        }

        public string HttpGet(string url, string token, int i)
        {
            //创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求方法
            request.Method = "Get";
            //设置超时时间
            request.Timeout = 20000;
            //request.Headers.Add("x-acs-dingtalk-access-token",context.Request.Headers["x-acs-dingtalk-access-token"]);
            request.Headers["x-acs-dingtalk-access-token"] = token; // 新增代码
            //发送请求
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //读取返回数据
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    JToken jToken = JToken.Parse(result);
                    request.Abort();

                    return result;
                }
            }
        }

        public string HttpGet(string url, string data)
        {
            //创建请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] bs = Encoding.UTF8.GetBytes(data);
            request.ContentLength = bs.Length;
            //设置请求方法
            request.Method = "Get";
            //设置超时时间
            request.Timeout = 20000;
            request.GetRequestStream().Write(bs, 0, bs.Length);
            //发送请求
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //读取返回数据
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string result = streamReader.ReadToEnd();
                    JToken jToken = JToken.Parse(result);
                    request.Abort();

                    return result;
                }
            }
        }
    }
}