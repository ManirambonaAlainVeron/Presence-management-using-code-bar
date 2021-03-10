using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Threading;
using System.IO;

namespace GestionPresence
{
    public partial class Authentification : System.Web.UI.Page
    {
        public static string MyString = ConfigurationManager.ConnectionStrings["LocalConn"].ConnectionString;
        public static string resulta = "", nom="", prenom="";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

       

        protected void connect_btn_Click(object sender, ImageClickEventArgs e)
        {
            if (login_txt.Text == "" && password_txt.Text == "")
            {
                Response.Write("<script>alert('Entrez votre identifiant et mot de passe !')</script>");
                return;
            }
            else if(login_txt.Text == "")
            {
                Response.Write("<script>alert('Entrez votre identifiant !')</script>");
                return;
            }
            else if (password_txt.Text == "")
            {
                Response.Write("<script>alert('Entrez votre mot de passe !')</script>");
                return;
            }
            else
            {
                MySqlConnection con = new MySqlConnection(Authentification.MyString);
                string req = "select count(*) from utilisateur where login=@login and password=@password";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(req, con);
                cmd.Parameters.AddWithValue("@login", login_txt.Text);
                cmd.Parameters.AddWithValue("@password",Admin.user_personnel.getMD5(password_txt.Text));
                int nbr = Convert.ToInt32(cmd.ExecuteScalar());
                
                if(nbr == 0)
                {
                    Response.Write("<script>alert('Echec de connexion, utilisateur inconnue !')</script>");
                }
                else
                {
                    string req3 = "select count(*) from utilisateur where login=@login and password=@password and etat='activer'";
                    MySqlCommand cmd3 = new MySqlCommand(req3, con);
                    cmd3.Parameters.AddWithValue("@login", login_txt.Text);
                    cmd3.Parameters.AddWithValue("@password", Admin.user_personnel.getMD5(password_txt.Text));
                    int num = Convert.ToInt32(cmd.ExecuteScalar());

                    if (num == 0)
                    {
                        Response.Write("<script>alert('Votre compte n'est pas actif, contacter votre administrateur !')</script>");
                    }
                    else
                    {
                            string req1 = "select user_type, nom,prenom from utilisateur inner join personnel on utilisateur.id_personnel=personnel.id_personnel where login=@login and password=@password";
                            
                            MySqlCommand cmd1 = new MySqlCommand(req1, con);
                            cmd1.Parameters.AddWithValue("@login", login_txt.Text);
                            cmd1.Parameters.AddWithValue("@password", Admin.user_personnel.getMD5(password_txt.Text));
                            MySqlDataReader dr = cmd1.ExecuteReader();
                            while (dr.Read())
                            {
                                resulta = dr.GetString(0);
                                nom = dr.GetString(1);
                                prenom = dr.GetString(2);
                            }
                            dr.Close();

                            if (resulta == "administrateur")
                            {
                                Session["user"] = login_txt.Text;
                                Response.Redirect("Admin/personnel_enregistrement.aspx");
                            }
                            else if (resulta == "Delegue")
                            {
                                Session["user"] = login_txt.Text;
                                Response.Redirect("Etudiant/prestations.aspx");
                            }
                            else if (resulta == "Doyen")
                            {
                                Session["user"] = login_txt.Text;
                                Response.Redirect("Doyen/pointage_statistique.aspx");
                            }
                            else if (resulta == "Directeur")
                            {
                                Session["user"] = login_txt.Text;
                                Response.Redirect("d_acad.aspx");
                            }
                            else
                            {
                                Session["user"] = login_txt.Text;
                                Response.Redirect("Secretaire_charg_inscr/etudiant_enregistrement.aspx");
                            }

                    }
                }
                con.Close();
            }
        }


    }
}