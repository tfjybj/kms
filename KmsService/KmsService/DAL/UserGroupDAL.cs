using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    public class UserGroupDAL
    {
        #region 已不使用

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
            SQLHelper helper = new SQLHelper();
            DataTable dt = helper.ExecuteQuery(sql, mySqls, CommandType.Text);
            foreach (DataRow item in dt.Rows)
            {
                result = item["chat_id"].ToString();
            }
            if (result == null)
            {
                //LoggerHelper.Error("D层查询用户对应的群ID值：" + result);
                result = "flase";
            }
            return result;
        }

        #endregion 已不使用
    }
}