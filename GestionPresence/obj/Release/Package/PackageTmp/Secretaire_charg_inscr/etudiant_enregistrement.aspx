<%@ Page Title="" Language="C#" MasterPageFile="~/Secretaire_charg_inscr/SecretaireMasterPage.Master" AutoEventWireup="true" CodeBehind="etudiant_enregistrement.aspx.cs" Inherits="GestionPresence.Secretaire_charg_inscr.etudiant_enregistrement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="../Styles/fontawesome/css/bootstrap.min.css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
            <div style="margin:25px;">
                <fieldset style="height:300px; padding:20px; color:#042331; border:solid 2px #042331;">
                <legend><h3>Information d'un etudiant</h3></legend>
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
             
             <div id="information" style="position:absolute;">
             
             <div id="un" style="float:left">
                <div id="id_personnel" style="float:left; margin-right:40px">
                    <asp:TextBox ID="NomTextBox" runat="server" placeholder="Nom" Height="25px" Width="250px" Font-Size="Medium"  ></asp:TextBox><br /><br />
                    <asp:TextBox ID="PrenomTextBox" runat="server" placeholder="Prenom" Height="25px" Width="250px" Font-Size="Medium" ></asp:TextBox><br /><br />
                    <asp:TextBox ID="NaissanceDatePicker" runat="server" placeholder="Prenom" Height="25px" Width="250px" Font-Size="Medium" TextMode="Date" ></asp:TextBox><br /><br />
                    <div>
                        <asp:DropDownList ID="Genre_ComboBox" runat="server" placeholder="Prenom" Height="25px" Width="122px" Font-Size="Medium" AutoPostBack="True" >
                            <asp:ListItem Value="-1" Text="Genre"></asp:ListItem>
                            <asp:ListItem Value="M" Text="Masculin"></asp:ListItem>
                            <asp:ListItem Value="F" Text="Feminin"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="EtatCivil_ComboBox" runat="server" placeholder="Prenom" Height="25px" Width="123px" Font-Size="Medium" >
                            <asp:ListItem Value="-1" Text="Etat Civil"></asp:ListItem>
                            <asp:ListItem Value="Celibataire" Text="Celibataire"></asp:ListItem>
                            <asp:ListItem Value="Marie(e)" Text="Marie(e)"></asp:ListItem>
                            <asp:ListItem Value="Divorce(e)" Text="Divorce(e)"></asp:ListItem>
                            <asp:ListItem Value="Autre" Text="Autre"></asp:ListItem>
                        </asp:DropDownList>
                    </div><br />
                    <div>
                        <asp:DropDownList ID="Identite_Type_ComboBox" runat="server" placeholder="Type d'identite" Height="25px" Width="146px" Font-Size="Medium"  OnSelectedIndexChanged="Identite_Type_ComboBox_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="-1" Text="Type d'identite"></asp:ListItem>
                            <asp:ListItem Value="CNI" Text="CNI"></asp:ListItem>
                            <asp:ListItem Value="PASSEPORT" Text="PASSEPORT"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="IdentiteNumberTextBox" runat="server" placeholder="Numero" Height="25px" Width="100px" Font-Size="Medium" ></asp:TextBox>
                    </div><br />
                    <asp:TextBox ID="PhoneTextBox" runat="server" placeholder="Telephone" Height="25px" Width="250px" Font-Size="Medium"  TextMode="Phone"></asp:TextBox>
                </div>
                <div id="adresse" style="float:right;">
                    <asp:TextBox ID="EmailTextBox" runat="server" placeholder="Email" Height="25px" Width="250px" Font-Size="Medium" TextMode="Email" ></asp:TextBox><br /><br />
                    <asp:DropDownList ID="Region_ComboBox" runat="server"  Height="25px" Width="250px" Font-Size="Medium"  OnSelectedIndexChanged="Region_ComboBox_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="-1" Text="Region"></asp:ListItem>
                            <asp:ListItem Value="EAC-Burundi" Text="EAC-Burundi"></asp:ListItem>
                            <asp:ListItem Value="EAC-Kenya" Text="EAC-Kenya"></asp:ListItem>
                            <asp:ListItem Value="EAC-Ouganda" Text="EAC-Ouganda"></asp:ListItem>
                            <asp:ListItem Value="EAC-Rwanda" Text="EAC-Rwanda"></asp:ListItem>
                            <asp:ListItem Value="EAC-Soudan du Sud" Text="EAC-Soudan du Sud"></asp:ListItem>
                            <asp:ListItem Value="EAC-Tanzanie" Text="EAC-Tanzanie"></asp:ListItem>
                            <asp:ListItem Value="Autre-RDC" Text="Autre-RDC"></asp:ListItem>
                            <asp:ListItem Value="Autre" Text="Autre"></asp:ListItem>
                    </asp:DropDownList><br /><br />
                    <asp:TextBox ID="PaysTextBox"  runat="server" placeholder="Pays" Height="25px" Width="250px" Font-Size="Medium"></asp:TextBox><br /><br />
                    <asp:TextBox ID="ProvinceTextBox"  runat="server" placeholder="Province" Height="25px" Width="250px" Font-Size="Medium"></asp:TextBox><br /><br />
                    <asp:TextBox ID="CommuneTextBox"  runat="server" placeholder="Commune" Height="25px" Width="250px" Font-Size="Medium"></asp:TextBox><br /><br />
                    <asp:TextBox ID="ZoneTextBox"  runat="server" placeholder="Zone" Height="25px" Width="250px" Font-Size="Medium"></asp:TextBox><br />
                </div>
               </div>
             <div id="deux" style="float:right;margin-left:40px">
                <div style="float:left">
                    <div>
                        <asp:TextBox ID="EtablissementTextbox" runat="server"  placeholder="Etablissement" Height="25px" Width="165px" Font-Size="Medium"></asp:TextBox>
                        <asp:TextBox ID="PromotionTextbox" runat="server"  placeholder="Promotion" Height="25px" Width="80px" Font-Size="Medium"></asp:TextBox>
                    </div><br />
                    <asp:TextBox ID="SectionTextBox" runat="server" placeholder="Section"  Height="25px" Width="250px" Font-Size="Medium"></asp:TextBox><br /><br />
                    <div>
                        <asp:TextBox ID="NoteClasseTextbox" runat="server" placeholder="Note classe"  Height="25px" Width="122px" Font-Size="Medium"></asp:TextBox>
                        <asp:TextBox ID="NoteExetatTextBox" runat="server" placeholder="Note extat"  Height="25px" Width="122px" Font-Size="Medium"></asp:TextBox>
                    </div><br />
                    <div>
                        <asp:TextBox ID="NoteTestMedecineTextBox" runat="server" placeholder="Note Medecine"  Height="25px" Width="122px" Font-Size="Medium"></asp:TextBox>
                        <asp:TextBox ID="NoteSynthesetextBox" runat="server" placeholder="Note Synthese" Height="25px" Width="122px" Font-Size="Medium" ></asp:TextBox>
                    </div><br />
                    <asp:DropDownList ID="Hebergement_ComboBox" runat="server" Height="25px" Width="250px"  Font-Size="Medium" OnSelectedIndexChanged="Hebergement_ComboBox_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Hebergement"></asp:ListItem>
                            <asp:ListItem Value="0" Text="En dehors de l’Université"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Par l’Université"></asp:ListItem>
                    </asp:DropDownList><br /><br />
                    <asp:DropDownList ID="Boursier_ComboBox" runat="server"  Height="25px" Width="250px"  Font-Size="Medium" OnSelectedIndexChanged="Boursier_ComboBox_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="-1" Text="Etat sur Bourse"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Non Boursier"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Boursier de l’Université"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Boursier du Gouvernement"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Boursier: Autre bourse"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="check_box" style="float:right; margin-left:35px;">
                    <div style="margin-left:10px" >
                        <div style="float:left" >
                            <asp:Label ID="Label1" runat="server" Text="Extat réussi"></asp:Label>
                            <asp:CheckBox ID="Reussi_Exetat_CheckBox" runat="server"  OnCheckedChanged="Reussi_Exetat_CheckBox_CheckedChanged" AutoPostBack="True" />
                        </div>
                        <div style="float:right">
                            <asp:Label ID="Label2" runat="server" Text="Extat échoué"></asp:Label>
                            <asp:CheckBox ID="Echoue_Exetat_CheckBox" runat="server"  OnCheckedChanged="Echoue_Exetat_CheckBox_CheckedChanged" AutoPostBack="True" />
                        </div>
                        
                    </div><br /><br />
                    <div style="margin-left:10px" >
                        <div style="float:left;margin-right:10px">
                            <asp:Label ID="Label3" runat="server" Text="Etudes encours"></asp:Label>
                            <asp:CheckBox ID="Encours_ComboBox" runat="server" OnCheckedChanged="Encours_ComboBox_CheckedChanged" AutoPostBack="True" />
                        </div>
                        <div style="float:right">
                            <asp:Label ID="Label4" runat="server" Text="Terminées ou suspendues"></asp:Label>
                            <asp:CheckBox ID="Suspendu_CheckBox" runat="server" OnCheckedChanged="Suspendu_CheckBox_CheckedChanged" AutoPostBack="True" />
                        </div>
                    </div><br /><br />
                    <div style="margin-left:90px">
                        <asp:Image ID="imagePic" runat="server" Height="150px" Width="150px" ImageUrl="~/Images/personne.png" BorderStyle="Solid"  /><br />
                        <asp:FileUpload ID="FileUpload_rec" runat="server" onchange="this.form.submit();"/>
                    </div>
                </div>
             </div>
                  
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </fieldset>
                <br />
                 <div id="id_btn" style="margin-top:10px; text-align:center">
                <asp:Button ID="Enregistrer_Button" runat="server" Text="Enregistrer" BackColor="#042331" BorderStyle="None" ForeColor="White" width="130px" Height="35px" Font-Size="Larger" Style="margin-right:25px;" OnClick="Enregistrer_Button_Click"/>
                <asp:Button ID="bt_modifier" runat="server" Text="Modifier" BackColor="#042331"  BorderStyle="None" ForeColor="White" width="130px" Height="35px" Font-Size="Larger" Style="margin-right:25px" OnClick="bt_modifier_Click"/>
                <asp:Button ID="bt_supprimer" runat="server" Text="Supprimer" BackColor="#042331"  BorderStyle="None" ForeColor="White" width="130px" Height="35px" Font-Size="Larger" Style="margin-right:25px" OnClick="bt_supprimer_Click"/>
                <asp:Button ID="Button2" runat="server" Text="Actualiser" BackColor="#042331"  BorderStyle="None" ForeColor="White" width="130px" Height="35px" Font-Size="Larger" OnClick="Button2_Click"/>
         </div>
        
                 <div id="id_grid">
            
                <asp:TextBox ID="MatriculeTextbox" runat="server" placeholder="matricule" Height="30px" style="margin-top:15px; margin-right:0px" Width="165px"></asp:TextBox>
                <asp:ImageButton ID="Search_Button" ImageUrl="~/Images/searchIcon.png" runat="server" style="margin-top:15px; margin-left:0px; position:absolute" Height="31px" Width="33px" OnClick="Search_Button_Click"/>
                <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" style="position:relative; margin-left:45px; " Height="28px" Width="33px" BackColor="#3399FF" BorderColor="#3399FF" BorderStyle="None" ForeColor="White" />
            <div style="width:auto; height:305px; overflow:scroll">
            <asp:GridView ID="Gridetudiant" runat="server"
                 AutoGenerateColumns="False" 
                     OnRowDeleting="Gridetudiant_RowDeleting" DataKeyNames="id_etudiant" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">

                
                                    <AlternatingRowStyle BackColor="#CCCCCC" />

                
                                    <columns>
                                            
                                            <asp:TemplateField HeaderText="Etudiant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("etudiant") %>'></asp:Label>
                                                </ItemTemplate>
             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="matricule">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("matricule") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("telephone") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nationalite">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("naissance_pays") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Note classe">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("note_classe") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Note exetat">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("note_exetat") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                     <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/Images/delete.png" CommandName="Delete" />
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
             
                                        </columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#042331" Font-Bold="True" ForeColor="White" />
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
       
</asp:Content>
