<%@ Page Title="" Language="C#" MasterPageFile="~/Doyen/DoyenMasterPage.Master" AutoEventWireup="true" CodeBehind="horaire.aspx.cs" Inherits="GestionPresence.Doyen.horaire" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></cc1:toolkitscriptmanager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-4" style="margin-top:5px;"><h4>Entré</h4></div>

                <div class="col-4"  style="text-align:center;">
                    <asp:Label ID="Label_Success_Message" runat="server" Text="" ForeColor="Green"></asp:Label>
                    <asp:Label ID="Label_Error_Message" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-12 border-bottom my-3"></div>

            <div class="d-block position-relative">
                <div class="row">

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Année academique</div>
                        </div>
                        <asp:DropDownList ID="Annee_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Faculté</div>
                        </div>
                        <asp:DropDownList ID="Faculte_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Département</div>
                        </div>
                        <asp:DropDownList ID="Departement_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Classe</div>
                        </div>
                        <asp:DropDownList ID="Classe_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Classe_ComboBox_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="row">
                    <div class="col-4"></div>
                    <div class="col-4">AM</div>
                    <div class="col-4" style="text-align:center">PM</div>
                </div>

                <div class="row">

                    <div class="col-2">
                        <div style="margin-top:20px;">
                            <asp:Label ID="Lundi" runat="server" Text="Lundi"></asp:Label><br />
                            <strong><asp:Label ID="Lundi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br />
                        </div>
                        
                        <div style="margin-top:35px;">
                             <asp:Label ID="Mardi"  runat="server" Text="Mardi"></asp:Label><br />
                            <strong><asp:Label ID="Mardi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br />
                        </div>

                        <div style="margin-top:35px;">
                            <asp:Label ID="Mercredi" runat="server" Text="Mercredi"></asp:Label><br />
                            <strong><asp:Label ID="Mercredi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br />
                        </div>
                        
                        <div style="margin-top:35px;">
                            <asp:Label ID="Jeudi" runat="server" Text="Jeudi"></asp:Label><br />
                            <strong><asp:Label ID="Jeudi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br />
                        </div>
                        
                        <div style="margin-top:35px;">
                            <asp:Label ID="Vendredi" runat="server" Text="Vendredi"></asp:Label><br />
                            <strong><asp:Label ID="Vendredi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br />
                        </div>
                        
                        <div style="margin-top:35px;">
                            <asp:Label ID="Samedi" runat="server" Text="Samedi"></asp:Label><br />
                            <strong><asp:Label ID="Samedi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br />
                        </div>
                        
                        <div style="margin-top:35px;">
                            <asp:Label ID="Dimanche" runat="server" Text="Dimanche"></asp:Label><br />
                            <strong><asp:Label ID="Dimanche_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong>
                        </div>
                    </div>
                    <div class="col-5" style="">
                            <asp:Panel ID="Horaire_Panel_AM" runat="server"></asp:Panel>
                    </div>
                    <div class="col-5" style="">
                            <asp:Panel ID="Horaire_Panel_PM" runat="server"></asp:Panel>
                    </div>
                    <div class="col-3"></div>
                    <div class="col-9">
                        <asp:Button ID="Preview_Button" runat="server" Text="<< Semaine précédente>>" ToolTip="semaine precedent" Width="200px" Height="35px" Style="padding: 1px" OnClick="Preview_Button_Click" Font-Size="Medium" />
                        <asp:Button ID="Inition" runat="server" Text="<< Semaine encours >>" ToolTip="Semaine encours" Width="200px" Height="35px" Style="padding: 1px; margin-left:20px" OnClick="Inition_Click" Font-Size="Medium" />
                        <asp:Button ID="Next_Button" runat="server" Text="<<Semaine suivante >>" ToolTip="Semaine suivante" Width="200px" Height="35px" Style="padding: 1px; margin-left:20px" OnClick="Next_Button_Click" Font-Size="Medium" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

<%------------------------------horaire cours popup---------------------------------%>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        
            <asp:Label ID="llt" runat="server" Text=""></asp:Label>
        
            <cc1:ModalPopupExtender ID="hc" PopupControlID="Panel1" TargetControlID="llt" CancelControlID="exit" runat="server" PopupDragHandleControlID="headerdiv"></cc1:ModalPopupExtender>
            
            <div id="Panel1" class="">
                <center>
                <asp:Panel ID="Pa1" runat="server" CssClass="modalPopup" >
                      <div id="headerdiv" class="header" >
                          <asp:Label ID="jours_tt" runat="server" Text=""></asp:Label>
                      </div>
                      <div id="divdetails" class="details">
                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="Operation_ComboBox" runat="server" class="input__field input__field--hoshi" Style="width: 380px; color: #000008" AutoPostBack="True" OnSelectedIndexChanged="Operation_ComboBox_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Supprimer un cours sur Horaire"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Programmer un cours pour Enseignement"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Programmer un cours pour Evaluation 1er Session"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Programmer un cours pour Evaluation 2 eme Session"></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Operation_ComboBox">
                                    <span class="input__label-content input__label-content--hoshi">Opération </span>
                                </label>
                            </div>

                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="cours_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 470px; color: #000008" OnSelectedIndexChanged="cours_Combo_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="cours_Combo">
                                    <span class="input__label-content input__label-content--hoshi">Cours </span>
                                </label>
                            </div>

                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="salle_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 470px; color: #000008" OnSelectedIndexChanged="salle_Combo_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="salle_Combo">
                                    <span class="input__label-content input__label-content--hoshi">Salle </span>
                                </label>
                            </div>

                      </div>
                      <div id="footerdiv" class="footer" >
                          <asp:Button id="exit" runat="server" Text="Quiter" OnClick="exit_Click"/>
                          <asp:Button ID="Enregistre_Btn" runat="server" Text="Enregistrer" OnClick="Enregistre_Btn_Click" />
                      </div>
                </asp:Panel>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

            <%-----------------------------END of horaire cours popup---------------------------------%>

</asp:Content>
