<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="horaire.aspx.cs" Inherits="BMDSysWeb._Enseignements.horaire" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <style type="text/css">
        .content_element fieldset {
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            margin: 0px 0px 10px 0px;
            border: 1px solid #FFD2D2;
            padding: 10px;
            background-color: #ebfcff;
            box-shadow: inset 0px 0px 10px #FFE5E5;
            -moz-box-shadow: inset 0px 0px 10px #FFE5E5;
            -webkit-box-shadow: inset 0px 0px 10px #FFE5E5;
        }

        .content_element fieldset legend {
                color: white;
                border-top: 1px solid #FFD2D2;
                border-left: 1px solid #FFD2D2;
                border-right: 1px solid #FFD2D2;
                border-radius: 5px 5px 0px 0px;
                -webkit-border-radius: 5px 5px 0px 0px;
                -moz-border-radius: 5px 5px 0px 0px;
                background: #291f23;
                padding: 3px;
                box-shadow: -0px -1px 2px #F1F1F1;
                -moz-box-shadow: -0px -1px 2px #F1F1F1;
                -webkit-box-shadow: -0px -1px 2px #F1F1F1;
                font-weight: normal;
                font-size: 12px;
            }
            
          
    </style>
    <div runat="server" id="MainDiv" style="width: 780px; height: 650px; background-color: #ccdcee; padding: 0px 10px 10px 10px; position: relative; border:double;border-color:#3399cc">
       <div style="width: 775px; height: 30px;margin:0px 0px 20px -10px; background-color: #3399cc; color:white;">
            <span style="display:inline-block; position:relative;margin:auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire de la creation de l'horaire</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style=" display:inline-block; position:relative; float:right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
        <div class="container">
            <div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border:  none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Annee_Combo" runat="server" AutoPostBack="True" class="input__field input__field--hoshi" Style="width: 150px; color: #000008;" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Annee_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">A-A</span>
                </label>
            </div>


            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Faculte_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 500px; color: #000008;" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Faculte_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Faculté ou l'institut</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Departement_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 500px; color: #000008;" AutoPostBack="True" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Departement_Combo">
                    <span class="input__label-content input__label-content--hoshi">Département</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Classe_ComboBox" runat="server" class="input__field input__field--hoshi" Style="width: 150px; color: #000008;" AutoPostBack="True" OnSelectedIndexChanged="Classe_ComboBox_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="ClasseCombo">
                    <span class="input__label-content input__label-content--hoshi">Classe</span>
                </label>
            </div>
            </div>


            <div style="position:relative;float:left;margin-top:10px;margin-left:260px"><asp:Label ID="Label1" runat="server" Text="AM"></asp:Label></div>
            <div style="position:absolute;float:right;margin-top:10px;margin-left:560px"><asp:Label ID="Label2" runat="server" Text="PM"></asp:Label></div>


            <div>
                <div style="float:left;position:absolute;width:20%; margin-top:30px">
                    <br /><asp:Label ID="Lundi" runat="server" Text="Lundi"></asp:Label><br />
                    <strong><asp:Label ID="Lundi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br /><br /><br />

                    <asp:Label ID="Mardi"  runat="server" Text="Mardi"></asp:Label><br />
                    <strong><asp:Label ID="Mardi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br /><br /><br />

                    <asp:Label ID="Mercredi" runat="server" Text="Mercredi"></asp:Label><br />
                    <strong><asp:Label ID="Mercredi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br /><br /><br />

                    <asp:Label ID="Jeudi" runat="server" Text="Jeudi"></asp:Label><br />
                    <strong><asp:Label ID="Jeudi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br /><br /><br />

                    <asp:Label ID="Vendredi" runat="server" Text="Vendredi"></asp:Label><br />
                    <strong><asp:Label ID="Vendredi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br /><br /><br />

                    <asp:Label ID="Samedi" runat="server" Text="Samedi"></asp:Label><br />
                    <strong><asp:Label ID="Samedi_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong><br /><br /><br />

                    <asp:Label ID="Dimanche" runat="server" Text="Dimanche"></asp:Label><br />
                    <strong><asp:Label ID="Dimanche_Date_Label" runat="server" Text="Date" style="color:red"></asp:Label></strong>
                </div>
                <div style="float:right;position:relative;width:80%;">
                    <div style="float:left;position:relative;width:50%; height:445px">
                        <asp:Panel ID="Horaire_Panel_AM" runat="server"></asp:Panel>
                    </div>
                    <div style="float:right;position:relative; width:50%;height:445px">
                        <asp:Panel ID="Horaire_Panel_PM" runat="server"></asp:Panel>
                    </div>
                    <br />
                    <asp:Button ID="Preview_Button" runat="server" Text="<< Semaine précédente>>" ToolTip="semaine precedent" Width="170px" Height="35px" Style="padding: 1px" OnClick="Preview_Button_Click" Font-Size="Small" />
                    <asp:Button ID="Inition" runat="server" Text="<< Semaine encours >>" ToolTip="Semaine encours" Width="170px" Height="35px" Style="padding: 1px; margin-left:20px" OnClick="Inition_Click" Font-Size="Small" />
                    <asp:Button ID="Next_Button" runat="server" Text="<<Semaine suivante >>" ToolTip="Semaine suivante" Width="170px" Height="35px" Style="padding: 1px; margin-left:20px" OnClick="Next_Button_Click" Font-Size="Small" />
            
                </div>
            </div>
            <script src="../_Styles/TextInputEffects/js/classie.js"></script>
                    <script>
                        (function () {
                            // trim polyfill : https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/Trim
                            if (!String.prototype.trim) {
                                (function () {
                                    // Make sure we trim BOM and NBSP
                                    var rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;
                                    String.prototype.trim = function () {
                                        return this.replace(rtrim, '');
                                    };
                                })();
                            }

                            [].slice.call(document.querySelectorAll('input.input__field')).forEach(function (inputEl) {
                                // in case the input is already filled..
                                if (inputEl.value.trim() !== '') {
                                    classie.add(inputEl.parentNode, 'input--filled');
                                }

                                // events:
                                inputEl.addEventListener('focus', onInputFocus);
                                inputEl.addEventListener('blur', onInputBlur);
                            });

                            function onInputFocus(ev) {
                                classie.add(ev.target.parentNode, 'input--filled');
                            }

                            function onInputBlur(ev) {
                                if (ev.target.value.trim() === '') {
                                    classie.remove(ev.target.parentNode, 'input--filled');
                                }
                            }
                        })();
		        </script>
        </div>
        </div>

    <cc1:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server"></cc1:toolkitscriptmanager>

<%------------------------------horaire cours popup---------------------------------%>
            <asp:Label ID="llt" runat="server" Text=""></asp:Label>
        
            <cc1:ModalPopupExtender ID="hc" PopupControlID="Panel1" TargetControlID="llt" CancelControlID="exit" runat="server" PopupDragHandleControlID="headerdiv"></cc1:ModalPopupExtender>
            
            <div id="Panel1" class="backgrnd">
                <center>
                <asp:Panel ID="Pa1" runat="server" CssClass="modalPopup" >
                      <div id="headerdiv" class="header" >
                          <asp:Label ID="jours_tt" runat="server" Text=""></asp:Label>
                      </div>
                      <div id="divdetails" class="details">
                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="Operation_ComboBox" runat="server" class="input__field input__field--hoshi" Style="width: 380px; color: #000008" AutoPostBack="True" OnSelectedIndexChanged="Operation_ComboBox_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Supprimer un cours sur Horaire"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Programmer un cours pour Enseignement"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Programmer un cours pour Evaluation 1er Session"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Programmer un cours pour Evaluation 2 eme Session"></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Operation_ComboBox">
                                    <span class="input__label-content input__label-content--hoshi">Opération </span>
                                </label>
                            </div>

                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="cours_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 470px; color: #000008" OnSelectedIndexChanged="cours_Combo_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="cours_Combo">
                                    <span class="input__label-content input__label-content--hoshi">Cours </span>
                                </label>
                            </div>

                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="salle_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 470px; color: #000008" OnSelectedIndexChanged="salle_Combo_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="salle_Combo">
                                    <span class="input__label-content input__label-content--hoshi">Salle </span>
                                </label>
                            </div>

                      </div>
                      <div id="footerdiv" class="footer" >
                          <asp:Button id="exit" runat="server" Text="Quiter" OnClick="exit_Click"/>
                          <asp:Button ID="Enregistre_Btn" runat="server" Text="Enregistrer" OnClick="Enregistre_Btn_Click" />
                      </div>
                </asp:Panel>
                </center>
            </div>
            <%-----------------------------END of horaire cours popup---------------------------------%>

</asp:Content>
