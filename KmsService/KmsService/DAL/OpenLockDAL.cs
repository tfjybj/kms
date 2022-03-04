using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    public class OpenLockDAL
    {
        private SQLHelper helper = new SQLHelper();

        /// <summary>
        /// 更新会议室使用状态
        /// </summary>
        /// <param name="KeyNumber">钥匙ID</param>
        /// <returns></returns>
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