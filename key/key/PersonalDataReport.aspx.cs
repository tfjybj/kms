using System;
using ServiceReference;
using Less.Html;
public partial class PersonalDataReport : System.Web.UI.Page
{
    string dingDingID;
    ServiceClient service = new ServiceClient();
    //string state ="0";
    public  void Page_Load(object sender, EventArgs e)
    {
        //定时任务.PersonReportBAL personReportBLL = new PersonReportBAL();
         dingDingID = Request.QueryString.ToString();//截取钉钉id

        // dingDingID = "2669160061179688068";
        txtRoomUsage.Text = Convert.ToString(service.WeekUseCount(dingDingID));//开会的次数

        MaxTimeSlot.Text = Convert.ToString(service.WeekUseTime(dingDingID));//最多的时间段
        
        Room.Text  = service.WeekRoomName(dingDingID);//使用次数最多的教室

    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        string state = service.UserPushState(dingDingID);
        
        if (state == "0")
        {
            state = "1";
            service.ModifyState(dingDingID, state);
            //消息提示信息

            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('已成功为您修改为月推送！');{location.href='PersonalDataReport.aspx'}</script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "alert('已成功为您修改为月推送！')", true);
            System.Web.UI.ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "unReport", "alert(' 已成功为您修改为月推送！ ');", true);


           // txt1.Text = "yue";
            
        }
        else if (state=="1")
        {
            state = "0";
            service.ModifyState(dingDingID, state);
            //消息提示
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('已成功为您修改为周推送！');{location.href='PersonalDataReport.aspx'}</script>");
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "success", "alert('已成功为您修改为周推送！')", true);
            // txt1.Text = "zhou";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "unReport", "alert(' 已成功为您修改为周推送！！ ');", true);
        }
        
        
    }
}