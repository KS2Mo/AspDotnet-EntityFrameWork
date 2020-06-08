<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_MKT.Master" AutoEventWireup="true" CodeBehind="WB_PreOrderReport.aspx.cs" Inherits="PGMKTStock.BackEnd.WB_PreOrderReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link href="../App_Themes/jquery-ui-1.11.4.custom/jquery-ui.css" rel="stylesheet" />
    <link href="../App_Themes/bootswatch/bootstrap.css" rel="stylesheet" />
     <script>
         $(function () {
             $("#ContentPlaceHolder1_txtTradeDate").datepicker();
             function ShowDialog() {
                 $(function () {
                     $('#ContentPlaceHolder1_dialog').dialog({
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>ข้อมูลการซื้อขายของลูกค้า</h2>

<div class="well">
    <div class="row">
       <div class="col-md-1">วันที่</div>
       <div class="col-md-2"><asp:TextBox ID="txtTradeDate" onkeypress="checknumber()"  class="form-control input-sm" title="กรุณาเลือกวันที่ที่มีการซื้อ-ขาย" runat="server"></asp:TextBox></div>
       <div class="col-md-1">ชื่อลูกค้า</div>
       <div class="col-md-2"><asp:DropDownList  class="form-control input-sm" ID="ddlCustomer" runat="server"></asp:DropDownList></div>
       <div class="col-md-1"><asp:Button ID="btnTrade" class="btn btn-primary  btn-sm" runat="server" Text="ค้นหา" OnClick="btnTrade_Click" /></div>
    </div>
</div>

<div class="row">

        <div class="col-md-12">
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
                        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            <Report FileName="CRDailyReportCustomerForheader.rpt"></Report>
                        </CR:CrystalReportSource>
         </div>

</div>           
         
            
      <%--  </CR:CrystalReportSource>--%>
<div id="dialog" runat="server" title="เกิดข้อผิดพลาด" style="display: none">
    <p><asp:Label ID="lblErrorInPopup" runat="server" ></asp:Label></p>
</div>


</asp:Content>
