using MySql.Data.MySqlClient;
using System.Data;
namespace KmsService.DAL
{
    /// <summary>
    /// 日程表更新
    /// </summary>
    public class UpdateCalendar
    {
        /// <summary>
        /// 更新领取钥匙时间
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <returns>受影响行数</returns>
        public int UpdateGetTime(string calendarID)
        {
            string sql = "update t_calendar set get_time=now() where calendar_id=@calendarID";
            MySqlParameter[] mySqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameters, CommandType.Text);
        }


        /// <summary>
        /// 更新is_start、is_end
        /// </summary>
        /// <param name="calendarID">审批ID</param>
        /// <param name="isStartState">会议开始时间的状态</param>
        /// <param name="isEndState">会议结束时间的状态</param>
        /// <returns>受影响行数</returns>
        public int UpdateIsStartIsEnd(string calendarID, string isStartState,string isEndState)
        {
            string sql = "update t_calendar set is_start =@isStartState ,is_end=@isEndState where calendar_id=@calendarID";
            MySqlParameter[] mySqlParameters = new MySqlParameter[] 
            {
                new MySqlParameter("@calendarID",calendarID),
                new MySqlParameter("@isStartState",isStartState),
                new MySqlParameter("@isEndState",isEndState)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameters, CommandType.Text);
        }

        /// <summary>
        /// 更新日程作废
        /// </summary>
        /// <param name="approveID">审批ID</param>
        /// <param name="state">作废状态：0同意，1不同意</param>
        /// <returns>受影响行数</returns>
        public int UpdateCalendarVoid(string calendarID, string state)
        {
            string sql = "update t_calendar set calendar_is_void =@state where calendar_id=@calendarID";
            MySqlParameter[] mySqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID),
                new MySqlParameter("@state",state)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameters, CommandType.Text);
        }

        /// <summary>
        /// 更新审批ID
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <returns>受影响的条数</returns>
        public int UpdateApproveID(string calendarID,string approveID)
        {
            string sql = "update t_calendar set approve_id =@approveID where calendar_id=@calendarID";
            MySqlParameter[] mySqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID),
                new MySqlParameter("@approveID",approveID)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameters, CommandType.Text);
        }

        /// <summary>
        /// 更新is_delete字段为1
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <returns>受影响行数</returns>
        public int UpdateIsDelete(string calendarID)
        {
            string sql = "update t_calendar set is_delete=@isDelete where calendar_id=@calendarID";
            MySqlParameter[] mySqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@calendarID",calendarID),
                new MySqlParameter("@isDelete","1")
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameters, CommandType.Text);
        }
    }
}