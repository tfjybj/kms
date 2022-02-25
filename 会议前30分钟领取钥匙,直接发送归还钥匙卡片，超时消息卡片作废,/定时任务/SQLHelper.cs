using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Configuration;
using System.Data;

namespace 定时任务
{
    public class SQLHelper
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader sdr;
        public SQLHelper()
        {
            //配置文件连接数据库
            string connstr = "server=192.168.60.54;database=kms_dev;uid=kms;pwd=kms;Charset=utf8";
            conn = new MySqlConnection(connstr);
        }
        private MySqlConnection GetConn()
        {
            //打开数据库连接
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn;
        }
        /// <summary>
        /// 该方法执行不带参数的的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">SQL语句或存储过程</param>
        /// <param name="type">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, CommandType type)
        {
            int res;
            try
            {
                cmd = new MySqlCommand(cmdText, GetConn());
                cmd.CommandType = type;
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return res;
        }

        /// <summary>
        ///  该方法执行带参数的的增删改SQL语句或存储过程
        /// </summary>
        /// <param name="cmdText">SQL语句或存储过程</param>
        /// <param name="paras">参数集合</param>
        /// <param name="type">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, MySqlParameter[] paras, CommandType type)
        {
            int res;
            using (cmd = new MySqlCommand(cmdText, GetConn()))
            {
                cmd.CommandType = type;
                cmd.Parameters.AddRange(paras);
                res = cmd.ExecuteNonQuery();
            }
            return res;
        }

        /// <summary>
        /// 该方法执行不带参数的的查询语句或存储过程
        /// </summary>
        /// <param name="cmdText">SQL语句或存储过程</param>
        /// <param name="type">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string cmdText, CommandType type)
        {
            DataTable dataTable = new DataTable();
            cmd = new MySqlCommand(cmdText, GetConn());
            cmd.CommandType = type;
            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dataTable.Load(sdr);
            }
            return dataTable;
        }
          
        /// <summary>
        ///  该方法执行带参数的的查询语句或存储过程
        /// </summary>
        /// <param name="cmdText">SQL语句或存储过程</param>
        /// <param name="paras">参数集合</param>
        /// <param name="type">命令类型</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string cmdText, MySqlParameter[] paras, CommandType type)
        {
            DataTable dataTable = new DataTable();
            cmd = new MySqlCommand(cmdText, GetConn());
            cmd.CommandType = type;
            cmd.Parameters.AddRange(paras);

            using (sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                dataTable.Load(sdr);
            }
            return dataTable;
        }

    }
}