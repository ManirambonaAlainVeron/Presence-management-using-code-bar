using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionPresence
{
    public partial class d_acad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void doyen_button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Doyen/pointage_statistique.aspx");
        }

        protected void d_Acad_button_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Directeur_academique/annee.aspx");
        }
    }
}