using System;
using System.Collections.Generic;
using System.Data;
using KmsService.AuthInterface;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.KeyBLL;
using KmsService.KeyBLL.CalendarStrategyHandler;
using KmsService.KeyBLL.RoomConfigurationHandler;
using KmsService.KeyBLL.SendApproveHandler;
using KmsService.PointInterface;
namespace KmsService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class Service : IService
    {
        #region 获取审批人
        public List<AllusersEntitiesItem> GetApprover()
        {
            GetApproverBLL getApproverBLL = new GetApproverBLL();
            return getApproverBLL.GetApprover();
        }
        #endregion
        #region 管理员会议室配置
        public bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            ModifyRoomBLL modifyRoomBLL = new ModifyRoomBLL();
            return modifyRoomBLL.ModifyRoom(basicDataStr, newBasicData, oldBasicData, allLockNumber);
        }
        #endregion

        #region 更新教室数据基础表        
        /// <summary>
        /// 取出所有教室信息
        /// </summary>
        /// <returns>基本数据配置信息</returns>
        public DataTable SelectBasicData()
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.SelectBasicData();
        }

        /// <summary>
        /// 判断教室名称是否已存在
        /// </summary>
        /// <param name="roomName">教室名称</param>
        /// <returns>受影响的条数</returns>
        public bool RoomNameIsExists(string roomName)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.RoomNameIsExists(roomName);
        }

        /// <summary>
        /// 判断是否存在此教室id
        /// </summary>
        /// <param name="caName">教室名称</param>
        /// <returns>true:存在此id的教室；false：不存在此i的教室</returns>
        public bool RoomIdIsExists(string roomId)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.RoomIdIsExists(roomId);
        }
        /// <summary>
        /// 更新基本数据配置表
        /// </summary>
        /// <param name="basicDataEntity">基本数据配置实体</param>
        /// <returns>受影响的条数</returns>
        public int UpdateBasicData(BasicDataEntity basicDataEntity)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.UpdateBasicData(basicDataEntity);
        }

        /// <summary>
        /// 添加教室
        /// </summary>
        /// <param name="basicDataEntity">基本数据配置实体</param>
        /// <returns>受影响的条数</returns>
        public int InsertMetting(BasicDataEntity basicDataEntity)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.InsertMetting(basicDataEntity);
        }

        /// <summary>
        /// 删除会议室
        /// </summary>
        /// <param name="roomId">会议室id</param>
        /// <returns>受影响的条数</returns>
        public int DeleteMetting(string roomId)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.DeleteMetting(roomId);
        }

        // 获取t_room表已经使用了的锁的编号               
        public List<string> GetLockNumber()
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.GetLockNumber();
        }

        // 更新t_room表会议室名称
        public int UpdateRoomName(int id, string roomName)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.UpdateRoomName(id, roomName);
        }

        // t_room表添加教室               
        public int InsertRoomMetting(RoomInfoEntity roomEntity)
        {
            ModifyConfigurationDAL modify = new ModifyConfigurationDAL();
            return modify.InsertRoomMetting(roomEntity);
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
        //public List<string> AllDingDingID()
        //{
        //    PersonReportBLL personReportBLL = new PersonReportBLL();
        //    return personReportBLL.AllDingDingID();
        //}


        public void RemoveWeekDuplication()//7天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        {
            PersonReportBLL personReport = new PersonReportBLL();
            personReport.RemoveWeekDuplication();
        }


        public void RemoveMonthDuplication()//30天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        {
            PersonReportBLL personReport = new PersonReportBLL();
            personReport.RemoveMonthDuplication();
        }

        /// <summary>
        /// 根据用户的需求，把月推送改为周推送，周推送改为月推送
        /// </summary>
        /// <param name="ddID"></param>
        /// <param name="state"></param>
        public void ModifyState(string ddID, string state)
        {
            PersonReportDAL personreport = new PersonReportDAL();
            personreport.ModifyState(ddID, state);
        }

        /// <summary>
        /// 获取用户要推送的状态
        /// </summary>
        /// <param name="ddID"></param>
        /// <returns></returns>
        public string UserPushState(string ddID)
        {
            PersonReportBLL personreport = new PersonReportBLL();
            return personreport.UserPushState(ddID);
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

        public void SendApprove(string calendarID, string userID, string roomName, string approveType)
        {
            KeyBLL.SendApproveHandler.SendApproveBLL sendApprove = new KeyBLL.SendApproveHandler.SendApproveBLL();
            sendApprove.SendApprove(calendarID, userID, roomName, approveType);

        }
        #region 判断审批是否同意
        public void GetApproveResult(ApproveInstanceModel approveContent)
        {
            new GetApproveResultBLL().GetApproveResult(approveContent);
        }
        #endregion

        //public void AutoSendApprove(string calendarID, string userID, string roomName)
        //{
        //    SendApproveBLL sendApprove = new SendApproveBLL();
        //    sendApprove.AutoSendApprove(calendarID, userID, roomName);
        //}

        #endregion 发送审批并且自动同意或者不自动同意

        #region 更新归还钥匙时间

        public void UpdateReturnTime(string calendarID)
        {
            BeforeMeetingDAL beforeMeetingDAL = new BeforeMeetingDAL();
            beforeMeetingDAL.UpdateReturnTime(calendarID);
        }

        #endregion 更新归还钥匙时间

        #region 查询基本数据配置表全部信息

        public BasicDataEntity SelectAllBasicData(string roomName)
        {
            BasicDataDAL basicData = new BasicDataDAL();
            return basicData.SelectAllBasicData(roomName);
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
        public string GetRoom(string room, string managerID)
        {
            return new ManagerGetRoom().GetRoom(room, managerID);

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

        /// <summary>
        /// 申请会议室加分
        /// </summary>
        /// <param name="token">积分机器人token</param>
        /// <param name="authID">用户权限ID</param>
        /// <returns></returns>
        public string AddPoints(string token, string authID)
        {
            AddIntegral addIntegral = new AddIntegral();
            return addIntegral.AddPoints(token, authID); 
        }

        #endregion
    }
}
