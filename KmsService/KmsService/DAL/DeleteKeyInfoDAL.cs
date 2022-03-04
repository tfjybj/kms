using MySql.Data.MySqlClient;
using System.Data;

namespace KmsService.DAL
{
    public class DeleteKeyInfoDAL
    {
        public int DeleteKeyInfo(string keyName)
        {
            string sql = "delete from t_room_info where keyName=@keyName";
            MySqlParameter[] mySqlParameter = new MySqlParameter[] { new MySqlParameter("@keyName", keyName) };
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameter, CommandType.Text);
        }
    }
}