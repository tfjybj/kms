using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    public class UpdateLockStateDAL
    {
        public int UpdateLockState(string roomName, string lockState)
        {
            string sql = "update t_room set lock_state=@LockState where room_name=@roomName";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@LockState",lockState),
                new MySqlParameter("@roomName",roomName)
            };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySql, CommandType.Text);
        }
    }
}