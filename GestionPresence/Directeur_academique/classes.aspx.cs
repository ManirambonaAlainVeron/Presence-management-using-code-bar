using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO.MemoryMappedFiles;
using System.Configuration;

namespace GestionPresence.Directeur_academique
{
    public partial class classes : System.Web.UI.Page
    {
        string annee, type, classe;
        public static string etat_avancement, date_debut, date_fin;
        public static int id_annee_gestion = -1, id_type = -1;
        public static int id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1, id_annee_combo = -1, id_faculte_combo = -1, id_departement_combo = -1, id_operation = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Operations();
                //Classes_In_Combo();
            }
        }
        private void Load_Operations()
        {
            Operation_Combo.Items.Clear();
            
            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "Choisissez l'opération";
            Operation_Combo.Items.Add(item0);

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Création des classes";
            Operation_Combo.Items.Add(item);

            ListItem item1 = new ListItem();
            item1.Value = "1";
            item1.Text = "Organisation des classes par département";
            Operation_Combo.Items.Add(item1);

            ListItem item2 = new ListItem();
            item2.Value = "2";
            item2.Text = "Gestion des états d'avancement";
            Operation_Combo.Items.Add(item2);
        }
        protected void Operation_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyMultiView.ActiveViewIndex = Convert.ToInt32(Operation_Combo.SelectedValue);
            id_operation = MyMultiView.ActiveViewIndex;
            switch (id_operation)
            {
                case 0: Classes_In_GridView();
                    break;
                case 1: Load_Annee();
                    Classes_In_Combo();
                    break;
                case 2: Load_Annee();
                    break;
                default:
                    break;
            }
        }
        private void Load_Annee()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                String reqdgv = "SELECT id_annee, annee FROM annee WHERE etat_annee=@etat_annee";
                MySqlCommand cmd = new MySqlCommand(reqdgv, conn);
                cmd.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = cmd.ExecuteReader();

                Annee_Combo.Items.Clear();
                Annee_Etat_Combo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "";
                switch (id_operation)
                {
                    case 1:
                        Annee_Combo.Items.Add(item0);
                        while (dr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = dr.GetInt32(0).ToString();
                            item.Text = dr.GetString(1);
                            Annee_Combo.Items.Add(item);
                        }
                        dr.Close();
                        break;
                    case 2:
                        Annee_Etat_Combo.Items.Add(item0);
                        while (dr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = dr.GetInt32(0).ToString();
                            item.Text = dr.GetString(1);
                            Annee_Etat_Combo.Items.Add(item);
                        }
                        dr.Close();
                        break;
                    default:
                        break;
                }


                conn.Close();
            }
            catch (Exception ex)
            {
                string t = ex.Message;
            }
        }
        public void Facultes_par_Annee(int id_annee)
        {
            id_annee_combo = id_annee;
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string req = "SELECT faculte_par_annee.id_faculte, faculte.faculte" +
                " FROM faculte INNER JOIN faculte_par_annee ON faculte.id_faculte = faculte_par_annee.id_faculte" +
                " WHERE faculte_par_annee.id_annee=@id_annee";
            MySqlCommand cmd = new MySqlCommand(req, conn);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);
            MySqlDataReader dr = cmd.ExecuteReader();

            Faculte_Combo.Items.Clear();
            Faculte_Etat_Combo.Items.Clear();

            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "";
            switch (id_operation)
            {
                case 1:
                    Faculte_Combo.Items.Add(item0);
                    while (dr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = dr.GetInt32(0).ToString();
                        item.Text = dr.GetString(1);
                        Faculte_Combo.Items.Add(item);
                    }
                    dr.Close();
                    break;
                case 2:
                    Faculte_Etat_Combo.Items.Add(item0);
                    while (dr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Value = dr.GetInt32(0).ToString();
                        item.Text = dr.GetString(1);
                        Faculte_Etat_Combo.Items.Add(item);
                    }
                    dr.Close();
                    break;
                default:
                    break;
            }
            conn.Close();
        }
        private void Departements_par_Faculte(int idannee, int idfaculte)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string req = "SELECT DISTINCT departement_par_faculte.id_departement, departement.departement" +
                    " FROM departement INNER JOIN departement_par_faculte ON departement.id_departement = departement_par_faculte.id_departement" +
                    " WHERE departement_par_faculte.id_annee=@id_annee AND departement_par_faculte.id_faculte=@id_faculte";

                MySqlCommand cmt = new MySqlCommand(req, conn);
                cmt.Parameters.AddWithValue("@id_annee", id_annee);
                cmt.Parameters.AddWithValue("@id_faculte", id_faculte);
                MySqlDataReader dr = cmt.ExecuteReader();

                Departement_DropDownList.Items.Clear();
                Departement_Etat_Combo.Items.Clear();
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "";
                switch (id_operation)
                {
                    case 1:
                        Departement_DropDownList.Items.Add(item0);
                        while (dr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = dr.GetInt32(0).ToString();
                            item.Text = dr.GetString(1);
                            Departement_DropDownList.Items.Add(item);
                        }
                        dr.Close();
                        break;
                    case 2:
                        Departement_Etat_Combo.Items.Add(item0);
                        while (dr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Value = dr.GetInt32(0).ToString();
                            item.Text = dr.GetString(1);
                            Departement_Etat_Combo.Items.Add(item);
                        }
                        dr.Close();
                        break;
                    default:
                        break;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void Classes_In_GridView()
        {
            clearMessage();
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();

            string req = "SELECT DISTINCT id_classe, classe FROM classe";
            MySqlDataAdapter da = new MySqlDataAdapter(req, conn);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            GDV_Creation.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //GDV_Creation.RowStyle.HorizontalAlign = HorizontalAlign.Center;
            if (dtable.Rows.Count > 0)
            {
                GDV_Creation.DataSource = dtable;
                GDV_Creation.DataBind();
            }
            else
            {
                dtable.Rows.Add(dtable.NewRow());
                GDV_Creation.DataSource = dtable;
                GDV_Creation.DataBind();

                GDV_Creation.Rows[0].Cells.Clear();
                GDV_Creation.Rows[0].Cells.Add(new TableCell());
                GDV_Creation.Rows[0].Cells[0].ColumnSpan = dtable.Columns.Count;
                GDV_Creation.Rows[0].Cells[0].Text = "Aucune classe créée à cette date";
                GDV_Creation.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
        }
        private void Classes_In_Combo()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string req = "SELECT DISTINCT id_classe, classe FROM classe";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                Classe_DropdownList.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "";
                Classe_DropdownList.Items.Add(item0);
                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Classe_DropdownList.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch { }
        }
        private void Classes_par_Departement(int id_annee, int id_faculte, int id_departement, int operation)
        {
            id_annee_combo = id_annee;
            id_faculte_combo = id_faculte;
            id_departement_combo = id_departement;
            int id_operation = operation;

            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string req = "SELECT classe_par_departement.id_classe, classe.classe,classe_par_departement.etat_avancement " +
                " FROM classe_par_departement INNER JOIN classe ON classe_par_departement.id_classe = classe.id_classe" +
                " WHERE classe_par_departement.id_annee=@id_annee AND classe_par_departement.id_faculte=@id_faculte AND classe_par_departement.id_departement=@id_departement";
            MySqlCommand cmd = new MySqlCommand(req, conn);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);
            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
            cmd.Parameters.AddWithValue("@id_departement", id_departement);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);

            switch (id_operation)
            {
                case 1:
                    GDV_Gestion.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    GDV_Gestion.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    GDV_Gestion.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    if (dtable.Rows.Count > 0)
                    {
                        GDV_Gestion.DataSource = dtable;
                        GDV_Gestion.DataBind();
                    }
                    else
                    {
                        dtable.Rows.Add(dtable.NewRow());
                        GDV_Gestion.DataSource = dtable;
                        GDV_Gestion.DataBind();

                        GDV_Gestion.Rows[0].Cells.Clear();
                        GDV_Gestion.Rows[0].Cells.Add(new TableCell());
                        GDV_Gestion.Rows[0].Cells[0].ColumnSpan = dtable.Columns.Count;
                        GDV_Gestion.Rows[0].Cells[0].Text = "Aucune classe dans ce départemnt";
                        GDV_Gestion.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    }
                    break;
                case 2:
                    GDV_Etat_Avancement.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    GDV_Etat_Avancement.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    GDV_Etat_Avancement.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
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
                        GDV_Etat_Avancement.Rows[0].Cells[0].Text = "Aucune classe dans ce départemnt";
                        GDV_Etat_Avancement.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    }
                    break;
            }
            conn.Close();
        }
        //===================1ere operation========================================================
        protected void GDV_Creation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            clearMessage();
        }
        protected void GDV_Creation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                clearMessage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string requette = "SELECT COUNT(*) FROM classe_par_departement WHERE id_classe=@id_classe";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                int result = Convert.ToInt32(cmda.ExecuteScalar());

                if (result == 0)
                {
                    string rqt = "DELETE FROM classe WHERE id_classe=@id_classe";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.ExecuteNonQuery();
                    Label_Success_Message.Text = "Supprimé avec succès";
                }
                else
                {
                    Label_Error_Message.Text = "Impossible du supprimer une classe encours d'utilisation";
                }
                conn.Close();

                Classes_In_GridView();
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void GDV_Creation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            clearMessage();
            GDV_Creation.EditIndex = e.NewEditIndex;
            Classes_In_GridView();
        }
        protected void GDV_Creation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                clearMessage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string clsse = "";

                string req = "SELECT classe FROM classe WHERE id_classe=@id_classe";
                MySqlCommand cmdo = new MySqlCommand(req, conn);
                cmdo.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                MySqlDataReader dr = cmdo.ExecuteReader();
                while (dr.Read())
                {
                    clsse = dr.GetString(0);
                }
                dr.Close();

                classe = (GDV_Creation.Rows[e.RowIndex].FindControl("Classe_TextBox_Editing") as TextBox).Text.Trim();

                if (classe.Length > 0 && classe != clsse)
                {
                    string requette = "SELECT COUNT(*) FROM classe WHERE classe=@classe";
                    MySqlCommand cmda = new MySqlCommand(requette, conn);
                    cmda.Parameters.AddWithValue("@classe", classe);
                    int result = Convert.ToInt32(cmda.ExecuteScalar());
                    if (result == 0)
                    {
                        string rqt = "UPDATE classe SET classe=@classe WHERE id_classe=@id_classe";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                        cmd.Parameters.AddWithValue("@classe", classe);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        GDV_Creation.EditIndex = -1;
                        Label_Success_Message.Text = "Modification réussie";
                    }
                    else
                    {
                        Label_Success_Message.Text = "Cet élément existe déjà dans la base de données \n ou que vous avez saisi le meme nom que l'encien";
              
                    }
                }
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
            Classes_In_GridView();
        }
        protected void GDV_Creation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clearMessage();
            GDV_Creation.EditIndex = -1;
            Classes_In_GridView();
        }
        protected void enre_but_Click(object sender, EventArgs e)
        {
            try
            {
                clearMessage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string classe = Classe_TextBox.Value;
                if (classe.Length > 0)
                {
                    string requette = "SELECT COUNT(*) FROM classe WHERE classe=@classe";
                    MySqlCommand cmda = new MySqlCommand(requette, conn);
                    cmda.Parameters.AddWithValue("@classe", classe);
                    int result = Convert.ToInt32(cmda.ExecuteScalar());

                    if (result == 0)
                    {
                        string rqt = "INSERT INTO classe(classe) values(@classe)";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@classe", classe);
                        cmd.ExecuteNonQuery();
                        Label_Success_Message.Text = "Enregistrement reussi";
                    }
                    else
                    {
                        Label_Error_Message.Text = "Cette est deja creee";
                    }
                    conn.Close();
                    Classes_In_GridView();
                    Classe_TextBox.Value = "";
                }
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }
        //==================2eme operation==========================================================
        protected void GDV_Gestion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                clearMessage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string requette = "SELECT COUNT(*) FROM unite WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
                cmda.Parameters.AddWithValue("@id_departement", id_departement);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "DELETE FROM classe_par_departement WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Label_Success_Message.Text = "Suppression réussie avec succès";
                }
                else
                {
                    Label_Error_Message.Text = "Suppression impossible puisque cet élément est encours d'utilisation quelque part!";
                }
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
            Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedItem.Value);
            annee = Annee_Combo.SelectedItem.Text;
            Facultes_par_Annee(id_annee);
        }
        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearMessage();
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedItem.Value);
            Departements_par_Faculte(id_annee, id_faculte);
        }
        protected void Departement_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_departement = Convert.ToInt32(Departement_DropDownList.SelectedItem.Value);
            Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
        }
        protected void Classe_DropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearMessage();
            id_classe = Convert.ToInt32(Classe_DropdownList.SelectedItem.Value);
        }
        protected void Insert_Button_Click(object sender, EventArgs e)
        {
            try
            {
                clearMessage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string req = "SELECT COUNT(*) FROM classe_par_departement WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(req, conn);
                cmda.Parameters.AddWithValue("@id_classe", id_classe);
                cmda.Parameters.AddWithValue("@id_departement", id_departement);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "INSERT INTO classe_par_departement(id_classe,etat_avancement,id_departement,id_faculte,id_annee) values(@id_classe,@etat_avancement,@id_departement,@id_faculte,@id_annee)";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_classe", id_classe);
                    cmd.Parameters.AddWithValue("@etat_avancement", "En attente");
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Label_Success_Message.Text = "Enregistrement reussi ";
                }
                else
                {
                    Label_Error_Message.Text = "Cette classe est deja ajoutee";
                }


            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
            Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
        }


        //==================3eme operation==========================================================
        protected void Annee_Etat_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearMessage();
            id_annee = Convert.ToInt32(Annee_Etat_Combo.SelectedItem.Value);
            Facultes_par_Annee(id_annee);
        }

        protected void Faculte_Etat_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearMessage();
            id_faculte = Convert.ToInt32(Faculte_Etat_Combo.SelectedItem.Value);
            Departements_par_Faculte(id_annee, id_faculte);
        }

        protected void Departement_Etat_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearMessage();
            id_departement = Convert.ToInt32(Departement_Etat_Combo.SelectedItem.Value);
            Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
        }

        protected void GDV_Etat_Avancement_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clearMessage();
            GDV_Etat_Avancement.EditIndex = -1;
            Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
        }

        protected void GDV_Etat_Avancement_RowEditing(object sender, GridViewEditEventArgs e)
        {
            clearMessage();
            GDV_Etat_Avancement.EditIndex = e.NewEditIndex;
            Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
        }

        protected void GDV_Etat_Avancement_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                clearMessage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string etat = "";

                string req = "SELECT etat_avancement FROM classe_par_departement WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(req, conn);
                cmda.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Etat_Avancement.DataKeys[e.RowIndex].Value.ToString()));
                cmda.Parameters.AddWithValue("@id_departement", id_departement);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader dra = cmda.ExecuteReader();
                if (dra.Read())
                {
                    etat_avancement = dra.GetString(0);
                }
                dra.Close();

                switch ((GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim())
                {
                    case "En attente":
                        etat = (GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim();
                        break;
                    case "Encours":
                        etat = (GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim();
                        break;
                    case "Clôturée":
                        etat = (GDV_Etat_Avancement.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim();
                        break;
                    default:
                        break;
                }
                string rqt = "UPDATE classe_par_departement SET etat_avancement=@etat_avancement WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_classe", Convert.ToInt32(GDV_Etat_Avancement.DataKeys[e.RowIndex].Value.ToString()));
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                cmd.Parameters.AddWithValue("@etat_avancement", etat);
                cmd.ExecuteNonQuery();

                GDV_Etat_Avancement.EditIndex = -1;
                conn.Close();


                Classes_par_Departement(id_annee, id_faculte, id_departement, id_operation);
                Label_Success_Message.Text = "Modification réussie";

            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }

        protected void clearMessage()
        {
            Label_Error_Message.Text = "";
            Label_Success_Message.Text = "";
        }

    }
}