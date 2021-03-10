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

namespace GestionPresence.Doyen
{
    public partial class departement : System.Web.UI.Page
    {
        string annee, type;
        public static string etat_annee, date_debut, date_fin;
        public static int id_annee_gestion = -1, id_type = -1;
        public static int id_annee = -1, id_faculte = -1, id_departement = -1, id_annee_combo = -1, id_faculte_combo = -1;
        string depart;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Operations();
            }
        }
        public void Liste_Facultes_Affectees_En_Combo(int id_annee)
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
        private void Liste_Departemts_Crees_In_GridView()
        {
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();

            string req = "SELECT DISTINCT id_departement, departement FROM departement";
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
                GDV_Creation.Rows[0].Cells[0].Text = "Aucun departemnt n’a été créée à cette date";
                GDV_Creation.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
        }
        private void Liste_Departemts_Crees_In_Combo()
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string req = "SELECT id_departement, departement FROM departement";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

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
                Label_Error_Message.Text = ex.Message;
            }
        }

        private void Liste_Departemts_Affectes_In_GridView(int id_annee, int id_faculte)
        {
            id_annee_combo = id_annee;
            id_faculte_combo = id_faculte;

            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string req = "SELECT departement_par_faculte.id_departement, departement.departement" +
                " FROM departement_par_faculte INNER JOIN departement ON departement_par_faculte.id_departement = departement.id_departement" +
                " WHERE departement_par_faculte.id_annee=@id_annee AND departement_par_faculte.id_faculte=@id_faculte";
            MySqlCommand cmd = new MySqlCommand(req, conn);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);
            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            GDV_Gestion.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
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
                GDV_Gestion.Rows[0].Cells[0].Text = "Aucun departemnt n’a été affecté";
                GDV_Gestion.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
        }

        private void Fill_Annee_Combo()
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                String reqdgv = "SELECT id_annee, annee FROM annee WHERE etat_annee=@etat_annee";
                MySqlCommand cmd = new MySqlCommand(reqdgv, conn);
                cmd.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = cmd.ExecuteReader();

                Annee_Combo.Items.Clear();
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisisser l'année";
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
                Label_Error_Message.Text = ex.Message;
            }
        }
        private void Fill_Faculte_Par_Annee_Combo()
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string req = "SELECT faculte_par_annee.id_faculte, faculte.faculte" +
                    " FROM faculte INNER JOIN faculte_par_annee ON faculte.id_faculte = faculte_par_annee.id_faculte" +
                    " WHERE faculte_par_annee.id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader dr = cmd.ExecuteReader();

                Faculte_Combo.Items.Clear();
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez la Faculté";
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
                Label_Error_Message.Text = ex.Message;
            }
        }

        private void Load_Operations()
        {
            Operation_Combo.Items.Clear();

            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "Choisissez l’opération à exécuter";
            Operation_Combo.Items.Add(item0);

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Création des départements";
            Operation_Combo.Items.Add(item);

            ListItem item1 = new ListItem();
            item1.Value = "1";
            item1.Text = "Organisation des départements par facultés/Instituts";
            Operation_Combo.Items.Add(item1);
        }
        protected void Operation_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_meassage();
            Departement_Combo.SelectedValue = "-1";
            Annee_Combo.SelectedValue = "-1";
            Faculte_Combo.SelectedValue = "-1";
            MyMultiView.ActiveViewIndex = Convert.ToInt32(Operation_Combo.SelectedValue);
            int id_operation = MyMultiView.ActiveViewIndex;
            switch (id_operation)
            {
                case 0: Liste_Departemts_Crees_In_GridView();
                    break;
                case 1: Fill_Annee_Combo();
                    break;
                default:
                    break;
            }
        }

        //===================CREATION========================================================
        protected void GDV_Creation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            clear_meassage();
        }
        protected void GDV_Creation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string requette = "SELECT COUNT(*) FROM departement_par_faculte WHERE id_departement=@id_departement";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_departement", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {

                    string rqt = "DELETE FROM departement WHERE id_departement=@id_departement";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_departement", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Label_Success_Message.Text = "Enregistrement supprimé avec succès";
                    Label_Error_Message.Text = "";
                }
                else
                {
                    Label_Success_Message.Text = "";
                    Label_Error_Message.Text = "Impossible de supprimer. Cet element est utilise dans un autre endroit";
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
            Liste_Departemts_Crees_In_GridView();
        }
        protected void GDV_Creation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GDV_Creation.EditIndex = e.NewEditIndex;
            Liste_Departemts_Crees_In_GridView();
        }
        protected void GDV_Creation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string dptmnt = "";

                string req = "SELECT departement FROM departement WHERE id_departement=@id_departement";
                MySqlCommand cmdo = new MySqlCommand(req, conn);
                cmdo.Parameters.AddWithValue("@id_departement", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                MySqlDataReader dr = cmdo.ExecuteReader();
                while (dr.Read())
                {
                    dptmnt = dr.GetString(0);
                }
                dr.Close();

                depart = (GDV_Creation.Rows[e.RowIndex].FindControl("Departement_TextBox_Editing") as TextBox).Text.Trim();

                if (depart.Length > 0 && (depart.CompareTo(dptmnt) != 0))
                {
                    string rqt = "UPDATE departement SET departement=@departement WHERE id_departement=@id_departement";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_departement", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@departement", depart);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Label_Success_Message.Text = "Modification réussie";
                    Label_Error_Message.Text = "";
                }
                else
                {
                    Label_Success_Message.Text = "Il est possible que Cet élément existe déjà dans la base de données \n ou que vous avez saisi le meme nom que l'encien";
                    Label_Error_Message.Text = "";
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
            GDV_Creation.EditIndex = -1;
            Liste_Departemts_Crees_In_GridView();
        }
        protected void GDV_Creation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GDV_Creation.EditIndex = -1;
            Liste_Departemts_Crees_In_GridView();
        }
        protected void AddNew_Button_Click(object sender, EventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string departement = Departement_TextBox_Footer.Value;
                if (departement.Length > 0)
                {
                    string requette = "SELECT COUNT(*) FROM departement WHERE departement=@departement";
                    MySqlCommand cmda = new MySqlCommand(requette, conn);
                    cmda.Parameters.AddWithValue("@departement", departement);
                    int result = Convert.ToInt32(cmda.ExecuteScalar());

                    if (result == 0)
                    {
                        string rqt = "INSERT INTO departement(departement) values(@departement)";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@departement", departement);
                        cmd.ExecuteNonQuery();
                        Label_Success_Message.Text = "Enregistrement reussi";

                    }
                    else
                    {
                        Label_Error_Message.Text = "Ce departement est deja creee";
                    }
                    conn.Close();
                    Liste_Departemts_Crees_In_GridView();
                }
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }


        //==================GESTION==========================================================
        protected void GDV_Gestion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                clear_meassage();
                Departement_Combo.SelectedValue = "-1";
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string requette = "SELECT COUNT(*) FROM classe_par_departement WHERE id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_departement", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "DELETE FROM departement_par_faculte WHERE id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_departement", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
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
            Liste_Departemts_Affectes_In_GridView(id_annee, id_faculte);
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_meassage();
            id_annee = Convert.ToInt32(Annee_Combo.SelectedItem.Value);
            annee = Annee_Combo.SelectedItem.Text;
            Liste_Facultes_Affectees_En_Combo(id_annee);

            //Fill_Faculte_Par_Annee_Combo();
        }
        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_meassage();
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedItem.Value);
            depart = Faculte_Combo.SelectedItem.Text;
            Liste_Departemts_Affectes_In_GridView(id_annee, id_faculte);
            Liste_Departemts_Crees_In_Combo();
        }
        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear_meassage();
            id_departement = Convert.ToInt32(Departement_Combo.SelectedItem.Value);
        }
        protected void Insert_Button_Click(object sender, EventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string req = "SELECT COUNT(*) FROM departement_par_faculte WHERE id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(req, conn);
                cmda.Parameters.AddWithValue("@id_departement", id_departement);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "INSERT INTO departement_par_faculte(id_departement,id_faculte,id_annee) values(@id_departement,@id_faculte,@id_annee)";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Label_Success_Message.Text = "Enregistrement reussi ";

                    Departement_Combo.SelectedValue = "-1";
                }
                else
                {
                    Label_Error_Message.Text = "Ce departement est deja creee";
                }


            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
            Liste_Departemts_Affectes_In_GridView(id_annee, id_faculte);
        }


        protected void OnDataBound(object sender, EventArgs e)
        {
            DropDownList ddlCountries = GDV_Gestion.FooterRow.FindControl("Faculte_DropDown_Footer") as DropDownList;
            ddlCountries.DataSource = GetData("SELECT DISTINCT faculte FROM faculte");
            ddlCountries.DataTextField = "faculte";
            ddlCountries.DataValueField = "faculte";
            ddlCountries.DataBind();
            ddlCountries.Items.Insert(0, new ListItem("", "-1"));
        }
        private DataTable GetData(string query)
        {
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        protected void clear_meassage()
        {
            Label_Error_Message.Text = "";
            Label_Success_Message.Text = "";
        }
    }
}