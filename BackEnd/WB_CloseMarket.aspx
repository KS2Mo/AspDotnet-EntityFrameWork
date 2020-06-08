<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_MKT.Master" AutoEventWireup="true" CodeBehind="WB_CloseMarket.aspx.cs" Inherits="PGMKTStock.BackEnd.WB_CloseMarket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <link href="../App_Themes/jquery-ui-1.11.4.custom/jquery-ui.css" rel="stylesheet" />


    <link href="../App_Themes/bootswatch/bootstrap.css" rel="stylesheet" />
     <script>
         $(function () {
             $("#ContentPlaceHolder1_txtTradeDate").datepicker();
             $("#ContentPlaceHolder1_txtTradeDate2").datepicker();

             $("#ContentPlaceHolder1_txtTradeDate").change(function () {
                 $("#ContentPlaceHolder1_btnCheckDate").click();
             });

         });

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<h2>Close Marketing</h2>

<div class="row">
   <div class="col-md-8">
       <div class="bs-component">
            <div class="bs-component">
                  <div class="panel panel-primary">
     
                    <div class="panel-body">
                       <table style="width:auto">
                          <tr>
                            <td>วันที่เพิ่ม</td>
                            <td><asp:TextBox ID="txtTradeDate" class="form-control input-sm"  runat="server" ></asp:TextBox></td>
                             <td>วันถัดไป</td>
                            <td><asp:TextBox ID="txtTradeDate2" class="form-control input-sm"  runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button style='display:none;' ID="btnCheckDate"  class="btn btn-primary btn-sm"  runat="server" Text="Check Date" OnClick="btnCheckDate_Click" />
                                <asp:Button ID="btnSearch"  class="btn btn-primary btn-sm"  runat="server" Text="CLOSE MARKET" OnClick="btnShowReport_Click" /></td>
                          </tr>
                        </table> 
                    </div>
                  </div>
                </div>
        </div>  
    </div>
</div>


<div id="dialog" runat="server" title="เกิดข้อผิดพลาด" style="display: none">
    <p><asp:Label ID="lblErrorInPopup" runat="server" ></asp:Label></p>
</div>

</asp:Content>
