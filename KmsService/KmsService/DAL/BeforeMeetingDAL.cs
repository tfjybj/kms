/*
 * 创建人：王梦杰
 * 创建日期：2022年4月1日09:59:18
 * 描述：会议前D层操作类
 */
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    /// <summary>
    /// 会议前DAL类
    /// </summary>
    public class BeforeMeetingDAL
    {
        private SQLHelper sQLHelper = new SQLHelper();

        /// <summary>
        /// 会议结束前10分钟发送还钥匙消息
        /// </summary>
        /// <returns>日程信息实体</returns>
        public List<CalendarInfoEntity> BeforeMeetingEnd()
        {
            //实例化日程信息类型list集合
            List<CalendarInfoEntity> calendarList = new List<CalendarInfoEntity>();

            string sql = "select organizer_id, end_time,room_name,calendar_id,start_time,get_time from t_calendar where is_start =1 and return_time is Null and is_end=0 and is_delete=0";
            DataTable dt = sQLHelper.ExecuteQuery(sql, CommandType.Text);

            //将返回的钉钉id和会议结束时间遍历到list集合中
            foreach (DataRow row in dt.Rows)
            {
                calendarList.Add(new CalendarInfoEntity()
                {
                    RoomName = row["room_name"].ToString().Trim(),      //会议室ID
                    CalendarID = row["calendar_id"].ToString().Trim(),   //日程ID
                    OrganizerID = row["organizer_id"].ToString().Trim(),   //钉钉id
                    EndTime = row["end_time"].ToString().Trim(),//会议结束时间
                    StartTime = row["start_time"].ToString(),
                    GetTime = row["get_time"].ToString()
                });
            }
            return calendarList;
        }

        /// <summary>
        /// 更新钥匙归还时间
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <returns></returns>
        public int UpdateReturnTime(string calendarID)
        {
            string sql = "update t_calendar set return_time = now() where calendar_id = @calendarID and is_delete=0";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID)
            };
           return sQLHelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        /// <summary>
        /// 会议前30分钟发送领取钥匙消息链接
        /// </summary>
        /// <returns></returns>
        public List<CalendarInfoEntity> BeforeMeetingStart()
        {
            string sql = "select organizer_id ,calendar_id,room_name, start_time  from t_calendar where is_start=0 and is_delete=0";

            DataTable result = sQLHelper.ExecuteQuery(sql, CommandType.Text);

            List<CalendarInfoEntity> information = new List<CalendarInfoEntity>();

            foreach (DataRow row in result.Rows)
            {
                information.Add(new CalendarInfoEntity
                {
                    OrganizerID = row["organizer_id"].ToString(),
                    StartTime = row["start_time"].ToString(),
                    CalendarID = row["calendar_id"].ToString(),
                    RoomName = row["room_name"].ToString()
                });
            }
            return information;
        }

        /// <summary>
        /// 消息领取钥匙卡片id更新到数据库
        /// </summary>
        /// <param name="calendarID"></param>
        /// <param name="outTrackid"></param>
        public void UpdateOutTrackID(string calendarID, string outTrackid)
        {
            string sql = "update t_calendar set out_track_id =@outTrackid where calendar_id=@calendarID ";
            MySqlParameter[] mysql = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID),
                new MySqlParameter("@outTrackid",outTrackid)
            };

            sQLHelper.ExecuteQuery(sql, mysql, CommandType.Text);
        }

        /// <summary>
        /// 给用户发信息后修改数据库中is_start的状态
        /// </summary>
        /// <param name="ScheduleID">日程id</param>
        /// <returns></returns>
        public void UpdateIsStart(string ScheduleID)
        {
            string sql = "update t_calendar set is_start ='1' where calendar_id= @ScheduleID";
            MySqlParameter[] mysql = new MySqlParameter[]
            {
                new MySqlParameter("@ScheduleID",ScheduleID),
            };
            sQLHelper.ExecuteNonQuery(sql, mysql, CommandType.Text);
        }

        /// <summary>
        /// 更新发送归还钥匙消息卡片状态
        /// </summary>
        /// <param name="calendarID"></param>
        public void UpdateIsEnd(string calendarID)
        {
            string sql = "Update t_calendar  set is_end =1 where calendar_id = @calendarID";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID)
            };
            sQLHelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        /// <summary>
        /// 获取calendar表中的isstart=1且isend=0的教室名称和会议开始时间
        /// </summary>
        public List<CalendarInfoEntity> CancelCard()
        {
            string sql = "select room_name ,out_track_id, organizer_id, calendar_id, start_time  from t_calendar where is_start=1 and is_end=0 and is_delete=0 and get_time is Null";
            DataTable result = sQLHelper.ExecuteQuery(sql, CommandType.Text);
            List<CalendarInfoEntity> information = new List<CalendarInfoEntity>();

            foreach (DataRow row in result.Rows)
            {
                information.Add(new CalendarInfoEntity
                {
                    RoomName = row["room_name"].ToString(),
                    StartTime = row["start_time"].ToString(),
                    OrganizerID = row["organizer_id"].ToString(),
                    CalendarID = row["calendar_id"].ToString(),
                    OutTrackID = row["out_track_id"].ToString()
                });
            }

            return information;
        }

        /// <summary>
        /// 查询教室的使用状态
        /// </summary>
        /// <param name="roomName">教室名称</param>
        public string RoomState(string roomName)
        {
            string sql = "select lock_state from t_room where room_name=@roomName";
            MySqlParameter[] mysql = new MySqlParameter[]
            {
                new  MySqlParameter("roomName",roomName),
            };
            DataTable result = sQLHelper.ExecuteQuery(sql, mysql, CommandType.Text);
            string state = null;
            foreach (DataRow row in result.Rows)
            {
                state = row["lock_state"].ToString();
            }
            return state;
        }

       

    }
}