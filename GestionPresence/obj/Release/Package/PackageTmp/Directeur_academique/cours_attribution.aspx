<%@ Page Title="" Language="C#" MasterPageFile="~/Directeur_academique/DirecteurMasterPage.Master" AutoEventWireup="true" CodeBehind="cours_attribution.aspx.cs" Inherits="GestionPresence.Directeur_academique.cours_attribution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div class="row">
                <div class="col-4" style="margin-top:5px;"><h4>Entré</h4></div>
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
                </div>

                <div class="col-12 border-bottom my-3"></div>

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
                                        <asp:GridView ID="GDV_Attribution" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="id_cours"
                                            Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" HorizontalAlign="Center" CellPadding="1" PageSize="10" 
                                            OnRowEditing="GDV_Attribution_RowEditing"
                                            OnRowCancelingEdit="GDV_Attribution_RowCancelingEdit"
                                            OnRowUpdating="GDV_Attribution_RowUpdating"
                                            OnRowDataBound="GDV_Attribution_RowDataBound"
                                            AllowPaging="True" AllowSorting="True"
                                            OnPageIndexChanging="GDV_Attribution_PageIndexChanging">
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
                                                <asp:TemplateField HeaderText="Intitulé du cours">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cours_Nom" runat="server" Width="280px" Text='<%# Eval("cours") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enseignant titulaire du cours">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_Etat" runat="server" Width="280px" Text='<%# string.Concat(Eval("sigle"), " ",Eval("nom"), " ", Eval("prenom"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="Enseignant_DropDown" Width="280px" runat="server">
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
                                    <div class="col-2"></div>
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-8">
                                        <asp:GridView ID="GDV_Etat_Avancement" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="id_cours"
                                            Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" HorizontalAlign="Center" CellPadding="1" PageSize="10"
                                            OnRowCancelingEdit="GDV_Etat_Avancement_RowCancelingEdit" AllowPaging="True" AllowSorting="True"
                                            OnRowEditing="GDV_Etat_Avancement_RowEditing"
                                            OnRowUpdating="GDV_Etat_Avancement_RowUpdating"
                                            OnPageIndexChanging="GDV_Etat_Avancement_PageIndexChanging">
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
                                                <asp:TemplateField HeaderText="Intitulé du cours">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cours_Nom" runat="server" Width="280px" Text='<%# Eval("cours") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cours_Code" runat="server" Style="text-align: center;" Text='<%# Eval("code_cours") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Crédits">
                                                    <ItemTemplate>
                                                        <asp:Label ID="credit_cour" runat="server" Style="text-align: center;" Text='<%# Eval("credits") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Etat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_Etat_Gestion" runat="server" Text='<%# Eval("etat") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="Etat_Cours_DropDown" Width="90px" runat="server" SelectedValue='<%# Eval("etat") %>'>
                                                            <asp:ListItem Value="En attente" Text="En attente"></asp:ListItem>
                                                            <asp:ListItem Value="Encours" Text="Encours"></asp:ListItem>
                                                            <asp:ListItem Value="Cloturee" Text="Cloturee"></asp:ListItem>
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
                                    <div class="col-2"></div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
