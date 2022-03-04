/*
 * 创建人：武梓龙
 * 创建时间：2021年12月21日16点26分
 * 描述：插入日程的信息
 */

using MySql.Data.MySqlClient;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    public class InsertCalendarDateDAL
    {
        public int InsertCalendar(CalendarInfoEntity calendarInfo)
        {
            string sql = "Insert into t_calendar (calendar_id,content,end_time,start_time,return_time,room_name,attend_count,organizer,approver,send_people,organizer_id,create_time,update_time,is_start,is_end) select @calendarID,@content,@endTime,@startTime,@returnTime,@roomID,@attendCount,@organizer,@approver,@sendPeople,@organizerID,@createTime,@updateTime,@isstart,@isend FROM DUAL WHERE NOT EXISTS( SELECT calendar_id FROM t_calendar WHERE calendar_id = @calendarID)";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarInfo.CalendarID),
                new MySqlParameter("@content",calendarInfo.Content),
                new MySqlParameter("@endTime",calendarInfo.EndTime),
                new MySqlParameter("@startTime",calendarInfo.StartTime),
                new MySqlParameter("@returnTime",calendarInfo.ReturnTime),
                new MySqlParameter("@roomID",calendarInfo.RoomName),
                new MySqlParameter("@attendCount",calendarInfo.AttendCount),
                new MySqlParameter("@organizer",calendarInfo.Organizer),
                new MySqlParameter("@organizerID",calendarInfo.OrganizerID),
                new MySqlParameter("@approver",calendarInfo.Approver),
                new MySqlParameter("@sendPeople",calendarInfo.SendPeople),
                new MySqlParameter("@createTime",calendarInfo.CreateTime),
                new MySqlParameter("@updateTime",calendarInfo.UpdateTime),
                new MySqlParameter("@isstart",calendarInfo.IsStart),
                new MySqlParameter("@isend",calendarInfo.IsEnd)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqls, CommandType.Text);
        }
    }
}