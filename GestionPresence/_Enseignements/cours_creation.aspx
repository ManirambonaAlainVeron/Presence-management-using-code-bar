<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="cours_creation.aspx.cs" Inherits="BMDSysWeb._Enseignements.cours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <div runat="server" id="MainDiv" style="width: 700px; height: auto; background-color: #ccdcee; margin: 100px auto; padding: 0px 10px 10px 10px; position: relative; border: double; border-color: #3399cc">
        <div style="width: 695px; height: 30px; margin: 0px 0px 10px -10px; background-color: #3399cc; color: white;">
            <span style="display: inline-block; position: relative; margin: auto; padding: 5px; text-align: center; text-decoration: none;">Création de cours par UE</span>
            <asp:Button ID="Edit_Button" runat="server" Text="&#10060;" Style="display: inline-block; position: relative; float: right; padding: 0px; background-color: khaki" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" />
        </div>
        <div class="container">
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border:  none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Annee_Combo" runat="server" AutoPostBack="True" class="input__field input__field--hoshi" Style="width: 150px; color: #000008;" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Annee_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">A-A</span>
                </label>
            </div>


            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Faculte_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 500px; color: #000008;" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Faculte_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Faculté ou l'institut</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Departement_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 500px; color: #000008;" AutoPostBack="True" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Departement_Combo">
                    <span class="input__label-content input__label-content--hoshi">Département</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="ClasseCombo" runat="server" class="input__field input__field--hoshi" Style="width: 150px; color: #000008;" AutoPostBack="True" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="ClasseCombo">
                    <span class="input__label-content input__label-content--hoshi">Classe</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Unite_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 665px; color: #000008;" OnSelectedIndexChanged="Unite_Combo_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="EtatCivil_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Unité d'enseignement</span>
                </label>
            </div>
        </div>
        <div style="margin: 15px auto;">
            <asp:Label ID="Label_Success_Message" runat="server" Text="" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label_Error_Message" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:GridView ID="CoursListView" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="True" DataKeyNames="id_cours"
                Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="2"
                OnRowEditing="GDV_Creation_RowEditing"
                OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                OnRowUpdating="GDV_Creation_RowUpdating"
                OnRowDeleting="GDV_Creation_RowDeleting" OnRowCommand="CoursListView_RowCommand">
                <%-- Teme properties --%>
                <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                <HeaderStyle BackColor="Khaki" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <Columns>
                    <asp:TemplateField HeaderText="Nom du Cours">
                        <ItemTemplate>
                            <asp:Label ID="Label_nom_cour" runat="server" Width="350px" Text='<%# Eval("cours") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="nom_cour_TextBox" runat="server" Text='<%# Eval("cours") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Cours_Editing_TextBox" runat="server" placeholder="Nom du cours"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="Label_code" runat="server" Text='<%# Eval("code_cours") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="code_TextBox" runat="server" Text='<%# Eval("code_cours") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Code_Editing_TextBox" runat="server" placeholder="Code"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Crédits">
                        <ItemTemplate>
                            <asp:Label ID="Label_credits" runat="server" Text='<%# Eval("credits") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="credits_TextBox" runat="server" Text='<%# Eval("credits") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="Credits_Editing_TextBox" runat="server" placeholder="Credits"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                            <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/deleteIcon.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update_Button" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                            <asp:ImageButton ID="Cancel_Button" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="AddNew_Button" runat="server" Text="&#10010;" CommandName="AddNew" ToolTip="Ajouter" Style="padding: 0px" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
