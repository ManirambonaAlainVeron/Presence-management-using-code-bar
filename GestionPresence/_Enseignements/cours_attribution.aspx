<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="cours_attribution.aspx.cs" Inherits="BMDSysWeb._Enseignements.cours_attribution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <div runat="server" id="MainDiv" style="width: 720px; height: auto; background-color: #ccdcee; margin: 100px auto; padding: 0px 10px 10px 10px; position: relative; border: double; border-color: #3399cc">
        <div style="width: 715px; height: 30px; margin: 0px 0px 10px -10px; background-color: #3399cc; color: white;">
            <span style="display: inline-block; position: relative; margin: auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire d'attribution de cours</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style="display: inline-block; position: relative; float: right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
        <div class="container">
            <div class="input input--hoshi"> 
                <asp:DropDownList ID="Annee_Combo" runat="server" Style="width: 150px" AutoPostBack="True" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text="**A-A**"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="input input--hoshi">
                <asp:DropDownList ID="Faculte_Combo" runat="server" Style="width: 525px;" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text="**Faculté ou l'institut**"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="input input--hoshi">
                <asp:DropDownList ID="Departement_Combo" runat="server" Style="width: 525px" AutoPostBack="True" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text="**Département**"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="input input--hoshi">
                <asp:DropDownList ID="ClasseCombo" runat="server" Style="width: 150px;" AutoPostBack="True" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text="**Classe**"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div style="margin: 15px auto;">
            <asp:Label ID="Label_Success_Message" runat="server" Text="" ForeColor="Blue"></asp:Label>
            <asp:Label ID="Label_Error_Message" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:GridView ID="Etat_Attribution_GridView" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="id_cours"
                Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" HorizontalAlign="Center" CellPadding="1" PageSize="10"
                OnRowEditing="Etat_Attribution_GridView_RowEditing" OnRowCancelingEdit="Etat_Attribution_GridView_RowCancelingEdit" OnRowUpdating="Etat_Attribution_GridView_RowUpdating"
                OnRowDataBound="Etat_Attribution_GridView_RowDataBound" AllowPaging="True" AllowSorting="True">
                <%-- Teme properties --%>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#eee3fc" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <Columns>
                    <asp:TemplateField HeaderText="Intitulé du cours">
                        <ItemTemplate>
                            <asp:Label ID="Cours_Nom" runat="server" Width="300px" Text='<%# Eval("cours") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Crédits">
                        <ItemTemplate>
                            <asp:Label ID="credit_cour" runat="server" Style="text-align: center;" Text='<%# Eval("credits") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Enseignant du cours">
                        <ItemTemplate>
                            <asp:Label ID="Label_Etat" runat="server" Width="200px" Text='<%# string.Concat(Eval("sigle"), " ",Eval("nom"), " ", Eval("prenom"))%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="Enseignant_DropDown" Width="200px" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update_Button" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                            <asp:ImageButton ID="Cancel_Button" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
