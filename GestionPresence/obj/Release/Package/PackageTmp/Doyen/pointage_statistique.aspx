<%@ Page Title="" Language="C#" MasterPageFile="~/Doyen/DoyenMasterPage.Master" AutoEventWireup="true" CodeBehind="pointage_statistique.aspx.cs" Inherits="GestionPresence.Doyen.pointage_statistique" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Script/sb.min.css" rel="stylesheet"/>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

       
    <div class="row">
        <div class="col-4" style="margin-top:5px;"></div>
        <div class="col-4">
             <asp:Label ID="Label_Success_Message" runat="server" Text="" ForeColor="Green"></asp:Label>
             <asp:Label ID="Label_Error_Message" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>


    <div class="d-block position-relative">
         <div class="row">
             <div class="col-3" ></div>
             <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                 <div class="input-group-prepend">
                      <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Generer par :</div>
                 </div>
                  <asp:DropDownList ID="DropDown_type_recherche" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="DropDown_type_recherche_SelectedIndexChanged" >
                       <asp:ListItem Value="-1" Text=""></asp:ListItem>
                  </asp:DropDownList>
              </div>
             <div class="col-3"></div>
         </div>
            <div class="col-12 border-bottom my-3"></div>


        <asp:MultiView runat="server" ID="Mult1">
            <asp:View runat="server" ID="view1">
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
            </asp:View>
            </asp:MultiView>
            <asp:MultiView runat="server" ID="Multi2">
            <asp:View runat="server" ID="view2">
                <div class="row">

                    <asp:HiddenField runat="server" ID="jours_dat" />
                    <asp:HiddenField runat="server" ID="nomb_etud" />
                <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                      <div class="input-group-prepend">
                           <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Cours</div>
                      </div>
                      <asp:DropDownList ID="Cours_ComboBox" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Cours_ComboBox_SelectedIndexChanged">
                           <asp:ListItem Value="-1" Text=""></asp:ListItem>
                      </asp:DropDownList>
                 </div>
                <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                    <asp:Button ID="generer_btn" runat="server"  class="btn btn-success btn-lg btn-block" Text="Generer un rapport" OnClick="generer_btn_Click" Height="37px" Font-Size="Larger"/>
                </div> </div>
             </asp:View>
            </asp:MultiView>
            
            <asp:MultiView runat="server" ID="Multi3">
                <asp:View runat="server" ID="view3">
                <%--<div class="row">

                        <div class="col-xl-12 col-lg-12">

                          <!-- Area Chart -->
                          <div class="card shadow mb-4">
                            <div class="card-header py-3">
                              <h6 class="m-0 font-weight-bold text-primary">La participation au cours des étidiants</h6>
                            </div>
                            <div class="card-body">
                              <div class="chart-area">
                                <canvas id="myAreaChart"></canvas>
                              </div>
                  
                            </div>
                          </div>

                      </div>

                </div>--%>
            </asp:View>
        </asp:MultiView>

        <asp:MultiView ID="MultiV_etud" runat="server">
            <asp:View ID="View4" runat="server">
                <div class="row">
            
                     <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                         <div class="input-group-prepend">
                              <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Etudiant</div>
                         </div>
                         <asp:DropDownList ID="Etudiant" runat="server" AutoPostBack="True" class="form-control py-0" OnSelectedIndexChanged="Etudiant_SelectedIndexChanged">
                              <asp:ListItem Value="-1" Text=""></asp:ListItem>
                         </asp:DropDownList>
                     </div>
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <div class="input-group-prepend">
                          <div class="input-group-text" style="background-color:#002341;opacity:0.9; color:white;width: 150px;">Date</div>
                        </div>
                        <input runat="server" type="date" autocomplete="off" class="form-control py-0" id="date_text" placeholder="Date de recerche"/>
                    </div>
                    <div class="input-group col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12 mb-4">
                        <asp:Button ID="Button_etud" runat="server"  class="btn btn-success btn-lg btn-block" Text="Generer un rapport" OnClick="Button_etud_Click" Height="37px" Font-Size="Larger"/>
                    </div>
                  </div>
            </asp:View>
        </asp:MultiView>
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
    <script src="../Styles/Script/Chart.min.js"></script>
    <script src="../Styles/Script/area_chart.js"></script>
</asp:Content>