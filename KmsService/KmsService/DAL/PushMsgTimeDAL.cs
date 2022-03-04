using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace KmsService.DAL
{
    public class PushMsgTimeDAL
    {
        private SQLHelper sqlhelper = new SQLHelper();

        public DateTime BeforeMeetingStart(string calendarid)
        {
            string sql = "select start_time from t_calendar where calendar_id=@calendarid";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@calendarid",calendarid)
            };
            DataTable data = sqlhelper.ExecuteQuery(sql, mySqls, CommandType.Text);
            DateTime dateTime = DateTime.Now;
            foreach (DataRow row in data.Rows)
            {
                dateTime = Convert.ToDateTime(row["start_time"]);
            }

            return dateTime;
        }

        public DateTime BeforeMeetingEnd(string calendarid)
        {
            string sql = "select end_time from t_calendar where calendar_id=@calendarid";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@calendarid",calendarid)
            };
            DataTable data = sqlhelper.ExecuteQuery(sql, mySqls, CommandType.Text);
            DateTime dateTime = DateTime.Now;
            foreach (DataRow row in data.Rows)
            {
                dateTime = Convert.ToDateTime(row["end_time"]);
            }

            return dateTime;
        }
    }
}