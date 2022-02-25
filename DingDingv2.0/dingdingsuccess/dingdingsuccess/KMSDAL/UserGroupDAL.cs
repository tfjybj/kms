

using MySql.Data.MySqlClient;
using System.Data;
using dingdingsuccess.Log4;
namespace dingdingsuccess.KMSDAL
{
    /// <summary>
    /// 群会话表操作类
    /// </summary>
    public class UserGroupDAL
    {

        private SQLHelper sqlHelper;
        public UserGroupDAL()
        {
            sqlHelper = new SQLHelper();
        }

        /// <summary>
        /// 插入群ID记录
        /// </summary>
        /// <param name="userid">用户钉ID</param>
        /// <param name="chatid">群ID</param>
        /// <returns>影响行数</returns>
        public int Insert(string userid, string chatid)
        {
            string sql = "Insert into t_user_group (chat_id,user_id)values(@chatid,@userid)";
            MySqlParameter[] mySqls = new MySqlParameter[]
            {
                new MySqlParameter("@chatid",chatid),
                new MySqlParameter("@userid",userid),
            };
            int result = sqlHelper.ExecuteNonQuery(sql, mySqls, CommandType.Text);
            LoggerHelper.Error("插入群ID受影响行数：" + result);
            return result;
        }



        /// <summary>
        /// 查询群ID
        /// </summary>
        /// <param name="userid">用户钉ID</param>
        /// <returns>群ID</returns>
        public string SelectChatID(string userid)
        {
            string result = null;
            string sql = "select chat_id from t_user_group where user_id=@userid";
            MySqlParameter[] mySqls = new MySqlParameter[]
           {
                new MySqlParameter("@userid",userid),
           };
            DataTable dt = sqlHelper.ExecuteQuery(sql, mySqls, CommandType.Text);
            foreach (DataRow item in dt.Rows)
            {
                result = item["chat_id"].ToString();
            }
            if (result == null)
            {
                result = "flase";
                LoggerHelper.Error("D层查询用户对应的群ID值：" + result);
                
            }

            return result;
        }

    }
}