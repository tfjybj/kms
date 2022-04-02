
/*
 * 创建人：武梓龙
 * 时间：2022年3月6日
 * 描述：自动发送审批
 */

using KmsService.AuthInterface;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using KmsService.Entity;
namespace KmsService.KeyBLL.SendApproveHandler
{
    public class AutomaticSendApproveBLL : SendApproveHandlerBLL
    {
        /// <summary>
        /// 自动发送审批
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉ID</param>
        /// <param name="roomName">会议室</param>
        /// <param name="approveType">审批类型</param>
        /// <returns>空值</returns>
        public override string SendApproveBLL(string calendarID, string userID, string roomName, string approveType)
        {
            //调用钉钉接口获取单个日程详情
            SelectCalendar selectCalendar = new SelectCalendar();
            SelectCalendarModel calendarModel = selectCalendar.SelectCalendarInfo(userID, "primary", calendarID);

            BasicDataDAL basicDataDAL = new BasicDataDAL();
            BasicDataEntity basicDataEntity = basicDataDAL.SelectAllBasicData(roomName);

            //给发审批实体赋值
            SendApproveModel send = new SendApproveModel();
            Form_Component_Values fromName = new Form_Component_Values();
            Form_Component_Values startTime = new Form_Component_Values();
            Form_Component_Values endTime = new Form_Component_Values();
            Form_Component_Values useExplain = new Form_Component_Values();
            send.agent_id = 10358038;
            send.originator_user_id = userID;
            send.process_code = processCode;
            send.approvers = basicDataEntity.ApproverID;
            //send.cc_list = sendPeople;
            send.cc_position = postion;
            fromName.name = "会议室选择";
            fromName.value = roomName;
            List<Form_Component_Values> list = new List<Form_Component_Values>();
            list.Add(fromName);
            send.form_component_values = list;
            startTime.name = "开始时间";
            startTime.value = calendarModel.start.dateTime.ToString();
            list.Add(startTime);
            send.form_component_values = list;
            endTime.name = "结束时间";
            endTime.value = calendarModel.end.dateTime.ToString();
            list.Add(endTime);
            send.form_component_values = list;
            useExplain.name = "使用说明";
            useExplain.value = calendarModel.summary;
            list.Add(useExplain);
            send.form_component_values = list;
            //自动发送审批
            SendApproveExample sendApprove = new SendApproveExample();
            SendApproveRe_valueModel sendApproveRe_Value = sendApprove.SendApprove(send);
            //更新审批ID
            UpdateCalendar updateCalendar = new UpdateCalendar();
            updateCalendar.UpdateApproveID(calendarID, sendApproveRe_Value.process_instance_id);
            if (approveType == "false")
            {
                //通过userid获取手机号
                GetUnionID getUnion = new GetUnionID();
                GetUnionIDModel getUnionID = getUnion.GetDingDingUnionID(basicDataEntity.ApproverID);
                basicDataEntity = new BasicDataDAL().SelectAllBasicData(fromName.name);
                //通过手机号获取用户信息
                GetUserToken getUserToken = new GetUserToken();
                UserTokenModel userToken = getUserToken.GetToken(getUnionID.result.mobile);
                string ManagerName = userToken.data.name;
                string message = string.Format("系统已为您发送申请，等待管理员({0})通过之后即可领取钥匙,如若着急请电话联系！\n领取钥匙卡片会在管理员通过审批后会议开始前{1}分钟发送给你", ManagerName, basicDataEntity.BeforeTakeKey);
                string adminURL = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, message);
                httpHelper.HttpPost(adminURL);
                return null;
            }
            else
            {
                //获取审批实例详情
                ApproveContent approve = new ApproveContent();
                ApproveContentTask approveContent = approve.GetApproveContent(sendApproveRe_Value.process_instance_id);
                //给自动同意审批接口实体赋值
                AgreeRequestModel agreeModel = new AgreeRequestModel();
                Request requestModel = new Request();
                requestModel.process_instance_id = sendApproveRe_Value.process_instance_id;
                requestModel.remark = remark;
                requestModel.result = "agree";
                requestModel.actioner_userid = approveContent.process_instance.tasks[0].userid;
                requestModel.task_id = Convert.ToInt64(approveContent.process_instance.tasks[0].taskid);
                agreeModel.request = requestModel;
                agreeModel.request.process_instance_id = sendApproveRe_Value.process_instance_id;
                agreeModel.request.remark = remark;
                agreeModel.request.result = "agree";
                agreeModel.request.actioner_userid = approveContent.process_instance.tasks[0].userid;
                agreeModel.request.task_id = Convert.ToInt64(approveContent.process_instance.tasks[0].taskid);
                //调用自动同意审批接口
                AgreeRequest agreeRequest = new AgreeRequest();
                string agreeResult = agreeRequest.AgreeRequestInfo(agreeModel, requestModel);
                string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, "系统已为您发送申请，由于您的申请条件满足会议室申请人数和时间上的条件限制，KMS项目组已为您自动同意此审批，祝您使用愉快！");
                httpHelper.HttpPost(url);
                return null;
            }
        }
    }
}