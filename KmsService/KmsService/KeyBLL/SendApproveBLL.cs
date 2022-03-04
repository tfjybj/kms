/*
 * 创建人：王梦杰
 * 时间：2022年1月6日16:08:40
 * 描述：自动发送审批
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.Log4;
using KmsService.KeyBLL.Scheduled;

namespace KmsService.KeyBLL
{
    //发日程自动发送审批
    public class SendApproveBLL
    {
        //查询日程表中的数据
        SelectCalendarInfoDAL selectCalendarInfo = new SelectCalendarInfoDAL();
        string processCode = ConfigurationManager.ConnectionStrings["process_code"].ConnectionString;
        string approver = ConfigurationManager.ConnectionStrings["approver"].ConnectionString;
        //string sendPeople = ConfigurationManager.ConnectionStrings["cc_list"].ConnectionString;
        string postion = ConfigurationManager.ConnectionStrings["cc_position"].ConnectionString;
        string remark = ConfigurationManager.ConnectionStrings["remark"].ConnectionString;


        /// <summary>
        /// 自动发送审批
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户钉钉ID</param>
        /// <param name="roomName">会议室名称</param>
        public void SendApprove(string calendarID, string userID, string roomName)
        {
            //调用钉钉接口获取单个日程详情
            SelectCalendar selectCalendar = new SelectCalendar();
            SelectCalendarModel calendarModel = selectCalendar.SelectCalendarInfo(userID, "primary", calendarID);

            SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();
            RoomInfoEntity roomInfo = selectRoomInfo.SelectRoomInfo(roomName);
            string lockState = roomInfo.LockState + "used";
            if (roomInfo.LockState != lockState)
            {
                try
                {
                    //给日程实体赋值
                    CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
                    calendarInfo.CalendarID = calendarID;
                    calendarInfo.Content = calendarModel.summary;
                    calendarInfo.StartTime = calendarModel.start.dateTime.ToString();
                    calendarInfo.EndTime = calendarModel.end.dateTime.ToString();
                    calendarInfo.RoomName = roomName;
                    calendarInfo.AttendCount = calendarModel.attendees.Count().ToString();
                    calendarInfo.Organizer = calendarModel.organizer.displayName;
                    calendarInfo.OrganizerID = userID;
                    calendarInfo.CreateTime = Convert.ToString(DateTime.Now);
                    calendarInfo.UpdateTime = Convert.ToString(DateTime.Now);
                    calendarInfo.IsEnd = 0;
                    calendarInfo.IsStart = 0;
                    calendarInfo.Approver = approver;
                    //calendarInfo.SendPeople = sendPeople;

                    //判断日程表中是否存在此日程记录
                    CalendarInfoEntity calendarInfoEntity = selectCalendarInfo.SelectCalendarInfo(calendarID);
                    if (calendarInfoEntity.CalendarID == null)
                    {
                        InsertCalendarDateDAL insertCalendar = new InsertCalendarDateDAL();
                        insertCalendar.InsertCalendar(calendarInfo);

                        //给发审批实体赋值
                        SendApproveModel send = new SendApproveModel();
                        Form_Component_Values fromName = new Form_Component_Values();
                        Form_Component_Values startTime = new Form_Component_Values();
                        Form_Component_Values endTime = new Form_Component_Values();
                        Form_Component_Values useExplain = new Form_Component_Values();
                        send.agent_id = 10358038;
                        send.originator_user_id = userID;
                        send.process_code = processCode;
                        send.approvers = approver;
                        //send.cc_list = sendPeople;
                        send.cc_position = postion;
                        fromName.name = "会议室选择";
                        fromName.value = roomName;
                        List<Form_Component_Values> list = new List<Form_Component_Values>();
                        list.Add(fromName);
                        send.form_component_values = list;

                        startTime.name = "开始时间";
                        startTime.value = calendarModel.start.dateTime.ToString();
                        list.Add(startTime);
                        send.form_component_values = list;
                        endTime.name = "结束时间";
                        endTime.value = calendarModel.end.dateTime.ToString();
                        list.Add(endTime);
                        send.form_component_values = list;

                        useExplain.name = "使用说明";
                        useExplain.value = calendarModel.summary;
                        list.Add(useExplain);
                        send.form_component_values = list;

                        //更新is_start和is_end状态
                        UpdateCalendar updateCalendar = new UpdateCalendar();
                        updateCalendar.UpdateIsStartIsEnd(calendarID, "1", "1");

                        //更新日程作废,默认审批同意                       
                        updateCalendar.UpdateCalendarVoid(calendarID, "0");


                        //自动发送审批
                        SendApproveExample sendApprove = new SendApproveExample();
                        SendApproveRe_valueModel sendApproveRe_Value = sendApprove.SendApprove(send);


                        //获取审批实例详情
                        ApproveContent approve = new ApproveContent();
                        ApproveContentTask approveContent = approve.GetApproveContent(sendApproveRe_Value.process_instance_id);

                        //将会议室的状态更改为已经被申请
                        UpdateLockStateDAL updateLockState = new UpdateLockStateDAL();
                        updateLockState.UpdateLockState(roomName, lockState);
                    }
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error("用户点击同意自动发送审批：" + ex.Message + "堆栈信息：" + ex.StackTrace + "所需参数：日程ID、userID：" + calendarID + userID);
                }
            }
        }

        /// <summary>
        /// 自动同意审批
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="roomName">房间号</param>
        public void AutoSendApprove(string calendarID, string userID, string roomName)
        {
            SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();
            RoomInfoEntity roomInfo = selectRoomInfo.SelectRoomInfo(roomName);
            string lockState = roomInfo.LockState + "used";
            if (roomInfo.LockState != lockState)
            {
                try
                {
                    //调用钉钉接口获取单个日程详情
                    SelectCalendar selectCalendar = new SelectCalendar();
                    SelectCalendarModel calendarModel = selectCalendar.SelectCalendarInfo(userID, "primary", calendarID);
                    //给日程实体赋值
                    CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
                    calendarInfo.CalendarID = calendarID;
                    calendarInfo.Content = calendarModel.summary;
                    calendarInfo.StartTime = calendarModel.start.dateTime.ToString();
                    calendarInfo.EndTime = calendarModel.end.dateTime.ToString();
                    calendarInfo.RoomName = roomName;
                    calendarInfo.AttendCount = calendarModel.attendees.Count().ToString();
                    calendarInfo.Organizer = calendarModel.organizer.displayName;
                    calendarInfo.OrganizerID = userID;
                    calendarInfo.CreateTime = Convert.ToString(DateTime.Now);
                    calendarInfo.UpdateTime = Convert.ToString(DateTime.Now);
                    calendarInfo.IsEnd = 0;
                    calendarInfo.IsStart = 0;
                    calendarInfo.Approver = approver;
                    calendarInfo.CalendarIsVoid = 0;
                    //calendarInfo.SendPeople = sendPeople;


                    //判断日程表中是否存在此日程记录
                    CalendarInfoEntity calendarInfoEntity = selectCalendarInfo.SelectCalendarInfo(calendarID);
                    if (calendarInfoEntity.CalendarID == null)
                    {
                        //插入日程表
                        InsertCalendarDateDAL insertCalendar = new InsertCalendarDateDAL();
                        insertCalendar.InsertCalendar(calendarInfo);
                        //给发审批实体赋值
                        SendApproveModel send = new SendApproveModel();
                        Form_Component_Values fromName = new Form_Component_Values();
                        Form_Component_Values startTime = new Form_Component_Values();
                        Form_Component_Values endTime = new Form_Component_Values();
                        Form_Component_Values useExplain = new Form_Component_Values();
                        send.agent_id = 10358038;
                        send.originator_user_id = userID;
                        send.process_code = processCode;
                        send.approvers = approver;
                        //send.cc_list = sendPeople;
                        send.cc_position = postion;
                        fromName.name = "会议室选择";
                        fromName.value = roomName;
                        List<Form_Component_Values> list = new List<Form_Component_Values>();
                        list.Add(fromName);
                        send.form_component_values = list;

                        startTime.name = "开始时间";
                        startTime.value = calendarModel.start.dateTime.ToString();
                        list.Add(startTime);
                        send.form_component_values = list;
                        endTime.name = "结束时间";
                        endTime.value = calendarModel.end.dateTime.ToString();
                        list.Add(endTime);
                        send.form_component_values = list;

                        useExplain.name = "使用说明";
                        useExplain.value = calendarModel.summary;
                        list.Add(useExplain);
                        send.form_component_values = list;
                        //自动发送审批
                        SendApproveExample sendApprove = new SendApproveExample();
                        SendApproveRe_valueModel sendApproveRe_Value = sendApprove.SendApprove(send);

                        //获取审批实例详情
                        ApproveContent approve = new ApproveContent();
                        ApproveContentTask approveContent = approve.GetApproveContent(sendApproveRe_Value.process_instance_id);


                        //给自动同意审批接口实体赋值
                        AgreeRequestModel agreeModel = new AgreeRequestModel();
                        Request requestModel = new Request();
                        requestModel.process_instance_id = sendApproveRe_Value.process_instance_id;
                        requestModel.remark = remark;
                        requestModel.result = "agree";
                        requestModel.actioner_userid = approveContent.process_instance.tasks[0].userid;
                        requestModel.task_id = Convert.ToInt64(approveContent.process_instance.tasks[0].taskid);
                        agreeModel.request = requestModel;
                        agreeModel.request.process_instance_id = sendApproveRe_Value.process_instance_id;
                        agreeModel.request.remark = remark;
                        agreeModel.request.result = "agree";
                        agreeModel.request.actioner_userid = approveContent.process_instance.tasks[0].userid;
                        agreeModel.request.task_id = Convert.ToInt64(approveContent.process_instance.tasks[0].taskid);

                        //调用自动同意审批接口
                        AgreeRequest agreeRequest = new AgreeRequest();
                        string agreeResult = agreeRequest.AgreeRequestInfo(agreeModel, requestModel);
                        //LoggerHelper.Error("调用自动同意审批接口结果：" + agreeResult);


                        //}

                        //根据日程ID插入审批ID
                        UpdateCalendar updateCalendar = new UpdateCalendar();
                        updateCalendar.UpdateApproveID(calendarID, sendApproveRe_Value.process_instance_id);

                        //将会议室的状态更改为已经被申请
                        UpdateLockStateDAL updateLockState = new UpdateLockStateDAL();
                        updateLockState.UpdateLockState(roomName, lockState);

                    }
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error("用户点击同意自动发送审批并且同意接口：" + ex.Message + Environment.NewLine + "堆栈信息：" + ex.StackTrace + Environment.NewLine + "所需参数：日程ID、userID：" + calendarID + "、" + userID);
                }
            }
        }

        /// <summary>
        /// 判断审批是否同意
        /// </summary>
        /// <param name="approveContent">审批实体</param>
        public void GetApproveResult(ApproveInstanceModel approveContent)
        {
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
                        LoggerHelper.Info("审批结果："+ approveContent.type+"、"+ approveContent.result);
                        //更新is_start和is_end状态为0，可以发送消息卡片
                        updateCalendar.UpdateIsStartIsEnd(calendarInfo.CalendarID, "0", "0");

                        //出发定时任务
                        ConferenceStartBLL conferenceStartBLL = new ConferenceStartBLL();
                        conferenceStartBLL.GetConferenceStartKey(calendarInfo.StartTime);
                    }
                    else
                    {
                        //更新calendar_is_void状态为1，日程作废
                        updateCalendar.UpdateCalendarVoid(calendarInfo.CalendarID, "1");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用判断审批是否同意方法的错误信息："+ex.ToString());
                
            }

        }
    }
}