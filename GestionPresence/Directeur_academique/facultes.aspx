<%@ Page Title="" Language="C#" MasterPageFile="~/Directeur_academique/DirecteurMasterPage.Master" AutoEventWireup="true" CodeBehind="facultes.aspx.cs" Inherits="GestionPresence.Directeur_academique.facultes" %>
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

                <div>
                        <asp:MultiView ID="MyMultiView" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-8">
                                            <asp:GridView ID="GDV_Creation" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_faculte"
                                                Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                                                OnRowEditing="GDV_Creation_RowEditing"
                                                OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                                                OnRowUpdating="GDV_Creation_RowUpdating"
                                                OnRowDeleting="GDV_Creation_RowDeleting">
                                                <%-- Teme properties --%>
                                                <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle  BackColor="#002341" Font-Bold="True" ForeColor="White"/>
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nom de la faculte ou institut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Faculte_Label" Width="400px" runat="server" Text='<%# Eval("faculte") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="Departement_TextBox_Editing" Width="400px" runat="server" Text='<%# Eval("faculte") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sigle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Sigle_Label"  runat="server" Text='<%# Eval("sigle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="Sigle_TextBox_Editing" Width="60px"  runat="server" Text='<%# Eval("sigle") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Type_Label" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="Type_DropDown_Editing" Width="100px" runat="server" SelectedValue='<%# Eval("type") %>'>
                                                                <asp:ListItem Value="Faculté" Text="Faculté"></asp:ListItem>
                                                                <asp:ListItem Value="Institut" Text="Institut"></asp:ListItem>
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
                                                </Columns>
                                            </asp:GridView>
                                    </div>
                                    <div class="col-2"></div>
                                </div>
                                <div class="col-12 border-bottom my-3"></div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text" style="background-color:#002341; opacity:0.9; color:white;width: 200px;">Nouvel élément</div>
                                        </div>
                                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Departement_TextBox_Footer" placeholder="Nouvel élément"/>
                                    </div>
                                    <div class="col-2"></div><div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text" style="background-color:#002341; opacity:0.9; color:white;width: 200px;">Sigle</div>
                                        </div>
                                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Sigle_TextBox_Footer" placeholder="Sigle"/>
                                    </div>
                                    <div class="col-2"></div><div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                          <div class="input-group-prepend">
                                                <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Type</div>
                                          </div>
                                          <asp:DropDownList ID="Type_DropDown" runat="server" AutoPostBack="True" class="form-control py-0">
                                               <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                               <asp:ListItem Value="1" Text="Faculté"></asp:ListItem>
                                               <asp:ListItem Value="2" Text="Institut"></asp:ListItem>
                                          </asp:DropDownList>
                                      </div><div class="col-2"></div>
                                          <div class="col-4"></div>
                                          <div class="input-group col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12 mb-4">
                                                <asp:Button ID="AddNew_Button" runat="server" ToolTip="Ajouter" Text="Ajouter" ImageAlign="Middle"  class="btn btn-success btn-lg btn-block" OnClick="AddNew_Button_Click"/>
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
                                    <div class="col-2"></div>
                                </div>
                                    

                                    <div class="row">
                                        <div class="col-2"></div>
                                            <div class="col-8">
                                                <asp:GridView ID="GDV_Gestion" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="false" DataKeyNames="id_faculte"
                                                    Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                                                    OnRowDeleting="GDV_Gestion_RowDeleting">
                                                    <%-- Teme properties --%>
                                                    <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#002341" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle  BackColor="#002341" Font-Bold="True" ForeColor="White"/>
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Nom de la faculte ou institut">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Faculte_Label" Width="580px" runat="server" Text='<%# Eval("faculte") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="Departement_TextBox_Editing" Width="580px" runat="server" Text='<%# Eval("faculte") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sigle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Sigle_Label"  runat="server" Text='<%# Eval("sigle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="Sigle_TextBox_Editing"  runat="server" Text='<%# Eval("sigle") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-2"></div>
                                        </div>
                                <div class="col-12 border-bottom my-3"></div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="input-group col-xl-8 col-lg-8 col-md-8 col-sm-8 col-12 mb-4">
                                        <div class="input-group-prepend">
                                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 200px;">Nouveau faculté</div>
                                        </div>
                                        <asp:DropDownList ID="Departement_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2"></div>
                                </div>
                                <div class="row">
                                    <div class="col-4"></div>
                                            <div class="input-group col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12 mb-4">
                                                <asp:Button ID="Insert_Button" runat="server" Text="Ajouter" class="btn btn-success btn-lg btn-block" ToolTip="Ajouter un autre element" OnClick="Insert_Button_Click" />
                                            </div>
                                    <div class="col-4"></div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                 </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
