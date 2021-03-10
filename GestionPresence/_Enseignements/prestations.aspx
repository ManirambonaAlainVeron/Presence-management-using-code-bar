<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="prestations.aspx.cs" Inherits="BMDSysWeb._Enseignements.prestations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div runat="server" id="MainDiv" style="width: 860px; height: 600px; background-color: #ccdcee; padding: 0px 10px 10px 10px; position: relative; border: double; border-color: #3399cc">
        <div style="width: 855px; height: 30px; margin: 0px 0px 20px -10px; background-color: #3399cc; color: white;">
            <span style="display: inline-block; position: relative; margin: auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire du prestation du cours</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style="display: inline-block; position: relative; float: right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
        <div class="container">
            <div style="position: relative; margin-top: 5px; margin-right: 2px; display: inline-block; width: 500px; float: left">
                <div class="input input--hoshi" style="height: 52px; margin-top: -10px; border: none; border-bottom: 1px solid #000008;">
                    <asp:UpdatePanel ID="Update_Annee_Combo" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Annee_Combo" runat="server" Style="width: 100px; color: #000008;" class="input__field input__field--hoshi" AutoPostBack="True" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="*"></asp:ListItem>
                            </asp:DropDownList>
                            <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Annee_Combo">
                                <span class="input__label-content input__label-content--hoshi">A-A</span>
                            </label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Annee_Combo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="input input--hoshi" style="height: 52px; margin-top: -10px; border: none; border-bottom: 1px solid #000008;">
                    <asp:UpdatePanel ID="Update_Faculte_Combo" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Faculte_Combo" runat="server" Style="width: 350px; color: #000008;" class="input__field input__field--hoshi" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Faculte_ComboBox">
                                <span class="input__label-content input__label-content--hoshi">Faculté ou l'institut</span>
                            </label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Faculte_Combo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="input input--hoshi" style="height: 52px; margin-top: -10px; border: none; border-bottom: 1px solid #000008;">
                    <asp:UpdatePanel ID="Update_Departement_Combo" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Departement_Combo" runat="server" Style="width: 330px; color: #000008;" class="input__field input__field--hoshi" AutoPostBack="True" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Departement_Combo">
                                <span class="input__label-content input__label-content--hoshi">Département</span>
                            </label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Departement_Combo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="input input--hoshi" style="height: 52px; margin-top: -10px; border: none; border-bottom: 1px solid #000008;">
                    <asp:UpdatePanel ID="Update_ClasseCombo" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ClasseCombo" runat="server" Style="width: 120px; color: #000008;" class="input__field input__field--hoshi" AutoPostBack="True" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="ClasseCombo">
                                <span class="input__label-content input__label-content--hoshi">Classe</span>
                            </label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ClasseCombo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="input input--hoshi" style="height: 52px; margin-top: -10px; border: none; border-bottom: 1px solid #000008;">
                    <asp:UpdatePanel ID="Update_Cours_ComboBox" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Cours_ComboBox" runat="server" Style="width: 450px; color: #000008;" class="input__field input__field--hoshi" AutoPostBack="True" OnSelectedIndexChanged="Cours_ComboBox_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Cours_ComboBox">
                                <span class="input__label-content input__label-content--hoshi">Cours</span>
                            </label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Cours_ComboBox" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                    <asp:DropDownList ID="Operation_Combo" Font-Size="Medium" runat="server" AutoPostBack="True" class="input__field input__field--hoshi" Style="width: 400px; color: #000008;" OnSelectedIndexChanged="Operation_Combo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Operation_Combo">
                        <span class="input__label-content input__label-content--hoshi">Choisissez l’opération à exécuter </span>
                    </label>
                </div>
                <div style="width: 500px;">
                    <div>
                        <asp:MultiView ID="MyMultiView" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:UpdatePanel ID="Update_Operation_Combo_1" runat="server">
                                    <ContentTemplate>
                                        <div style="width: 100%; margin: auto; margin-top: 3px; transition: opacity 1s; -webkit-transition: opacity 1s;">
                                            <fieldset style="">
                                                <legend>LES PRESTATIONS EXISTANTS</legend>
                                                <asp:Panel ID="Horaire_Panel" runat="server">
                                                </asp:Panel>
                                            </fieldset>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Operation_Combo" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:UpdatePanel ID="Update_Operation_Combo_2" runat="server">
                                    <ContentTemplate>
                                        <div style="width: 100%; margin: auto; margin-top: 3px; transition: opacity 1s; -webkit-transition: opacity 1s;">
                                            <div style="float: left;">
                                                <fieldset>
                                                    <legend>CRENEAUX VALIDES</legend>
                                                    <div style="height: 350px; overflow: scroll;">
                                                        <asp:GridView ID="Jour_Prestation_ListView" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="id_prestation"
                                                            Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" HorizontalAlign="Center" CellPadding="2">
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <HeaderStyle BackColor="#eee3fc" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                            <RowStyle ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Size="Larger" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="grdv_ligne" runat="server" AutoPostBack="true" OnCheckedChanged="grdv_ligne_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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

                                                </fieldset>
                                            </div>
                                            <div style="margin-left: 5px; float: right;">
                                                <fieldset>
                                                    <legend>MATIERES ENSEIGNEES PAR CRENEAU</legend>
                                                    <div style="height: 350px; overflow: scroll;">
                                                        <asp:GridView ID="Matiere_Enseigne_ListView" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false"
                                                            ShowHeader="false" ShowFooter="True" DataKeyNames="id_prestation_details" Width="100%" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" HorizontalAlign="Center" CellPadding="2"
                                                            OnRowDeleting="Matiere_Enseigne_ListView_RowDeleting" OnRowCancelingEdit="Matiere_Enseigne_ListView_RowCancelingEdit"
                                                            OnRowEditing="Matiere_Enseigne_ListView_RowEditing" OnRowUpdating="Matiere_Enseigne_ListView_RowUpdating"
                                                            OnRowCommand="Matiere_Enseigne_ListView_RowCommand">

                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                                            <RowStyle ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Size="Larger" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="points" runat="server" Text='<%# Eval("pointAborde") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="point_aborde" runat="server" Text='<%# Eval("pointAborde") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="nouveauPoint" runat="server" placeholder="Point abordé"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                                                                        <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/deleteIcon.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton ID="Update_Button" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                                                                        <asp:ImageButton ID="Cancel_Button" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="AddNew_Button" ImageUrl="~/Images/AddNew.png" runat="server" CommandName="AddNew" ToolTip="Ajouter" Width="20px" Height="20px" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Operation_Combo" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:View>
                        </asp:MultiView>
                    </div>
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

        <asp:Label ID="llt" runat="server" Text=""></asp:Label>

        <cc1:ModalPopupExtender ID="mpe" PopupControlID="Panel1" TargetControlID="llt" CancelControlID="exit" runat="server" PopupDragHandleControlID="headerdiv"></cc1:ModalPopupExtender>

        <div id="Panel1" class="backgrnd">
            <center>
                <asp:Panel ID="Pa1" runat="server" CssClass="modalPopup" >
                      <div id="headerdiv" class="header" >
                          <asp:Label ID="jours_tt" runat="server" Text=""></asp:Label>
                      </div>
                      <div id="divdetails" class="details">
                          <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                                <asp:DropDownList ID="Operation_ComboBox" runat="server" class="input__field input__field--hoshi" Style="width: 300px; color: #000008" AutoPostBack="True" OnSelectedIndexChanged="Operation_ComboBox_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" Text="Choisissez"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Invalider une prestation"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Valider une prestation"></asp:ListItem>
                                </asp:DropDownList>
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Operation_ComboBox">
                                    <span class="input__label-content input__label-content--hoshi">Opération </span>
                                </label>
                            </div>

                          <div class="input input--hoshi">
                                <input runat="server" class="input__field input__field--hoshi" type="text" id="Debut_TextBox" style="width: 230px; color: #000008; background-color: transparent; border: none; border-bottom: 1px solid #000008;" />
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Debut_TextBox">
                                    <span class="input__label-content input__label-content--hoshi">Début </span>
                                </label>
                          </div>

                          <div class="input input--hoshi">
                                <input runat="server" class="input__field input__field--hoshi" type="text" id="Fin_TextBox" style="width: 230px; color: #000008; background-color: transparent; border: none; border-bottom: 1px solid #000008;" />
                                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="Fin_TextBox">
                                    <span class="input__label-content input__label-content--hoshi">Fin </span>
                                </label>
                          </div>
                      </div>
                      <div id="footerdiv" class="footer" >
                          <asp:Button id="exit" runat="server" Text="Quiter"/>
                          <asp:Button ID="Enregistre_Btn" runat="server" Text="Enregistrer" OnClick="Enregistre_Btn_Click" />
                      </div>
                </asp:Panel>
                </center>
        </div>
    </div>
</asp:Content>
