/*
 * 创建人：王梦杰
 * 创建日期：2022年3月12日19:45:39
 * 描述：申请人的一周组织会议的次数
 */
using System;
using System.Collections.Generic;
using KmsService.DAL;
using KmsService.Log4;
namespace KmsService.KeyBLL
{
    /// <summary>
    /// 用户报表
    /// </summary>
    public class PersonReportBLL
    {
        public  PersonReportDAL personReportDAL = new PersonReportDAL();

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
        /// <returns>会议室名称</returns>
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
        /// <returns>时间段</returns>
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

        //public List<string> AllDingDingID()
        //{
        //   // return personReportDAL.AllddID();
        //}


        #region 7天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        public void  RemoveWeekDuplication()
        {
          List<string> caldar_id=  personReportDAL.WeekddID();//获取需要推送周报的数据用户
            //List<string> report_id = personReportDAL.month();//获取需要推送月报的用户数据
            List<string> report_id = personReportDAL.Report();//报表中所有的钉钉id
            List<string> sameID = null;//存放两个表中相同的数据id
            foreach (var item in caldar_id)//日程表和报表中相同的集合，删除日程表中的id
            {
                if (report_id.Contains(item))
                {
                    sameID = new List<string>() { item }; 
                    caldar_id.Remove(item);//删除相同的id
                }
                
            }
            foreach (var addID in caldar_id)//把多出来的值添加到数据库中
            {
                personReportDAL.AddOrganizerID(addID);
            }
            foreach (var allddID in sameID)
            {
                personReportDAL.AddOrganizerID(allddID);//判断有哪些id是需要7天发一次的
            }
           

        }
        #endregion

        #region 30天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        public void  RemoveMonthDuplication()
        {
            List<string> caldar_id = personReportDAL.MonthddID();//获取日程表中需要推送月报的用户
            List<string> report_id = personReportDAL.Report();//报表中所有的钉钉id
            List<string> sameID=null ;//存放两个集合重复的数据
            foreach (var item in caldar_id)//把两个表中重复的数据删除
            {
                if (report_id.Contains(item))
                {
                     sameID  = new List<string>() { item };//接收两个集合重复的id


                caldar_id.Remove(item);//把重复的id删除
                }
            }
            foreach (var addID in caldar_id)//把多出来的值添加到报表数据库中
            {
                personReportDAL.AddOrganizerID(addID);//调用添加数据的方法
            }

            foreach (var allddID in sameID)//给需要月发送的用户发送报表
            {
                personReportDAL.Month(allddID);
            }

            
        }
        #endregion
        /// <summary>
        /// 用户状态为月推送时，修改状态为周推送，周推送改为月推送
        /// </summary>
        /// <param name="ddID"></param>
        public void ModifyState(string ddID,string state)
        {
             personReportDAL.ModifyState(ddID,state); 

        }
        /// <summary>
        /// 获取用户的推送状态
        /// </summary>
        /// <param name="ddID"></param>
        /// <returns>状态</returns>
        public string UserPushState(string ddID)
        {
            return personReportDAL.UserPushState(ddID);
             
        }
    }
}