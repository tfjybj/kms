using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KmsService.DingDingModel;
using KmsService.Entity;

namespace KmsService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService
    {
        //管理员会议室配置
        [OperationContract]
        bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber);

        //获取审批人
        [OperationContract]
        List<AllusersEntitiesItem> GetApprover();

        #region 更新教室基础数据表
        //更新基本数据配置表
        [OperationContract]
        int UpdateBasicData(BasicDataEntity basicDataEntity);


        //取出所有教室信息
        [OperationContract]
        DataTable SelectBasicData();


        //判断教室名称是否已存在
        [OperationContract]
        bool RoomNameIsExists(string roomName);

        //添加教室
        [OperationContract]
        int InsertMetting(BasicDataEntity basicDataEntity);

        //判断是否存在此教室id
        [OperationContract]
        bool RoomIdIsExists(string roomId);

        //删除会议室
        [OperationContract]
        int DeleteMetting(string roomId);


        // 获取t_room表已经使用了的锁的编号       
        [OperationContract]
        List<string> GetLockNumber();

        // 更新t_room表会议室名称
        [OperationContract]
        int UpdateRoomName(int id, string roomName);

        // t_room表添加教室       
        [OperationContract]
        int InsertRoomMetting(RoomInfoEntity roomEntity);
        #endregion 更新教室基础数据表


        //更新领取钥匙卡片消息的id
        [OperationContract]
        void UpdateOutTrackID(string calendarID, string outTrackid);

        //卡片过时不领取作废
        [OperationContract]
        List<CalendarInfoEntity> CancelCard();

        //查询当前时间>=会议开始时间的教室名称
        [OperationContract]
        string RoomState(string roomName);

        //提前30分钟给用户发消息，确保只给用户发一次
        [OperationContract]
        void UpdateIsStart(string ScheduleID);

        //会议结束前10分钟给用户发消息，确保只给用户发一次
        [OperationContract]
        void UpdateIsEnd(string calendarID);

        [OperationContract]
        void OpenLock(string value);

        [OperationContract]
        string GetAccessToken();


        [OperationContract]
        SendApproveRe_valueModel SendApproveTask(SendApproveModel sendApproveModel);

        //[OperationContract]
        //void CreateApprove();

        [OperationContract]
        string GetApproveID(DateTime start_time, DateTime end_time);

        [OperationContract]
        ApproveContentTask GetApproveContent(string processInstance);

        [OperationContract]
        UserCodeModel GetDingID(string phoneNumber);

        [OperationContract]
        string GetDeptID(string phoneNumber);

        [OperationContract]
        RoomInfoEntity SelectRoomInfo(string roomName);//获取有关教室的信息（锁号）



        /// <summary>
        /// 会议结束前10分钟发送还钥匙消息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CalendarInfoEntity> BeforeMeetingEnd();

        /// <summary>
        /// 会议前30分钟发送领取钥匙消息链接
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CalendarInfoEntity> BeforeMeetingStart();

        #region 推送会议室


        [OperationContract]
        string PushRoom(string calendarID, string userID);

        #endregion 推送会议室

        #region 给用户推送周报
        [OperationContract]
        void RemoveWeekDuplication();//7天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中
        [OperationContract]
        void RemoveMonthDuplication();//30天需要发送报表的用户，把报表中没有的用户筛选出来，后面会添加到报表中


        /// <summary>
        /// 获取日程信息详情
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="CalendarID"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        [OperationContract]
        SelectCalendarModel SelectCalendarInfo(string userID, string CalendarID, string eventID);

        /// <summary>
        /// 开锁
        /// </summary>
        /// <param name="userId">userid</param>
        [OperationContract]
        void Open(string userId, string eventID);

        /// <summary>
        /// 更新归还钥匙时间
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <returns></returns>
        [OperationContract]
        void UpdateReturnTime(string calendarID);

        /// <summary>
        /// 插入日程表
        /// </summary>
        /// <param name="calendarInfo">日程实体</param>
        /// <returns></returns>
        [OperationContract]
        int InsertCalendar(CalendarInfoEntity calendarInfo);

        /// <summary>
        /// 自动同意审批
        /// </summary>
        /// <param name="model">审批实体</param>
        /// <param name="agreeRequest"></param>
        /// <returns></returns>
        [OperationContract]
        string AgreeRequestInfo(AgreeRequestModel model, Request agreeRequest);

        //[OperationContract]
        //int UsageTimes();

        #endregion 给用户推送周报    
        /// <summary>
        /// 用户状态为周推送时，修改状态为月推送,月推送改为周推送
        /// </summary>
        [OperationContract]
        void ModifyState(string ddID, string state);

        [OperationContract]
        string UserPushState(string ddID);



        #region 根据参会人数推送可用会议室

        [OperationContract]
        string PushRoomName(int participants);

        #endregion 根据参会人数推送可用会议室

        #region 自动发送会议室申请审批并同意

        [OperationContract]
        void SendApprove(string calendarID, string userID, string roomName, string approveType);

        #endregion 自动发送会议室申请审批并同意


        #region 判断审批是否同意
        [OperationContract]
        void GetApproveResult(ApproveInstanceModel approveContent);
        #endregion



        #region 未正常归还钥匙进行扣分处理

        /// <summary>
        /// 未正常归还钥匙进行扣分处理
        /// </summary>
        /// <param name="calendarID">日程id</param>
        /// <param name="userid">用户dingid</param>
        [OperationContract]
        void ReturnKeyLate(string calendarID, string userid);

        #endregion 未正常归还钥匙进行扣分处理

        #region 动态归还

        [OperationContract]
        void DynamicReturnKey(string calendarID);

        #endregion 动态归还

        #region 查询基本数据表全部信息

        [OperationContract]
        BasicDataEntity SelectAllBasicData(string roomName);

        #endregion 查询基本数据表全部信息

        #region 每周会议室使用情况推送-会议室名称

        [OperationContract]
        string WeekRoomName(string dingDingID);

        #endregion 每周会议室使用情况推送-会议室名称

        #region 每周会议室使用情况推送-使用时间段

        [OperationContract]
        string WeekUseTime(string dingDingID);

        #endregion 每周会议室使用情况推送-使用时间段

        #region 每周会议室使用情况推送-使用次数

        [OperationContract]
        string WeekUseCount(string dingDingID);

        #endregion 每周会议室使用情况推送-使用次数

        #region 每周会议室使用情况推送-发送的所有人

        //[OperationContract]
        //List<string> AllDingDingID();

        #endregion 每周会议室使用情况推送-发送的所有人


        #region 管理员开锁

        [OperationContract]
        void ManagerOpenLock(string roomName);

        #endregion 管理员开锁

        [OperationContract]
        int UpdateLockState(string roomName, string LockState);


        #region 管理员发送消息获取消息中的关键字
        [OperationContract]
        string GetRoom(string room, string managerID);
        #endregion


        #region 管理员领取钥匙
        /// <summary>
        /// 管理员领取钥匙插入记录
        /// </summary>
        /// <param name="managerRecord"></param>
        [OperationContract]
        void InsertRoomRecord(ManagerRecordEntity managerRecord);
        /// <summary>
        /// 管理员取消卡片
        /// </summary>
        /// <param name="cardID"></param>
        [OperationContract]
        void UpdateCancelRecord(string cardID);
        /// <summary>
        /// 领取钥匙更新记录
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="returncardid"></param>
        [OperationContract]
        int UpdateGetKey(string cardID, string returncardid);
        /// <summary>
        /// 管理员归还钥匙
        /// </summary>
        /// <param name="returncardid"></param>
        [OperationContract]
        void UpdateReturnKey(string returncardid);

        /// <summary>
        /// 查询未归还钥匙信息，根据领取钥匙卡片id查询
        /// </summary>
        /// <param name="cardID">领取钥匙卡片</param>
        /// <returns></returns>
        [OperationContract]
        ManagerRecordEntity SelectGetRecord(string cardID);
        #endregion


        /// <summary>
        /// 发生错误时获取的信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="eventID">日程ID</param>
        /// <param name="RoomName">会议室名称</param>
        /// <returns></returns>
        [OperationContract]
        ErrorInfoEntity ErrorRemind(string userID, string eventID, string RoomName);

        [OperationContract]
        string AddPoints(string token, string authID);


        #region 取消日程，删除日程会议室申请
        [OperationContract]
        void DeleteCalendar(string calendarID);
        #endregion
    }

}
