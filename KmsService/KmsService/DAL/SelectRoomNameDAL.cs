/*
 * 创建人：武梓龙
 * 创建时间：2021年12月21日16点26分
 * 描述：查询可使用的会议室
 */

using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace KmsService.DAL
{
    /// <summary>
    /// 会议室名字操作类
    /// </summary>
    public class SelectRoomNameDAL
    {
        /// <summary>
        /// 查询参会人数大于十人小于二十人的会议室
        /// </summary>
        /// <returns>返回会议室名称集合</returns>
        public List<string> SelectRoomNameTen(string frontMinUseNumber, string endMinUseNumber)
        {
            string sql = "SELECT room_name FROM t_room where min_use_number>=@frontMinUseNumber and min_use_number<@endMinUseNumber and lock_state LIKE @lockState";
            MySqlParameter[] mysql = new MySqlParameter[]
            {
                new MySqlParameter("@lockState","%isLock"),
                new MySqlParameter("@frontMinUseNumber",frontMinUseNumber),
                new MySqlParameter("@endMinUseNumber",endMinUseNumber)
            };
            SQLHelper sqlhelper = new SQLHelper();
            DataTable dataTable = sqlhelper.ExecuteQuery(sql, mysql, CommandType.Text);
            List<string> list = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(row["room_name"].ToString());
            }
            return list;
        }

        /// <summary>
        /// 查询参会人数大于等于二十人小于四十人的会议室
        /// </summary>
        /// <returns>返回会议室名称集合</returns>
        public List<string> SelectRoomNameTwenty(string frontMinUseNumber, string endMinUseNumber)
        {
            string sql = "select room_name from t_room where min_use_number >=@frontMinUseNumber and min_use_number<@endMinUseNumber and lock_state LIKE @lockState";
            MySqlParameter[] mysql = new MySqlParameter[]
            {
                new MySqlParameter("@lockState","%isLock"),
                new MySqlParameter("@frontMinUseNumber",frontMinUseNumber),
                new MySqlParameter("@endMinUseNumber",endMinUseNumber)
            };
            SQLHelper sqlhelper = new SQLHelper();
            DataTable dataTable = sqlhelper.ExecuteQuery(sql, mysql, CommandType.Text);
            List<string> list = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(row["room_name"].ToString());
            }
            return list;
        }

        /// <summary>
        /// 查询参会人数大于等于四十人的会议室
        /// </summary>
        /// <returns>返回会议室名称集合</returns>
        public List<string> SelectRoomNameFourty(string minUseNumber)
        {
            string sql = "select room_name from t_room where min_use_number >=@minUseNumber and lock_state LIKE @lockState";
            MySqlParameter[] mysql = new MySqlParameter[]
            {
                new MySqlParameter("@lockState","%isLock"),
                new MySqlParameter("@minUseNumber",minUseNumber)
            };
            SQLHelper sqlhelper = new SQLHelper();
            DataTable dataTable = sqlhelper.ExecuteQuery(sql, mysql, CommandType.Text);
            List<string> list = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(row["room_name"].ToString());
            }
            return list;
        }
    }
}