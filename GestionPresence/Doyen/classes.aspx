<%@ Page Title="" Language="C#" MasterPageFile="~/Doyen/DoyenMasterPage.Master" AutoEventWireup="true" CodeBehind="classes.aspx.cs" Inherits="GestionPresence.Doyen.classes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-4" style="margin-top:5px;"></div>

                <div class="col-4"  style="text-align:center;">
                    <asp:Label ID="Label_Success_Message" runat="server" Text="" ForeColor="Green"></asp:Label>
                    <asp:Label ID="Label_Error_Message" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="col-12 my-3"></div>

            <div class="d-block position-relative">
                <div class="row">
                    <div class="col-2"></div>
                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Opération</div>
                        </div>
                        <asp:DropDownList ID="Operation_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Operation_Combo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-2"></div>
                </div>

                    <asp:MultiView ID="MyMultiView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">  
                                <div class="col-2"></div>
                                <div class="col-8">
                                            <asp:GridView ID="GDV_Creation" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_classe"
                                                Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                                                OnRowEditing="GDV_Creation_RowEditing"
                                                OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                                                OnRowUpdating="GDV_Creation_RowUpdating"
                                                OnRowDeleting="GDV_Creation_RowDeleting" OnRowCommand="GDV_Creation_RowCommand">
                                                <%-- Teme properties --%>
                                                <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle  BackColor="#002341" Font-Bold="True" ForeColor="White"/>
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nom de la classe">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Annee" runat="server" Text='<%# Eval("classe") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="Classe_TextBox_Editing" Width="400px" runat="server" Text='<%# Eval("classe") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            
                                                        </FooterTemplate>
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
                                            </asp:GridView><br />
                                    </div>
                                    <div class="col-2"></div>
                                    <div class="col-2"></div>
                                        <div  class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                            <div class="input-group-prepend">
                                              <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Classe</div>
                                            </div>
                                            <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Classe_TextBox" placeholder="classe"/>
                                        </div>
                                    <div class="col-2"></div>
                                </div>
                                <div class="row">
                                        <div class="col-4"></div>
                                        <div class="input-group col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12 mb-4">
                                             <asp:Button ID="enre_but" runat="server" class="btn btn-success btn-lg btn-block" Text="Ajouter"  ToolTip="Ajouter un autre element" OnClick="Insert_Button_Click" />
                                        </div>
                                        <div class="col-4"></div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="row">  
                                    <div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Année academique</div>
                                        </div>
                                        <asp:DropDownList ID="Annee_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div><div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Faculté</div>
                                        </div>
                                        <asp:DropDownList ID="Faculte_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div><div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Département</div>
                                        </div>
                                        <asp:DropDownList ID="Departement_DropDownList" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Departement_DropDownList_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div>

                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-8">
                                        <asp:GridView ID="GDV_Gestion" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_classe"
                                                Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                                                OnRowDeleting="GDV_Gestion_RowDeleting">
                                                <%-- Teme properties --%>
                                                <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle  BackColor="#002341" Font-Bold="True" ForeColor="White"/>
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nom de la classe">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Classe_Label" Width="250px" runat="server" Style="padding-left: 5px" Text='<%# Eval("classe") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Etat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_Etat_Gestion" Width="120px" runat="server" Text='<%# Eval("etat_avancement") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/deleteIcon.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                    </div>
                                    <div class="col-2"></div>
                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Classe</div>
                                        </div>
                                        <asp:DropDownList ID="Classe_DropdownList" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Classe_DropdownList_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
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
                            </asp:View>
                            <asp:View ID="View3" runat="server">
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">A-A</div>
                                        </div>
                                        <asp:DropDownList ID="Annee_Etat_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Annee_Etat_Combo_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div><div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Faculte ou Institut</div>
                                        </div>
                                        <asp:DropDownList ID="Faculte_Etat_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Faculte_Etat_Combo_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div><div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Departement</div>
                                        </div>
                                        <asp:DropDownList ID="Departement_Etat_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Departement_Etat_Combo_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div>
                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-8">
                                        <asp:GridView ID="GDV_Etat_Avancement" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_classe"
                                            Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                                            OnRowCancelingEdit="GDV_Etat_Avancement_RowCancelingEdit"
                                            OnRowEditing="GDV_Etat_Avancement_RowEditing"
                                            OnRowUpdating="GDV_Etat_Avancement_RowUpdating">
                                            <%-- Teme properties --%>
                                            <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle  BackColor="#002341" Font-Bold="True" ForeColor="White"/>
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nom de la classe">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Classe_Label" Width="250px" runat="server" Style="padding-left: 5px" Text='<%# Eval("classe") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Etat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_Etat_Gestion" Width="120px" runat="server" Text='<%# Eval("etat_avancement") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="Operation_DropDown" Width="90px" runat="server" SelectedValue='<%# Eval("etat_avancement") %>'>
                                                            <asp:ListItem Value="En attente" Text="En attente"></asp:ListItem>
                                                            <asp:ListItem Value="Encours" Text="Encours"></asp:ListItem>
                                                            <asp:ListItem Value="Clôturée" Text="Clôturée"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
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
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
