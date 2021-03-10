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


namespace GestionPresence
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        MySqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void modifier_btn_Click(object sender, EventArgs e)
        {
            if(txt_login.Value == "" || txt_password.Value == "" || txt_new_password.Value == "" || txt_confirmation.Value == "" )
            {
                Response.Write("<script>alert('Completez tous les champs !')</script>");
                return;
            }
            else
            {
                con = new MySqlConnection(Authentification.MyString);
                string req = "select count(*) from utilisateur where login=@login and password=@password";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(req, con);
                cmd.Parameters.AddWithValue("@login", txt_login.Value);
                cmd.Parameters.AddWithValue("@password", Admin.user_personnel.getMD5(txt_password.Value));
                int nbr = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (nbr == 0)
                {
                    Response.Write("<script>alert('Echec, identification inconnue !')</script>");
                    return;
                }
                else
                {
                    if (txt_new_password.Value != txt_confirmation.Value)
                    {
                        Response.Write("<script>alert('Le mot de passe et la confirmation ne sont pas identique')</script>");
                        txt_new_password.Value = "";
                        txt_confirmation.Value = "";
                        return;
                    }
                    else
                    { 
                        con = new MySqlConnection(Authentification.MyString);
                        con.Open();
                        string rqt = "UPDATE utilisateur SET password=@newpassword WHERE login=@login and password=@password";
                        MySqlCommand cmd1 = new MySqlCommand(rqt, con);
                        cmd1.Parameters.AddWithValue("@newpassword", Admin.user_personnel.getMD5(txt_new_password.Value));
                        cmd1.Parameters.AddWithValue("@password", Admin.user_personnel.getMD5(txt_password.Value));
                        cmd1.Parameters.AddWithValue("@login", txt_login.Value);
                        cmd1.ExecuteNonQuery();
                        Response.Write("<script>alert('Modification reussie !')</script>");
                        txt_new_password.Value = "";
                        txt_confirmation.Value = "";
                        txt_password.Value = "";
                        txt_login.Value = "";
                        con.Close();
                    }
                }
            }
        }
    }
}