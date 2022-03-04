/*
 * 创建人：武梓龙
 * 创建时间：2021年12月21日16点26分
 * 描述：根据日程ID查询信息
 */

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    public class SelectCalendarInfoDAL
    {
        public CalendarInfoEntity SelectCalendarInfo(string calendarID)
        {
            string sql = "select calendar_id,content,start_time,end_time,return_time,room_name,attend_count,organizer,organizer_id,send_people,create_time,update_time,get_time from t_calendar  where calendar_id=@calendarID";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID)
            };

            SQLHelper helper = new SQLHelper();
            Entity.CalendarInfoEntity calendarInfo = new Entity.CalendarInfoEntity();
            DataTable dataTable = helper.ExecuteQuery(sql, mySqls, CommandType.Text);
            foreach (DataRow row in dataTable.Rows)
            {
                calendarInfo.CalendarID = row["calendar_id"].ToString();
                calendarInfo.Content = row["content"].ToString();
                calendarInfo.StartTime = row["start_time"].ToString();
                calendarInfo.EndTime = row["end_time"].ToString();
                calendarInfo.ReturnTime = row["return_time"].ToString();
                calendarInfo.RoomName = row["room_name"].ToString();
                calendarInfo.AttendCount = row["attend_count"].ToString();
                calendarInfo.Organizer = row["organizer"].ToString();
                //calendarInfo.AttendPersonID = row["attendPersonID"].ToString();
                calendarInfo.OrganizerID = row["organizer_id"].ToString();
                //calendarInfo.ApproverID = row["approveID"].ToString();
                calendarInfo.SendPeople = row["send_people"].ToString();
                //calendarInfo.SendPeopleID = row["sendPeopleID"].ToString();
                calendarInfo.CreateTime = row["create_time"].ToString();
                calendarInfo.UpdateTime = row["update_time"].ToString();
                calendarInfo.GetTime = row["get_time"].ToString();
            }
            return calendarInfo;
        }

        /// <summary>
        /// 查询同一天内该会议室的使用情况
        /// </summary>
        /// <param name="roomName">房间名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>日程信息集合</returns>
        public List<CalendarInfoEntity> SelectCalendarTime(string roomName, string startTime, string endTime)
        {
            string sql = "SELECT calendar_id,room_name,start_time,end_time FROM t_calendar where return_time IS	NOT NULL AND room_name =@roomName AND end_time LIKE @endTime AND start_time LIKE @startTime";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new  MySqlParameter("@roomName",roomName),
                new MySqlParameter("@startTime",startTime+'%'),
                new MySqlParameter("@endTime",endTime+'%')
            };
            SQLHelper helper = new SQLHelper();
            DataTable selectCalendarTimeDataTable = helper.ExecuteQuery(sql, mySqls, CommandType.Text);
            List<CalendarInfoEntity> calendarInfos = new List<CalendarInfoEntity>();
            foreach (DataRow row in selectCalendarTimeDataTable.Rows)
            {
                calendarInfos.Add(new CalendarInfoEntity
                {
                    CalendarID = row["calendar_id"].ToString(),
                    RoomName = row["room_name"].ToString(),
                    StartTime = row["start_time"].ToString(),
                    EndTime = row["end_time"].ToString()
                });
            }
            return calendarInfos;

        }


        /// <summary>
        /// 根据组织者id和开始时间查询日程表
        /// </summary>
        /// <param name="organizerID">组织者id</param>
        /// <param name="startTime">开始时间</param>
        /// <returns>日程信息集合</returns>
        public List<CalendarInfoEntity> SelectSameTimePlace(string organizerID, string startTime)
        {
            string sql = "SELECT calendar_id,room_name,organizer,start_time,end_time FROM t_calendar WHERE organizer_id=@organizerID AND start_time Like @startTime";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@organizerID",organizerID),
                new MySqlParameter("@startTime",'%'+startTime+'%')
            };
            SQLHelper helper = new SQLHelper();
            DataTable sameTimePlaceTable = helper.ExecuteQuery(sql, mySqls, CommandType.Text);
            List<CalendarInfoEntity> calendarInfos = new List<CalendarInfoEntity>();
            foreach (DataRow row in sameTimePlaceTable.Rows)
            {
                calendarInfos.Add(new CalendarInfoEntity
                {
                    CalendarID = row["calendar_id"].ToString(),
                    RoomName = row["room_name"].ToString(),
                    Organizer = row["organizer"].ToString(),
                    StartTime = row["start_time"].ToString(),
                    EndTime = row["end_time"].ToString()
                });
            }
            return calendarInfos;
        }

        /// <summary>
        /// 根据审批ID查询日程信息
        /// </summary>
        /// <param name="processInstanceId">审批实例id</param>
        /// <returns>日程信息集合</returns>
        public CalendarInfoEntity SelectApproveContent(string approveID)
        {
            string sql = "select calendar_id,start_time from t_calendar where approve_id=@approveID";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@approveID",approveID)
            };
            SQLHelper helper = new SQLHelper();
            DataTable sameTimePlaceTable = helper.ExecuteQuery(sql, mySqls, CommandType.Text);

            CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
           

         
            foreach (DataRow row in sameTimePlaceTable.Rows)
            {

                calendarInfo.CalendarID = row["calendar_id"].ToString();
                calendarInfo.StartTime = row["start_time"].ToString();

            }
            return calendarInfo;
        }
    }
}