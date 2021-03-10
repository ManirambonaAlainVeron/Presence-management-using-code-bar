<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckPage.aspx.cs" Inherits="BMDSysWeb._Enseignements.CheckPage" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title></title>
    <link rel='stylesheet prefetch' href='https://cdn.jsdelivr.net/foundation/5.0.2/css/foundation.css'>
    <link href="CheckPageStyle/mtree.css" rel="stylesheet" type="text/css">
    <style>
        .upperDiv {
            width: 100%;
            height: 40px;
            background-color: #111;
            position: relative;
            display: inline-block;
        }

        .leftDiv {
            margin-top: 10px;
            margin-left: 5px;
            width: 335px;
            float: left;
            height: calc(100vh - 45px);
            height: -moz-calc(100vh - 45px);
            height: -webkit-calc(100vh - 45px);
            max-height: -moz-calc(100vh - 45px);
            max-height: -webkit-calc(100vh - 45px);
            max-height: calc(100vh - 45px);
            background-color: #BBB;
        }

        .rightDiv {
            width: calc(100% - 350px);
            float: right;
            margin-right: 5px;
            margin-top: 10px;
            height: calc(100vh - 45px);
            height: -moz-calc(100vh - 45px);
            height: -webkit-calc(100vh - 45px);
            max-height: -moz-calc(100vh - 45px);
            max-height: -webkit-calc(100vh - 45px);
            max-height: calc(100vh - 40px);
            background-color: #BBB;
        }

        .bottomDiv {
            position: fixed;
            bottom: 0px;
            width: 100%;
            background-color: #AAA;
            height: 25px;
        }
    </style>
</head>

<body style="background-color: #111">
    <div class="leftDiv">
        <ul class="mtree jet">
            <li><a href="#">&#128107; Etudiants et Enseignants</a>
                <ul>
                    <li><a href="etudiant_enregistrement.aspx">&#9592;Enregistrement des etudiants</a></li>
                    <li><a href="documents_par_session.aspx">&#9592;Gestion des documents</a></li>
                    <li><a href="etudiant_inscription.aspx">&#9592;Inscription des etudiants</a></li>
                    <li><a href="personnel_enregistrement.aspx">&#9592;Enregistrement du personnel</a></li>
                </ul>
            </li>
            <li><a href="#">&#128221; Gestion des enseignements</a>
                <ul>
                    <li><a href="#">Cours et UE</a>
                        <ul>
                            <li><a href="unite_enseignement.aspx">&#9592;Creation des UE</a></li>
                            <li><a href="cours_creation.aspx">&#9592;Création du cours</a></li>
                            <li><a href="cours_attribution.aspx">&#9592;Attribution du cours</a></li>
                            <li><a href="cours_etat_enseignement.aspx">&#9592;Etat d'enseignement</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Suivi des enseignements</a>
                        <ul>
                            <li><a href="salle_creation.aspx">&#9592;Creation des salles</a></li>
                            <li><a href="horaire.aspx">&#9592;Création de l'horaire</a></li>
                            <li><a href="prestations.aspx">&#9592;Validation des prestattions</a></li>
                            <li><a href="Imprimer_horaire.aspx">&#9592;Imprimer l'horaire</a></li>
                            <li><a href="#">&#9592;Imprimer les matieres enseignes</a></li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li><a href="#">&#9803; Switcher vers</a>
                <ul>
                    <li runat="server" id="Admin_MenuItem"><a href="/_AdminSI/emptyAdmin.aspx">&#9592;Administration du SI</a></li>
                    <li runat="server" id="Scolarite_MenuItem"><a href="/_Scolarite/emptyScolarite.aspx">&#9592;Service Etudiants & Scolarité</a></li>
                    <li runat="server" id="Finance_MenuItem"><a href="/_Recouvrement/emptyRecouvrement.aspx">&#9592;Recouvrement des frais</a></li>
                    <li runat="server" id="Bibliotheque_MenuItem"><a href="/_Bibliotheque/emptyBibliotheque.aspx">&#9592;Bibliothèque numérique</a></li>
                    <li runat="server" id="Rapports_MenuItem"><a href="/_Rapports_Statistiques/emptyRapports.aspx">&#9592;Rapports et Statistiques</a></li>
                </ul>
            </li>
            <li><a href="#">&#128272; Compte d'utilisateur</a>
                <ul>
                    <li class="classLi"><a href="#">
                        <asp:Label ID="User_Label" runat="server" ForeColor="#060624"></asp:Label></a></li>
                    <li class="classLi"><a href="/user_profile.aspx">&#9785; Profile</a></li>
                    <li class="classLi"><a href="/PWDChange.aspx">&#128273; Password</a></li>
                    <li class="classLi"><a href="/LoginForm.aspx">&#9851; Log out</a></li>
                </ul>
            </li>
        </ul>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <script src='http://cdnjs.cloudflare.com/ajax/libs/velocity/0.2.1/jquery.velocity.min.js'></script>
        <script src="CheckPageStyle/mtree.js"></script>
    </div>
    <div class="rightDiv">
    </div>
    <div class="bottomDiv"></div>
</body>
</html>
