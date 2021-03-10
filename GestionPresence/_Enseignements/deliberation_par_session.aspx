<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="deliberation_par_session.aspx.cs" Inherits="BMDSysWeb._Enseignements.deliberation_par_session" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">

    <style type="text/css">
        .content_element fieldset {
                border-radius: 5px;
                -webkit-border-radius: 5px;
                -moz-border-radius: 5px;
                margin: 0px 0px 10px 0px;
                border: 1px solid #FFD2D2;
                padding: 10px;
                background-color: #bbcffa;
                box-shadow: inset 0px 0px 10px #FFE5E5;
                -moz-box-shadow: inset 0px 0px 10px #FFE5E5;
                -webkit-box-shadow: inset 0px 0px 10px #FFE5E5;
        }
        .content_element fieldset legend {
	                color: white;
                    border-top: 1px solid #FFD2D2;
                    border-left: 1px solid #FFD2D2;
                    border-right: 1px solid #FFD2D2;
                    border-radius: 5px 5px 0px 0px;
                    -webkit-border-radius: 5px 5px 0px 0px;
                    -moz-border-radius: 5px 5px 0px 0px;
                    background: #475566;
                    padding: 3px;
                    box-shadow: -0px -1px 2px #F1F1F1;
                    -moz-box-shadow: -0px -1px 2px #F1F1F1;
                    -webkit-box-shadow: -0px -1px 2px #F1F1F1;
                    font-weight: normal;
                    font-size: 12px;
            }
            
          
    </style>
    <div runat="server" id="MainDiv" style="width: 785px; height: auto; background-color: #ccdcee; margin: 50px auto; padding: 0px 10px 10px 10px; position: relative; border:double;border-color:#3399cc">
       <div style="width: 780px; height: 30px;margin:0px 0px 20px -10px; background-color: #3399cc; color:white;">
            <span style="display:inline-block; position:relative;margin:auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire de deliberation par session</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style=" display:inline-block; position:relative; float:right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
        <div class="content_element">
           
                    <div style="margin-left:25%">
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Annee_Combo" runat="server" CssClass="DropDownClass"  Width="350px" AutoPostBack="True" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="choisissez la A-A"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Faculte_Combo" runat="server" CssClass="DropDownClass"  Width="350px" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="choisissez la faculte"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Departement_Combo" runat="server" CssClass="DropDownClass"  Width="350px" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="-1" Text="choisissez le departement"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="ClasseCombo" runat="server" CssClass="DropDownClass"  Width="350px" AutoPostBack="True" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="choisissez la classe"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Session_ComboBo" runat="server" CssClass="DropDownClass"  Width="350px" AutoPostBack="True">
                                <asp:ListItem Value="-1" Text="choisissez la session"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Action_ComboBox" runat="server" CssClass="DropDownClass"  Width="350px" OnSelectedIndexChanged="Action_ComboBox_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="choisissez l'operation"></asp:ListItem>
                                <asp:ListItem Value="1">Clôturer la session</asp:ListItem>
                                <asp:ListItem Value="2">Modifier la date</asp:ListItem>
                                <asp:ListItem Value="3">Annuler et réinitialiser</asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:Label ID="Label5" runat="server" Text="Date :" Width="30%"></asp:Label>
                            <asp:TextBox ID="Date_TimePicker" runat="server" TextMode="Date"  Width="350px" AutoPostBack="True"></asp:TextBox>
                        </div>
                    </div>
             
                <div style="text-align:center;">
                    <asp:ImageButton ID="EnregistrerButton" runat="server" ToolTip="valider" ImageUrl="~/Images/validate_icon.png" OnClick="EnregistrerButton_Click" />
                </div>
            </div>
</asp:Content>
