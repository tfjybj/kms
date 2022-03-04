using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KmsService;
using KmsService.AuthInterface;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.KeyBLL;
using KmsService.KeyBLL.CalendarStrategyHandler;

namespace KmsService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class Service : IService
    {
        #region 更新教室数据基础表

        /// <summary>
        /// 更新教室名称
        /// </summary>
        /// <param name="ID">教室id</param>
        /// <param name="caName">新的教室名称</param>
        /// <returns></returns>
        public int UpdateRoomName(string ID, string caName)
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.UpdateRoomName(ID, caName);
        }

        /// <summary>
        /// 最少使用人数
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="minUseNumber"></param>
        /// <returns></returns>
        public int UpdateMinUseNumber(string ID, string minUseNumber)
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.UpdateMinUseNumber(ID, minUseNumber);
        }

        public int UpdateBeforeTakeKey(string ID, string beforeTakeKey)//会议前*分钟取钥匙
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.UpdateBeforeTakeKey(ID, beforeTakeKey);
        }

        public int UpdateAfterReturnKey(string ID, string afterReturnKey)//会议前*分钟归还钥匙
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.UpdateAfterReturnKey(ID, afterReturnKey);
        }

        public int UpdateUpperTime(string ID, string upperTime)//会议最长使用时间
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.UpdateUpperTime(ID, upperTime);
        }

        public int UpdateLowerTime(string ID, string lowerTime)//会议最少使用时间
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.UpdateLowerTime(ID, lowerTime);
        }

        public DataTable SelectBasicData()//取出所有教室信息
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.SelectBasicData();
        }

        public bool CaNameIsExists(string caName)//判断教室名称是否已存在
        {
            ModifyConfiguration modify = new ModifyConfiguration();
            return modify.CaNameIsExists(caName);
        }

        #endregion 更新教室数据基础表

        //private MQTTServer mqtt = MQTTServer.Instance();

        /// <summary>
        /// 更新值班人员姓名
        /// </summary>
        /// <param name="OldDutyName">值班人员</param>
        /// <param name="NewDutyName">替班人员</param>
        /// <returns></returns>
        public int UpdateDutyName(string OldDutyName, string NewDutyName)
        {
            UpdateDutyNameDAL updateDutyName = new UpdateDutyNameDAL();
            return updateDutyName.UpdateDutyName(OldDutyName, NewDutyName);
        }

        /// <summary>
        /// 获取教室的有关信息
        /// </summary>
        /// <param name="roomName">教室名称</param>
        /// <returns></returns>
        public RoomInfoEntity SelectRoomInfo(string roomName)
        {
            SelectRoomInfoDAL roomInfo = new SelectRoomInfoDAL();
            return roomInfo.SelectRoomInfo(roomName);
        }

        /// <summary>
        /// 获取访问token
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            AccessToken accessToken = new AccessToken();
            return accessToken.GetAccessToken();
        }

        #region 用户半小时前领钥匙，会议结束前提示还钥匙，消息卡片作废

        //更新领取钥匙卡片消息的id
        public void UpdateOutTrackID(string calendarID, string outTrackid)
        {
            BeforeMeetingDAL before = new BeforeMeetingDAL();
            before.UpdateOutTrackID(calendarID, outTrackid);
        }

        //过时未领取消息卡片作废
        public List<CalendarInfoEntity> CancelCard()
        {
            BeforeMeetingDAL beforeMeetingDAL = new BeforeMeetingDAL();

            return beforeMeetingDAL.CancelCard();
        }

        //查询当前时间>=会议开始时间的教室名称
        public string RoomState(string roomName)
        {
            BeforeMeetingDAL before = new BeforeMeetingDAL();
            return before.RoomState(roomName);
        }

        /// <summary>
        /// 给用户发信息后修改数据库中is_start的状态
        /// </summary>
        /// <param name="ScheduleID"></param>
        /// <returns></returns>
        public void UpdateIsStart(string ScheduleID)
        {
            BeforeMeetingDAL before = new BeforeMeetingDAL();
            before.UpdateIsStart(ScheduleID);
        }

        public void UpdateIsEnd(string calendarID)
        {
            BeforeMeetingDAL before = new BeforeMeetingDAL();
            before.UpdateIsEnd(calendarID);
        }

        #endregion 用户半小时前领钥匙，会议结束前提示还钥匙，消息卡片作废

        //public int UpdateState(string lockNumber)
        //{
        //    OpenLockDAL open = new OpenLockDAL();
        //    return open.UpdateState(lockNumber);
        //}
        /// <summary>
        /// 获取审批实例ID
        /// </summary>
        /// <param name="start_time">审批开始时间</param>
        /// <param name="end_time">审批结束时间</param>
        /// <returns></returns>
        public string GetApproveID(DateTime start_time, DateTime end_time)
        {
            ApproveID approve = new ApproveID();
            return approve.GetApproveID(start_time, end_time);
        }

        /// <summary>
        /// 开锁/关锁
        /// </summary>
        /// <param name="value"></param>
        public void OpenLock(string value)
        {
            MQTTServer mqtt = MQTTServer.Instance();
            mqtt.OpenLock(value);
        }

        /// <summary>
        /// 获取审批实例详情
        /// </summary>
        /// <param name="startTime">审批开始时间</param>
        /// <param name="endTime">审批结束时间</param>
        /// <returns></returns>
        public ApproveContentTask GetApproveContent(string processInstance)
        {
            ApproveContent approve = new ApproveContent();
            ApproveContentTask approveContent = approve.GetApproveContent(processInstance);

            return approveContent;
        }

        /// <summary>
        /// 发起审批实例
        /// </summary>
        /// <param name="sendApproveModel">具体审批具体参数发起人ID、审批人ID、部门ID、审批内容</param>
        /// <returns>返回审批ID、发起人ID</returns>
        public SendApproveRe_valueModel SendApprove(SendApproveModel sendApproveModel)
        {
            SendApproveExample send = new SendApproveExample();
            return send.SendApprove(sendApproveModel);
        }

        public UserCodeModel GetDingID(string phoneNumber)
        {
            GetUserCode getUser = new GetUserCode();
            return getUser.GetDingID(phoneNumber);
        }

        public string GetDeptID(string phoneNumber)
        {
            GetUserDeptID deptID = new GetUserDeptID();
            return deptID.GetDeptID(phoneNumber);
        }

        public void DingDingMessage(DingMessageModel model, Link link)
        {
            ApproverMessage dingMessage = new ApproverMessage();
            dingMessage.SendMessage(model, link);
        }

        #region 用户推送消息

        public void SendMessageUser()
        {
            SendMessages sm = new SendMessages();

            sm.SendMessageUser();
        }

        #endregion 用户推送消息

        #region 推送会议室

        /// <summary>
        /// 推送可用会议室名称
        /// </summary>
        /// <param name="Participants">参会人</param>
        /// <returns></returns>
        //public List<string> PushRoomNameTen(int Participants)
        //{
        //    PushRoomNameTenBLL pushRoomNameTen = new PushRoomNameTenBLL();
        //    return pushRoomNameTen.PushRoomNameTen(Participants);
        //}

        ///// <summary>
        ///// 推送可用会议室名称
        ///// </summary>
        ///// <param name="Participants">参会人</param>
        ///// <returns></returns>
        //public List<string> PushRoomNameTwenty(int Participants)
        //{
        //    PushRoomNameTwentyBLL pushRoomNameTwenty = new PushRoomNameTwentyBLL();
        //    return pushRoomNameTwenty.PushRoomNameTwenty(Participants);
        //}



        public CalendarInfoEntity CalendarDate(string calendarID)
        {
            CalendarDataBLL calendarData = new CalendarDataBLL();
            return calendarData.calendarDate(calendarID);
        }


        public SelectCalendarModel SelectCalendarInfo(string userID, string CalendarID, string eventID)
        {
            SelectCalendar selectCalendar = new SelectCalendar();
            return selectCalendar.SelectCalendarInfo(userID, CalendarID, eventID);
        }

        public void Open(string userId, string eventID)
        {
            OpenLockBLL openLock = new OpenLockBLL();
            openLock.Open(userId, eventID);
        }

        public int InsertCalendar(CalendarInfoEntity calendarInfo)
        {
            InsertCalendarDateDAL insert = new InsertCalendarDateDAL();
            return insert.InsertCalendar(calendarInfo);
        }

        public string SelectGroupID(string userID)
        {
            SelectGroupIDBLL selectGroup = new SelectGroupIDBLL();
            return selectGroup.SelectGroupID(userID);
        }

        /// <summary>
        /// 自动同意审批
        /// </summary>
        /// <param name="model"></param>
        /// <param name="agreeRequest"></param>
        /// <returns></returns>
        public string AgreeRequestInfo(AgreeRequestModel model, Request agreeRequest)
        {
            AgreeRequest request = new AgreeRequest();
            return request.AgreeRequestInfo(model, agreeRequest);
        }

        /// <summary>
        /// 会议结束前10分钟发送还钥匙消息
        /// </summary>
        /// <returns></returns>
        public List<CalendarInfoEntity> BeforeMeetingEnd()
        {
            BeforeMeetingDAL beforeMettingDAL = new BeforeMeetingDAL();
            return beforeMettingDAL.BeforeMeetingEnd();
        }

        /// <summary>
        /// 会议前30分钟发送领取钥匙消息链接
        /// </summary>
        /// <returns></returns>
        public List<CalendarInfoEntity> BeforeMeetingStart()
        {
            BeforeMeetingDAL beforeMettingDAL = new BeforeMeetingDAL();
            return beforeMettingDAL.BeforeMeetingStart();
        }



        //public int UsageTimes(UseRecordEntity ddID)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion 推送会议室

        #region 根据参会人数推送可用会议室

        public string PushRoomName(int participants)
        {
            PushRoomBLL pushRoom = new PushRoomBLL();
            return pushRoom.PushRoom(participants);
        }


        #endregion 根据参会人数推送可用会议室

        #region 每周会议室使用情况推送-会议室名称

        /// <summary>
        ///申请人一周使用最多的时间段
        /// </summary>
        /// <param name="dingDingID">申请人钉钉id</param>
        /// <returns>会议室名称</returns>
        public string WeekRoomName(string dingDingID)
        {
            PersonReportBLL personReportBLL = new PersonReportBLL();
            return personReportBLL.MaxUsedRoom(dingDingID);
        }

        #endregion 每周会议室使用情况推送-会议室名称

        #region 每周会议室使用情况推送-使用时间段

        /// <summary>
        ///申请人一周使用最多的时间段
        /// </summary>
        /// <param name="ddID">申请人钉钉id</param>
        /// <returns>使用次数</returns>
        public string WeekUseTime(string dingDingID)
        {
            PersonReportBLL personReportBLL = new PersonReportBLL();
            return personReportBLL.MaxStartTime(dingDingID);
        }

        #endregion 每周会议室使用情况推送-使用时间段

        #region 每周会议室使用情况推送-使用次数

        /// <summary>
        /// 申请人一周共使用多少次会议室
        /// </summary>
        /// <param name="dingDingID">申请人dingdingID</param>
        /// <returns>使用次数</returns>
        public string WeekUseCount(string dingDingID)
        {
            PersonReportBLL personReportBLL = new PersonReportBLL();
            return personReportBLL.UsageTimes(dingDingID);
        }

        #endregion 每周会议室使用情况推送-使用次数

        #region 每周会议室使用情况推送-发送的所有人

        /// <summary>
        /// 获取要发给的所有用户的dingdingID
        /// </summary>
        /// <returns>dingdingID的集合</returns>
        public List<string> AllDingDingID()
        {
            PersonReportBLL personReportBLL = new PersonReportBLL();
            return personReportBLL.AllDingDingID();
        }

        #endregion 每周会议室使用情况推送-发送的所有人

        #region 还钥匙

        /// <summary>
        /// 还钥匙
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        public void DynamicReturnKey(string calendarID)
        {
            MQTTServer.Instance().MqttSubscribe(calendarID);

        }

        #endregion 还钥匙

        #region 未正常归还钥匙

        /// <summary>
        /// 未正常归还钥匙进行扣分处理
        /// </summary>
        /// <param name="calendarID">日程id</param>
        public void ReturnKeyLate(string calendarID, string userID)
        {
            ReturnKeyLateBLL returnKeyLate = new ReturnKeyLateBLL();
            returnKeyLate.ReturnKeyLate(calendarID, userID);
        }

        #endregion 未正常归还钥匙

        #region 发送审批

        public SendApproveRe_valueModel SendApproveTask(SendApproveModel sendApproveModel)
        {
            SendApproveExample sendApprove = new SendApproveExample();
            SendApproveRe_valueModel sendApproveRe_Value = sendApprove.SendApprove(sendApproveModel);
            return sendApproveRe_Value;
        }

        #endregion 发送审批

        #region 发送审批并且自动同意或者不自动同意

        public void SendApprove(string calendarID, string userID, string roomName)
        {
            SendApproveBLL sendApprove = new SendApproveBLL();
            sendApprove.SendApprove(calendarID, userID, roomName);
        }
        #region 判断审批是否同意
        public void GetApproveResult(ApproveInstanceModel approveContent)
        {
            new SendApproveBLL().GetApproveResult(approveContent);
        }
        #endregion

        public void AutoSendApprove(string calendarID, string userID, string roomName)
        {
            SendApproveBLL sendApprove = new SendApproveBLL();
            sendApprove.AutoSendApprove(calendarID, userID, roomName);
        }

        #endregion 发送审批并且自动同意或者不自动同意

        #region 更新归还钥匙时间

        public void UpdateReturnTime(string calendarID)
        {
            BeforeMeetingDAL beforeMeetingDAL = new BeforeMeetingDAL();
            beforeMeetingDAL.UpdateReturnTime(calendarID);
        }

        #endregion 更新归还钥匙时间

        #region 查询基本数据配置表全部信息

        public BasicDataEntity SelectAllBasicData()
        {
            BasicDataDAL basicData = new BasicDataDAL();
            return basicData.SelectAllBasicData();
        }

        #endregion 查询基本数据配置表全部信息

        #region 管理员开锁

        public void ManagerOpenLock(string roomName)
        {
            ManagerGetKeyBLL managerGetKeyBLL = new ManagerGetKeyBLL();
            managerGetKeyBLL.ManagerGetKey(roomName);
        }

        #endregion 管理员开锁

        #region 值班人员领取钥匙

        /// <summary>
        /// 推送值班人员领取钥匙的卡片消息
        /// </summary>
        /// <param name="userID"></param>
        public void DutyReceiveKey(string userID)
        {
            DutyReceiveKeyBLL dutyReceiveKeyBLL = new DutyReceiveKeyBLL();
            dutyReceiveKeyBLL.DutyReceiveKey(userID);
        }

        /// <summary>
        /// 推送是否为值班人员的卡片消息
        /// </summary>
        public void PushDutyMsg()
        {
            PushDutyMsgBLL pushDutyMsgBLL = new PushDutyMsgBLL();
            pushDutyMsgBLL.PushDutyMsg();
        }

        #endregion 值班人员领取钥匙

        public int UpdateLockState(string roomName, string LockState)
        {
            UpdateLockStateDAL updateLockState = new UpdateLockStateDAL();
            return updateLockState.UpdateLockState(roomName, LockState);
        }

        public string PushRoom(string calendarID, string userID)
        {
            CalendarApproveBLL calendarApprove = new CalendarApproveBLL();
            return calendarApprove.PushRoom(calendarID, userID);
        }

        #region 管理员发送消息获取消息中的关键字
        public string GetRoom(string room)
        {
            return new ManagerGetRoom().GetRoom(room);

        }
        #endregion


        #region 管理员领取钥匙
        /// <summary>
        /// 管理员领取钥匙插入记录
        /// </summary>
        /// <param name="managerRecord"></param>
        public void InsertRoomRecord(ManagerRecordEntity managerRecord)
        {
            ManagerGetRoom managerGet = new ManagerGetRoom();
            managerGet.InsertRoomRecord(managerRecord);
        }

        /// <summary>
        /// 管理员取消卡片
        /// </summary>
        /// <param name="cardID"></param>
        public void UpdateCancelRecord(string cardID)
        {
            new ManagerGetRoom().UpdateCancelRecord(cardID);
        }

        /// <summary>
        /// 领取钥匙更新记录
        /// </summary>
        /// <param name="cardID">领取钥匙卡片ID</param>
        /// <param name="returncardid">归还钥匙卡片ID</param>
        public int UpdateGetKey(string cardID, string returncardid)
        {
            return new ManagerGetRoom().UpdateGetKey(cardID, returncardid);
        }


        /// <summary>
        /// 管理员归还钥匙
        /// </summary>
        /// <param name="returncardid">归还卡片ID</param>
        /// <returns></returns>
        public void UpdateReturnKey(string returncardid)
        {
            new ManagerGetRoom().UpdateReturnKey(returncardid);
        }


        /// <summary>
        /// 发生错误时获取的信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="eventID">日程ID</param>
        /// <param name="RoomName">会议室名称</param>
        /// <returns></returns>
        public ErrorInfoEntity ErrorRemind(string userID, string eventID, string RoomName)
        {
            ErrorRemindBLL errorRemind = new ErrorRemindBLL();
            return errorRemind.ErrorRemind(userID, eventID, RoomName);

        }
        /// <summary>
        /// 查询未归还钥匙信息，根据领取钥匙卡片id查询
        /// </summary>
        /// <param name="cardID">领取钥匙卡片</param>
        /// <returns></returns>
        public ManagerRecordEntity SelectGetRecord(string cardID)
        {
            return new ManagerGetRoom().SelectGetRecord(cardID);
        }

        #endregion
    }
}
