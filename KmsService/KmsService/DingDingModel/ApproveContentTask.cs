using System.Collections.Generic;

namespace KmsService.DingDingModel
{
    /// <summary>
    /// 审批实例返回值接收实体
    /// </summary>
    public class ApproveContentTask
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 返回码描述
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 审批实例详情
        /// </summary>
        public Process_Instance process_instance { get; set; }

        /// <summary>
        /// 请求ID
        /// </summary>
        public string request_id { get; set; }

        /// <summary>
        /// 审批实例详情类
        /// </summary>
        public class Process_Instance
        {
            /// <summary>
            /// 审批人userid
            /// </summary>
            public string[] approver_userids { get; set; }

            /// <summary>
            /// 审批附属实例列表，当已经通过的审批实例被修改或撤销，会生成一个新的实例，作为原有审批实例的附属。
            ///如果想知道当前已经通过的审批实例的状态，可以依次遍历它的附属列表，查询里面每个实例的biz_action。
            /// </summary>
            public object[] attached_process_instance_ids { get; set; }

            /// <summary>
            ///审批实例业务动作：
            ///MODIFY：表示该审批实例是基于原来的实例修改而来
            ///REVOKE：表示该审批实例是由原来的实例撤销后重新发起的
            ///NONE表示正常发起
            /// </summary>
            public string biz_action { get; set; }

            /// <summary>
            /// 审批实例业务编号
            /// </summary>
            public string business_id { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string create_time { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public string finish_time { get; set; }

            /// <summary>
            /// 表单详情列表
            /// </summary>
            public List<Form1_Component_Values> form_component_values { get; set; }

            /// <summary>
            /// 操作记录列表
            /// </summary>
            public List<Operation_Records> operation_records { get; set; }

            /// <summary>
            /// 发起人的部门。-1表示根部门。
            /// </summary>
            public string originator_dept_id { get; set; }

            /// <summary>
            /// 发起人的部门名称
            /// </summary>
            public string originator_dept_name { get; set; }

            /// <summary>
            /// 发起人的userid。
            /// </summary>
            public string originator_userid { get; set; }

            /// <summary>
            /// 审批结果：
            ///
            ///
            ///
            /// ：同意
            ///refuse：拒绝
            /// </summary>
            public string result { get; set; }

            /// <summary>
            ///审批状态：
            ///NEW：新创建
            ///RUNNING：审批中
            ///TERMINATED：被终止
            ///COMPLETED：完成
            ///CANCELED：取消
            /// </summary>
            public string status { get; set; }

            /// <summary>
            /// 任务列表
            /// </summary>
            public List<Task> tasks { get; set; }

            /// <summary>
            /// 审批实例标题
            /// </summary>
            public string title { get; set; }
        }

        /// <summary>
        /// 表单详情列表类
        /// </summary>
        public class Form1_Component_Values
        {
            /// <summary>
            /// 组件类型
            /// </summary>
            public string component_type { get; set; }

            /// <summary>
            /// 组件ID
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// 标签名
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 标签值
            /// </summary>
            public string value { get; set; }

            /// <summary>
            /// 标签扩展值
            /// </summary>
            public string ext_value { get; set; }
        }

        /// <summary>
        /// 操作记录列表类
        /// </summary>
        public class Operation_Records
        {
            /// <summary>
            /// 操作时间
            /// </summary>
            public string date { get; set; }

            /// <summary>
            /// 操作结果：
            /// AGREE：同意
            ///REFUSE：拒绝
            ///NONE
            /// </summary>
            public string operation_result { get; set; }

            /// <summary>
            /// 操作类型：
            ///EXECUTE_TASK_NORMAL：正常执行任务
            ///EXECUTE_TASK_AGENT：代理人执行任务
            ///APPEND_TASK_BEFORE：前加签任务
            ///APPEND_TASK_AFTER：后加签任务
            ///REDIRECT_TASK：转交任务
            ///START_PROCESS_INSTANCE：发起流程实例
            ///TERMINATE_PROCESS_INSTANCE：终止(撤销)流程实例
            ///FINISH_PROCESS_INSTANCE：结束流程实例
            ///ADD_REMARK：添加评论
            ///REDIRECT_PROCESS：审批退回
            ///PROCESS_CC：抄送
            /// </summary>
            public string operation_type { get; set; }

            /// <summary>
            /// 操作人userid
            /// </summary>
            public string userid { get; set; }
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        public class Task
        {
            /// <summary>
            ///
            /// </summary>
            public string activity_id { get; set; }

            public string create_time { get; set; }
            public string finish_time { get; set; }
            public string task_result { get; set; }

            /// <summary>
            ///任务状态：
            ///NEW：未启动
            ///RUNNING：处理中
            ///PAUSED：暂停
            ///CANCELED：取消
            ///COMPLETED：完成
            //TERMINATED：终止
            /// </summary>
            public string task_status { get; set; }

            /// <summary>
            /// 任务节点ID
            /// </summary>
            public string taskid { get; set; }

            /// <summary>
            /// 任务URL
            /// </summary>
            public string url { get; set; }

            /// <summary>
            /// 任务处理人
            /// </summary>
            public string userid { get; set; }
        }
    }


}