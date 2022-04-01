﻿/*
 * 创建人：王梦杰
 * 创建日期：2022年3月12日19:45:39
 * 描述：开锁
 */
using System;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;
using System.Configuration;
using System.Threading;
using KmsService.PointInterface;
using KmsService.AuthInterface;
using KmsService.DingDingModel;
using KmsService.DingDingInterface;
namespace KmsService.KeyBLL
{
    /// <summary>
    /// 开锁发送消息逻辑类
    /// </summary>
    public class OpenLockBLL
    {
        //private static bool Implement = true;
        HttpHelper http = new HttpHelper();
        //string eventID = null;
        /// <summary>
        /// 开锁方法
        /// </summary>
        /// <param name="keyNumber">钥匙ID</param>
        public void Open(string userId, string eventID)
        {
            LoggerHelper.Info("调用开锁方法:代码走到这里了"+userId+"、"+eventID);
            try
            {
               
                TimeSpan Interval = TimeSpan.FromMilliseconds(100);
                //线程休眠防止快速点击数据库查询信息延时
                Thread.Sleep(Interval);
                //获取日程详情
                SelectCalendarInfoDAL selectAttendPersonDAL = new SelectCalendarInfoDAL();
                CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
                calendarInfo = selectAttendPersonDAL.SelectCalendarInfo(eventID);



                LoggerHelper.Info("判断之外:代码走到这里了"+calendarInfo.GetTime+calendarInfo.CalendarID);

                if (""==calendarInfo.GetTime)
                {
                    LoggerHelper.Info("判断之内：代码走到这里了");
                    //Implement = false;
                    //智能开锁
                    //获取房间ID
                    string roomID = calendarInfo.RoomName;
                    //获取日程ID
                    string calendarID = calendarInfo.CalendarID;
                    //组织者ID
                    string organizerID = calendarInfo.OrganizerID;
                    //获取锁ID
                    SelectRoomInfoDAL selectRoomInfoDAL = new SelectRoomInfoDAL();
                    RoomInfoEntity roomInfo = selectRoomInfoDAL.SelectRoomInfo(roomID);
                    //获取锁的状态
                    string roomState = roomInfo.LockState;

                    MQTTServer.Instance().OpenLock(roomInfo.LockNumber);

                    UpdateCalendar updateCalendar = new UpdateCalendar();
                    updateCalendar.UpdateGetTime(calendarID);
                    //更新会议室使用状态
                    OpenLockDAL openLock = new OpenLockDAL();
                    int result = openLock.UpdateState(roomInfo.LockNumber.ToString());
                    string url = ConfigurationManager.ConnectionStrings["returnKeyCard"].ConnectionString + string.Format("?roomName={0}&calendarID={1}&userID={2}", roomID, calendarID, organizerID);

                    string urlResult = http.HttpGet(url);

                    
                    LoggerHelper.Info("推送归还钥匙卡片消息：" + urlResult);//把执行的结果展示在日志中

                    if (roomID==ConfigurationManager.ConnectionStrings["insideRoom"].ConnectionString || roomID == ConfigurationManager.ConnectionStrings["outsideRoom"].ConnectionString)
                    {

                    }
                    else
                    {
                        AddIntegral addIntegral = new AddIntegral();
                        GetUnionID unionID = new GetUnionID();
                        GetUnionIDModel unionIDModel = unionID.GetDingDingUnionID(userId);
                        string phoneNumber = unionIDModel.result.mobile;
                        GetUserToken getUserToken = new GetUserToken();
                        UserTokenModel authID = getUserToken.GetToken(phoneNumber);
                        UserTokenModel userToken= getUserToken.GetToken("superAdmin");
                        string token = userToken.data.token;
                        string userAuthID = authID.data.userId;
                        addIntegral.AddPoints(token, userAuthID);
                    }
                }
            }

            catch (Exception ex)
            {
                LoggerHelper.Error("调用开锁方法的错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }

        }       
    }
}
