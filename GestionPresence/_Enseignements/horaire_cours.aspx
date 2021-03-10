<%@ Page Title="" Language="C#" MasterPageFile="~/_Enseignements/EnseignementsMasterPage.Master" AutoEventWireup="true" CodeBehind="horaire_cours.aspx.cs" Inherits="BMDSysWeb._Enseignements.horaire_cours" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    
    <div runat="server" id="MainDiv" style="width: 785px; height: auto; background-color: #ccdcee; margin: 50px auto; padding: 0px 10px 10px 10px; position: relative; border:double;border-color:#3399cc">
       <div style="width: 780px; height: 30px;margin:0px 0px 20px -10px; background-color: #3399cc; color:white;">
            <span style="display:inline-block; position:relative;margin:auto; padding: 5px; text-align: center; text-decoration: none;">Formulaire d’identification de l’institution universitaire</span>
            <asp:ImageButton ID="Edit_Button" ImageUrl="~/Images/exitIcon.png" runat="server" Style=" display:inline-block; position:relative; float:right; text-align: center; text-decoration: none;" OnClick="ExitButton_Click" ToolTip="Quitter ce formulaire" Width="29px" Height="29px" ImageAlign="Middle" BorderStyle="None" />
        </div>
        <div class="content_element">
 
            
                <div style="text-align:center">
                    <div class="input input--hoshi">
                        <asp:DropDownList ID="Operation_ComboBox" runat="server"  Width="300px">
                            <asp:ListItem Value="-1" Text="Choisir l'operation"></asp:ListItem>
                        </asp:DropDownList>
                    </div><br /><br />

                    <div class="input input--hoshi">
                        <input runat="server" class="input__field input__field--hoshi" type="text" id="Cours_TextBox" style="width: 300px;" />
                        <label class="input__label input__label--hoshi input__label--hoshi-color-4" for="Cours_TextBox">
                            <span class="input__label-content input__label-content--hoshi">cours</span>
                        </label>
                    </div><br /><br />
                    <div class="input input--hoshi">
                        <input runat="server" class="input__field input__field--hoshi" type="text" id="Salle_TextBox" style="width: 300px;" />
                        <label class="input__label input__label--hoshi input__label--hoshi-color-4" for="Salle_TextBox">
                            <span class="input__label-content input__label-content--hoshi">salle</span>
                        </label>
                    </div><br /><br />
                    
                </div>
         
            <div style="text-align:center">
                <asp:Button ID="EnregistrerButton" runat="server" Text="Enregistrer" Width="150px" Height="35px" Style="padding: 1px"/> 
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
        </div></div>
</asp:Content>
