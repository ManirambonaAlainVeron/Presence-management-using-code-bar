<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="unite_enseignement.aspx.cs" Inherits="BMDSysWeb._Enseignements.unite_enseignement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">

    <div runat="server" id="MainDiv" style="width: 700px; height: auto; background-color: #ccdcee; margin: 50px auto; padding: 0px 10px 10px 10px; position: relative; border: double; border-color: #3399cc">
        <div style="width: 695px; height: 30px; margin: 0px 0px 20px -10px; background-color: #3399cc; color: white;">
            <span style="display: inline-block; position: relative; margin: auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire de gestion des UE</span>
            <asp:Button ID="Edit_Button" runat="server" Text="&#10060;" Style="display: inline-block; position: relative; float: right; padding: 0px; background-color: khaki" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" />
        </div>


        <div class="content_element">

            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Annee_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 150px; color: #000008;" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="Annee_Combo_SelectedIndexChanged">
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="EtatCivil_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">A-A</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Faculte_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 500px; color: #000008;" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="Faculte_Combo_SelectedIndexChanged">
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="EtatCivil_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Faculte ou Institut</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="Departement_Combo" runat="server" class="input__field input__field--hoshi" Style="width: 440px; color: #000008;" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="Departement_Combo_SelectedIndexChanged">
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="EtatCivil_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Departement</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="ClasseCombo" runat="server" class="input__field input__field--hoshi" Style="width: 100px; color: #000008;" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ClasseCombo_SelectedIndexChanged">
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="EtatCivil_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Classe</span>
                </label>
            </div>
            <div class="input input--hoshi" style="height: 52px; margin-top: -13px; border: none; border-bottom: 1px solid #000008;">
                <asp:DropDownList ID="SemestreCombo" runat="server" class="input__field input__field--hoshi" Style="width: 100px; color: #000008;" AppendDataBoundItems="True" OnSelectedIndexChanged="SemestreCombo_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
                <label class="input__label input__label--hoshi input__label--hoshi-color-1" for="EtatCivil_ComboBox">
                    <span class="input__label-content input__label-content--hoshi">Semestre</span>
                </label>
            </div>

            <div style="margin-top: 10px">
                <asp:Label ID="Label_Success_Message" runat="server" Text="" ForeColor="Blue"></asp:Label>
                <asp:Label ID="Label_Error_Message" runat="server" Text="" ForeColor="Red"></asp:Label>
                <asp:GridView ID="GDV_Creation" Style="margin: 0 auto;" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowFooter="true" DataKeyNames="id_unite"
                    Width="100%" BackColor="White" BorderColor="CadetBlue" BorderStyle="None" HorizontalAlign="Center" CellPadding="3"
                    OnRowCancelingEdit="GDV_Creation_RowCancelingEdit"
                    OnRowEditing="GDV_Creation_RowEditing"
                    OnRowUpdating="GDV_Creation_RowUpdating" OnRowCommand="GDV_Creation_RowCommand" OnRowDeleting="GDV_Creation_RowDeleting">

                    <FooterStyle BackColor="#ccdcee" ForeColor="#000066" />
                    <HeaderStyle BackColor="Khaki" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    <Columns>
                        <asp:TemplateField HeaderText="Nom de l'unite">
                            <ItemTemplate>
                                <asp:Label ID="label_salle" runat="server" Width="430px" Text='<%# Eval("unite") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="nom_TextBox" runat="server" Width="430px" Text='<%# Eval("unite") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NomUE_TextBox_Footer" runat="server" Width="430px" placeholder="Nouvelle UE"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="Label_code" runat="server" Width="80px" Text='<%# Eval("code_unite") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="code_TextBox" runat="server" Width="80px" Text='<%# Eval("code_unite") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="CodeUE_TextBox_Footer" runat="server" Width="80px" placeholder="Code UE"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="&#9889;">
                            <ItemTemplate>
                                <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/editIcon.png" runat="server" CommandName="Edit" ToolTip="Modifier" Width="20px" Height="20px" />
                                <asp:ImageButton ID="Delete_Button" ImageUrl="~/Images/deleteIcon.png" runat="server" CommandName="Delete" ToolTip="Supprimer" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="Update_Button" ImageUrl="~/Images/saveIcon.png" runat="server" CommandName="Update" ToolTip="Actualiser" Width="20px" Height="20px" />
                                <asp:ImageButton ID="Cancel_Button" ImageUrl="~/Images/CancelIcon.png" runat="server" CommandName="Cancel" ToolTip="Abandonner" Width="20px" Height="20px" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="AddNew_Button" runat="server" Text="&#10010;" CommandName="AddNew" ToolTip="Ajouter" Style="padding: 0px" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

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
</asp:Content>
