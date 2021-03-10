using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionPresence.Directeur_academique
{
    public partial class DirecteurMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_utlilisateur.Text = Authentification.nom + " " + Authentification.prenom;
            }
        }
    }
}