using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    public class ModifyConfiguration
    {
        private SQLHelper sqlhelper = new SQLHelper();

        /// <summary>
        /// 更新教室名称
        /// </summary>
        public int UpdateRoomName(string ID, string caName)
        {
            string sql = "update t_basicdata set room_name =@name where id=@ID";
            MySqlParameter[] sqlParameters = new MySqlParameter[] {
                new MySqlParameter("@name", caName),
                new MySqlParameter("@ID", ID) };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        //更新至少使用人数
        public int UpdateMinUseNumber(string ID, string minUseNumber)
        {
            string sql = "update t_basicdata set min_use_number =@minUseNumber where id=@ID";
            MySqlParameter[] sqlParameters = new MySqlParameter[] {
                new MySqlParameter("@minUseNumber", minUseNumber),
                new MySqlParameter("@ID", ID) };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        //更新会议开始前*分钟取钥匙
        public int UpdateBeforeTakeKey(string ID, string beforeTakeKey)
        {
            string sql = "update t_basicdata set before_take_key =@beforeTakeKey where id=@ID";
            MySqlParameter[] sqlParameters = new MySqlParameter[] {
                new MySqlParameter("@beforeTakeKey", beforeTakeKey),
                new MySqlParameter("@ID", ID) };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        //更新会议结束前*分钟还钥匙
        public int UpdateAfterReturnKey(string ID, string afterReturnKey)
        {
            string sql = "update t_basicdata set after_return_key =@afterReturnKey where id=@ID";
            MySqlParameter[] sqlParameters = new MySqlParameter[] {
                new MySqlParameter("@afterReturnKey", afterReturnKey),
                new MySqlParameter("@ID", ID) };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        //更新会议室使用时间上限
        public int UpdateUpperTime(string ID, string upperTime)
        {
            string sql = "update t_basicdata set upper_time =@upperTime where id=@ID";
            MySqlParameter[] sqlParameters = new MySqlParameter[] {
                new MySqlParameter("@upperTime", upperTime),
                new MySqlParameter("@ID", ID) };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        //更新会议室使用时间下限
        public int UpdateLowerTime(string ID, string lowerTime)
        {
            string sql = "update t_basicdata set lower_time =@lowerTime where id=@ID";
            MySqlParameter[] sqlParameters = new MySqlParameter[] {
                new MySqlParameter("@lowerTime", lowerTime),
                new MySqlParameter("@ID", ID) };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        /// <summary>
        ///  取出所有教室信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBasicData()
        {
            DataTable dt = new DataTable();
            string sql = "select * from t_basicdata";
            dt = sqlhelper.ExecuteQuery(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 判断教室名称是否已存在
        /// </summary>
        /// <param name="caName">教室名称</param>
        /// <returns></returns>
        public bool CaNameIsExists(string caName)
        {
            bool flag = false;

            string sql = "select * from t_basicdata where room_name ='" + caName + "'";

            DataTable dt = sqlhelper.ExecuteQuery(sql, CommandType.Text);

            if (dt.Rows.Count > 0)
            {
                flag = true;
            }

            return flag;
        }
    }
}