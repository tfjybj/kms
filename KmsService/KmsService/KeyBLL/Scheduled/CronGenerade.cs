/*
 * 创建人：王梦杰
 * 创建时间：2022年2月25日19:53:25
 * 描述：根据传入一个时间生成cron表达式
 */
using NPOI.SS.Util;
using System;

namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 生成cron表达式
    /// </summary>
    public class CronGenerade
    {
        /// <summary>
        /// 获取cron表达式
        /// </summary>
        /// <param name="dateTime">传入一个时间值</param>
        /// <returns>cron表达式</returns>
        public string GetCron(string dateTime)
        {
            DateTime date = Convert.ToDateTime(dateTime);
            string dateFormat = "ss mm HH dd MM ? yyyy";

            return FormatDateByPattern(date, dateFormat);
        }

        /// <summary>
        /// 按照日期格式生成cron表达式
        /// </summary>
        /// <param name="date">时间值</param>
        /// <param name="dateFormat">日期格式</param>
        /// <returns>cron表达式</returns>
        public static string FormatDateByPattern(DateTime date, string dateFormat)
        {
            SimpleDateFormat sdf = new SimpleDateFormat(dateFormat);
            string formatTimeStr = null;
            if (date != null)
            {
                formatTimeStr = sdf.Format(date);
            }
            return formatTimeStr;
        }
    }
}