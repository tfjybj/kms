using System;
using System.Web.UI.WebControls;
using System.Configuration;
using ServiceReference;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Web.UI;

public partial class MettingConfiguration : System.Web.UI.Page
{
    #region 实例化对象
    ServiceClient service = new ServiceClient();
    BasicDataEntity newBasicData = new BasicDataEntity();
    static BasicDataEntity oldBasicData = new BasicDataEntity();
    List<string> approverList = new List<string>();
    static bool btnState = false;    //静态变量，用于记录添加会议室按钮点击状态
    protected System.Web.UI.HtmlControls.HtmlInputHidden myHidden;
    List<string> lockNumbers = new List<string>();
    RoomInfoEntity roomInfo = new RoomInfoEntity();
    List<string> allLockNumber = new List<string>(); 
    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    public MettingConfiguration()
    {
        //获取所有锁号
        for (int i = 1; i <= 10; i++)
        {
            allLockNumber.Add("0" + i.ToString());
        }
        allLockNumber.Add("0a");
        allLockNumber.Add("0b");
        allLockNumber.Add("0c");
        allLockNumber.Add("0d");
        allLockNumber.Add("0e");
        allLockNumber.Add("0f");
    }


    /// <summary>
    /// 窗体加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面第一次加载时，绑定数据源
        if (!IsPostBack)
        {            
            bind();
        }
    }


    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //获取选中行
        GridView1.EditIndex = e.NewEditIndex;

        //调用后端方法获取审批人
        List<AllusersEntitiesItem> list = service.GetApprover();

        //获取t_BasicData表所有数据并放入到dataTable表中
        DataTable basicDataTable = service.SelectBasicData();

        //为dataTable表添加新行，名为ManagerName用于存放审批人
        basicDataTable.Columns.Add(new DataColumn() { ColumnName = "ManagerName" });

        //遍历审批人集合
        for (int i = 0; i < list.Count; i++)
        {
            basicDataTable.Rows[i]["ManagerName"] = list[i].name;
        }


        GridView1.EditIndex = e.NewEditIndex;
        //循环判断要赋值的内容是否为空
        for (int i = 1; i <= GridView1.Rows[e.NewEditIndex].Cells.Count - 3; i++)
        {
            //如果为空则给个默认值
            if (GridView1.Rows[e.NewEditIndex].Cells[i].Text == "&nbsp;")
            {
                GridView1.Rows[e.NewEditIndex].Cells[i].Text = 0.ToString();
            }
        }
        //获取选中行信息
        oldBasicData.RoomName = GridView1.Rows[e.NewEditIndex].Cells[1].Text;
        oldBasicData.MinUseNumber = Convert.ToInt32(GridView1.Rows[e.NewEditIndex].Cells[2].Text);
        oldBasicData.BeforeTakeKey = Convert.ToInt32(GridView1.Rows[e.NewEditIndex].Cells[3].Text);
        oldBasicData.AfterReturnKey = Convert.ToInt32(GridView1.Rows[e.NewEditIndex].Cells[4].Text);
        oldBasicData.UpperTime = Convert.ToInt32(GridView1.Rows[e.NewEditIndex].Cells[5].Text);
        oldBasicData.LowerTime = Convert.ToInt32(GridView1.Rows[e.NewEditIndex].Cells[6].Text);
        Label approver = (Label)(GridView1.Rows[e.NewEditIndex].FindControl("lblApprover"));
        oldBasicData.Approver = approver.Text;



        //GridView1.Rows[e.NewEditIndex].Cells[7].Text;
        //如果添加会议室点击按钮状态为true
        if (btnState)
        {
            //重新绑定数据源，并添加新的空白行
            InsertMetting();
        }
        else
        {
            //绑定数据源
            bind();
        }
    }


    /// <summary>
    /// 验证文本框内容是否为空
    /// </summary>
    /// <param name="values">要验证的文本</param>
    public void Check(string str)
    {
        char[] chr = new char[] { ' ', ',' };     //选择要切割的字符
        string[] resultt = str.Split(chr, StringSplitOptions.RemoveEmptyEntries);     //使用split关键字进行切割
        //判断切割的会议室信息是否大于7,否则抛出错误提示信息
        if (resultt.Length < 7)
        {
            throw new Exception("请将信息补充完整");
        }
    }


    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {           
            string basicData = "";

            //获取要修改的会议室信息
            for (int i = 1; i <= GridView1.Rows[e.RowIndex].Cells.Count - 4; i++)
            {
                basicData += ((TextBox)GridView1.Rows[e.RowIndex].Cells[i].Controls[0]).Text.ToString().Trim() + ",";
            }
            //获取选择的审批人并添加到字符串中
            basicData += ((DropDownList)this.GridView1.Rows[e.RowIndex].Cells[7].FindControl("ApproverDropDownList")).SelectedItem.Text;

            //信息是否填写完整
            Check(basicData);

            //将修改之后的配置信息赋值给实体对象
            newBasicData.RoomName = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            newBasicData.MinUseNumber = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim());
            newBasicData.BeforeTakeKey = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim());
            newBasicData.AfterReturnKey = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim());
            newBasicData.UpperTime = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim());
            newBasicData.LowerTime = Convert.ToInt32(((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim());
            newBasicData.Approver = ((DropDownList)this.GridView1.Rows[e.RowIndex].Cells[7].FindControl("ApproverDropDownList")).SelectedItem.Text;
            newBasicData.ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

            //更新会议室信息，并获取返回的按钮状态
            btnState = service.ModifyRoom(basicData, newBasicData, oldBasicData, allLockNumber);

            //弹出提示框提示更新成功
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('更新成功');{location.href='MettingConfiguration.aspx'}</script>");
        }
        catch (Exception ex)
        {
            //抛出错误语句
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('" + ex.Message + "');{location.href='MettingConfiguration.aspx'}</script>");

            //表格可编辑
            GridView1.EditIndex = -1;

            //绑定数据源
            bind();
        }
    }


    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //获取或设置要编辑的行的索引.默认值为-1，指示正在编辑任何行
        GridView1.EditIndex = -1;

        //绑定数据源
        bind();
    }


    /// <summary>
    /// 绑定数据源
    /// </summary>
    public void bind()
    {
        //调用后端方法绑定数据源
        GridView1.DataSource = service.SelectBasicData();

        //设置主键
        GridView1.DataKeyNames = new string[] { "id" };

        //绑定数据源
        GridView1.DataBind();
    }


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //删除会议室
        service.DeleteMetting(GridView1.DataKeys[e.RowIndex].Value.ToString());

        //绑定数据源
        bind();
    }


    /// <summary>
    /// 添加会议室-绑定数据源并添加新的空白行
    /// </summary>
    public void InsertMetting()
    {
        //实例化DataTable对象
        DataTable tr = new DataTable();
        //获取会议室信息
        tr = service.SelectBasicData();
        //添加新行
        DataRow dr = tr.NewRow();
        //把新行添加到DataTable中
        tr.Rows.Add(dr);
        //赋值数据源
        GridView1.DataSource = tr;
        //设置主键为id字段
        GridView1.DataKeyNames = new string[] { "id" };
        //绑定数据源
        GridView1.DataBind();
    }


    /// <summary>
    /// 添加会议室按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        //设置按钮初始状态为true
        btnState = true;
        //调用插入会议室方法
        InsertMetting();
    }


    /// <summary>
    /// 给审批人下拉框赋值
    /// </summary>
    /// <returns>会议室信息</returns>
    public DataTable ManagerNameBind()
    {
        //获取审批人
        List<AllusersEntitiesItem> list = service.GetApprover();
        //获取会议室信息
        DataTable basicDataTable = service.SelectBasicData();
        //为dataTable表添加新行，名为ManagerName用于存放审批人
        basicDataTable.Columns.Add(new DataColumn() { ColumnName = "ManagerName" });
        //遍历审批人集合
        for (int i = 0; i < list.Count; i++)
        {
            basicDataTable.Rows[i]["ManagerName"] = list[i].name;
        }        
        return basicDataTable;
    }

    /// <summary>
    /// 下拉框选择事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //获取审批人
        List<AllusersEntitiesItem> list = service.GetApprover();
        //获取会议室信息
        DataTable basicDataTable = service.SelectBasicData();
        //为dataTable表添加新行，名为ManagerName用于存放审批人
        basicDataTable.Columns.Add(new DataColumn() { ColumnName = "ManagerName" });
        //遍历审批人集合
        for (int i = 0; i < list.Count; i++)
        {
            basicDataTable.Rows[i]["ManagerName"] = list[i].name;
        }
        //把审批人集合赋值给下拉框
        this.GridView1.DataSource = list;
        //绑定数据源
        this.GridView1.DataBind();
    }
}




