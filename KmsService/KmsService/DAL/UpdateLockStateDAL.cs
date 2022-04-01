/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：room表D层操作类
 */
using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    /// <summary>
    /// 锁状态操作类
    /// </summary>
    public class UpdateLockStateDAL
    {
        /// <summary>
        /// 更新会议室表钥匙状态
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="lockState">钥匙状态</param>
        /// <returns></returns>
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