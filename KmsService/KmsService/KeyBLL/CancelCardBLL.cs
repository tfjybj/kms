
/*
 * 创建人：王梦杰
 * 创建日期：2022年3月12日19:45:39
 * 描述：卡片过期
 */
using System;
using System.Collections.Generic;
using KmsService.DAL;
using KmsService.Entity;
using System.Configuration;
using KmsService.Log4;
namespace KmsService.KeyBLL
{
    /// <summary>
    /// 卡片过期
    /// </summary>
    public class CancelCardBLL
    {
        /// <summary>
        /// 卡片过期
        /// </summary>
        public void Cancel()
        {
            HttpHelper httphelper = new HttpHelper();


            BeforeMeetingDAL beforeMeetingDAL = new BeforeMeetingDAL();
            List<CalendarInfoEntity> information = beforeMeetingDAL.CancelCard();

            string newtrackid = null;//存放分割之后的消息卡片id
            string roomName = null;//存放开始时间>=会议开始时间的教室名称


            foreach (var item in information)
            {
                //切割消息卡片id的字符串
                string trackid = item.OutTrackID;//卡片消息id
                char[] chr = new char[] { '"' };
                string[] result = trackid.Split(chr, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    newtrackid = result[i].ToString();
                }

                string calendar = item.CalendarID;//日程id
                //当前时间>=会议开始时间
                if (DateTime.Now > Convert.ToDateTime(item.StartTime).AddMinutes(6))
                {

                    roomName = item.RoomName;//查出符合条件的教室

                    SelectRoomInfoDAL roomInfo = new SelectRoomInfoDAL();

                    RoomInfoEntity RoomInformation = roomInfo.SelectRoomInfo(roomName);//查出教室的锁号
                    string lockstate = RoomInformation.LockNumber + "isLock";//锁号+锁的关闭状态
                    //当查询到的教室的状态是否是开
                    BeforeMeetingDAL before = new BeforeMeetingDAL();
                    string state = before.RoomState(roomName);
                    //string newState = state.Substring(2, 6);

                    UpdateLockStateDAL updateLockState = new UpdateLockStateDAL();
                    updateLockState.UpdateLockState(roomName, lockstate);//修改教室的状态
                    string url = ConfigurationManager.ConnectionStrings["updateCard"].ConnectionString + string.Format("?roomName={0}&OutTrackId={1}", roomName, newtrackid);//卡片消息作废
                    LoggerHelper.Info("卡片过期的url地址：" + url);
                    before.UpdateIsEnd(calendar);//更新isend状态
                    string urlResult = httphelper.HttpGet(url);
                    LoggerHelper.Info("卡片过期的url执行结果：" + urlResult);
                    //卡片过期就删除日程表中此日程记录
                    UpdateCalendar updateCalendar = new UpdateCalendar();
                    updateCalendar.UpdateIsDelete(calendar);



                }

            }

        }
    }
}