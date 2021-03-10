<%@ Page Title="" Language="C#" MasterPageFile="~/Etudiant/EtudiantMasterPage.Master" AutoEventWireup="true" CodeBehind="prestations.aspx.cs" Inherits="GestionPresence.Etudiant.prestations" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-4" style="margin-top:5px;"><h4></h4></div>

                <div class="col-4"  style="text-align:center;">
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
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Cours</div>
                        </div>
                        <asp:DropDownList ID="Cours_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Cours_ComboBox_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Opération</div>
                        </div>
                        <asp:DropDownList ID="Operation_Combo" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Operation_Combo_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="row">
                    <asp:MultiView ID="MyMultiView" runat="server">
                         <asp:View ID="View1" runat="server">
                             <div class="col-2"></div>
                             <div class="col-8">
                                 <div style="width: 100%; margin: auto; margin-top: 3px; transition: opacity 1s; -webkit-transition: opacity 1s;">
                                     <asp:Panel ID="Horaire_Panel" runat="server">
                                     </asp:Panel>
                                 </div>
                             </div>
                             <div class="col-2"></div>

                             </asp:View>
                             <asp:View ID="View2" runat="server">
                                 <h4>Liste de créneaux validés</h4>
                                 <div class="col-12 border-bottom my-3"></div>
                                 <div class="col-2"></div>
                                 <div class="col-8">
                                      <asp:GridView ID="Jour_Prestation_ListView" Style="margin: auto;" runat="server" AutoGenerateColumns="false"
                                                ShowHeaderWhenEmpty="true" DataKeyNames="id_prestation" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" HorizontalAlign="Center" CellPadding="1" AutoGenerateSelectButton="true" PageSize="5"
                                                OnSelectedIndexChanged="Jour_Prestation_ListView_SelectedIndexChanged1"
                                                OnPageIndexChanging="Jour_Prestation_ListView_PageIndexChanging"
                                                AllowSorting="True" ItemStyle-HorizontalAlign="Center">
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
                                                    <asp:TemplateField HeaderText="Jour">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_jour" runat="server" Text='<%# Eval("jour") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_date" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Periode">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_periode" runat="server" Text='<%# Eval("periode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_heureD" runat="server" Text='<%# Eval("heureD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fin">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label_heureF" runat="server" Text='<%# Eval("heureF") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                     </div>
                                     <div class="col-2"></div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                </div>

    <%------------------------------prestation cours popup---------------------------------%>
            <asp:Label ID="llt" runat="server" Text=""></asp:Label>
            <cc1:ModalPopupExtender ID="mpe" PopupControlID="Panel1" TargetControlID="llt" CancelControlID="exit" runat="server" PopupDragHandleControlID="headerdiv"></cc1:ModalPopupExtender>

            <div id="Panel1" class="">
                <center>
                <asp:Panel ID="Pa1" runat="server" CssClass="modalPopup" >
                      <div id="headerdiv" class="header" >
                          <asp:Label ID="jours_tt" runat="server" Text=""></asp:Label>
                      </div>
                      <div id="divdetails" class="details">
                          <div class="input-group col-12">
                              <div class="input-group-prepend">
                                  <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 120px;">Opération</div>
                              </div>
                                <asp:DropDownList ID="Operation_ComboBox" runat="server"  class="form-control py-0"  AutoPostBack="True" OnSelectedIndexChanged="Operation_ComboBox_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" Text="Choisissez"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Invalider une prestation"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Valider une prestation"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                          <br />

                          <div class="row">
                                <div  class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 80px;">Début</div>
                                    </div>
                                    <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Debut_TextBox" placeholder="Début du cours"/>
                                </div>

                                  <div  class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 80px;">Fin</div>
                                    </div>
                                    <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Fin_TextBox" placeholder="Début du cours"/>
                                </div>
                          </div>

                      </div>
                      <div id="footerdiv" class="footer" >
                          <asp:Button ID="Enregistre_Btn" CssClass="btn btn-primary" runat="server" Text="Enregistrer" OnClick="Enregistre_Btn_Click" />
                          <asp:Button id="exit" CssClass="btn btn-danger"  runat="server" Text="Quiter"/>
                      </div>
                </asp:Panel>
                </center>
            </div>
            <%--------------------------------------FIN-------------------------------------------%>

            <%------------------------------prestation detaillé---------------------------------%>
            <asp:Label ID="llt1" runat="server" Text=""></asp:Label>
            <cc1:ModalPopupExtender ID="presDt_popup" PopupControlID="Div1" TargetControlID="llt1" CancelControlID="prest_Exit" runat="server" PopupDragHandleControlID="headerdiv"></cc1:ModalPopupExtender>
            <div id="Div1" class="">
                <center>
                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="height: auto;">
                    <div id="Div2" class="header">
                        <asp:Label ID="Label2" runat="server" Text="Liste des points abordes"></asp:Label>
                    </div>
                    <div id="div3" class="details" style="margin-top: 20px; margin-bottom: -40px;">
                        <div class="col-12">
                            <asp:GridView ID="Matiere_Enseigne_ListView" Style="margin: auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="false" DataKeyNames="id_prestation_details"
                                Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" HorizontalAlign="Center" CellPadding="1" PageSize="5"
                                OnRowDeleting="Matiere_Enseigne_ListView_RowDeleting"
                                OnRowCancelingEdit="Matiere_Enseigne_ListView_RowCancelingEdit"
                                OnRowEditing="Matiere_Enseigne_ListView_RowEditing"
                                OnRowUpdating="Matiere_Enseigne_ListView_RowUpdating"
                                OnRowCommand="Matiere_Enseigne_ListView_RowCommand"
                                OnPageIndexChanging="Matiere_Enseigne_ListView_PageIndexChanging"
                                AllowSorting="True">

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
                                    <asp:TemplateField HeaderText="Liste des points abordés durant ce créneau">
                                        <ItemTemplate>
                                            <asp:Label ID="points" runat="server" Width="400px" Text='<%# Eval("pointAborde") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="point_aborde" runat="server" Width="400px" Text='<%# Eval("pointAborde") %>'></asp:TextBox>
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
                        </div><br />
                            <div class="row">
                                <div  class="input-group col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Point abordé</div>
                                    </div>
                                    <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="nouveauPoint" placeholder="Nouveau point"/>
                                </div>
                            </div>
                    </div>
                    <div id="Div4" class="footer">    
                        <asp:Button ID="AddNew_Button" CssClass="btn btn-primary" runat="server" ToolTip="Ajouter" Text="Ajouter"  OnClick="AddNew_Button_Click" />
                        <asp:Button ID="prest_Exit"  CssClass="btn btn-danger"  runat="server" Text="Quiter" />
                    </div>
                </asp:Panel>
                </center>
            </div>
            <%---------------------------------------------------------------------------------%>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
