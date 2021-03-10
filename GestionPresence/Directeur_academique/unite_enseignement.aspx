<%@ Page Title="" Language="C#" MasterPageFile="~/Directeur_academique/DirecteurMasterPage.Master" AutoEventWireup="true" CodeBehind="unite_enseignement.aspx.cs" Inherits="GestionPresence.Directeur_academique.unite_enseignement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="d-block position-relative" style="margin-top:30px">
        <div class="row" >
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">A-A</div>
                        </div>
                        <asp:DropDownList ID="Annee_Combo" runat="server"  AutoPostBack="true" class="form-control py-0" Font-Size="Larger"  OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                        </asp:DropDownList>
             </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Classe</div>
                        </div>
                        <asp:DropDownList ID="ClasseCombo" runat="server" AutoPostBack="true" class="form-control py-0" Font-Size="Larger"   OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Classe"></asp:ListItem>
                        </asp:DropDownList><br /><br />
                    </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Faculte</div>
                        </div>
                        <asp:DropDownList ID="Faculte_Combo" runat="server" AutoPostBack="true" class="form-control py-0" Font-Size="Larger"  OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Faculte"></asp:ListItem>
                        </asp:DropDownList><br /><br />
                    </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Semestre</div>
                        </div>
                         <asp:DropDownList ID="SemestreCombo" runat="server" AutoPostBack="true" Font-Size="Larger" class="form-control py-0" OnSelectedIndexChanged="SemestreCombo_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Semestre"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
            <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Departement</div>
                        </div>
                        <asp:DropDownList ID="Departement_Combo" runat="server"  AutoPostBack="true" class="form-control py-0" Font-Size="Larger" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Departement"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
        </div>
            
    
   
                <div style="margin-top: 130px; position:absolute; margin-left:300px">
                <asp:GridView ID="GDV_Creation" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="id_unite"
                    Width="400px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" HorizontalAlign="Center" CellPadding="3"
                    OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                    OnRowEditing="GDV_Creation_RowEditing"
                    OnRowUpdating="GDV_Creation_RowUpdating" OnRowCommand="GDV_Creation_RowCommand" OnRowDeleting="GDV_Creation_RowDeleting" BorderWidth="1px" ForeColor="Black" GridLines="Vertical">

                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="#042331" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Nom de l'unite">
                            <ItemTemplate>
                                <asp:Label ID="label_salle" runat="server" Width="430px" Text='<%# Eval("unite") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="nom_TextBox" runat="server" Width="430px" Text='<%# Eval("unite") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NomUE_TextBox_Footer" runat="server" Width="430px" placeholder="Nouvelle UE" Height="30px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="Label_code" runat="server" Width="80px" Text='<%# Eval("code_unite") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="code_TextBox" runat="server" Width="80px" Text='<%# Eval("code_unite") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="CodeUE_TextBox_Footer" runat="server" Width="80px" Height="30px" placeholder="Code UE"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="&#9889;">
                            <ItemTemplate>
                                <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                                <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="Update_Button" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                                <asp:ImageButton ID="Cancel_Button" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="AddNew_Button" runat="server" Text="&#10010;" Width="50px" Height="30px" CommandName="AddNew" ToolTip="Ajouter" Style="padding: 0px" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
             </div>
     
            </div>
        
</asp:Content>
