<%@ Page Title="" Language="C#" MasterPageFile="~/Doyen/DoyenMasterPage.Master" AutoEventWireup="true" CodeBehind="cours_creation.aspx.cs" Inherits="GestionPresence.Doyen.cours_creation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-4" style="margin-top:5px;"></div>
                <div class="col-4" style="text-align:center;">
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
                        <asp:DropDownList ID="ClasseCombo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                     <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Unité</div>
                        </div>
                        <asp:DropDownList ID="Unite_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Unite_Combo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div class="col-12 border-bottom my-3"></div>

                <div class="row">
                    <div class="col-2"></div>
                    <div class="col-8">
                        <asp:GridView ID="CoursListView" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_cours"
                            Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="2"
                            OnRowEditing="GDV_Creation_RowEditing"
                            OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                            OnRowUpdating="GDV_Creation_RowUpdating"
                            OnRowDeleting="GDV_Creation_RowDeleting" OnRowCommand="CoursListView_RowCommand">
                            <%-- Teme properties --%>
                            <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                            <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle  BackColor="#002341" Font-Bold="True" ForeColor="White"/>
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            <Columns>
                                <asp:TemplateField HeaderText="Nom du Cours">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_nom_cour" runat="server"  Text='<%# Eval("cours") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="nom_cour_TextBox" runat="server" Width="300px" Text='<%# Eval("cours") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_code" runat="server" Text='<%# Eval("code_cours") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="code_TextBox" runat="server" Width="100px" Text='<%# Eval("code_cours") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crédits">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_credits" runat="server" Text='<%# Eval("credits") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="credits_TextBox" runat="server" Width="50px" Text='<%# Eval("credits") %>'></asp:TextBox>
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
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-2"></div>
                </div>
                <div class="col-12 my-3"></div>
                <div class="row">
                    <div class="col-2"></div>
                    <div  class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Nouveau cours</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Cours_TextBox" placeholder="Nom du cours"/>
                    </div>
                    <div class="col-2"></div>
                    <div class="col-2"></div>
                    <div  class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Code</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Code_TextBox" placeholder="Code du cours"/>
                    </div>
                    <div class="col-2"></div>
                    <div class="col-2"></div>
                    <div  class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Corédit</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Credit_TextBox" placeholder="Crédit du cours"/>
                    </div>
                    <div class="col-2"></div>
                </div>

                <div class="row">
                    <div class="col-4"></div>
                    <div class="input-group col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12 mb-4">
                         <asp:Button ID="Insert_Button" runat="server" class="btn btn-success btn-lg btn-block" Text="Ajouter"  ToolTip="Ajouter un autre element" OnClick="Insert_Button_Click" />
                    </div>
                    <div class="col-4"></div>
                </div>
            </div>
    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
