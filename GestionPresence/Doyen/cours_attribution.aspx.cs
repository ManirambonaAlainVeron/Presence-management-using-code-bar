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

namespace GestionPresence.Doyen
{
    public partial class cours_attribution : System.Web.UI.Page
    {
        MySqlConnection conn;

        public static int annee_precise, id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1, id_semestre = -1, id_ue = -1, id_cours = -1;
        int idf = -1, idp = -1;

        public static string annee, etat_annee, etat_classe, type_faculte, nom_faculte, etat_cours;


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
                //Load_Personnel();
            }
        }
        private void Load_Annee()
        {
            try
            {
                clear_meassage();
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                String reqdgv = "SELECT id_annee, annee FROM annee WHERE etat_annee=@etat_annee";
                MySqlCommand cmd = new MySqlCommand(reqdgv, conn);
                cmd.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = cmd.ExecuteReader();

                Annee_Combo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez l'année";
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
        private void Load_Operations()
        {
            Operation_Combo.Items.Clear();

            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "Selectioner l'opération";
            Operation_Combo.Items.Add(item0);

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Attribution du cours";
            Operation_Combo.Items.Add(item);

            ListItem item1 = new ListItem();
            item1.Value = "1";
            item1.Text = "Gestion de l'état d'enseignement du cours";
            Operation_Combo.Items.Add(item1);
        }
        public void Load_Attributions()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string req = " SELECT cours.id_cours, cours.cours, personnel.nom,personnel.prenom,personnel_diplome.sigle, cours.credits,personnel.id_personnel" +
                    " FROM (personnel INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON personnel.id_personnel = attribution_cours.id_personnel) INNER JOIN personnel_diplome ON personnel.id_diplome = personnel_diplome.id_diplome" +
                    " WHERE cours.id_classe=@id_classe AND cours.id_departement=@id_departement AND cours.id_faculte=@id_faculte AND cours.id_annee=@id_annee" +
                    " ORDER BY cours.cours";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@id_classe", id_classe);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                da.Fill(dtable);
                GDV_Attribution.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                if (dtable.Rows.Count > 0)
                {
                    GDV_Attribution.DataSource = dtable;
                    GDV_Attribution.DataBind();
                }
                else
                {
                    dtable.Rows.Add(dtable.NewRow());
                    GDV_Attribution.DataSource = dtable;
                    GDV_Attribution.DataBind();
                    GDV_Attribution.Rows[0].Cells.Clear();
                    GDV_Attribution.Rows[0].Cells.Add(new TableCell());
                    GDV_Attribution.Rows[0].Cells[0].ColumnSpan = dtable.Columns.Count;
                    GDV_Attribution.Rows[0].Cells[0].Text = "Aucune année académique n’a été créée à cette date";
                    GDV_Attribution.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }
        public void Load_Etats()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string req = "SELECT DISTINCT attribution_cours.id_cours, cours.cours, cours.credits,cours.code_cours, attribution_cours.etat" +
                    " FROM (unite INNER JOIN cours ON unite.id_unite = cours.id_unite) INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours" +
                    " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee";
                MySqlCommand cmc = new MySqlCommand(req, conn);
                cmc.Parameters.AddWithValue("@idcl", id_classe);
                cmc.Parameters.AddWithValue("@id_departement", id_departement);
                cmc.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmc.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataAdapter da = new MySqlDataAdapter(cmc);
                DataTable dtable = new DataTable();
                da.Fill(dtable);
                GDV_Etat_Avancement.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GDV_Etat_Avancement.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GDV_Etat_Avancement.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GDV_Etat_Avancement.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                if (dtable.Rows.Count > 0)
                {
                    GDV_Etat_Avancement.DataSource = dtable;
                    GDV_Etat_Avancement.DataBind();
                }
                else
                {
                    dtable.Rows.Add(dtable.NewRow());
                    GDV_Etat_Avancement.DataSource = dtable;
                    GDV_Etat_Avancement.DataBind();
                    GDV_Etat_Avancement.Rows[0].Cells.Clear();
                    GDV_Etat_Avancement.Rows[0].Cells.Add(new TableCell());
                    GDV_Etat_Avancement.Rows[0].Cells[0].ColumnSpan = dtable.Columns.Count;
                    GDV_Etat_Avancement.Rows[0].Cells[0].Text = "Aucune année académique n’a été créée à cette date";
                    GDV_Etat_Avancement.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }
        protected void Operation_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_meassage();
            MyMultiView.ActiveViewIndex = Convert.ToInt32(Operation_Combo.SelectedValue);
            int id_operation = MyMultiView.ActiveViewIndex;
            switch (id_operation)
            {
                case 0: Load_Attributions();
                    break;
                case 1: Load_Etats();
                    break;
                default:
                    break;
            }
        }
        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32((Annee_Combo.SelectedItem).Value);
            annee = Annee_Combo.SelectedItem.Text;
            //etat_annee = (Annee_Combo.SelectedItem as ListItem).string_value_1;

            Load_Faculte();
        }
        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32((Faculte_Combo.SelectedItem as ListItem).Value);
            nom_faculte = (Faculte_Combo.SelectedItem as ListItem).Text;

            Load_Departements(id_annee, id_faculte);
        }
        protected void ClasseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32((ClasseCombo.SelectedItem as ListItem).Value);
            etat_classe = (ClasseCombo.SelectedItem as ListItem).Text;
            Load_Operations();
            //Unite_Par_Classe();
            //Load_Attributions();
            //Load_Cours_Etat_Avancement();
            //Load_Cours_Par_Classe();
        }
        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_departement = Convert.ToInt32((Departement_Combo.SelectedItem as ListItem).Value);
            Load_Classe(id_annee, id_faculte, id_departement);
        }

        //============================Attribution cours GDV================================
        protected void GDV_Attribution_RowEditing(object sender, GridViewEditEventArgs e)
        {
            clear_meassage();
            GDV_Attribution.EditIndex = e.NewEditIndex;
            Load_Attributions();
        }
        protected void GDV_Attribution_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            clear_meassage();
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string rqt = "UPDATE attribution_cours SET id_personnel= @id_personnel WHERE id_cours=@id_cours";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_personnel", Convert.ToInt32((GDV_Attribution.Rows[e.RowIndex].FindControl("Enseignant_DropDown") as DropDownList).SelectedValue));
                cmd.Parameters.AddWithValue("@id_cours", Convert.ToInt32(GDV_Attribution.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                conn.Close();

                GDV_Attribution.EditIndex = -1;
                Load_Attributions();
                Label_Success_Message.Text = "Modification réussie";
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void GDV_Attribution_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clear_meassage();
            GDV_Attribution.EditIndex = -1;
            Load_Attributions();
        }
        protected void GDV_Attribution_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            clear_meassage();
            if (e.Row.RowType == DataControlRowType.DataRow && GDV_Attribution.EditIndex == e.Row.RowIndex)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    conn = new MySqlConnection(Authentification.MyString);
                    conn.Open();

                    string req = "SELECT DISTINCT personnel_fonction.id_personnel, CONCAT( personnel_diplome.sigle,' ',personnel.nom,' ',personnel.prenom) AS fullname " +
                        " FROM (personnel_diplome INNER JOIN (personnel INNER JOIN personnel_fonction ON personnel.id_personnel = personnel_fonction.id_personnel) ON personnel_diplome.id_diplome = personnel.id_diplome) INNER JOIN personnel_type ON personnel.id_personnel_type = personnel_type.id_personnel_type" +
                        " WHERE personnel_fonction.fin_activite=@fin_activite  AND personnel_type.id_personnel_type=@id_personnel_type";

                    MySqlCommand cmd = new MySqlCommand(req, conn);
                    cmd.Parameters.AddWithValue("@id_personnel_type", 1);
                    cmd.Parameters.AddWithValue("@fin_activite", " ");

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    conn.Close();


                    DataRowView drv = e.Row.DataItem as DataRowView;
                    DropDownList DropDownList = (DropDownList)e.Row.FindControl("Enseignant_DropDown");
                    DropDownList.DataTextField = "fullname";
                    DropDownList.DataValueField = "id_personnel";
                    DropDownList.DataSource = ds;
                    DropDownList.DataBind();
                    DropDownList.SelectedValue = drv["id_personnel"].ToString();
                }
            }
        }
        protected void GDV_Attribution_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            clear_meassage();
            GDV_Attribution.PageIndex = e.NewPageIndex;
            GDV_Attribution.DataBind();
            Load_Attributions();
        }
        //============================Etat avancement GDV================================
        protected void GDV_Etat_Avancement_RowEditing(object sender, GridViewEditEventArgs e)
        {
            clear_meassage();
            GDV_Etat_Avancement.EditIndex = e.NewEditIndex;
            Load_Etats();
        }
        protected void GDV_Etat_Avancement_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clear_meassage();
            GDV_Etat_Avancement.EditIndex = -1;
            Load_Etats();
        }
        protected void GDV_Etat_Avancement_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string etat = "";

                string req = "SELECT etat FROM attribution_cours WHERE id_cours=@id_cours";
                MySqlCommand cmda = new MySqlCommand(req, conn);
                cmda.Parameters.AddWithValue("@id_cours", Convert.ToInt32(GDV_Etat_Avancement.DataKeys[e.RowIndex].Value.ToString()));
                MySqlDataReader dra = cmda.ExecuteReader();
                if (dra.Read())
                {
                    etat_cours = dra.GetString(0);
                }
                dra.Close();

                switch ((GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Etat_Cours_DropDown") as DropDownList).Text.Trim())
                {
                    case "En attente":
                        etat = (GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Etat_Cours_DropDown") as DropDownList).Text.Trim();
                        break;
                    case "Encours":
                        etat = (GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Etat_Cours_DropDown") as DropDownList).Text.Trim();
                        break;
                    case "Cloturee":
                        etat = (GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Etat_Cours_DropDown") as DropDownList).Text.Trim();
                        break;
                    default:
                        break;
                }
                string rqt = "UPDATE attribution_cours SET etat= @etat WHERE id_cours=@id_cours";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_cours", Convert.ToInt32(GDV_Etat_Avancement.DataKeys[e.RowIndex].Value.ToString()));
                cmd.Parameters.AddWithValue("@etat", etat);
                cmd.ExecuteNonQuery();
                conn.Close();

                GDV_Etat_Avancement.EditIndex = -1;
                Load_Etats();
                Label_Success_Message.Text = "Modification réussie";
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }

        }
        protected void GDV_Etat_Avancement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            clear_meassage();
            GDV_Etat_Avancement.PageIndex = e.NewPageIndex;
            GDV_Etat_Avancement.DataBind();
            Load_Etats();
        }


        protected void clear_meassage()
        {
            Label_Error_Message.Text = "";
            Label_Success_Message.Text = "";
        }
    }
}