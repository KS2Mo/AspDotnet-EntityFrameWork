<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WB_Report.aspx.cs" Inherits="PGMKTStock.BackEnd.WB_Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<head runat="server">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link href="../App_Themes/jquery-ui-1.11.4.custom/jquery-ui.css" rel="stylesheet" />


    <link href="../App_Themes/bootswatch/bootstrap.css" rel="stylesheet" />
     <script>
         $(function () {
             $("#txtTradeDate").datepicker();
             function ShowDialog() {
                 $(function () {
                     $('#dialog').dialog({
                         modal: true,
                         width: 400,
                         draggable: true,
                         buttons: {
                             'OK': function () { $(this).dialog('close'); }
                         }
                     })
                 }).dialog();

             }
         });

         function checknumber(sText) {
             key = event.keyCode
             event.returnValue = false;

         }
    </script>
</head>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>วันที่</td>
                <td><asp:TextBox ID="txtTradeDate" onkeypress="checknumber()"  class="form-control input-sm" title="กรุณาเลือกวันที่ที่มีการซื้อ-ขาย" runat="server"></asp:TextBox></td>
                <td>ชื่อลูกค้า</td>
                <td><asp:DropDownList  class="form-control input-sm" ID="ddlCustomer" runat="server"></asp:DropDownList></td>
                <td><asp:Button ID="btnTrade" class="btn btn-default  btn-sm" runat="server" Text="ค้นหา" OnClick="btnTrade_Click" /></td>
            </tr>
            <tr>
                <td colspan="5"><br /></td>
            </tr>
            <tr>
                <td colspan="5">
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            <Report FileName="CRDailyReportCustomerForheader.rpt"></Report>
                        </CR:CrystalReportSource>
                </td>
            </tr>
        </table>
       


        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="CrystalReport.rpt">
            </Report>
        </CR:CrystalReportSource>--%>
        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  ToolPanelView="None" DisplayPage="False" DisplayToolbar="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ReportSourceID="CrystalReportSource1" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="CRDailyReportCustomerForheader.rpt">
            </Report>--%>
           
         
            
      <%--  </CR:CrystalReportSource>--%>
<div id="dialog" runat="server" title="เกิดข้อผิดพลาด" style="display: none">
    <p><asp:Label ID="lblErrorInPopup" runat="server" ></asp:Label></p>
</div>

    </div>
    </form>
</body>
</html>
