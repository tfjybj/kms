/*
 * 创建人：武梓龙
 * 时间：2022年3月6日
 * 描述：发送审批的父类
 */

using KmsService.DAL;
using System.Configuration;

namespace KmsService.KeyBLL.SendApproveHandler
{

    public abstract class SendApproveHandlerBLL
    {
        protected SendApproveHandlerBLL successor;

        //查询日程表中的数据
        protected SelectCalendarInfoDAL selectCalendarInfo = new SelectCalendarInfoDAL();
        protected string processCode = ConfigurationManager.ConnectionStrings["process_code"].ConnectionString;
        protected string approver = ConfigurationManager.ConnectionStrings["approver"].ConnectionString;
        protected string postion = ConfigurationManager.ConnectionStrings["cc_position"].ConnectionString;
        protected string remark = ConfigurationManager.ConnectionStrings["remark"].ConnectionString;
        protected HttpHelper httpHelper = new HttpHelper();
        /// <summary>
        /// 调用子类
        /// </summary>
        /// <param name="successor">子类名</param>
        public void SetSuccessor(SendApproveHandlerBLL successor)
        {
            this.successor = successor;
        }
        /// <summary>
        /// 发送审批的抽象方法
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉ID</param>
        /// <param name="roomName">会议室</param>
        /// <param name="approveType">审批类型</param>
        /// <returns>空值</returns>
        public abstract string SendApproveBLL(string calendarID, string userID, string roomName,string approveType);
    }

}