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

using MySql.Data.MySqlClient;
using System.Diagnostics.CodeAnalysis;

namespace BMDSysWeb._Enseignements
{
    public partial class e_note : System.Web.UI.Page
    {
        MySqlConnection conn;
        public static int id_departement = -1, id_classe = -1, id_cours = -1, id_session = -1, id_faculte = -1, id_annee = -1;
        public static string etat, etat1;
        string[] ID_COURS, ID_ETUDIANT, MATRICULE, NOM, PRENOM, DONNEE_ETUDIANT;
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

                Annee_Combo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez A-A";
                Annee_Combo.Items.Add(item0);

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
                Response.Write("<script>alert('Echec de chargement !')</script>");
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
                Faculte_Combo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez la faculte";
                Faculte_Combo.Items.Add(item0);

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
                Response.Write("<script>alert('Echec de chargement !')</script>");
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
                Departement_Combo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez le departement";
                Departement_Combo.Items.Add(item0);

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

                if (id_session == 0 || id_session == 1)
                {
                    string req1 = "SELECT classe.id_classe, classe.classe, classe_par_departement.etat_avancement" +
                        " FROM classe INNER JOIN classe_par_departement ON classe.id_classe = classe_par_departement.id_classe" +
                        " WHERE classe_par_departement.id_faculte=@id_faculte AND classe_par_departement.id_annee=@id_annee AND classe_par_departement.id_departement=@id_departement AND classe_par_departement.etat_avancement=@etat_avancement ";
                    MySqlCommand comd = new MySqlCommand(req1, conn);
                    comd.Parameters.AddWithValue("@id_departement", id_departement);
                    comd.Parameters.AddWithValue("@etat_avancement", "Encours");
                    comd.Parameters.AddWithValue("@id_annee", id_annee);
                    comd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    MySqlDataReader drg = comd.ExecuteReader();
                    Classe_ComboBox.Items.Clear();

                    ListItem item0 = new ListItem();
                    item0.Value = "-1";
                    item0.Text = "Choisissez la classe";
                    Classe_ComboBox.Items.Add(item0);

                    while (drg.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value= drg.GetInt32(0).ToString();
                        //item.text_to_fill_with = drg.GetString(1) + " " + drg.GetString(2);
                        item.Text = drg.GetString(1);

                        Classe_ComboBox.Items.Add(item);
                    }
                    drg.Close();
                }
                else if (id_session == 2)
                {
                    string req1 = "SELECT classe.id_classe, classe.classe, classe_par_departement.etat_avancement" +
                        " FROM classe INNER JOIN classe_par_departement ON classe.id_classe = classe_par_departement.id_classe" +
                        " WHERE classe_par_departement.id_faculte=@id_faculte AND classe_par_departement.id_annee=@id_annee AND classe_par_departement.id_departement=@id_departement AND classe_par_departement.etat_avancement=@etat_avancement ";
                    MySqlCommand comd = new MySqlCommand(req1, conn);
                    comd.Parameters.AddWithValue("@id_departement", id_departement);
                    comd.Parameters.AddWithValue("@etat_avancement", "Clôturée ");
                    comd.Parameters.AddWithValue("@id_annee", id_annee);
                    comd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    MySqlDataReader drg = comd.ExecuteReader();
                    Classe_ComboBox.Items.Clear();

                    ListItem item0 = new ListItem();
                    item0.Value = "-1";
                    item0.Text = "Choisissez la classe";
                    Classe_ComboBox.Items.Add(item0);

                    while (drg.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = drg.GetInt32(0).ToString();
                        //item.text_to_fill_with = drg.GetString(1) + " " + drg.GetString(2);
                        item.Text = drg.GetString(1);
                        Classe_ComboBox.Items.Add(item);
                    }
                    drg.Close();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des classes')</script>");
                return;
            }
        }

        private void Load_Actions()
        {
            Operation_ComboBox.Items.Clear();

            ListItem item1 = new ListItem();
            item1.Value = "-1";
            item1.Text = "selectionnez l'action";
            Operation_ComboBox.Items.Add(item1);

            ListItem item2 = new ListItem();
            item2.Value = "0";
            item2.Text = "Répartition des points";
            Operation_ComboBox.Items.Add(item2);

            ListItem item3 = new ListItem();
            item3.Value = "1";
            item3.Text = "Note de 1ere Session";
            Operation_ComboBox.Items.Add(item3);

            ListItem item4 = new ListItem();
            item4.Value = "2";
            item4.Text = "Note de la 2eme Session";
            Operation_ComboBox.Items.Add(item4);
        }

        private void Load_Cours()
        {

            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                if (id_session == 0 || id_session == 1)
                {
                    string re = "SELECT DISTINCT attribution_cours.id_cours, cours.cours" +
                        " FROM (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) INNER JOIN horaire ON cours.id_cours = horaire.id_cours" +
                        " WHERE horaire.id_classe=@id_classe AND horaire.id_departement=@id_departement AND horaire.id_faculte=@id_faculte AND horaire.id_annee=@id_annee AND horaire.id_horaire_type=@id_type AND attribution_cours.etat=@etat" +
                        " ORDER BY attribution_cours.id_cours";
                    MySqlCommand cm = new MySqlCommand(re, conn);
                    cm.Parameters.AddWithValue("@id_classe", id_classe);
                    cm.Parameters.AddWithValue("@id_departement", id_departement);
                    cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cm.Parameters.AddWithValue("@id_annee", id_annee);
                    cm.Parameters.AddWithValue("@etat", "Cloturee");
                    cm.Parameters.AddWithValue("@id_type", 2);
                    MySqlDataReader drg = cm.ExecuteReader();
                    Cours_ComboBox.Items.Clear();

                    ListItem item0 = new ListItem();
                    item0.Value = "-1";
                    item0.Text = "Choisissez le cours";
                    Cours_ComboBox.Items.Add(item0);

                    while (drg.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = drg.GetInt32(0).ToString();
                        item.Text = drg.GetString(1);
                        Cours_ComboBox.Items.Add(item);
                    }
                    drg.Close();
                }
                else if (id_session == 2)
                {
                    string re = "SELECT DISTINCT etudiant_note.id_cours, cours.cours" +
                        " FROM unite INNER JOIN (cours INNER JOIN etudiant_note ON cours.id_cours = etudiant_note.id_cours) ON unite.id_unite = cours.id_unite" +
                        " WHERE unite.id_annee=@id_annee AND unite.id_faculte=@id_faculte AND unite.id_departement=@id_departement AND unite.id_classe=@id_classe AND etudiant_note.id_session=@id_session" +
                        " ORDER BY etudiant_note.id_cours";
                    MySqlCommand cm = new MySqlCommand(re, conn);
                    cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cm.Parameters.AddWithValue("@id_annee", id_annee);
                    cm.Parameters.AddWithValue("@id_departement", id_departement);
                    cm.Parameters.AddWithValue("@id_classe", id_classe);
                    cm.Parameters.AddWithValue("@id_session", id_session);
                    MySqlDataReader drg = cm.ExecuteReader();
                    Cours_ComboBox.Items.Clear();

                    ListItem item0 = new ListItem();
                    item0.Value = "-1";
                    item0.Text = "Choisissez le cours";
                    Cours_ComboBox.Items.Add(item0);

                    while (drg.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = drg.GetInt32(0).ToString();
                        item.Text = drg.GetString(1);
                        Cours_ComboBox.Items.Add(item);
                    }
                    drg.Close();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des cours')</script>");
                return;
            }
        }



        protected void ExitButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/_Enseignements/emptyEnseignements.aspx");
        }

        protected void Operation_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Departement_Combo.Items.Clear();
            Classe_ComboBox.Items.Clear();
            Cours_ComboBox.Items.Clear();


            id_session = Convert.ToInt32(Operation_ComboBox.SelectedValue);
            Load_Departements();
           
        }

        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            id_departement = Convert.ToInt32(Departement_Combo.SelectedValue);
            Load_Classe();

        }

        protected void Classe_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            id_classe = Convert.ToInt32(Classe_ComboBox.SelectedValue);
            Load_Cours();
        
        }

        protected void Cours_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_cours = Convert.ToInt32(Cours_ComboBox.SelectedValue);
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            Load_Faculte();
        }

        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedValue);
            Load_Actions();
        }

        protected void UpLoad_Button_Click(object sender, EventArgs e)
        {
            
        }

    
    }
}