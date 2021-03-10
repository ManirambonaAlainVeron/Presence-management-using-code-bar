<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="personnel_enregistrement.aspx.cs" Inherits="GestionPresence.Admin.personnel_enregistrement" %>
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

            <div class="col-12"></div>

            <div class="d-block position-relative">
                <div class="row">
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="background-color:#002341; opacity:0.9; color:white;width: 150px;">Nom</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="NomTextBox" placeholder="Nom du personnel"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px; ">prenom</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="PrenomTextBox" placeholder="Prénom du personnel"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Genre</div>
                        </div>
                        <asp:DropDownList ID="Genre_ComboBox" runat="server" AutoPostBack="true" class="form-control py-0">
                             <asp:ListItem Value="-1" Text="Genre"></asp:ListItem>
                             <asp:ListItem Value="M" Text="Masculin"></asp:ListItem>
                             <asp:ListItem Value="F" Text="Féminin"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Date de Naissance</div>
                        </div>
                        <input runat="server" type="date" placeholder="dd-mm-yyyy" autocomplete="off" class="form-control py-0" id="Naissance_DateTimerPicker"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Etat civil</div>
                        </div>
                        <asp:DropDownList ID="EtatCivil_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                             <asp:ListItem Value="-1" Text="Etat civil"></asp:ListItem>
                             <asp:ListItem Value="Celibataire" Text="Célibataire"></asp:ListItem>
                             <asp:ListItem Value="Marie(e)" Text="Marié(e)"></asp:ListItem>
                             <asp:ListItem Value="Divorce(e)" Text="Divorcé(e)"></asp:ListItem>
                             <asp:ListItem Value="Autres" Text="Autres"></asp:ListItem>
                        </asp:DropDownList>
                    </div>


                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Région</div>
                        </div>
                        <asp:DropDownList ID="Region_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Region_ComboBox_SelectedIndexChanged">
                             <asp:ListItem Value="-1" Text="Région"></asp:ListItem>
                             <asp:ListItem Value="EAC-Burundi" Text="EAC-Burundi"></asp:ListItem>
                             <asp:ListItem Value="EAC-Kenya" Text="EAC-Kenya"></asp:ListItem>
                             <asp:ListItem Value="EAC-Ouganda" Text="EAC-Ouganda"></asp:ListItem>
                             <asp:ListItem Value="EAC-Rwanda" Text="EAC-Rwanda"></asp:ListItem>
                             <asp:ListItem Value="EAC-Soudan du Sud" Text="EAC-Soudan du Sud"></asp:ListItem>
                             <asp:ListItem Value="EAC-Tanzanie" Text="EAC-Tanzanie"></asp:ListItem>
                             <asp:ListItem Value="Autre-RDC" Text="Autre-RDC"></asp:ListItem>
                             <asp:ListItem Value="Autre" Text="Autre"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Pays</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Pays_TextBox" placeholder="Pays d'origine"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Télephone</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Phone_TextBox" placeholder="Numéro du télephone"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">E-mail</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Email_TextBox" placeholder="Adresse mail"/>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Type ID</div>
                        </div>
                        <asp:DropDownList ID="Identite_Type_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                             <asp:ListItem Value="-1" Text="Type de l'identité"></asp:ListItem>
                             <asp:ListItem Value="CNI" Text="CNI"></asp:ListItem>
                             <asp:ListItem Value="PASSEPORT" Text="PASSEPORT"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Identité</div>
                        </div>
                        <input runat="server" type="text" autocomplete="off" class="form-control py-0" id="Identite_TextBox" placeholder="Numéro de l'identé (ID)"/>
                    </div>
                
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                         <div class="input-group-prepend">
                              <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Diplôme</div>
                         </div>
                         <asp:DropDownList ID="Diplome_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                              <asp:ListItem Value="-1" Text=""></asp:ListItem>
                         </asp:DropDownList>
                    </div>

                </div>

                <div class="col-12 border-bottom my-3"></div>
                <div class="row">

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Grade academique</div>
                        </div>
                        <asp:DropDownList ID="Grade_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Type de personnel</div>
                        </div>
                        <asp:DropDownList ID="Personnel_Type_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Categorie</div>
                        </div>
                        <asp:DropDownList ID="Categorie_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Type de vacation</div>
                        </div>
                        <asp:DropDownList ID="Vacation_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0">
                             <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="row">
                    <div class="col">
                        <asp:Button ID="Enregistrer_Button" class="btn btn-success btn-lg btn-block" runat="server" OnClick="Enregistrer_Button_Click" Text="" ToolTip="Cliquez ici pour ajouter un personnel" />
                    </div>
                    <div class="col">
                        <asp:Button ID="Initialiser_Button" class="btn btn-success btn-lg btn-block" runat="server" OnClick="Initialiser_Button_Click" Text="Initialiser"  ToolTip="Initialiser les champs" />
                    </div>
               </div>
                <div class="row">
                    <div class="col-4">
                          <asp:TextBox ID="MatriculeTextbox" runat="server" placeholder="matricule" Height="30px" style="margin-top:15px; margin-right:0px" Width="165px"></asp:TextBox>
                          <asp:ImageButton ID="Search_Button" ImageUrl="~/Images/searchIcon.png" runat="server" style="margin-top:15px; margin-left:0px; position:absolute" Height="31px" Width="33px" OnClick="Search_Button_Click"/>
                          <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" style="position:relative; margin-left:45px; " Height="28px" Width="33px" BackColor="#3399FF" BorderColor="#3399FF" BorderStyle="None" ForeColor="White" />
                    </div>
                    <div class="col-12">

           
                    <asp:GridView ID="GridPersonnel" runat="server"
                        AutoGenerateColumns="False" OnSelectedIndexChanged="GridPersonnel_SelectedIndexChanged"
                     OnRowDeleting="GridPersonnel_RowDeleting" DataKeyNames="id_personnel" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AutoGenerateSelectButton="True">

                
                                    <AlternatingRowStyle BackColor="#CCCCCC" />

                
                                    <columns>
                                            
                                            <asp:TemplateField HeaderText="Personnel">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("personnel") %>'></asp:Label>
                                                </ItemTemplate>
             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="matricule">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("matricule") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("telephone") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                     <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/delete.png" CommandName="Delete" />
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                        </columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#042331" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />

                    </asp:GridView>
                </div>
                </div>
            </div>
       </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
