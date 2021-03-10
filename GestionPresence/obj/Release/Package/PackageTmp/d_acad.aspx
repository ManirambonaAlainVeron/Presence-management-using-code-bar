<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d_acad.aspx.cs" Inherits="GestionPresence.d_acad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

        <link rel="stylesheet" href="~/Styles/MasterPageStyle.css"/>
        <link rel="stylesheet"  href="~/Styles/fontawesome/css/all.css"/>
        <link rel="stylesheet" href="~/Styles/fontawesome/css/bootstrap.min.css" />
        <link rel="stylesheet" type="text/css" href="../Styles/modalPopup/popup_horaire.css" />

        <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
        <link rel="stylesheet" href="Styles/AuthentificationStyle.css"/>
        <script type="text/javascript">
            function preventBack() { window.history.forward(); }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
        </script>
</head>
<body  style="background-color:rgb(218,218,182)">
    <form id="form1" runat="server">
    <div>
        <div style="margin-top:120px"></div>

        <div class="row" style="text-align:center">
            <div class="col-2"></div>
            <div class="col-3">    
                <asp:ImageButton ID="doyen_button" runat="server" ImageUrl="~/Images/books.png" OnClick="doyen_button_Click"/>
            </div>
            <div class="col-2"></div>
            <div class="col-3">    
                <asp:ImageButton ID="d_Acad_button" runat="server" ImageUrl="~/Images/books.png" OnClick="d_Acad_button_Click"/>
            </div>
        </div>
        <div class="row" style="text-align:center">
            <div class="col-2"></div>
            <div class="col-3">
                    <h4><asp:Label ID="Label2" runat="server" Text="Droit aux faculté" Font-Bold="True"></asp:Label></h4>
            </div>
            <div class="col-2"></div> 
              <div class="col-3">  
                  <h4><asp:Label ID="Label1" runat="server" Text="Dédié au directeur academique" Font-Bold="True"></asp:Label></h4>
            </div>
         </div>
    </div>
    </form>
</body>
</html>
