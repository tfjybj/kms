<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonalDataReport.aspx.cs" Inherits="PersonalDataReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script src="../js/jquery.js" type="text/javascript"></script>
     <%--  <script src="../js/function.js" rel="stylesheet"></script>--%>
    <link href="css/style.css" rel="stylesheet" />

</head>

    
<body style="height: 940px; width: 713px; margin-right: 0px;">


    <form id="form1" runat="server"> 
<%--<script type="text/javascript">
    function ChangeText() {
        var month = document.getElementById("月");
        var week = document.getElementById("周");
        if (month) {
            month.click();
            week.innerHTML = "周";
        }
        else {
            week.click();
            month.innerHTML = "月";

        }
    }
</script>--%>
          
        <div   style="height:35px; width :490px">
            <%--<select align="right"  style="height:35px;width :200px" name="D1">
                <option selected hidden disabled value ="">推送周期</option>
                <option value="SevenDays">7天</option>
                <option value="FourteenDays">14天</option>
                <option value="OneMonth">30天</option>
            </select>--%>

            <label align="left" >点击右下侧按钮选择是周推送或月推送哦！</label>&nbsp;
        <%--    滑动按钮--%>
           <%-- <asp:button ID="Button1" runat="server" Text="周/月" align="right" OnClick="Button1_Click" type="button" OnClientClick="return false"/>--%>


 <asp:ScriptManager ID="ScriptManager1" runat="server" >
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" align="center">
<ContentTemplate>
<%--<asp:TextBox ID="txt1" runat="server" Width="150px" Height="25px"></asp:TextBox>--%>
<asp:Button runat="server" Text="周/月"   onclick="ChangeState_Click"/>
</ContentTemplate>
</asp:UpdatePanel>
             
            <%--<input type="checkbox" name="checkBox" class="checke" value="7" runat="server" align="right" id="btn" onclick="getValue()"/>--%> 
            
 <%--           <button type="button" align="right" click="Button1_Click" runat="server" />
            --%>
        </div>

        <div style="height: 211px; width: 489px" >
            <br />
            <asp:Label ID="Label1" runat="server" Text="Hi！"></asp:Label>
             <br />
             <br />
            <asp:Label ID="Label2" runat="server" Text="这一周里"></asp:Label>
             <br />
             <br />
            <asp:Label ID="Label3" runat="server" Text="你一共申请了"></asp:Label>
             <br />
             <br />
            <asp:Label ID="txtRoomUsage" runat="server" Font-Italic="True" Font-Size="X-Large" ForeColor="#6699FF"></asp:Label>
            <asp:Label ID="Label12" runat="server" Text="次会议室" Font-Italic="True" Font-Size="X-Large" ForeColor="#6699FF"></asp:Label>
        </div>

        <div align="left" style="height: 153px; width: 488px">
            <asp:Label ID="Label4" runat="server" Text="高贵的人" Font-Italic="True" Font-Size="X-Large" ForeColor="#FF3399"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="你的组织能力不错哦~继续努力" Font-Italic="True" Font-Size="X-Large" ForeColor="#FF3399"></asp:Label>
        </div >
        
        <div align="left" style="height: 266px; width: 487px; margin-right: 0px;">
            <br />
            <asp:Label ID="Label6" runat="server" Text="你申请最多的时间段是      " Font-Italic="True"></asp:Label>
           <br />
            <br />
            <br />
            <asp:Label ID="MaxTimeSlot" runat="server" Font-Italic="True" Font-Size="X-Large" ForeColor="#6699FF"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="前夜的辗转反侧" Font-Italic="True" Font-Size="X-Large" ForeColor="#FF3399"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="你终于在第二天早上得到才华施展" Font-Italic="True" Font-Size="X-Large" ForeColor="#FF3399"></asp:Label>
            </div>

        <div style="height: 280px; width: 485px">
            <br />
            <asp:Label ID="Room" runat="server" Font-Italic="True" Font-Size="X-Large" ForeColor="#6699FF"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label9" runat="server" Text="被你宠幸最多的会议室"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label10" runat="server" Text="相信" Font-Italic="True" Font-Size="X-Large" ForeColor="#FF3399"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label11" runat="server" Text="它对你一定很特别" Font-Italic="True" Font-Size="X-Large" ForeColor="#FF3399"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </form>
    <p>
        &nbsp;</p>
    </body>
</html>
