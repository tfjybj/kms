/*
 * 创建人：盖鹏军
 * 创建时间：2022年1月24日14点48分
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmsService.Entity
{
    /// <summary>
    /// 管理员领取钥匙实体
    /// </summary>
    public class ManagerRecordEntity
    {

        public ManagerRecordEntity()
        { }


        public ManagerRecordEntity(string id,string manager_name,string key_name,string user_id,DateTime create_time,DateTime update_time,string get_time,string is_return_key,string is_cancel,string get_out_track_id,string return_out_track_id)
        {
            this.id = id;
            this.create_time = create_time;
            this.manager_name = manager_name;
            this.key_name = key_name;
            this.user_id = user_id;
            this.get_time = get_time;
            this.is_return_key = is_return_key;
            this.is_cancel = is_cancel;
            this.update_time = update_time;
            this.get_out_track_id = get_out_track_id;
            this.return_out_track_id=return_out_track_id;
        }

        /// <summary>
        /// 自增ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string manager_name { get; set; }
        /// <summary>
        /// 领取钥匙名称
        /// </summary>
        public string key_name { get; set; }
        /// <summary>
        /// 用户钉钉ID
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_time { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public string get_time { get; set; }
        /// <summary>
        /// 钥匙是否归还
        /// </summary>
        public string is_return_key { get; set; }
        /// <summary>
        /// 卡片是否作废
        /// </summary>
        public string is_cancel { get; set; }

        /// <summary>
        /// 领取钥匙卡片消息ID
        /// </summary>
        public string get_out_track_id { get; set; }

        /// <summary>
        /// 归还钥匙卡片消息ID
        /// </summary>
        public string return_out_track_id { get; set; }


    }
}