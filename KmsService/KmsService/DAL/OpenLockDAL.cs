/*
 * 创建人：王梦杰
 * 创建日期：2022年4月1日10:04:03
 * 描述：开锁操作D层操作类
 */
using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    /// <summary>
    /// 开锁类
    /// </summary>
    public class OpenLockDAL
    {
        private SQLHelper helper = new SQLHelper();

        /// <summary>
        /// 更新会议室使用状态
        /// </summary>
        /// <param name="KeyNumber">钥匙ID</param>
        /// <returns>受影响行数</returns>
        public int UpdateState(string lockNumber)
        {
            string lockState = lockNumber + "isOpen";

            string sql = "UPDATE  t_room set lock_state=@lockState WHERE lock_number=@lockNumber";
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                   new MySqlParameter("@lockNumber",lockNumber),
                   new MySqlParameter ("@lockState",lockState)
            };
            return helper.ExecuteNonQuery(sql, mySqlParameter, CommandType.Text);        //返回数据库影响条数
        }
    }
}