using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    public class UpdateLockNumberDAL
    {
        /// <summary>
        /// 更新会议室表锁id根据会议室名称
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="lockNumber">锁ID</param>
        public int UpdateLockState(string roomID, string lockNumber)
        {
            string sql = "update t_room set lock_number = @LockNumber,update_time =now() where room_name =@roomID";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@LockNumber",lockNumber),
                new MySqlParameter("@roomID",roomID)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySql, CommandType.Text);
        }
    }
}