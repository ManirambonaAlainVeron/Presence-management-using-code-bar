<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="user_personnel.aspx.cs" Inherits="GestionPresence.Admin.user_personnel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div class="d-block position-relative" style="margin-top:25px">
                <div class="row">
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341 ;opacity:0.9; color:white;width: 150px;">Personnel</div>
                        </div>
                        <asp:DropDownList ID="combo_personnel" runat="server" AutoPostBack="true" class="form-control py-0">
                             <asp:ListItem Value="-1" Text="personnel"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">Login</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="txt_login" placeholder="login"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Profil</div>
                        </div>
                        <asp:DropDownList ID="combo_profil" runat="server" AutoPostBack="true" class="form-control py-0">
                             <asp:ListItem Value="-1" Text="profil"></asp:ListItem>
                             <asp:ListItem Value="administrateur" Text="Administrateur"></asp:ListItem>
                             <asp:ListItem Value="Delegue" Text="Delegue"></asp:ListItem>
                             <asp:ListItem Value="Doyen" Text="Doyen(ne)"></asp:ListItem>
                             <asp:ListItem Value="Directeur" Text="Directeur Academique"></asp:ListItem>
                             <asp:ListItem Value="Secretaire" Text="Secretaire"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">Password</div>
                        </div>
                        <input runat="server" type="password" autocomplete="off" class="form-control py-0" id="txt_password"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Etat</div>
                        </div>
                        <asp:DropDownList ID="combo_etat" runat="server" AutoPostBack="true" class="form-control py-0">
                             <asp:ListItem Value="-1" Text="etat"></asp:ListItem>
                             <asp:ListItem Value="activer" Text="Activer"></asp:ListItem>
                             <asp:ListItem Value="desactive" Text="Desactiver"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">Confirmation</div>
                        </div>
                        <input runat="server" type="password" autocomplete="off" class="form-control py-0" id="txt_confirmation"/>
                    </div>

                </div>

                <div class="row">
                    <div class="col" style="text-align:center; margin-top:25px">
                        <asp:Button ID="Enregistrer_btn" class="btn btn-success col-4 btn-lg " runat="server" Text="Enregistrer" ToolTip="Cliquez ici pour ajouter un utilisateur" OnClick="Enregistrer_btn_Click" />
                    </div>
                </div>
                <div style="margin-top:30px" >

                    <asp:TextBox ID="MatriculeTextbox" runat="server" Height="30px" style="margin-top:15px; margin-right:0px" Width="165px" placeholder="matricule"></asp:TextBox>
                    <asp:ImageButton ID="Search_Button" ImageUrl="~/Images/searchIcon.png" runat="server" style="margin-top:15px; margin-left:0px; position:absolute" Height="31px" Width="33px" OnClick="Search_Button_Click"/>
                    <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click"  style="position:relative; margin-left:45px; padding:2px"  BackColor="#3399FF" BorderColor="#3399FF" BorderStyle="None" ForeColor="White"  ToolTip="Afficher tous"/>
                    <div style="width:auto; height:400px; overflow:scroll">
                        <asp:GridView ID="utilisateur_grid" runat="server"  Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="id_utilisateur"
                            OnRowDeleting="utilisateur_grid_RowDeleting"
                            OnRowEditing="utilisateur_grid_RowEditing"
                            OnRowUpdating="utilisateur_grid_RowUpdating"
                            OnRowCancelingEdit="utilisateur_grid_RowCancelingEdit"
                            AutoGenerateColumns="false">

                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#042331" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />

                            <columns>
                                            
                                            <asp:TemplateField HeaderText="Utilisateur">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_utilisateur" runat="server" Text='<%#Eval("utilisateur") %>'></asp:Label>
                                                </ItemTemplate>
             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="matricule">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("matricule") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profil">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("user_type") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="profil_DropDown" Width="90px" runat="server" SelectedValue='<%# Eval("user_type") %>'>
                                                         <asp:ListItem Value="administrateur" Text="Administrateur"></asp:ListItem>
                                                         <asp:ListItem Value="Delegue" Text="Delegue"></asp:ListItem>
                                                         <asp:ListItem Value="Doyen" Text="Doyen(ne)"></asp:ListItem>
                                                         <asp:ListItem Value="Directeur" Text="Directeur Academique"></asp:ListItem>
                                                         <asp:ListItem Value="Secretaire" Text="Secretaire"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Login">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("login") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Etat">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("etat") %>'></asp:Label>
                                                </ItemTemplate>
                                               <EditItemTemplate>
                                                    <asp:DropDownList ID="etat_DropDown" Width="90px" runat="server" SelectedValue='<%# Eval("etat") %>'>
                                                        <asp:ListItem Value="activer" Text="Activer"></asp:ListItem>
                                                        <asp:ListItem Value="desactive" Text="Desactiver"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                                                            <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="Update_Button" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                                                            <asp:ImageButton ID="Cancel_Button" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                                                        </EditItemTemplate>
                                            </asp:TemplateField>
                                        </columns>
                        </asp:GridView>
                    </div>
            </div>
            </div>

</asp:Content>
