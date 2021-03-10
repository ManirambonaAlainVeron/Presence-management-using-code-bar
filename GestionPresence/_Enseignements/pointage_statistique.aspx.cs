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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics.CodeAnalysis;


namespace BMDSysWeb._Enseignements
{
    public partial class pointage_statistique : System.Web.UI.Page
    {
       

        MySqlConnection conn;
        public static int type = -1, nbr_cours = 0, id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1,has_departement = -1, id_semestre = -1, id_etudiant = -1, id_session = -1, id_cours = -1;
        public static int credit_cour=0;
        public static string nom_cours = "", nom_faculte = "", nom_departement = "", nom_classe = "", nom_anne="";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_type();
                Annee_Combo.Visible = false;
                Faculte_Combo.Visible = false;
                Departement_Combo.Visible = false;
                ClasseCombo.Visible = false;
                Cours_ComboBox.Visible = false;
            }
        }

        private void Load_type()
        {
            DropDown_type_recherche.Items.Clear();

            System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
            item0.Value = "-1";
            item0.Text = "Choisissez le type";
            DropDown_type_recherche.Items.Add(item0);

            System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem();
            item1.Value = "0";
            item1.Text = "Classe";
            DropDown_type_recherche.Items.Add(item1);

            System.Web.UI.WebControls.ListItem item2 = new System.Web.UI.WebControls.ListItem();
            item2.Value = "1";
            item2.Text = "Cours";
            DropDown_type_recherche.Items.Add(item2);
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

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez A-A";
                Annee_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez la faculte";
                Faculte_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
                Departement_Combo.Items.Clear();

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez le departement";
                Departement_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
                ClasseCombo.Items.Clear();
                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisir la classe";
                ClasseCombo.Items.Add(item0);
                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
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

        protected void ExitButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/_Enseignements/emptyEnseignements.aspx");
        }

        private void Load_Cours()
        {

            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string re = "SELECT  cours.id_cours, cours.cours FROM cours INNER JOIN annee ON cours.id_annee = annee.id_annee"
             + " INNER JOIN faculte ON cours.id_faculte = faculte.id_faculte INNER JOIN departement ON"
             + " cours.id_departement = departement.id_departement INNER JOIN classe ON cours.id_classe = classe.id_classe"
             + " where cours.id_annee=@id_annee and cours.id_faculte=@id_faculte AND cours.id_departement=@id_departement AND cours.id_classe=@id_classe";
                    MySqlCommand cm = new MySqlCommand(re, conn);
                    cm.Parameters.AddWithValue("@id_classe", id_classe);
                    cm.Parameters.AddWithValue("@id_departement", id_departement);
                    cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cm.Parameters.AddWithValue("@id_annee", id_annee);
                    MySqlDataReader drg = cm.ExecuteReader();
                    Cours_ComboBox.Items.Clear();

                    System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                    item0.Value = "-1";
                    item0.Text = "Choisissez le cours";
                    Cours_ComboBox.Items.Add(item0);

                    while (drg.Read())
                    {
                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                        item.Value = drg.GetInt32(0).ToString();
                        item.Text = drg.GetString(1);
                        Cours_ComboBox.Items.Add(item);
                    }
                    drg.Close();
            
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des cours')</script>");
                return;
            }
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            nom_anne = Annee_Combo.SelectedItem.Text;
            Load_Faculte();
            Departement_Combo.SelectedValue = "-1";
            ClasseCombo.SelectedValue = "-1";
            Cours_ComboBox.SelectedValue = "-1";
        }

        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedValue);
            nom_faculte = Faculte_Combo.SelectedItem.Text;
            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            //MessageBox.Show(id_annee + "  " + id_faculte);
            string sql0 = "SELECT COUNT(*) FROM departement_par_faculte WHERE id_annee=@id_annee AND id_faculte=@id_faculte";
            MySqlCommand cmd0 = new MySqlCommand(sql0, conn);
            cmd0.Parameters.AddWithValue("@id_annee", id_annee);
            cmd0.Parameters.AddWithValue("@id_faculte", id_faculte);
            has_departement = Convert.ToInt32(cmd0.ExecuteScalar());
            conn.Close();
            Load_Departements();
            ClasseCombo.SelectedValue = "-1";
            Cours_ComboBox.SelectedValue = "-1";
        }

        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_departement = Convert.ToInt32(Departement_Combo.SelectedValue);
            nom_departement = Departement_Combo.SelectedItem.Text;
            Load_Classe();
            Cours_ComboBox.SelectedValue = "-1";
        }

        protected void ClasseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32(ClasseCombo.SelectedValue);
            nom_classe = ClasseCombo.SelectedItem.Text;
            Load_Cours();

            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            //MessageBox.Show(id_annee + "  " + id_faculte);
            string sql0 = "SELECT COUNT(*) FROM cours WHERE id_annee = @id_annee AND id_faculte = @id_faculte AND id_departement = @id_departement AND id_classe = @id_classe;";
            MySqlCommand cmd0 = new MySqlCommand(sql0, conn);
            cmd0.Parameters.AddWithValue("@id_annee", id_annee);
            cmd0.Parameters.AddWithValue("@id_faculte", id_faculte);
            cmd0.Parameters.AddWithValue("@id_departement", id_departement);
            cmd0.Parameters.AddWithValue("@id_classe", id_classe);
            nbr_cours = Convert.ToInt32(cmd0.ExecuteScalar());
            conn.Close();
        }

        protected void Cours_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_cours = Convert.ToInt32(Cours_ComboBox.SelectedValue);
            nom_cours = Cours_ComboBox.SelectedItem.Text;
            MySqlConnection conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();

            string rqt1 = "SELECT cours.credits FROM cours WHERE cours.id_cours=@id_cou";

            MySqlCommand cmd = new MySqlCommand(rqt1,conn);
            cmd.Parameters.AddWithValue("@id_cou", id_cours);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                credit_cour = dr.GetInt32(0) * 15;
            }

            conn.Close();
        }


        protected void DropDown_type_recherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            Faculte_Combo.SelectedValue = "-1";
            Departement_Combo.SelectedValue = "-1";
            ClasseCombo.SelectedValue = "-1";
            Cours_ComboBox.SelectedValue = "-1";

             type = Convert.ToInt32(DropDown_type_recherche.SelectedValue);
            if(type == 0)
            {
                Load_Annee();
                Annee_Combo.Visible = true;
                Faculte_Combo.Visible = true;
                Departement_Combo.Visible = true;
                ClasseCombo.Visible = true;
                Cours_ComboBox.Visible = false;
            }
            else if (type == 1)
            {
                Load_Annee();
                Annee_Combo.Visible = true;
                Faculte_Combo.Visible = true;
                Departement_Combo.Visible = true;
                ClasseCombo.Visible = true;
                Cours_ComboBox.Visible = true;
                
            }
            else
            {
                Annee_Combo.Visible = false;
                Faculte_Combo.Visible = false;
                Departement_Combo.Visible = false;
                ClasseCombo.Visible = false;
                Cours_ComboBox.Visible = false;
            }
        }

        protected void generer_btn_Click(object sender, EventArgs e)
        {
            Class_Reports rap = new Class_Reports();
            if (type == 0)
            {
                conn =new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string rqt0 = "select count(DISTINCT semestre) from semestre INNER JOIN unite ON unite.id_semestre=semestre.id_semestre"
                            + "  WHERE unite.id_annee=@anne AND unite.id_faculte=@faculte AND unite.id_departement=@departement AND unite.id_classe=@classe";
                    MySqlCommand cmd0 = new MySqlCommand(rqt0, conn);
                    cmd0.Parameters.AddWithValue("@anne", id_annee);
                    cmd0.Parameters.AddWithValue("@faculte", id_faculte);
                    cmd0.Parameters.AddWithValue("@departement", id_departement);
                    cmd0.Parameters.AddWithValue("@classe", id_classe);
                    int nb_Semestre = Convert.ToInt32(cmd0.ExecuteScalar());
                    conn.Close();

                    if (nb_Semestre != 0)
                    {
                        rap.Etudiant_Rapport_Presence_par_Classe(id_annee, id_faculte, id_departement, id_classe, nom_anne, nom_faculte, nom_departement, nom_classe, has_departement, nbr_cours);
                    }
                    else
                    {
                        Response.Write("<script>alert('Echec, la classe ne contient pas des Semestres')</script>");
                    }
            }
            else if (type == 1)
            {
                rap.Etudiant_Rapport_Presence_par_Cours(id_annee, id_faculte, id_departement, id_classe, nom_anne, nom_faculte, nom_departement, nom_classe, has_departement, nbr_cours,nom_cours, credit_cour,id_cours);
            }
        }

    }
}