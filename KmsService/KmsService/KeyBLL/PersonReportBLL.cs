using System;
using System.Collections.Generic;
using KmsService.DAL;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    public class PersonReportBLL
    {
        private PersonReportDAL personReportDAL = new PersonReportDAL();

        /// <summary>
        /// 申请人的一周组织会议的次数
        /// </summary>
        /// <param name="ddID">申请人的钉钉id</param>
        /// <returns>申请人的一周组织会议的次数</returns>
        public string UsageTimes(string dingDingID)
        {
            string weekUseTimes = null;
            try
            {
                weekUseTimes = personReportDAL.UsageTimes(dingDingID).ToString();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("申请人的一周使用会议室的次数的错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }
            return weekUseTimes;
        }

        /// <summary>
        /// 申请人的一周会议使用最多次数的教室
        /// <param name="ddID">申请人的钉钉id</param>
        /// <returns></returns>
        public string MaxUsedRoom(string dingDingID)
        {
            string maxUseRoom = null;
            try
            {
                maxUseRoom = personReportDAL.MaxUsedRoom(dingDingID).ToString();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用申请人的一周会议室使用最多次数的会议室方法的错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }
            return maxUseRoom;
        }

        /// <summary>
        /// 获取时间段最多的一条数据
        /// </summary>
        /// <param name="ddID">申请会议人的id</param>
        /// <returns></returns>
        public string MaxStartTime(string dingDingID)
        {
            string maxTime = null;
            try
            {
                maxTime = personReportDAL.MaxStartTime(dingDingID).ToString();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用申请人一周使用会议室最多的时间段的方法的错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }
            return maxTime;
        }

        public List<string> AllDingDingID()
        {
            return personReportDAL.AllddID();
        }
    }
}