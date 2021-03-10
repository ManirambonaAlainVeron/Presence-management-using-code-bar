<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="pointage_statistique.aspx.cs" Inherits="BMDSysWeb._Enseignements.pointage_statistique" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <div runat="server" id="MainDiv" style="width: 700px; height: auto; background-color: #ccdcee; margin: 100px auto; padding: 0px 10px 10px 10px; position: relative; border: double; border-color: #3399cc">
        <div style="width: 695px; height: 30px; margin: 0px 0px 10px -10px; background-color: #3399cc; color: white;">
            <span style="display: inline-block; position: relative; margin: auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire de gestion des états d’avancement par cours</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style="display: inline-block; position: relative; float: right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
                    <div style="margin-left:30%">
                        <div class="input input--hoshi">
                            <label>Generer par :</label>
                            <asp:DropDownList ID="DropDown_type_recherche"   Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDown_type_recherche_SelectedIndexChanged"></asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Annee_Combo" runat="server"  Width="250px" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged" AutoPostBack="True">
                                     <asp:ListItem Value="-1" Text="Choisir l'annee academique"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Faculte_Combo" runat="server"  style="margin-top:10px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" Text="Choisir la faculte"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Departement_Combo" runat="server"  style="margin-top:10px" Width="250px" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged" AutoPostBack="True">
                                   <asp:ListItem Value="-1" Text="Choisir le departement"></asp:ListItem>
                            </asp:DropDownList>
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="ClasseCombo" runat="server"  style="margin-top:10px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                                  <asp:ListItem Value="-1" Text="Choisir la classe" ></asp:ListItem>
                             </asp:DropDownList><br />
                        </div><br /><br />
                        <div class="input input--hoshi">
                            <asp:DropDownList ID="Cours_ComboBox" runat="server"  style="margin-top:10px" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="Cours_ComboBox_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="Choisir le cours" ></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <asp:Button ID="generer_btn" runat="server" Text="Generer" Style="padding-left:95px; padding-right:95px; margin-top:10px" OnClick="generer_btn_Click" />
                    </div>
        </div>
</asp:Content>
