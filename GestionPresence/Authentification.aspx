<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authentification.aspx.cs" Inherits="GestionPresence.Authentification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>BMDSys</title>



    
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link rel="stylesheet" href="Styles/AuthentificationStyle.css"/>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body style="background-color:rgb(218,218,182)" >
    <form id="form1" runat="server">
        <div class="wrapper fadeInDown" style="margin-top:85px">
              <div id="formContent">
                <div style="margin-top:60px">
                    <div >
                        <asp:TextBox ID="login_txt" TextMode="SingleLine"   runat="server" autocomplete="off" placeholder="Quel est votre identite?" style="border:solid 1px white; border-bottom:solid 3px #042331" Height="40px" Width="340px" Font-Size="Medium"></asp:TextBox><br />
                        <asp:TextBox ID="password_txt" TextMode="Password" runat="server" autocomplete="off" placeholder="Quel est votre mot de passe?" style=" border:solid 1px white; border-bottom:solid 3px #042331; margin-top:80px" Height="40px" Width="340px" Font-Size="Medium"></asp:TextBox>
                    </div><br />
                  <asp:ImageButton ID="connect_btn" runat="server" ImageUrl="~/Images/connect.png" style="margin-top:30px" OnClick="connect_btn_Click" ToolTip="Se connecter"/>
                </div>
                <div id="formFooter">
                  <a class="underlineHover" href="ChangePassword.aspx">Changer le mot de passe</a>
                </div>

              </div>
         </div>

        <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target=".bd-example-modal-sm">Small modal</button>--%>


        <div class="modal fade bd-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-sm">
            <div class="modal-content">
              ...
            </div>
          </div>
        </div>
    </form>
</body>
</html>
