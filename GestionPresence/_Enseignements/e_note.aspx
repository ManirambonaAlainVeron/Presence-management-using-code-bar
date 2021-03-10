<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="e_note.aspx.cs" Inherits="BMDSysWeb._Enseignements.e_note" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <div runat="server" id="MainDiv" style="width: 785px; height: auto; background-color:  #ccdcee; margin: 50px auto; padding: 0px 10px 10px 10px; position: relative; border:double;border-color:#3399cc">
       <div style="width: 780px; height: 30px;margin:0px 0px 20px -10px; background-color: #3399cc; color:white;">
            <span style="display:inline-block; position:relative;margin:auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire de la gestion des notes</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style=" display:inline-block; position:relative; float:right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
        <div class="content_element">
            <div class="input input--hoshi">
                    <asp:DropDownList ID="Annee_Combo" runat="server"  Width="250px" AutoPostBack="True" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

            <div class="input input--hoshi">
                    <asp:DropDownList ID="Faculte_Combo" runat="server"  Width="250px" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

            <div class="input input--hoshi">
                    <asp:DropDownList ID="Operation_ComboBox" runat="server"  Width="250px" AutoPostBack="True" OnSelectedIndexChanged="Operation_ComboBox_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

            <div class="input input--hoshi">
                     <asp:DropDownList ID="Departement_Combo" runat="server"  Width="250px" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </div>

            <div class="input input--hoshi">
                    <asp:DropDownList ID="Classe_ComboBox" runat="server"  Width="250px" OnSelectedIndexChanged="Classe_ComboBox_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                   </div>

            <div class="input input--hoshi">
                    <asp:DropDownList ID="Cours_ComboBox" runat="server" Width="250px" OnSelectedIndexChanged="Cours_ComboBox_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
           </div>

            <div style="text-align:center; margin-top:20px">
                <asp:LinkButton ID="DownLoad_Button" runat="server" Text="Télécharger <br/> la liste <br/> des etudiants" BackColor="Gray" BorderColor="black" BorderWidth="1" Font-Underline="false" ForeColor="white" ></asp:LinkButton>
                <asp:LinkButton ID="UpLoad_Button" runat="server" Text="Charger les notes <br/> de tous les etudiants <br/> en même temps" BackColor="Gray" BorderColor="black" BorderWidth="1" Font-Underline="false" ForeColor="white" Style="margin-left:20px" OnClick="UpLoad_Button_Click"></asp:LinkButton>
                <asp:LinkButton ID="Note_Individuelle_Button" runat="server" Text="Gestion <br/> des notes <br/> par etudiant" BackColor="Gray" BorderColor="black" BorderWidth="1" Font-Underline="false" ForeColor="white" Style="margin-left:20px"></asp:LinkButton>
            </div>
        </div>

</asp:Content>
