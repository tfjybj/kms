<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MettingConfiguration.aspx.cs"
    Inherits="MettingConfiguration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会议室配置</title>
</head>


<body>
    <form id="form1" runat="server" style="text-align: center">
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333"
            OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDeleting="GridView1_RowDeleting"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <FooterStyle BackColor="#0050b3" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" />
                <asp:BoundField DataField="room_name" HeaderText="会议室名称" />
                <asp:BoundField DataField="min_use_number" HeaderText="至少使用人数" />
                <asp:BoundField DataField="before_take_key" HeaderText="会议开始前*分钟取钥匙" />
                <asp:BoundField DataField="after_return_key" HeaderText="会议结束前*分钟还钥匙" />
                <asp:BoundField DataField="upper_time" HeaderText="时间上限(分钟)" />
                <asp:BoundField DataField="lower_time" HeaderText="时间下限(分钟)" />
                <%--<asp:CommandField ButtonType="Button" HeaderText="审批人" />--%>
                <%--DataSource='<%# Ddlbind()%>'--%>
                <asp:TemplateField HeaderText="审批人">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ApproverDropDownList" runat="server" DataSource='<%#ManagerNameBind()%>' DataValueField="ManagerName" DataTextField="ManagerName"  Width="100px">
                             <%--DataValueField="approver" DataTextField="approver"  Width="100px">--%>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblApprover" runat="server" Text='<%#Bind("approver")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="编辑"
                    ShowEditButton="True" ButtonType="Button">
                    <ControlStyle BackColor="#bae7ff" />
                </asp:CommandField>
                <asp:CommandField HeaderText="删除"
                    ShowDeleteButton="True"
                    ButtonType="Button"
                    AccessibleHeaderText="&lt;div id=&quot;de&quot; onclick=&quot;JavaScript:return confirm('确定删除吗？')&quot;&gt;删除&lt;/div&gt;">
                    <ControlStyle BackColor="#bae7ff" ForeColor="red"/>
                </asp:CommandField>

            </Columns>
            <RowStyle ForeColor="#141414" />
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True"
                ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br />
        <br />

        <asp:Button ID="BtnAdd" runat="server"
            Text="添加会议室" Width="150px" Height="50px"
            BackColor="#bae7ff" OnClick="BtnAdd_Click" />
    </form>
</body>
</html>
