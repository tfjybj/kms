namespace KmsService.DingDingModel
{
    public class GetUnionIDModel
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public Result result { get; set; }
        public string request_id { get; set; }
    }

    public class Result
    {
        public bool active { get; set; }
        public bool admin { get; set; }
        public string avatar { get; set; }
        public bool boss { get; set; }
        public int[] dept_id_list { get; set; }
        public Dept_Order_List[] dept_order_list { get; set; }
        public string email { get; set; }
        public bool exclusive_account { get; set; }
        public string extension { get; set; }
        public bool hide_mobile { get; set; }
        public string job_number { get; set; }
        public Leader_In_Dept[] leader_in_dept { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public bool real_authed { get; set; }
        public string remark { get; set; }
        public bool senior { get; set; }
        public string state_code { get; set; }
        public string telephone { get; set; }
        public string title { get; set; }
        public Union_Emp_Ext union_emp_ext { get; set; }
        public string unionid { get; set; }
        public string userid { get; set; }
        public string work_place { get; set; }
    }

    public class Union_Emp_Ext
    {
    }

    public class Dept_Order_List
    {
        public int dept_id { get; set; }
        public long order { get; set; }
    }

    public class Leader_In_Dept
    {
        public int dept_id { get; set; }
        public bool leader { get; set; }
    }
}