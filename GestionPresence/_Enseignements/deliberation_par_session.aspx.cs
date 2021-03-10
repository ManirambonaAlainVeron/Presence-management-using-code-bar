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

namespace BMDSysWeb._Enseignements
{
    public partial class deliberation_par_session : System.Web.UI.Page
    {
        MySqlConnection conn;
        public static int id_session = -1, id_departement = -1, id_classe = -1, id_annee = -1, id_faculte = -1, action = -1, id_cours = -1;
        public string etat_annee = "", etat_classe = "", nom_classe = "", nom_departement = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Annee();
               
                
            }
        }

        private void Load_Annee()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                String reqdgv = "SELECT id_annee, annee FROM annee WHERE etat_annee=@etat_annee";
                MySqlCommand cmd = new MySqlCommand(reqdgv, conn);
                cmd.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = cmd.ExecuteReader();

                //Annee_Combo.Items.Clear();

                //ListItem item0 = new ListItem();
                //item0.Value = "-1";
                //item0.Text = "Choisissez A-A";
                //Annee_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Annee_Combo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des annees academique')</script>");
                return;
            }
        }

        private void Load_Session()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string reqdgv = "SELECT DISTINCT id_session, session FROM session";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                MySqlDataReader dr = cmt.ExecuteReader();
                //Session_ComboBo.Items.Clear();

                //ListItem item0 = new ListItem();
                //item0.Value = "-1";
                //item0.Text = "Choisissez la session";
                //Faculte_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Session_ComboBo.Items.Add(item);
                    
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des session')</script>");
                return;
            }
        }

        private void Load_Faculte()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string reqdgv = "SELECT faculte_par_annee.id_faculte, faculte.faculte" +
                    " FROM faculte INNER JOIN faculte_par_annee ON faculte.id_faculte = faculte_par_annee.id_faculte" +
                    " WHERE faculte_par_annee.id_annee=@idaa";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                cmt.Parameters.AddWithValue("@idaa", id_annee);
                MySqlDataReader dr = cmt.ExecuteReader();
                //Faculte_Combo.Items.Clear();

                //ListItem item0 = new ListItem();
                //item0.Value = "-1";
                //item0.Text = "Choisissez la faculte";
                //Faculte_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Faculte_Combo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des facultes')</script>");
                return;
            }
        }

        private void Load_Departements()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string reqdgv = "SELECT DISTINCT user_departement.id_departement, departement.departement" +
                    " FROM (departement_par_faculte INNER JOIN departement ON departement_par_faculte.id_departement = departement.id_departement) INNER JOIN user_departement ON departement.id_departement = user_departement.id_departement" +
                    " WHERE departement_par_faculte.id_faculte=@id_faculte AND departement_par_faculte.id_annee=@id_annee";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                cmt.Parameters.AddWithValue("@id_annee", id_annee);
                cmt.Parameters.AddWithValue("@id_faculte", id_faculte);

                MySqlDataReader dr = cmt.ExecuteReader();
                //Departement_Combo.Items.Clear();

                //ListItem item0 = new ListItem();
                //item0.Value = "-1";
                //item0.Text = "Choisissez le departement";
                //Departement_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Departement_Combo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des departements')</script>");
                return;
            }
        }

        private void Load_Classe()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                String requette = "SELECT classe_par_departement.id_classe, classe.classe, classe_par_departement.etat_avancement" +
                    " FROM classe INNER JOIN classe_par_departement ON classe.id_classe = classe_par_departement.id_classe" +
                    " WHERE classe_par_departement.id_departement=@id_departement AND classe_par_departement.id_faculte=@id_faculte AND classe_par_departement.id_annee=@id_annee ";
                MySqlCommand com = new MySqlCommand(requette, conn);
                com.Parameters.AddWithValue("@id_departement", id_departement);
                com.Parameters.AddWithValue("@id_faculte", id_faculte);
                com.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader dr = com.ExecuteReader();
               // ClasseCombo.Items.Clear();
                //ListItem item0 = new ListItem();
                //item0.Value = "-1";
                //item0.Text = "Choisir la classe";
                //ClasseCombo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    //item.Value = dr.GetString(2);
                    ClasseCombo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des classes')</script>");
                return;
            }
        }

        private void Session_Reinitialiser()
        {
            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            string requette = "select count(*) from classe_deliberation where id_classe=@idclasse and id_session=@id_session and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
            MySqlCommand com = new MySqlCommand(requette, conn);
            com.Parameters.AddWithValue("@idclasse", id_classe);
            com.Parameters.AddWithValue("@id_departement", id_departement);
            com.Parameters.AddWithValue("@id_faculte", id_faculte);
            com.Parameters.AddWithValue("@id_annee", id_annee);
            com.Parameters.AddWithValue("@id_session", id_session);
            int res = Convert.ToInt32(com.ExecuteScalar());
            if (res == 0)
            {
                Response.Write("<script>alert('Annulation impossible, la délibération pas encore eu lieu pour ')</script>");
                return;
            }
            else
            {

                if (id_session == 1)
                {
                    string rqt = "DELETE FROM classe_deliberation WHERE id_classe=@idcl and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@idcl", id_classe);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.ExecuteNonQuery();

                    string rqt1 = " DELETE FROM deliberation WHERE id_classe=@idcl and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                    MySqlCommand cmd1 = new MySqlCommand(rqt1, conn);
                    cmd1.Parameters.AddWithValue("@idcl", id_classe);
                    cmd1.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd1.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd1.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd1.ExecuteNonQuery();

                    string rqt2 = " DELETE FROM etudiant_inscription WHERE id_classe=@idcl and id_departement=@id_departement and id_session=@id_session and id_faculte=@id_faculte and id_annee=@id_annee";
                    MySqlCommand cmd2 = new MySqlCommand(rqt2, conn);
                    cmd2.Parameters.AddWithValue("@id_session", 2);
                    cmd2.Parameters.AddWithValue("@idcl", id_classe);
                    cmd2.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd2.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd2.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd2.ExecuteNonQuery();

                    string rqt3 = " DELETE etudiant_note FROM (unite INNER JOIN cours ON unite.id_unite = cours.id_unite) INNER JOIN etudiant_note ON cours.id_cours = etudiant_note.id_cours" +
                        " WHERE  unite.id_annee=@id_annee and unite.id_faculte=@id_faculte and unite.id_departement=@id_departement and unite.id_classe=@idcl  and etudiant_note.id_session=@id_session";
                    MySqlCommand cmd3 = new MySqlCommand(rqt3, conn);
                    cmd3.Parameters.AddWithValue("@id_session", 2);
                    cmd3.Parameters.AddWithValue("@idcl", id_classe);
                    cmd3.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd3.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd3.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd3.ExecuteNonQuery();

                    string rqt4 = "DELETE etudiant_note_complement FROM (unite INNER JOIN cours ON unite.id_unite = cours.id_unite) INNER JOIN etudiant_note_complement ON cours.id_cours = etudiant_note_complement.id_cours" +
                        " WHERE unite.id_annee=@id_annee and unite.id_faculte=@id_faculte and unite.id_classe=@idcl and unite.id_departement=@id_departement";
                    MySqlCommand cmd4 = new MySqlCommand(rqt4, conn);
                    cmd4.Parameters.AddWithValue("@idcl", id_classe);
                    cmd4.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd4.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd4.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd4.ExecuteNonQuery();

                    string rq1 = "UPDATE classe_par_departement SET etat_avancement=@etat_avancement, date_cloture=@date_cloture WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cm1 = new MySqlCommand(rq1, conn);
                    cm1.Parameters.AddWithValue("@id_classe", id_classe);
                    cm1.Parameters.AddWithValue("@id_departement", id_departement);
                    cm1.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cm1.Parameters.AddWithValue("@id_annee", id_annee);
                    cm1.Parameters.AddWithValue("@etat_avancement", "Encours");
                    cm1.Parameters.AddWithValue("@date_cloture", "");
                    cm1.ExecuteNonQuery();
                }
                else if (id_session == 2)
                {
                    string rqt = "DELETE FROM classe_deliberation WHERE id_classe=@idcl and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee and id_session=@id_session";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@idcl", id_classe);
                    cmd.Parameters.AddWithValue("@id_session", id_session);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.ExecuteNonQuery();

                    string rqt1 = " DELETE FROM deliberation WHERE id_classe=@idcl and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee and id_session=@id_session";
                    MySqlCommand cmd1 = new MySqlCommand(rqt1, conn);
                    cmd1.Parameters.AddWithValue("@idcl", id_classe);
                    cmd1.Parameters.AddWithValue("@id_session", id_session);
                    cmd1.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd1.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd1.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd1.ExecuteNonQuery();

                    string rqt4 = "DELETE etudiant_note_complement FROM (unite INNER JOIN cours ON unite.id_unite = cours.id_unite) INNER JOIN etudiant_note_complement ON cours.id_cours = etudiant_note_complement.id_cours" +
                        " WHERE unite.id_annee=@id_annee and unite.id_faculte=@id_faculte and unite.id_classe=@idcl and unite.id_departement=@id_departement";
                    MySqlCommand cmd4 = new MySqlCommand(rqt4, conn);
                    cmd4.Parameters.AddWithValue("@idcl", id_classe);
                    cmd4.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd4.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd4.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd4.ExecuteNonQuery();
                }
                //MessageBox.Show("Opération reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
        }
        private void Session_Modifier()
        {
            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            string requette = "select count(*) from classe_deliberation where id_classe=@idclasse and id_session=@id_session and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
            MySqlCommand com = new MySqlCommand(requette, conn);
            com.Parameters.AddWithValue("@idclasse", id_classe);
            com.Parameters.AddWithValue("@id_departement", id_departement);
            com.Parameters.AddWithValue("@id_faculte", id_faculte);
            com.Parameters.AddWithValue("@id_annee", id_annee);
            com.Parameters.AddWithValue("@id_session", id_session);
            int res = Convert.ToInt32(com.ExecuteScalar());
            if (res == 0)
            {
                Response.Write("<script>alert('Modification impossible, la délibération pas encore eu lieu pour ')</script>");
                return;
            }
            else
            {
                string rqt = "UPDATE classe_deliberation SET date_deliberation=@date_deliberation WHERE id_classe=@idcl and id_session=@id_session and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@idcl", id_classe);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                cmd.Parameters.AddWithValue("@id_session", id_session);
                cmd.Parameters.AddWithValue("@date_deliberation", Date_TimePicker.Text);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Opération reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
        }
        private void Session_Cloturer()
        {
            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            if ((etat_classe == "Encours" && id_session == 1) || (etat_classe == "Clôturée" && id_session == 2))
            {
                string requette = "select count(*) from classe_deliberation where id_classe=@idclasse and id_session=@id_session and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                MySqlCommand com = new MySqlCommand(requette, conn);
                com.Parameters.AddWithValue("@idclasse", id_classe);
                com.Parameters.AddWithValue("@id_departement", id_departement);
                com.Parameters.AddWithValue("@id_faculte", id_faculte);
                com.Parameters.AddWithValue("@id_annee", id_annee);
                com.Parameters.AddWithValue("@id_session", id_session);
                int res = Convert.ToInt32(com.ExecuteScalar());
                if (res == 0)
                {
                    string rqt = "INSERT INTO classe_deliberation (date_deliberation,id_session, id_classe, id_departement, id_faculte, id_annee) VALUES (@date_deliberation,@id_session, @id_classe, @id_departement, @id_faculte, @id_annee)";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_classe", id_classe);
                    cmd.Parameters.AddWithValue("@id_session", id_session);
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.Parameters.AddWithValue("@date_deliberation", Date_TimePicker.Text);
                    cmd.ExecuteNonQuery();
                    if (id_session == 1)
                    {
                        string rq = "UPDATE classe_par_departement SET etat_avancement=@etat_avancement, date_cloture=@date_cloture WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                        MySqlCommand cm = new MySqlCommand(rq, conn);
                        cm.Parameters.AddWithValue("@id_classe", id_classe);
                        cm.Parameters.AddWithValue("@id_departement", id_departement);
                        cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cm.Parameters.AddWithValue("@id_annee", id_annee);
                        cm.Parameters.AddWithValue("@etat_avancement", "Clôturée");
                        cm.Parameters.AddWithValue("@date_cloture", Date_TimePicker.Text);
                        cm.ExecuteNonQuery();
                    }
                    //MessageBox.Show("Opération reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response.Write("<script>alert('Pour cette session, la délibération a déjà eu lieu')</script>");
                    return;
                }
            }
            conn.Close();
        }

        private void Deuxieme_Session()
        {
            int ecue_sous_sept_value = 0, redoub_autorise = 0, change_filiere_autorise = 0, max_ecue_nv_par_annee = 0, cumul_ecue_nv = 0;

            int nbre_etudiant, nbre_unite, nbre_cours, total_cours = 0;
            double ecue_note = 0, ecue_note_ponderee;
            int ecue_sous_sept = 0, ecue_rate = 0, ecue_nv = 0, ecue_complement = 0;
            double ue_note_ponderee = 0, ue_moyenne = 0, ue_note_ponderee_avant_2eme_session = 0;
            int ue_credit = 0, ue_nv = 0;
            double etu_note_ponderee = 0, etu_pourcentage = 0;
            int total_credit = 0;
            string justification = "", fraude = "", sanction = "", pourca = "";
            int nbre_change_filiere = 0, nbre_redoublement = 0;
            double notek = 0;

            int id_decision = -1, id_mention = -1;
            string decisio = "", sigl = "";

            string[] Unite_ID, Cours_ID, Cours_Credit, Unite_Credit, Etudiant_ID, Cours_Note_Par_Etudiant;

            string[] Classe_ID, Departement_ID, Departements;
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();



                string s6 = "SELECT valeur FROM  deliberation_parametre_generaux WHERE id_parametre=@id_parametre";
                MySqlCommand c6 = new MySqlCommand(s6, conn);
                c6.Parameters.AddWithValue("@id_parametre", 4);
                MySqlDataReader d6 = c6.ExecuteReader();
                while (d6.Read())
                {
                    ecue_sous_sept_value = d6.GetInt32(0);
                }
                d6.Close();

                string s7 = "SELECT valeur FROM  deliberation_parametre_generaux WHERE id_parametre=@id_parametre";
                MySqlCommand c7 = new MySqlCommand(s7, conn);
                c7.Parameters.AddWithValue("@id_parametre", 5);
                MySqlDataReader d7 = c7.ExecuteReader();
                while (d7.Read())
                {
                    redoub_autorise = d7.GetInt32(0);
                }
                d7.Close();

                string s8 = "SELECT valeur FROM  deliberation_parametre_generaux WHERE id_parametre=@id_parametre";
                MySqlCommand c8 = new MySqlCommand(s8, conn);
                c8.Parameters.AddWithValue("@id_parametre", 6);
                MySqlDataReader d8 = c8.ExecuteReader();
                while (d8.Read())
                {
                    change_filiere_autorise = d8.GetInt32(0);
                }
                d8.Close();

                string s9 = "SELECT valeur FROM  deliberation_parametre_generaux WHERE id_parametre=@id_parametre";
                MySqlCommand c9 = new MySqlCommand(s9, conn);
                c9.Parameters.AddWithValue("@id_parametre", 7);
                MySqlDataReader d9 = c9.ExecuteReader();
                while (d9.Read())
                {
                    max_ecue_nv_par_annee = d9.GetInt32(0);
                }
                d9.Close();

                string s10 = "SELECT valeur FROM  deliberation_parametre_generaux WHERE id_parametre=@id_parametre";
                MySqlCommand c10 = new MySqlCommand(s10, conn);
                c10.Parameters.AddWithValue("@id_parametre", 8);
                MySqlDataReader d10 = c10.ExecuteReader();
                while (d10.Read())
                {
                    cumul_ecue_nv = d10.GetInt32(0);
                }
                d10.Close();



                string r = "SELECT COUNT( DISTINCT id_etudiant) FROM  etudiant_inscription WHERE id_classe=@idcl AND id_session=@id_session AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand c = new MySqlCommand(r, conn);
                c.Parameters.AddWithValue("@idcl", id_classe);
                c.Parameters.AddWithValue("@id_session", id_session);
                c.Parameters.AddWithValue("@id_departement", id_departement);
                c.Parameters.AddWithValue("@id_faculte", id_faculte);
                c.Parameters.AddWithValue("@id_annee", id_annee);
                nbre_etudiant = Convert.ToInt32(c.ExecuteScalar());
                Etudiant_ID = new string[nbre_etudiant];

                string ra = "SELECT DISTINCT id_etudiant FROM  etudiant_inscription WHERE id_classe=@idcl AND id_session=@id_session AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand ca = new MySqlCommand(ra, conn);
                ca.Parameters.AddWithValue("@idcl", id_classe);
                ca.Parameters.AddWithValue("@id_session", id_session);
                ca.Parameters.AddWithValue("@id_departement", id_departement);
                ca.Parameters.AddWithValue("@id_faculte", id_faculte);
                ca.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader da = ca.ExecuteReader();
                int m = 0;
                while (da.Read())
                {
                    Etudiant_ID[m] = string.Format("{0}", da.GetInt32(0).ToString());
                    m++;
                }
                da.Close();

                string requette = "SELECT COUNT(DISTINCT id_unite) FROM unite WHERE id_classe=@idcl AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand com = new MySqlCommand(requette, conn);
                com.Parameters.AddWithValue("@idcl", id_classe);
                com.Parameters.AddWithValue("@id_departement", id_departement);
                com.Parameters.AddWithValue("@id_faculte", id_faculte);
                com.Parameters.AddWithValue("@id_annee", id_annee);
                nbre_unite = Convert.ToInt32(com.ExecuteScalar());
                Unite_ID = new string[nbre_unite];

                string sql = "SELECT DISTINCT id_unite FROM unite WHERE id_classe=@idcl AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idcl", id_classe);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader dr1 = cmd.ExecuteReader();
                int i = 0;
                while (dr1.Read())
                {
                    Unite_ID[i] = string.Format("{0}", dr1.GetInt32(0).ToString());
                    i++;
                }
                dr1.Close();


                for (int etu = 0; etu < nbre_etudiant; etu++)
                {
                    etu_pourcentage = 0;
                    etu_note_ponderee = 0;
                    total_credit = 0;
                    total_cours = 0;
                    ecue_rate = 0;
                    ue_nv = 0;
                    ecue_nv = 0;

                    for (int ue = 0; ue < nbre_unite; ue++)
                    {
                        ecue_sous_sept = 0;
                        ue_credit = 0;
                        ue_moyenne = 0;
                        ue_note_ponderee = 0;

                        string requ = "SELECT COUNT(DISTINCT id_cours) FROM cours WHERE id_unite=@idue";
                        MySqlCommand comq = new MySqlCommand(requ, conn);
                        comq.Parameters.AddWithValue("@idue", Convert.ToInt32(Unite_ID[ue]));
                        nbre_cours = Convert.ToInt32(comq.ExecuteScalar());
                        total_cours += nbre_cours;

                        Cours_ID = new string[nbre_cours];
                        Cours_Credit = new string[nbre_cours];

                        string sq = "SELECT DISTINCT id_cours,credits FROM cours WHERE id_unite=@id_unite";
                        MySqlCommand cm = new MySqlCommand(sq, conn);
                        cm.Parameters.AddWithValue("@id_unite", Convert.ToInt32(Unite_ID[ue]));
                        MySqlDataReader dr2 = cm.ExecuteReader();
                        int k = 0;
                        while (dr2.Read())
                        {
                            Cours_ID[k] = string.Format("{0}", dr2.GetInt32(0).ToString());
                            Cours_Credit[k] = string.Format("{0}", dr2.GetInt32(1).ToString());
                            k++;
                        }
                        dr2.Close();

                        for (int ecue = 0; ecue < nbre_cours; ecue++)
                        {
                            ue_credit += Convert.ToInt32(Cours_Credit[ecue]);

                            string sqmo = "SELECT COUNT(*) FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                            MySqlCommand cmo = new MySqlCommand(sqmo, conn);
                            cmo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                            cmo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                            cmo.Parameters.AddWithValue("@id_session", id_session);
                            int exist = Convert.ToInt32(cmo.ExecuteScalar());
                            if (exist == 0)
                            {
                                /*ecue_rate++;*/
                                string sqmar = "SELECT note FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                MySqlCommand cmar = new MySqlCommand(sqmar, conn);
                                cmar.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                cmar.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                cmar.Parameters.AddWithValue("@id_session", 1);
                                MySqlDataReader drmar = cmar.ExecuteReader();
                                while (drmar.Read())
                                {
                                    //ecue_note = double.Parse(drmar.GetString(0), System.Globalization.CultureInfo.InvariantCulture);
                                    ecue_note = Convert.ToDouble(drmar.GetString(0));
                                    if (ecue_note < ecue_sous_sept_value)
                                    {
                                        ecue_sous_sept++;
                                    }
                                    ecue_note_ponderee = ecue_note * Convert.ToInt32(Cours_Credit[ecue]);
                                    ue_note_ponderee += ecue_note_ponderee;
                                    etu_note_ponderee += ecue_note_ponderee;
                                }
                                drmar.Close();
                            }
                            else
                            {
                                string sqmar = "SELECT note FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                MySqlCommand cmar = new MySqlCommand(sqmar, conn);
                                cmar.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                cmar.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                cmar.Parameters.AddWithValue("@id_session", id_session);
                                MySqlDataReader drmar = cmar.ExecuteReader();
                                while (drmar.Read())
                                {
                                    if (drmar.GetString(0) == "")
                                    {
                                        ecue_rate++;
                                    }
                                    else
                                    {
                                        //ecue_note = double.Parse(drmar.GetString(0), System.Globalization.CultureInfo.InvariantCulture);
                                        ecue_note = Convert.ToDouble(drmar.GetString(0));
                                        if (ecue_note < ecue_sous_sept_value)
                                        {
                                            ecue_sous_sept++;
                                        }
                                        ecue_note_ponderee = ecue_note * Convert.ToInt32(Cours_Credit[ecue]);
                                        ue_note_ponderee += ecue_note_ponderee;
                                        etu_note_ponderee += ecue_note_ponderee;
                                    }
                                }
                                drmar.Close();
                            }
                        }
                        ue_moyenne = ue_note_ponderee / ue_credit;
                        total_credit += ue_credit;

                        if (ue_moyenne < 10 || ecue_rate > 0 || ecue_sous_sept > 0)
                        {
                            ue_nv++;
                            for (int ecue = 0; ecue < nbre_cours; ecue++)
                            {
                                string sqmo = "SELECT COUNT(*) FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                MySqlCommand cmo = new MySqlCommand(sqmo, conn);
                                cmo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                cmo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                cmo.Parameters.AddWithValue("@id_session", id_session);
                                int exist = Convert.ToInt32(cmo.ExecuteScalar());
                                if (exist == 0)// le cours n'a pas ete candidat a la deuxieme session
                                {
                                }
                                else
                                {
                                    string sqv = "SELECT note FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                    MySqlCommand cmv = new MySqlCommand(sqv, conn);
                                    cmv.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                    cmv.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                    cmv.Parameters.AddWithValue("@id_session", id_session);
                                    MySqlDataReader drv = cmv.ExecuteReader();
                                    string note2 = "";
                                    while (drv.Read())
                                    {
                                        note2 = drv.GetString(0);
                                    }
                                    drv.Close();

                                    if (note2 == "")
                                    {
                                        ecue_rate++;
                                        ecue_nv++;
                                        string smo = "INSERT INTO etudiant_note_complement(note,id_cours,id_etudiant,date, etat_validation) VALUES (@note,@id_cours,@id_etudiant, @date, @etat_validation)";
                                        MySqlCommand mo = new MySqlCommand(smo, conn);
                                        mo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                        mo.Parameters.AddWithValue("@note", note2);
                                        mo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                        mo.Parameters.AddWithValue("@date", DateTime.Today.ToShortDateString());
                                        mo.Parameters.AddWithValue("@etat_validation", "Non");
                                        mo.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        //double not = double.Parse(note2, System.Globalization.CultureInfo.InvariantCulture);
                                        double not = Convert.ToDouble(note2);
                                        if (not < 10)
                                        {
                                            ecue_nv++;
                                            string smo = "INSERT INTO etudiant_note_complement(note,id_cours,id_etudiant,date, etat_validation) VALUES (@note,@id_cours,@id_etudiant,@date, @etat_validation)";
                                            MySqlCommand mo = new MySqlCommand(smo, conn);
                                            mo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                            mo.Parameters.AddWithValue("@note", note2);
                                            mo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                            mo.Parameters.AddWithValue("@date", DateTime.Today.ToShortDateString());
                                            mo.Parameters.AddWithValue("@etat_validation", "Non");
                                            mo.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                    }
                    etu_pourcentage = (etu_note_ponderee / (double)total_credit) * 5;

                    //-----------------------DEBUT DELIBERATION-------------------------------------------------------//
                    Class_Reports rap = new Class_Reports();
                    int ecue_nv_anterieur = rap.Count_ecue_nv_anterieur(id_annee, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    fraude = rap.Check_Fraude(id_annee, fraude, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    sanction = rap.Check_Sanction(id_annee, sanction, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    justification = rap.Load_Justificatif_Examen_Rate(id_annee, id_session, justification, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    nbre_redoublement = rap.Count_Redoublement(nbre_redoublement, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    nbre_change_filiere = rap.Count_Change_filliere(nbre_change_filiere, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    id_mention = rap.Load_Mention(etu_pourcentage, id_mention, conn);
                    rap.Decision_2eme_Session(id_session, redoub_autorise, change_filiere_autorise, max_ecue_nv_par_annee, cumul_ecue_nv, ecue_rate, ecue_nv, ue_nv, etu_pourcentage, justification, fraude, sanction, nbre_change_filiere, nbre_redoublement, ref id_decision, ref decisio, ref sigl, conn, ecue_nv_anterieur);
                    //-----------------------FIN DELIBERATION-------------------------------------------------------//



                    string reque = "select count(*) from deliberation where id_classe=@id_classe and id_etudiant=@id_etudiant and id_session=@id_session and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                    MySqlCommand coam = new MySqlCommand(reque, conn);
                    coam.Parameters.AddWithValue("@id_classe", id_classe);
                    coam.Parameters.AddWithValue("@id_departement", id_departement);
                    coam.Parameters.AddWithValue("@id_faculte", id_faculte);
                    coam.Parameters.AddWithValue("@id_annee", id_annee);
                    coam.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                    coam.Parameters.AddWithValue("@id_session", id_session);
                    int resulta = Convert.ToInt32(coam.ExecuteScalar());
                    if (resulta == 0)
                    {
                        string rqta = "INSERT INTO deliberation(id_classe,id_departement,id_faculte,id_annee, id_session,id_etudiant,id_mention,id_decision,ue_nv,ecue_nv,pourcentage) VALUES (@id_classe, @id_departement,@id_faculte,@id_annee,@id_session,@id_etudiant,@id_mention,@id_decision,@ue_nv,@ecue_nv,@pourcentage)";
                        MySqlCommand cmad = new MySqlCommand(rqta, conn);
                        cmad.Parameters.AddWithValue("@id_classe", id_classe);
                        cmad.Parameters.AddWithValue("@id_departement", id_departement);
                        cmad.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmad.Parameters.AddWithValue("@id_annee", id_annee);
                        cmad.Parameters.AddWithValue("@id_session", id_session);
                        cmad.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                        cmad.Parameters.AddWithValue("@id_mention", id_mention);
                        cmad.Parameters.AddWithValue("@id_decision", id_decision);
                        cmad.Parameters.AddWithValue("@ue_nv", ue_nv);
                        cmad.Parameters.AddWithValue("@ecue_nv", ecue_nv);
                        cmad.Parameters.AddWithValue("@pourcentage", etu_pourcentage);
                        cmad.ExecuteNonQuery();
                    }
                    else
                    {
                        string rqti = "UPDATE deliberation SET id_decision=@id_decision, id_mention=@id_mention, ue_nv=@ue_nv, ecue_nv=@ecue_nv, pourcentage=@pourcentage WHERE id_classe=@id_classe AND id_session=@id_session AND id_etudiant=@id_etudiant AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                        MySqlCommand cmdi = new MySqlCommand(rqti, conn);
                        cmdi.Parameters.AddWithValue("@id_classe", id_classe);
                        cmdi.Parameters.AddWithValue("@id_departement", id_departement);
                        cmdi.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmdi.Parameters.AddWithValue("@id_annee", id_annee);
                        cmdi.Parameters.AddWithValue("@id_session", id_session);
                        cmdi.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                        cmdi.Parameters.AddWithValue("@id_mention", id_mention);
                        cmdi.Parameters.AddWithValue("@id_decision", id_decision);
                        cmdi.Parameters.AddWithValue("@ue_nv", ue_nv);
                        cmdi.Parameters.AddWithValue("@ecue_nv", ecue_nv);
                        cmdi.Parameters.AddWithValue("@pourcentage", etu_pourcentage);
                        cmdi.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        private void Premiere_Session()
        {
            int ecue_note_minimale = 0, ue_ecue_sous_sept = 0;

            double ecue_note = 0, ecue_note_ponderee = 0;
            int ecue_rate = 0, ecue_nv = 0, ecue_complement = 0;

            double ue_note_ponderee = 0, ue_moyenne = 0, ue_note_ponderee_avant_2eme_session = 0;
            int ue_credit = 0;


            int nbre_etudiant, nbre_unite, nbre_cours, total_cours = 0;
            double etu_note_ponderee = 0, etu_pourcentage = 0;
            int total_credit = 0, etu_ue_nv = 0;

            string justification = "", fraude = "", sanction = "", pourca = "";
            int nbre_change_filiere = 0, nbre_redoublement = 0;
            double notek = 0;

            int id_decision = -1, id_mention = -1;
            string decisio = "", sigl = "";

            string[] Unite_ID, Cours_ID, Cours_Credit, Unite_Credit, Etudiant_ID, Cours_Note_Par_Etudiant;


            string[] Classe_ID, Departement_ID, Departements;
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();

                string s7 = "SELECT valeur FROM  deliberation_parametre_generaux WHERE id_parametre=@id_parametre";
                MySqlCommand c7 = new MySqlCommand(s7, conn);
                c7.Parameters.AddWithValue("@id_parametre", 4);
                MySqlDataReader d7 = c7.ExecuteReader();
                while (d7.Read())
                {
                    ecue_note_minimale = d7.GetInt32(0);
                }
                d7.Close();


                string r = "SELECT COUNT( DISTINCT id_etudiant) FROM  etudiant_inscription WHERE id_classe=@idcl AND id_session=@id_session AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand c = new MySqlCommand(r, conn);
                c.Parameters.AddWithValue("@idcl", id_classe);
                c.Parameters.AddWithValue("@id_session", id_session);
                c.Parameters.AddWithValue("@id_departement", id_departement);
                c.Parameters.AddWithValue("@id_faculte", id_faculte);
                c.Parameters.AddWithValue("@id_annee", id_annee);
                nbre_etudiant = Convert.ToInt32(c.ExecuteScalar());
                Etudiant_ID = new string[nbre_etudiant];

                string sa = "SELECT DISTINCT id_etudiant FROM  etudiant_inscription WHERE id_classe=@idcl AND id_session=@id_session AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand ca = new MySqlCommand(sa, conn);
                ca.Parameters.AddWithValue("@idcl", id_classe);
                ca.Parameters.AddWithValue("@id_session", id_session);
                ca.Parameters.AddWithValue("@id_departement", id_departement);
                ca.Parameters.AddWithValue("@id_faculte", id_faculte);
                ca.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader da = ca.ExecuteReader();
                int m = 0;
                while (da.Read())
                {
                    Etudiant_ID[m] = string.Format("{0}", da.GetInt32(0).ToString());
                    m++;
                }
                da.Close();

                string requette = "SELECT COUNT(DISTINCT id_unite) FROM unite WHERE id_classe=@idcl AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand com = new MySqlCommand(requette, conn);
                com.Parameters.AddWithValue("@idcl", id_classe);
                com.Parameters.AddWithValue("@id_departement", id_departement);
                com.Parameters.AddWithValue("@id_faculte", id_faculte);
                com.Parameters.AddWithValue("@id_annee", id_annee);
                nbre_unite = Convert.ToInt32(com.ExecuteScalar());
                Unite_ID = new string[nbre_unite];

                string sql = "SELECT DISTINCT id_unite FROM unite WHERE id_classe=@idcl AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idcl", id_classe);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader dr1 = cmd.ExecuteReader();
                int i = 0;
                while (dr1.Read())
                {
                    Unite_ID[i] = string.Format("{0}", dr1.GetInt32(0).ToString());
                    i++;
                }
                dr1.Close();


                for (int etu = 0; etu < nbre_etudiant; etu++)
                {
                    etu_pourcentage = 0;
                    etu_note_ponderee = 0;
                    total_credit = 0;
                    total_cours = 0;
                    ecue_rate = 0;
                    etu_ue_nv = 0;
                    ecue_nv = 0;

                    for (int ue = 0; ue < nbre_unite; ue++)
                    {
                        ue_ecue_sous_sept = 0;
                        ue_credit = 0;
                        ue_moyenne = 0;
                        ue_note_ponderee = 0;

                        string requ = "SELECT COUNT(DISTINCT id_cours) FROM cours WHERE id_unite=@idue";
                        MySqlCommand comq = new MySqlCommand(requ, conn);
                        comq.Parameters.AddWithValue("@idue", Convert.ToInt32(Unite_ID[ue]));
                        nbre_cours = Convert.ToInt32(comq.ExecuteScalar());

                        total_cours += nbre_cours;

                        Cours_ID = new string[nbre_cours];
                        Cours_Credit = new string[nbre_cours];

                        string sq = "SELECT DISTINCT id_cours,credits FROM cours WHERE id_unite=@id_unite";
                        MySqlCommand cm = new MySqlCommand(sq, conn);
                        cm.Parameters.AddWithValue("@id_unite", Convert.ToInt32(Unite_ID[ue]));
                        MySqlDataReader dr2 = cm.ExecuteReader();
                        int k = 0;
                        while (dr2.Read())
                        {
                            Cours_ID[k] = string.Format("{0}", dr2.GetInt32(0).ToString());
                            Cours_Credit[k] = string.Format("{0}", dr2.GetInt32(1).ToString());
                            k++;
                        }
                        dr2.Close();

                        for (int ecue = 0; ecue < nbre_cours; ecue++)
                        {
                            ue_credit += Convert.ToInt32(Cours_Credit[ecue]);

                            string sqmo = "SELECT COUNT(DISTINCT id_note) FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                            MySqlCommand cmo = new MySqlCommand(sqmo, conn);
                            cmo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                            cmo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                            cmo.Parameters.AddWithValue("@id_session", id_session);
                            int exist = Convert.ToInt32(cmo.ExecuteScalar());
                            if (exist == 0)
                            {
                                ecue_rate++;
                            }
                            else
                            {
                                string sqmar = "SELECT note FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                MySqlCommand cmar = new MySqlCommand(sqmar, conn);
                                cmar.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                cmar.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                cmar.Parameters.AddWithValue("@id_session", id_session);
                                MySqlDataReader drmar = cmar.ExecuteReader();
                                while (drmar.Read())
                                {
                                    ecue_note = Convert.ToDouble(drmar.GetString(0));
                                    if (ecue_note < ecue_note_minimale)
                                    {
                                        ue_ecue_sous_sept++;
                                    }

                                    //ecue_note = double.Parse(drmar.GetString(0), System.Globalization.CultureInfo.InvariantCulture);
                                    // MessageBox.Show(Cours_Credit[ecue] + "--" + ecue_note + "--" + ecue_note_ponderee + "---" + ue_note_ponderee + "---" + etu_note_ponderee);
                                    ecue_note_ponderee = ecue_note * Convert.ToInt32(Cours_Credit[ecue]);
                                    ue_note_ponderee += ecue_note_ponderee;
                                    etu_note_ponderee += ecue_note_ponderee;
                                }
                                drmar.Close();
                                //MessageBox.Show(Cours_Credit[ecue] + "--" + ecue_note + "--" + ecue_note_ponderee + "---" + ue_note_ponderee + "---" + etu_note_ponderee);
                            }
                        }
                        ue_moyenne = ue_note_ponderee / ue_credit;
                        total_credit += ue_credit;

                        if (ue_moyenne < 10 || ecue_rate > 0 || ue_ecue_sous_sept > 0)
                        {
                            //MessageBox.Show(Etudiant_ID[etu] + " " + Unite_ID[ue]);
                            etu_ue_nv++;
                            //----------------------------------INSCRIPTION A LA 2EME SESSION----------------------------------------
                            string requi = "SELECT COUNT(*) FROM etudiant_inscription WHERE id_annee=@id_annee AND id_faculte=@id_faculte AND id_session=@id_session AND id_classe=@id_classe AND id_departement=@id_departement AND id_etudiant=@id_etudiant";
                            MySqlCommand comqi = new MySqlCommand(requi, conn);
                            comqi.Parameters.AddWithValue("@id_session", 2);
                            comqi.Parameters.AddWithValue("@id_annee", id_annee);
                            comqi.Parameters.AddWithValue("@id_faculte", id_faculte);
                            comqi.Parameters.AddWithValue("@id_departement", id_departement);
                            comqi.Parameters.AddWithValue("@id_classe", id_classe);
                            comqi.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                            int insri = Convert.ToInt32(comqi.ExecuteScalar());
                            if (insri == 0)
                            {
                                string smoj = "INSERT INTO etudiant_inscription(date_inscription,id_annee, id_faculte,id_departement, id_classe,id_etudiant,id_session) VALUES (@date_inscription,@id_annee,@id_faculte, @id_departement, @id_classe,@id_etudiant,@id_session)";
                                MySqlCommand cmoj = new MySqlCommand(smoj, conn);
                                cmoj.Parameters.AddWithValue("@date_inscription", Date_TimePicker.Text);
                                cmoj.Parameters.AddWithValue("@id_session", 2);
                                cmoj.Parameters.AddWithValue("@id_annee", id_annee);
                                cmoj.Parameters.AddWithValue("@id_faculte", id_faculte);
                                cmoj.Parameters.AddWithValue("@id_departement", id_departement);
                                cmoj.Parameters.AddWithValue("@id_classe", id_classe);
                                cmoj.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                cmoj.ExecuteNonQuery();
                            }
                            //---------------------------------------------------------------------------------------------------------------------------
                            for (int ecue = 0; ecue < nbre_cours; ecue++)
                            {
                                string sqmo = "SELECT COUNT(*) FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                MySqlCommand cmo = new MySqlCommand(sqmo, conn);
                                cmo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                cmo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                cmo.Parameters.AddWithValue("@id_session", id_session);
                                int exist = Convert.ToInt32(cmo.ExecuteScalar());
                                if (exist == 0)
                                {
                                    ecue_nv++;

                                    string sqmi = "SELECT COUNT(*) FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                    MySqlCommand cmi = new MySqlCommand(sqmi, conn);
                                    cmi.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                    cmi.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                    cmi.Parameters.AddWithValue("@id_session", 2);
                                    int exist2 = Convert.ToInt32(cmi.ExecuteScalar());
                                    if (exist2 == 0)
                                    {
                                        string smo = "INSERT INTO etudiant_note(note,id_cours,id_etudiant,id_session) VALUES (@note,@id_cours,@id_etudiant,@id_session)";
                                        MySqlCommand mo = new MySqlCommand(smo, conn);
                                        mo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                        mo.Parameters.AddWithValue("@note", "");
                                        mo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                        mo.Parameters.AddWithValue("@id_session", 2);
                                        mo.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    string sqv = "SELECT note FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                    MySqlCommand cmv = new MySqlCommand(sqv, conn);
                                    cmv.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                    cmv.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                    cmv.Parameters.AddWithValue("@id_session", id_session);
                                    MySqlDataReader drv = cmv.ExecuteReader();
                                    string note2 = "";
                                    while (drv.Read())
                                    {
                                        note2 = drv.GetString(0);
                                    }
                                    drv.Close();

                                    //double not = double.Parse(note2, System.Globalization.CultureInfo.InvariantCulture);
                                    double not = Convert.ToDouble(note2);
                                    if (not < 10)
                                    {
                                        ecue_nv++;
                                        string sqmi = "SELECT COUNT(*) FROM etudiant_note WHERE id_cours=@id_cours AND id_etudiant=@id_etudiant AND id_session=@id_session";
                                        MySqlCommand cmi = new MySqlCommand(sqmi, conn);
                                        cmi.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                        cmi.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                        cmi.Parameters.AddWithValue("@id_session", 2);
                                        int exist2 = Convert.ToInt32(cmi.ExecuteScalar());
                                        if (exist2 == 0)
                                        {
                                            string smo = "INSERT INTO etudiant_note(note,id_cours,id_etudiant,id_session) VALUES (@note,@id_cours,@id_etudiant,@id_session)";
                                            MySqlCommand mo = new MySqlCommand(smo, conn);
                                            mo.Parameters.AddWithValue("@id_cours", Convert.ToInt32(Cours_ID[ecue]));
                                            mo.Parameters.AddWithValue("@note", note2);
                                            mo.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                                            mo.Parameters.AddWithValue("@id_session", 2);
                                            mo.ExecuteNonQuery();
                                        }
                                    }

                                }
                            }
                        }

                    }
                    etu_pourcentage = (etu_note_ponderee / (double)total_credit) * 5;

                    Class_Reports rap = new Class_Reports();
                    fraude = rap.Check_Fraude(id_annee, fraude, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    sanction = rap.Check_Sanction(id_annee, sanction, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    justification = rap.Load_Justificatif_Examen_Rate(id_annee, id_session, justification, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    nbre_redoublement = rap.Count_Redoublement(nbre_redoublement, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    nbre_change_filiere = rap.Count_Change_filliere(nbre_change_filiere, Convert.ToInt32(Etudiant_ID[etu]), conn);
                    id_mention = rap.Load_Mention(etu_pourcentage, id_mention, conn);

                    rap.Decision_1ere_Session(id_session, ecue_rate, etu_ue_nv, nbre_unite, total_cours, etu_pourcentage, fraude, sanction, ref pourca, ref id_decision, ref decisio, ref sigl, conn);


                    string reque = "select count(*) from deliberation where id_classe=@id_classe and id_etudiant=@id_etudiant and id_session=@id_session and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                    MySqlCommand coam = new MySqlCommand(reque, conn);
                    coam.Parameters.AddWithValue("@id_classe", id_classe);
                    coam.Parameters.AddWithValue("@id_departement", id_departement);
                    coam.Parameters.AddWithValue("@id_faculte", id_faculte);
                    coam.Parameters.AddWithValue("@id_annee", id_annee);
                    coam.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                    coam.Parameters.AddWithValue("@id_session", id_session);
                    int resulta = Convert.ToInt32(coam.ExecuteScalar());
                    if (resulta == 0)
                    {
                        string rqta = "INSERT INTO deliberation(id_classe,id_departement,id_faculte,id_annee, id_session,id_etudiant,id_mention,id_decision,ue_nv,ecue_nv,pourcentage) VALUES (@id_classe, @id_departement,@id_faculte,@id_annee,@id_session,@id_etudiant,@id_mention,@id_decision,@ue_nv,@ecue_nv,@pourcentage)";
                        MySqlCommand cmad = new MySqlCommand(rqta, conn);
                        cmad.Parameters.AddWithValue("@id_classe", id_classe);
                        cmad.Parameters.AddWithValue("@id_departement", id_departement);
                        cmad.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmad.Parameters.AddWithValue("@id_annee", id_annee);
                        cmad.Parameters.AddWithValue("@id_session", id_session);
                        cmad.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                        cmad.Parameters.AddWithValue("@id_mention", id_mention);
                        cmad.Parameters.AddWithValue("@id_decision", id_decision);
                        cmad.Parameters.AddWithValue("@ue_nv", etu_ue_nv);
                        cmad.Parameters.AddWithValue("@ecue_nv", ecue_nv);
                        cmad.Parameters.AddWithValue("@pourcentage", etu_pourcentage);
                        cmad.ExecuteNonQuery();
                    }
                    else
                    {
                        string rqti = "UPDATE deliberation SET id_decision=@id_decision, id_mention=@id_mention, ue_nv=@ue_nv, ecue_nv=@ecue_nv, pourcentage=@pourcentage WHERE id_classe=@id_classe AND id_session=@id_session AND id_etudiant=@id_etudiant AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                        MySqlCommand cmdi = new MySqlCommand(rqti, conn);
                        cmdi.Parameters.AddWithValue("@id_classe", id_classe);
                        cmdi.Parameters.AddWithValue("@id_departement", id_departement);
                        cmdi.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmdi.Parameters.AddWithValue("@id_annee", id_annee);
                        cmdi.Parameters.AddWithValue("@id_session", id_session);
                        cmdi.Parameters.AddWithValue("@id_etudiant", Convert.ToInt32(Etudiant_ID[etu]));
                        cmdi.Parameters.AddWithValue("@id_mention", id_mention);
                        cmdi.Parameters.AddWithValue("@id_decision", id_decision);
                        cmdi.Parameters.AddWithValue("@ue_nv", etu_ue_nv);
                        cmdi.Parameters.AddWithValue("@ecue_nv", ecue_nv);
                        cmdi.Parameters.AddWithValue("@pourcentage", etu_pourcentage);
                        cmdi.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec !')</script>");
                return;
            }
        }


        protected void EnregistrerButton_Click(object sender, ImageClickEventArgs e)
        {
            EnregistrerButton.Enabled = false;
            if (action == 1)
            {
                Session_Cloturer();
                switch (id_session)
                {
                    case 1: Premiere_Session();
                        break;
                    case 2: Deuxieme_Session();
                        break;
                    default:
                        break;
                }
            }
            else if (action == 2)
            {
                Session_Modifier();
            }
            else if (action == 3)
            {
                Session_Reinitialiser();
            }
            EnregistrerButton.Enabled = true;
            Response.Write("<script>alert('Operation fini !')</script>");
            return;
        }

        protected void ExitButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/_Enseignements/emptyEnseignements.aspx");
        }

        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedValue);
            id_departement = Convert.ToInt32(Departement_Combo.SelectedValue);
        
            //nom_departement = Departement_Combo.Text;
            Load_Classe();
        }

        protected void Action_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            action = Convert.ToInt32(Action_ComboBox.SelectedValue);
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            
            if (id_annee == -1)
            {
                Faculte_Combo.SelectedValue = "-1";
                ClasseCombo.SelectedValue = "-1";
                Action_ComboBox.SelectedValue = "-1";
                Departement_Combo.SelectedValue = "-1";
                Session_ComboBo.SelectedValue = "-1";
                Action_ComboBox.SelectedValue = "-1";
            }
            else
            {
                Load_Faculte();
            }
        }

        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedValue);
            Load_Departements();
        }

        protected void ClasseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32(ClasseCombo.SelectedValue);
            nom_classe =ClasseCombo.Text ;
            //etat_classe = ;
            Load_Session();
        }

        
    }
}