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

namespace GestionPresence.Directeur_academique
{
    public partial class cours_creation : System.Web.UI.Page
    {
        MySqlConnection conn;

        public static int annee_precise, id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1, id_semestre = -1, id_ue = -1, id_cours = -1;
        int idf = -1, idp = -1;

        public static string annee, etat_annee, etat_classe, type_faculte, nom_faculte;


        public static int idaa, idcours, operation = -1;
        int id_diplome;
        int vh, idpers, result;
        int iddep, idue;
        long idAttr;
        string name, diplome;
        string n, p;
        string eta;
        string[] CoursID = new string[100];
        string[] CoursCredit = new string[100];
        string[] CoursNom = new string[100];

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
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                String reqdgv = "SELECT id_annee, annee FROM annee WHERE etat_annee=@etat_annee";
                MySqlCommand cmd = new MySqlCommand(reqdgv, conn);
                cmd.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = cmd.ExecuteReader();

                Annee_Combo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez l'anné académique";
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
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Faculte()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
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
                item0.Text = "Choisissez la faculté";
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
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Departements(int idannee, int idfaculte)
        {
            int id_annee = idannee;
            int id_faculte = idfaculte;
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
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
                item0.Text = "Choisissez le département";
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
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Classe(int idannee, int idfaculte, int iddepartement)
        {
            int id_annee = idannee;
            int id_faculte = idfaculte;
            int id_departement = iddepartement;

            conn = new MySqlConnection(Authentification.MyString);
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
            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "Choisissez la classe";
            ClasseCombo.Items.Add(item0);
            while (dr.Read())
            {
                ListItem item = new ListItem();
                item.Value = dr.GetInt32(0).ToString();
                item.Text = dr.GetString(1);
                //item.string_value_1 = dr.GetString(2);
                ClasseCombo.Items.Add(item);
            }
            dr.Close();
            conn.Close();
        }
        private void Cours_Par_Unite()
        {
            conn = new MySqlConnection(Authentification.MyString);
            conn.Open();

            string sql = " SELECT id_cours, code_cours, cours, credits FROM cours WHERE id_unite=@idue AND id_annee=@id_annee AND id_faculte=@id_faculte AND id_departement=@id_departement AND id_classe=@id_classe";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idue", id_ue);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);
            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
            cmd.Parameters.AddWithValue("@id_departement", id_departement);
            cmd.Parameters.AddWithValue("@id_classe", id_classe);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            CoursListView.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            CoursListView.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            CoursListView.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            if (dtable.Rows.Count > 0)
            {
                CoursListView.DataSource = dtable;
                CoursListView.DataBind();
            }
            else
            {
                dtable.Rows.Add(dtable.NewRow());
                CoursListView.DataSource = dtable;
                CoursListView.DataBind();
                CoursListView.Rows[0].Cells.Clear();
                CoursListView.Rows[0].Cells.Add(new TableCell());
                CoursListView.Rows[0].Cells[0].ColumnSpan = dtable.Columns.Count;
                CoursListView.Rows[0].Cells[0].Text = "Cette UE ne contient aucune ECUE";
                CoursListView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();

        }
        private void Unite_Par_Classe()
        {
            conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string sql = "select id_unite, unite from unite where id_classe=@idc and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idc", id_classe);
            cmd.Parameters.AddWithValue("@id_departement", id_departement);
            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);
            MySqlDataReader dr = cmd.ExecuteReader();

            Unite_Combo.Items.Clear();

            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "Choisissez l'unité";
            Unite_Combo.Items.Add(item0);

            while (dr.Read())
            {
                ListItem item = new ListItem();
                item.Value = dr.GetInt32(0).ToString();
                item.Text = dr.GetString(1);
                Unite_Combo.Items.Add(item);
            }
            dr.Close();
            conn.Close();
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32((Annee_Combo.SelectedItem).Value);
            annee = (Annee_Combo.SelectedItem).Text;
            Departement_Combo.SelectedValue = "-1";
            ClasseCombo.SelectedValue = "-1";
            Unite_Combo.SelectedValue = "-1";
            //etat_annee = (Annee_Combo.SelectedItem as ListItem).Value;

            Load_Faculte();
        }
        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32((Faculte_Combo.SelectedItem as ListItem).Value);
            nom_faculte = (Faculte_Combo.SelectedItem as ListItem).Text;
            ClasseCombo.SelectedValue = "-1";
            Unite_Combo.SelectedValue = "-1";
            Load_Departements(id_annee, id_faculte);
        }
        protected void ClasseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32((ClasseCombo.SelectedItem as ListItem).Value);
            etat_classe = (ClasseCombo.SelectedItem as ListItem).Text;
            Unite_Par_Classe();
            //Load_Cours_Attribution();
            //Load_Cours_Etat_Avancement();
            //Load_Cours_Par_Classe();
        }
        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_departement = Convert.ToInt32(Departement_Combo.SelectedValue);
            Load_Classe(id_annee, id_faculte, id_departement);
            Unite_Combo.SelectedValue = "-1";
        }
        protected void GDV_Creation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CoursListView.EditIndex = e.NewEditIndex;
            Cours_Par_Unite();
        }
        protected void GDV_Creation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string cours = (CoursListView.Rows[e.RowIndex].FindControl("nom_cour_TextBox") as TextBox).Text.Trim();
                string code = (CoursListView.Rows[e.RowIndex].FindControl("code_TextBox") as TextBox).Text.Trim();
                int Credit = Convert.ToInt32((CoursListView.Rows[e.RowIndex].FindControl("credits_TextBox") as TextBox).Text.Trim());

                string rqt = "UPDATE cours SET cours=@Cours,code_cours=@code_cours,credits=@credits WHERE id_cours=@idcours AND id_annee=@id_annee AND id_faculte=@id_faculte AND id_departement=@id_departement AND id_classe=@id_classe";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@idcours", Convert.ToInt32(CoursListView.DataKeys[e.RowIndex].Value.ToString()));
                cmd.Parameters.AddWithValue("@Cours", cours);
                cmd.Parameters.AddWithValue("@code_cours", code);
                cmd.Parameters.AddWithValue("@Credits", Convert.ToInt32(Credit));
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_classe", id_classe);

                if (cmd.ExecuteNonQuery() != 1)
                    Label_Error_Message.Text=" Echec de la modification";
                else
                    Label_Success_Message.Text = "Modification avec succès";

                CoursListView.EditIndex = -1;
                Cours_Par_Unite();
                conn.Close();
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void GDV_Creation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CoursListView.EditIndex = -1;
            Cours_Par_Unite();
        }
        protected void GDV_Creation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string rqt = "DELETE FROM cours WHERE id_cours=@id_cours";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_cours", Convert.ToInt32(CoursListView.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                conn.Close();

                Cours_Par_Unite();
                //Response.Write("<script>confirm('Enregistrement supprimé avec succès')</script>");
                Label_Success_Message.Text = "Enregistrement supprimé avec succès";
                Label_Error_Message.Text = "";
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }

        protected void Unite_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_meassage();
            id_ue = Convert.ToInt32((Unite_Combo.SelectedItem as ListItem).Value);
            Cours_Par_Unite();
        }

        protected void CoursListView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                clear_meassage();
                if (e.CommandName.Equals("AddNew"))
                {
                    MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                    conn.Open();
                    string cours = (CoursListView.FooterRow.FindControl("Cours_Editing_TextBox") as TextBox).Text.Trim();
                    string code = (CoursListView.FooterRow.FindControl("Code_Editing_TextBox") as TextBox).Text.Trim();
                    string credits = (CoursListView.FooterRow.FindControl("Credits_Editing_TextBox") as TextBox).Text.Trim();

                    if (cours.Length > 0 && code.Length > 0 && credits.Length > 0 && id_ue > 0)
                    {
                        String requette = "select count(*) from cours where id_unite=@ue and cours=@cours and id_annee=@id_annee and id_faculte=@id_faculte and id_departement=@id_departement and id_classe=@id_classe";
                        MySqlCommand com = new MySqlCommand(requette, conn);
                        com.Parameters.AddWithValue("@ue", id_ue);
                        com.Parameters.AddWithValue("@cours", cours);
                        com.Parameters.AddWithValue("@id_annee", id_annee);
                        com.Parameters.AddWithValue("@id_faculte", id_faculte);
                        com.Parameters.AddWithValue("@id_departement", id_departement);
                        com.Parameters.AddWithValue("@id_classe", id_classe);
                        int result = Convert.ToInt32(com.ExecuteScalar());
                        if (result == 0)
                        {
                            string rqt = "INSERT INTO cours(cours, code_cours, id_unite,credits,id_annee, id_faculte, id_departement, id_classe) VALUES (@Cours,@code_cours, @UE,@Credits,@id_annee, @id_faculte, @id_departement, @id_classe)";
                            MySqlCommand cmd = new MySqlCommand(rqt, conn);
                            cmd.Parameters.AddWithValue("@Cours", cours);
                            cmd.Parameters.AddWithValue("@code_cours", code);
                            cmd.Parameters.AddWithValue("@UE", id_ue);
                            cmd.Parameters.AddWithValue("@Credits", Convert.ToInt32(credits));
                            cmd.Parameters.AddWithValue("@id_annee", id_annee);
                            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                            cmd.Parameters.AddWithValue("@id_departement", id_departement);
                            cmd.Parameters.AddWithValue("@id_classe", id_classe);
                            cmd.ExecuteNonQuery();
                            Cours_Par_Unite();
                            Response.Write("<script>alert('Opération réussie')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Ce cours existe déjà dans la base de données')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Veuillez respecter les consignes, Compléter tous les champs nécessaires, Vérifier que la classe est encours, Vérifier que année est encours')</script>");
                        conn.Close();
                        return;

                    }
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }

        protected void Insert_Button_Click(object sender, EventArgs e)
        {
            try
            {
                clear_meassage();

                int n;
                if (!int.TryParse(Credit_TextBox.Value.Trim(), out n))
                {
                    Label_Error_Message.Text = "Donner une valeur entiere pour le crédit du cours";
                    return;
                }

                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string cours = Cours_TextBox.Value.Trim();
                string code = Code_TextBox.Value.Trim();
                string credits = Credit_TextBox.Value.Trim();

                if (cours.Length > 0 && code.Length > 0 && credits.Length > 0 && id_ue > 0)
                {
                    String requette = "select count(*) from cours where id_unite=@ue and cours=@cours and id_annee=@id_annee and id_faculte=@id_faculte and id_departement=@id_departement and id_classe=@id_classe";
                    MySqlCommand com = new MySqlCommand(requette, conn);
                    com.Parameters.AddWithValue("@ue", id_ue);
                    com.Parameters.AddWithValue("@cours", cours);
                    com.Parameters.AddWithValue("@id_annee", id_annee);
                    com.Parameters.AddWithValue("@id_faculte", id_faculte);
                    com.Parameters.AddWithValue("@id_departement", id_departement);
                    com.Parameters.AddWithValue("@id_classe", id_classe);
                    int result = Convert.ToInt32(com.ExecuteScalar());
                    if (result == 0)
                    {
                        string rqt = "INSERT INTO cours(cours, code_cours, id_unite,credits,id_annee, id_faculte, id_departement, id_classe) VALUES (@Cours,@code_cours, @UE,@Credits,@id_annee, @id_faculte, @id_departement, @id_classe)";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@Cours", cours);
                        cmd.Parameters.AddWithValue("@code_cours", code);
                        cmd.Parameters.AddWithValue("@UE", id_ue);
                        cmd.Parameters.AddWithValue("@Credits", Convert.ToInt32(credits));
                        cmd.Parameters.AddWithValue("@id_annee", id_annee);
                        cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmd.Parameters.AddWithValue("@id_departement", id_departement);
                        cmd.Parameters.AddWithValue("@id_classe", id_classe);
                        cmd.ExecuteNonQuery();
                        conn.Close();


                        Cours_Par_Unite();

                        Label_Success_Message.Text = "Opération réussie";
                    }
                    else
                    {
                        Label_Error_Message.Text = "Ce cours existe déjà dans la base de données";
                    }
                }
                else
                {
                    Label_Error_Message.Text = "Veuillez respecter les consignes, Compléter tous les champs nécessaires, Vérifier que la classe est encours, Vérifier que année est encours";
                }

                Credit_TextBox.Value = "";
                Cours_TextBox.Value = "";
                Code_TextBox.Value = "";
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }

        protected void clear_meassage()
        {
            Label_Error_Message.Text = "";
            Label_Success_Message.Text = "";
        }
    }
}