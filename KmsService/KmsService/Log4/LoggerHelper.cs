/*
 * 创建人：王梦杰
 * 创建日期：2022年3月12日19:45:39
 * 描述：打日志方法
 */
using System;

namespace KmsService.Log4
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LoggerHelper
    {
        private static readonly log4net.ILog LogInfo = log4net.LogManager.GetLogger("LogInfo");

        private static readonly log4net.ILog LogError = log4net.LogManager.GetLogger("LogError");

        private static readonly log4net.ILog LogMonitor = log4net.LogManager.GetLogger("LogMonitor");

        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Error(string errorMsg, Exception ex = null)
        {
            if (ex != null)
            {
                LogError.Error(errorMsg, ex);
            }
            else
            {
                LogError.Error(errorMsg + "\n当前时间：" + GetTimeStamp());
            }
        }

        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="ex">异常对象</param>
        public static void Info(string msg, Exception ex = null)
        {
            if (ex != null)
            {
                LogInfo.Info(msg, ex);
            }
            else
            {
                LogInfo.Info(msg + "\n当前时间：" + GetTimeStamp());
            }
        }

        /// <summary>
        /// 记录Monitor日志
        /// </summary>
        /// <param name="msg">记录信息</param>
        public static void Monitor(string msg)
        {
            LogMonitor.Info(msg + "\n当前时间：" + GetTimeStamp());
        }

        #region 获取当前时间的时间戳
        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns>时间戳</returns>
        private static string GetTimeStamp()
        {
            //DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            //int timeStamp = Convert.ToInt32((DateTime.Now - dateStart).TotalSeconds);

            //TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
            //;
            //long time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

            //return ts.TotalSeconds.ToString();
            //获取当前时间戳，截至毫秒
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (DateTime.Now - startTime).TotalMilliseconds;
            return Math.Round(intResult, 0).ToString();
        }
        #endregion
    }
}