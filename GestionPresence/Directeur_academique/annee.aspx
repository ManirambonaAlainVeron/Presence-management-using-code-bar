<%@ Page Title="" Language="C#" MasterPageFile="~/Directeur_academique/DirecteurMasterPage.Master" AutoEventWireup="true" CodeBehind="annee.aspx.cs" Inherits="GestionPresence.Directeur_academique.annee" %>
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
                     <div class="col-2"></div>
                     <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                          <div class="input-group-prepend">
                                <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Opération</div>
                          </div>
                          <asp:DropDownList ID="Operation_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Operation_Combo_SelectedIndexChanged">
                               <asp:ListItem Value="-1" Text=""></asp:ListItem>
                          </asp:DropDownList>
                      </div>
                     <div class="col-2"></div>
                </div>
                <div class="row">
                        <asp:MultiView ID="MyMultiView" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="col-2"></div>
                                <div class="col-8">
                                    <asp:GridView ID="GDV_Creation" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="true" DataKeyNames="id_annee"
                                        Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="1"
                                        
                                        OnRowCommand="GDV_Creation_RowCommand"
                                        OnRowEditing="GDV_Creation_RowEditing"
                                        OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                                        OnRowUpdating="GDV_Creation_RowUpdating"
                                        OnRowDeleting="GDV_Creation_RowDeleting">
                                        <%-- Teme properties --%>
                                        <EditRowStyle BackColor="#042331" ForeColor="White" />
                                        <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="A-A">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Annee"  runat="server" Text='<%# Eval("annee") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debut">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Debut" runat="server" Height="30px"  Text='<%# Eval("debut") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Debut_TextBox" runat="server" Height="30px" Width="35px" Text='<%# Eval("debut") %>' BorderStyle="None" ></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="Debut_Editing_TextBox" Height="30px" runat="server" placeholder="Debut"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fin">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Fin_TextBox" runat="server" Height="30px" Width="35px" Text='<%# Eval("fin") %>' BorderStyle="None"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="Fin_Editing_TextBox" runat="server" Height="30px" placeholder="Fin"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Fin" runat="server" Height="30px" Text='<%# Eval("fin") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Etat">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Etat" runat="server" Height="30px"  Text='<%# Eval("etat_annee") %>'></asp:Label>
                                                </ItemTemplate>
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
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="AddNew_Button" ImageUrl="~/Images/AddNew.png" runat="server" CommandName="AddNew" ToolTip="Ajouter" ImageAlign="Middle" Width="40px" Height="40px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-2"></div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="col-2"></div>
                                <div class="col-8">
                                    <asp:GridView ID="GDV_Gestion" runat="server" Style="margin: auto;" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_annee"
                                        Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                                        OnRowCancelingEdit="GDV_Gestion_RowCancelingEdit"
                                        OnRowEditing="GDV_Gestion_RowEditing"
                                        OnRowUpdating="GDV_Gestion_RowUpdating">

                                        <%-- Teme properties --%>
                                        <EditRowStyle BackColor="#042331" ForeColor="White" />
                                        <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center"/>
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="A-A">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Annee_Gestion" runat="server" Text='<%# Eval("annee") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Annee_TextBox_Gestion" width="80px" runat="server" Text='<%# Eval("annee") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Etat">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Etat_Gestion" runat="server" Text='<%# Eval("etat_annee") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="Operation_DropDown" Width="90px" runat="server" SelectedValue='<%# Eval("etat_annee") %>'>
                                                        <asp:ListItem Value="En attente" Text="En attente"></asp:ListItem>
                                                        <asp:ListItem Value="Encours" Text="Encours"></asp:ListItem>
                                                        <asp:ListItem Value="Clôturée" Text="Clôturée"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="Date_Label" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Date_TextBox" runat="server" Width="90px" Text='<%# Eval("date") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate >
                                                    <asp:ImageButton ID="Edit" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="Update" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                                                    <asp:ImageButton ID="Cancel" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-2"></div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                     </div>
                </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
