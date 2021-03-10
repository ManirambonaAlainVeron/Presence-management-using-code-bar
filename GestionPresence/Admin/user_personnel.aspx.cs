using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GestionPresence.Admin
{
    public partial class user_personnel : System.Web.UI.Page
    {
        MySqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                load_personnel();
                Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
            }
        }

        public static string getMD5(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] resulta = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < resulta.Length; i++)
            {
                str.Append(resulta[i].ToString("x2"));
            }
            return str.ToString();
        }

        private void load_personnel()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                String req = "select id_personnel, nom, prenom, matricule from personnel";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                combo_personnel.Items.Clear();

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "personnel";
                combo_personnel.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1)+" "+dr.GetString(2)+" "+dr.GetString(3);
                    combo_personnel.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des personnels')</script>");
                return;
            }
        }

        protected void Enregistrer_btn_Click(object sender, EventArgs e)
        {
            if(combo_personnel.SelectedValue == "-1" || combo_etat .SelectedValue == "-1" || combo_profil.SelectedValue == "-1" || txt_login.Value =="" || txt_password.Value == "" || txt_confirmation.Value =="")
            {
                Response.Write("<script>alert('Completez tous les champs')</script>");
                return;
            }
            else
            {
                if(txt_password.Value != txt_confirmation.Value)
                {
                    Response.Write("<script>alert('Le mot de passe et la confirmation ne sont pas identique')</script>");
                    txt_password.Value = "";
                    txt_confirmation.Value = "";
                    return;
                }
                else
                {
                    conn = new MySqlConnection(Authentification.MyString);
                    conn.Open();
                    String req2 = "insert into utilisateur(id_personnel, user_type, login, password, etat)values(@id_personnel, @user_type, @login, @password, @etat)";
                    MySqlCommand cmd = new MySqlCommand(req2, conn);
                    cmd.Parameters.AddWithValue("@id_personnel",combo_personnel.SelectedValue);
                    cmd.Parameters.AddWithValue("@user_type",combo_profil.SelectedValue);
                    cmd.Parameters.AddWithValue("@login", txt_login.Value);
                    cmd.Parameters.AddWithValue("@password", getMD5(txt_password.Value));
                    cmd.Parameters.AddWithValue("@etat", combo_etat.SelectedValue);

                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        Response.Write("<script>alert('Enregistrement reussie !')</script>");
                        Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
                        combo_personnel.SelectedValue = "-1";
                        combo_profil.SelectedValue = "-1";
                        combo_etat.SelectedValue = "-1";
                        txt_login.Value = "";
                        txt_password.Value = "";
                        return;
                    }
                    else
                    {
                        Response.Write("<script>alert('Echec')</script>");
                        return;
                    }
                    
                }
            }
        }

        protected void utilisateur_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string rqt = "DELETE FROM utilisateur WHERE id_utilisateur=@id_utilisateur";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_utilisateur", Convert.ToInt32(utilisateur_grid.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script>alert('Suppression reussie')</script>");
                MatriculeTextbox.Text = "";
                Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec')</script>");
                Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
                return;
            }

        }

        protected void utilisateur_grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            utilisateur_grid.EditIndex = e.NewEditIndex;
            Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
        }

        protected void utilisateur_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string profil = (utilisateur_grid.Rows[e.RowIndex].FindControl("profil_DropDown") as DropDownList).SelectedValue.Trim();
                string etat = (utilisateur_grid.Rows[e.RowIndex].FindControl("etat_DropDown") as DropDownList).SelectedValue.Trim();
                
                if (profil.Length > 0 && etat.Length > 0)
                {
                    string rqt = "UPDATE utilisateur SET user_type=@user_type, etat=@etat WHERE id_utilisateur=@id_utilisateur";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_utilisateur", Convert.ToInt32(utilisateur_grid.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@user_type", profil);
                    cmd.Parameters.AddWithValue("@etat", etat);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    utilisateur_grid.EditIndex = -1;
                    Response.Write("<script>alert('Modification réussie')</script>");
                    Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' Echec de modification')</script>");
                return;
            }
        }

        protected void utilisateur_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            utilisateur_grid.EditIndex = -1;
            Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
        }

        protected void Search_Button_Click(object sender, ImageClickEventArgs e)
        {
            if (MatriculeTextbox.Text == "")
            {
                Response.Write("<script>alert(' Entrez la matricule')</script>");
                return;
            }
            else
            {
                Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel where matricule = '" + MatriculeTextbox.Text + "'", utilisateur_grid);
                return;
            }
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Base.charger_GridView(sender, e, "select id_utilisateur, concat(nom,' ',prenom)as utilisateur, matricule, user_type, login, etat from utilisateur inner join personnel on utilisateur.id_personnel = personnel.id_personnel ORDER BY id_utilisateur DESC LIMIT 20", utilisateur_grid);
            MatriculeTextbox.Text = "";
        }
    }
}