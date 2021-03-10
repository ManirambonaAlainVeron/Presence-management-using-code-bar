using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO.MemoryMappedFiles;
using System.Diagnostics.CodeAnalysis;

namespace BMDSysWeb
{
    public partial class EnseignementsMasterPage : System.Web.UI.MasterPage
    {
        int id_user;
        string nom_user, prenom_user, module_table;
        public static int id_module = -1;
        
        public int[] saveconfi = new int[30];
        public static int id_annee = -1, id_perso = -1, id_statistique = -1, annee_precise;
        public static string annee, date_op;

        public int type = -1;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id_user = LoginForm.id_user;
                nom_user = LoginForm.user_nom;
                prenom_user = LoginForm.user_prenom;
                module_table = LoginForm.module_table;

                User_Label.Text = prenom_user + " " + nom_user;
                id_module = 2;
                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (LoginForm.module_ID[i] == "-1")
                            {
                                Admin_MenuItem.Visible = false;
                            }
                            break;
                        case 1:
                            if (LoginForm.module_ID[i] == "-1")
                            {
                                Finance_MenuItem.Visible = false;
                            }
                            break;
                        case 3:
                            if (LoginForm.module_ID[i] == "-1")
                            {
                                Scolarite_MenuItem.Visible = false;
                            }
                            break;
                        case 4:
                            if (LoginForm.module_ID[i] == "-1")
                            {
                                Rapports_MenuItem.Visible = false;
                            }
                            break;
                        case 5:
                            if (LoginForm.module_ID[i] == "-1")
                            {
                                Bibliotheque_MenuItem.Visible = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void setType2Student(object sender, EventArgs e)
        {
            type = 1;
        }
        public void setType2Personnel(object sender, EventArgs e)
        {
            type = 2;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string message = "Ok man";
            ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('" + message + "');", true);
            Response.Redirect("/user_profile.aspx");
        }
        
    }
}