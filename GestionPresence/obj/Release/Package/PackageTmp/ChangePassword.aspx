<%@ Page Title="" Language="C#" MasterPageFile="~/ChangePasswordMasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="GestionPresence.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="d-block position-relative" style="margin-top:25px">
        <div class="row" style="margin-top:30px">
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">Login</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="txt_login" placeholder="login"/>
                    </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">New Password</div>
                        </div>
                        <input runat="server" type="password" autocomplete="off" class="form-control py-0" id="txt_new_password" />
                    </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; "> Passord</div>
                        </div>
                        <input runat="server" type="password" autocomplete="off" class="form-control py-0" id="txt_password" />
                    </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">Confirmation</div>
                        </div>
                        <input runat="server" type="password" autocomplete="off" class="form-control py-0" id="txt_confirmation" />
                    </div>
        </div>

         <div class="row">
                    <div class="col" style="text-align:center; margin-top:25px">
                        <asp:Button ID="modifier_btn" class="btn btn-success col-4 btn-lg " runat="server" Text="Modifier" ToolTip="Cliquez ici modifier le mot de passe" OnClick="modifier_btn_Click"/>
                    </div>
                </div>
     </div>
</asp:Content>
