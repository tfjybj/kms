/*
 * 创建人：邓礼梅
 * 时间：2022年1月6日16:08:40
 * 描述：判断审批是否同意
 */
using System;
using KmsService.DAL;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.Log4;
using KmsService.KeyBLL.Scheduled;
namespace KmsService.KeyBLL
{
    /// <summary>
    /// 发送日程自动发送审批
    /// </summary>
    public class GetApproveResultBLL
    {
        /// <summary>
        /// 判断审批是否同意
        /// </summary>
        /// <param name="approveContent">审批实体</param>
        public void GetApproveResult(ApproveInstanceModel approveContent)
        {
            LoggerHelper.Info("审批通过之后调用获取根据审批结果开启定时任务的方法GetApproveResult:走到这里了");
            LoggerHelper.Info("审批实体：" + approveContent.EventType + "、" + approveContent.processInstanceId + "、" + approveContent.finishTime + "、" + approveContent.corpId + "、" + approveContent.title + "、" + approveContent.type + "、" + approveContent.url + "、" + approveContent.result + "、" + approveContent.createTime + "、" + approveContent.staffId + "、" + approveContent.processCode);
            try
            {
                UpdateCalendar updateCalendar = new UpdateCalendar();
                //根据审批ID查询审批是否存在
                SelectCalendarInfoDAL selectCalendarInfoDAL = new SelectCalendarInfoDAL();
                CalendarInfoEntity calendarInfo = selectCalendarInfoDAL.SelectApproveContent(approveContent.processInstanceId);
                //返回的日程信息集合>0,审批存在
                if (calendarInfo != null)
                {
                    //如果审批结果为同意
                    if (approveContent.type == "finish" && approveContent.result == "agree")
                    {
                        LoggerHelper.Info("审批结果：" + approveContent.type + "、" + approveContent.result + "日程开始时间：" + calendarInfo.StartTime);
                        //更新is_start和is_end状态为0，可以发送消息卡片
                        int updateIsStartIsEndResult = updateCalendar.UpdateIsStartIsEnd(calendarInfo.CalendarID, "0", "0");
                        BasicDataDAL basicData = new BasicDataDAL();
                        BasicDataEntity basicDataEntity = basicData.SelectAllBasicData(calendarInfo.RoomName);
                        //创建会议结束时发送归还钥匙消息的定时任务
                        ConferenceEndBLL endScheduledJob = new ConferenceEndBLL();
                        ConferenceStartBLL conferenceStartBLL = new ConferenceStartBLL();

                        DateTime startTime = Convert.ToDateTime(calendarInfo.StartTime).AddMinutes(-(basicDataEntity.BeforeTakeKey));


                        int startResult = DateTime.Compare(startTime, DateTime.Now);
                        LoggerHelper.Info("领取钥匙的时间值比较结果：" + startResult);
                        if (startResult > 0)
                        {
                            LoggerHelper.Info("开始时间减三十分钟发领取钥匙卡片");
                            conferenceStartBLL.GetConferenceStartKey(Convert.ToDateTime(calendarInfo.StartTime).AddMinutes(-(basicDataEntity.BeforeTakeKey)).ToString());
                        }
                        else
                        {
                            LoggerHelper.Info("系统当前时间发领取钥匙卡片");
                            conferenceStartBLL.GetConferenceStartKey(DateTime.Now.ToString());
                        }

                        DateTime endTime = Convert.ToDateTime(calendarInfo.EndTime);
                        int endResult = DateTime.Compare(endTime, DateTime.Now);
                        LoggerHelper.Info("归还钥匙消息的时间值比较结果：" + endResult);
                        if (endResult > 0)
                        {
                            LoggerHelper.Info("结束时间减十分钟发归还钥匙消息提醒");
                            endScheduledJob.GetConferenceEndKey(Convert.ToDateTime(calendarInfo.EndTime).AddMinutes(-(basicDataEntity.AfterReturnKey)).ToString());
                        }
                        else
                        {
                            LoggerHelper.Info("系统当前时间发归还钥匙消息提醒");
                            endScheduledJob.GetConferenceEndKey(calendarInfo.EndTime.ToString());
                        }
                        //创建领取钥匙卡片过期定时任务
                        ConferenceOverdueBLL conferenceOverdue = new ConferenceOverdueBLL();
                        conferenceOverdue.GetConferenceOverdueCard(Convert.ToDateTime(calendarInfo.StartTime).AddMinutes(7).ToString());
                    }
                    else if (approveContent.type == "finish" && approveContent.result == "refuse")
                    {
                        LoggerHelper.Info("代码什么时候走到了这里！");
                        //更新calendar_is_void状态为1，日程作废
                        updateCalendar.UpdateIsDelete(calendarInfo.CalendarID);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用判断审批是否同意方法的错误信息：" + ex.ToString());
            }
        }
    }
}