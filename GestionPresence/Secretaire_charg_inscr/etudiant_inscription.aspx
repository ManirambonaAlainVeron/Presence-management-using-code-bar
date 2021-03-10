<%@ Page Title="" Language="C#" MasterPageFile="~/Secretaire_charg_inscr/SecretaireMasterPage.Master" AutoEventWireup="true" CodeBehind="etudiant_inscription.aspx.cs" Inherits="GestionPresence.Secretaire_charg_inscr.etudiant_inscription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Styles/fontawesome/css/bootstrap.min.css" />
    <style type="text/css">
        #overlay {
            position:fixed;
            z-index:99;
            top:0px;
            left:0px;
            background-color:#FFFFFF;
            width:100%;
            height:100%;
            filter:Alpha(Opacity=80);
            opacity:0.80;
            -noz-opacity:0.80;
        }

        #theprogress {
            background-color:#03BB9C;
            width:110px;
            height:24px;
            text-align:center;
            filter:Alpha(Opacity=100);
            opacity:1;
            -noz-opacity:1;
        }

        #modalprogress {
            position:absolute;
            top:35%;
            left:45%;
            margin:-11px 0 0 -55px;
            color:white
        }
        #Content2 > #modalprogress {
            position:fixed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="content" style="margin:25px">
               
                <div class="d-block position-relative" style="margin-top:25px">
                         <div class="row">
                             
                                  <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341 ;opacity:0.9; color:white;width: 150px;">A-A</div>
                                    </div>
                                    <asp:DropDownList ID="Annee_Combo" runat="server" class="form-control py-0"  Font-Size="Medium" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>

                                 <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341 ;opacity:0.9; color:white;width: 150px;">Classe</div>
                                    </div>
                                    <asp:DropDownList ID="ClasseCombo" runat="server" class="form-control py-0"   Font-Size="Medium" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="-1" Text="Classe"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                 <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341 ;opacity:0.9; color:white;width: 150px;">Faculte</div>
                                    </div>
                                    <asp:DropDownList ID="Faculte_Combo" runat="server"   Font-Size="Medium" class="form-control py-0" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="-1" Text="Faculte"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                 <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341 ;opacity:0.9; color:white;width: 150px;">Type d'inscription</div>
                                    </div>
                                    <asp:DropDownList ID="Type_Incription_ComboBox" runat="server"   Font-Size="Medium" class="form-control py-0"  OnSelectedIndexChanged="Type_Incription_ComboBox_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="-1" Text="Type d'inscription"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                 <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                                    <div class="input-group-prepend">
                                      <div class="input-group-text" style="background-color:#002341 ;opacity:0.9; color:white;width: 150px;">Departement</div>
                                    </div>
                                    <asp:DropDownList ID="Departement_Combo" runat="server"   Font-Size="Medium" class="form-control py-0"  OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="-1" Text="Departement"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                 <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-6 mb-4">
                                    <div class="input-group-prepend">
                                        <asp:Button ID="Button2" class="input-group-text" runat="server" Text="Chercher" ToolTip="Click pour chercher l'etudiant à inscrire" style="background-color:#002341;opacity:0.9; color:white;width: 150px;" OnClick="Button2_Click"/>
                                    </div>
                                    <asp:TextBox ID="Matricule_TextBox" runat="server" placeholder="matricule" class="form-control py-0" Font-Size="Medium" AutoPostBack="false" ></asp:TextBox>
                                </div>
                             
                             
                          
                        </div>
                        <div style="text-align:center; margin-top:20px; height:110px">
                    
                            <asp:Label ID="Label1" runat="server" Text="Etudiant c'est : "></asp:Label>
                            <strong><asp:Label ID="Nom_Prenom_Label" runat="server" Text="Label" Visible="false"></asp:Label></strong><br /><br />
                            <center><asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder></center>
                        </div>
             
                <div style="text-align:center; margin-top:20px">
                    <asp:ImageButton ID="Enregistrer_Button" runat="server" ImageUrl="~/Images/inscription.png" ToolTip="Inscription" OnClick="Enregistrer_Button_Click"/>
                </div>
                 <div style="margin-top:10px">
            <div>
            <asp:TextBox ID="num_text" placeholder="numero d'inscription" runat="server" Font-Size="Medium" Height="30px" style="margin-top:15px; margin-right:0px" Width="165px"></asp:TextBox>
            <asp:ImageButton ID="search_btn" runat="server" ImageUrl="~/Images/searchIcon.png" style="margin-top:15px; margin-left:0px; position:absolute" Height="31px" Width="33px" OnClick="search_btn_Click"/>
                <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" style="position:relative; margin-left:45px; " Height="28px" Width="33px" BackColor="#3399FF" BorderColor="#3399FF" BorderStyle="None" ForeColor="White" />
                </div>
            <div style="width:auto; height:250px; overflow:scroll">
            <asp:GridView ID="Grid_Inscription" runat="server"
                AutoGenerateColumns="False" 
                     OnRowDeleting="Grid_Inscription_RowDeleting" DataKeyNames="id_inscription" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">

                <AlternatingRowStyle BackColor="#CCCCCC" />

                <columns>
       
                                            <asp:TemplateField HeaderText="Numero d'inscription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("num_inscription") %>'></asp:Label>
                                                </ItemTemplate>
             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date d'inscription">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("date_inscription") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="A-A">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("annee") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Faculte">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("faculte") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departement">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("departement") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Classe">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Classe") %>'></asp:Label>
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
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="overlay">
                <div id="modalprogress">
                    <div id="theprogress">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/load.gif" />
                    </div>
                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
