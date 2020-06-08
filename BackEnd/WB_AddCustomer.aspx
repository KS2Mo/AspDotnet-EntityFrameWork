<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_MKT.Master" AutoEventWireup="true" CodeBehind="WB_AddCustomer.aspx.cs" Inherits="PGMKTStock.BackEnd.WB_AddCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link href="App_Themes/jquery-ui-1.11.4.custom/jquery-ui.css" rel="stylesheet" />
    <link href="App_Themes/bootswatch/bootstrap.css" rel="stylesheet" />
    <script>
      

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
            }).dialog();}
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2>เพิ่มข้อมูลลูกค้า</h2>

<div class="row">
   <div class="col-md-6">
       <div class="bs-component">
            <div class="bs-component">
                  <div class="panel panel-primary">
     
                    <div class="panel-body">
                       <table style="width:50%">
                          <tr>
                            <td>รหัสลูกค้า (streamming)</td>
                            <td><asp:TextBox ID="txtCustomerIdRef" class="form-control input-sm" runat="server"></asp:TextBox></td>
                            
                          </tr>
                          <tr>
                              <td>ชื่อ-นามสกุล</td>
                              <td><asp:TextBox ID="txtCustomerName" class="form-control input-sm" runat="server"></asp:TextBox></td>
                          </tr>
                           
                           <tr>
                               <td>พนักงานการตลาด</td>
                               <td><asp:DropDownList  class="form-control input-sm" ID="ddlEmployee" runat="server" ></asp:DropDownList></td>
                           </tr>
                           <tr>
                               <td>เปอร์เซ็นดอกเบี้ย</td>
                               <td><asp:DropDownList  class="form-control input-sm" ID="ddlInterest" runat="server" ></asp:DropDownList></td>
                           </tr>
                           <tr>
                               <td></td>
                               <td><asp:Button ID="btnSaveData" class="btn btn-primary btn-sm"  runat="server" Text="บันทึกข้อมูล" OnClick="btnSaveData_Click" /></td>
                           </tr>
                        </table> 
                    </div>
                  </div>
                </div>
        </div>  
    </div>
</div>

<div id="dialog" runat="server" title="เกิดข้อผิดพลาด" style="display: none">
    <p><asp:Label ID="lblErrorInPopup" runat="server" Text="Label"></asp:Label></p>
</div>

</asp:Content>
