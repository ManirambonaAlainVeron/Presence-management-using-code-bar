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
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace GestionPresence.Etudiant
{
    public partial class pointage : System.Web.UI.Page
    {
        MySqlConnection con;
        public static string numero = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void load_pointage()
        {
            con = new MySqlConnection(Authentification.MyString);
            con.Open();
            string requete = "SELECT etudiant.nom, etudiant.prenom, faculte.faculte, departement.Departement, classe.Classe, pointage.date, pointage.heure_entre, pointage.heure_sortie"
                               + " FROM etudiant INNER JOIN etudiant_inscription ON etudiant.id_etudiant = etudiant_inscription.id_etudiant"
                               + " INNER JOIN faculte ON etudiant_inscription.id_faculte = faculte.id_faculte"
                                + " INNER JOIN departement ON etudiant_inscription.id_departement = departement.id_departement INNER join classe ON   etudiant_inscription.id_classe = classe.id_classe"
                                + " INNER JOIN pointage WHERE etudiant_inscription.id_inscription = pointage.id_inscription and pointage.date=@dat AND etudiant_inscription.id_inscription=@id_ins ORDER BY heure_entre DESC LIMIT 1 ;";
            MySqlCommand c = new MySqlCommand(requete, con);
            c.Parameters.AddWithValue("@dat", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
            c.Parameters.AddWithValue("@id_ins", numero);
            MySqlDataReader d = c.ExecuteReader();
            pointage_grid.DataSource = d;
            pointage_grid.DataBind();
            con.Close();
        }

        protected void num_pointe_TextChanged(object sender, EventArgs e)
        {
                numero = "";
                int id_annee, id_departement, id_classe, id_faculte;
                con = new MySqlConnection(Authentification.MyString);
                con.Open();
                string req = "SELECT id_inscription, id_classe, id_departement, id_faculte,id_annee from etudiant_inscription WHERE num_inscription = @num";
                MySqlCommand cmd = new MySqlCommand(req, con);
                cmd.Parameters.AddWithValue("@num", num_pointe.Text);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    numero = dr.GetString(0);
                    id_classe = dr.GetInt32(1);
                    id_departement = dr.GetInt32(2);
                    id_faculte = dr.GetInt32(3);
                    id_annee = dr.GetInt32(4);
                    dr.Close();

                    string reqt = "select count(*) from horaire where id_annee = @anne and"
                                + " id_faculte = @faculte and id_departement = @departement and id_classe = @classe and date = @date";
                    MySqlCommand cmde = new MySqlCommand(reqt, con);
                    cmde.Parameters.AddWithValue("@anne", id_annee);
                    cmde.Parameters.AddWithValue("@faculte", id_faculte);
                    cmde.Parameters.AddWithValue("@departement", id_departement);
                    cmde.Parameters.AddWithValue("@classe", id_classe);
                    cmde.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd-MM-yyyy"));
                    int result = Convert.ToInt32(cmde.ExecuteScalar());
                    if (result != 0)
                    {
                        string rqt = "SELECT *FROM pointage WHERE id_inscription=@num and date=@date ORDER BY id_pointage DESC LIMIT 1;";
                        MySqlCommand comand = new MySqlCommand(rqt, con);
                        comand.Parameters.AddWithValue("@num", numero);
                        comand.Parameters.AddWithValue("@date", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                        MySqlDataReader d_r = comand.ExecuteReader();
                        if (d_r.Read())
                        {
                            if (d_r.GetString(4).Equals("00:00:00"))
                            {

                                string rq = "update pointage set heure_sortie=@heure_sortie where id_pointage = @id_pointage";
                                MySqlCommand cmed = new MySqlCommand(rq, con);
                                cmed.Parameters.AddWithValue("@heure_sortie", DateTime.Now);
                                cmed.Parameters.AddWithValue("@id_pointage", d_r.GetInt32(0));
                                d_r.Close();
                                cmed.ExecuteNonQuery();


                            }
                            else
                            {
                                d_r.Close();
                                string req_insert = "insert into pointage(id_inscription, date, heure_entre)values(@num, @date, @heure_entre)";
                                MySqlCommand cm = new MySqlCommand(req_insert, con);
                                cm.Parameters.AddWithValue("@num", numero);
                                cm.Parameters.AddWithValue("@date", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                                cm.Parameters.AddWithValue("@heure_entre", DateTime.Now);
                                cm.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            d_r.Close();
                            string req_insert = "insert into pointage(id_inscription, date, heure_entre)values(@num, @date, @heure_entre)";
                            MySqlCommand cmt = new MySqlCommand(req_insert, con);
                            cmt.Parameters.AddWithValue("@num", numero);
                            cmt.Parameters.AddWithValue("@date", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                            cmt.Parameters.AddWithValue("@heure_entre", DateTime.Now);
                            cmt.ExecuteNonQuery();


                        }

                    }
                    else
                    {
                        Response.Redirect("pointage.aspx");
                        return;
                    }

                }
                else
                {
                    dr.Close();
                }



                con.Close();
                num_pointe.Text = "";
                load_pointage();
                return;
            }


        
    }
}