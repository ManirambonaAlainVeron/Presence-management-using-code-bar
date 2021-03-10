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
    public partial class facultes : System.Web.UI.Page
    {
        string annee, type, faculte;
        public static string etat_annee, date_debut, date_fin;
        public static int id_annee_gestion = -1, id_type = -1;
        public static int id_annee = -1, id_faculte = -1, id_departement = -1, id_annee_combo = -1, id_faculte_combo = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Operations();
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
            item.Text = "Créer une faculté ou un institut";
            Operation_Combo.Items.Add(item);

            ListItem item1 = new ListItem();
            item1.Value = "1";
            item1.Text = "Organiser les facultés et instituts par A-A";
            Operation_Combo.Items.Add(item1);
        }
        protected void Operation_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyMultiView.ActiveViewIndex = Convert.ToInt32(Operation_Combo.SelectedValue);
            int id_operation = MyMultiView.ActiveViewIndex;
            switch (id_operation)
            {
                case 0: Facultes_Crees_In_GridView();
                    break;
                case 1: Load_Annee_Combo();
                    Facultes_Crees_In_Combo();
                    break;
                default:
                    break;
            }
        }
        private void Facultes_Crees_In_GridView()
        {
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();

            string req = "SELECT faculte.id_faculte, faculte.faculte, faculte.type sigle, faculte_type.type" +
                    " FROM faculte INNER JOIN faculte_type ON faculte.id_type = faculte_type.id_type";
            MySqlDataAdapter da = new MySqlDataAdapter(req, conn);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            GDV_Creation.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GDV_Creation.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GDV_Creation.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
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
                GDV_Creation.Rows[0].Cells[0].Text = "Aucune faculté n’a été créée à cette date";
                GDV_Creation.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
        }
        private void Load_Annee_Combo()
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
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "";
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
                string t = ex.Message;
            }
        }
        private void Facultes_Affectes_In_GridView(int id_annee)
        {
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string req = "SELECT faculte_par_annee.id_faculte, faculte.faculte, faculte.type sigle" +
                    " FROM faculte INNER JOIN faculte_par_annee ON faculte.id_faculte = faculte_par_annee.id_faculte" +
                    " WHERE faculte_par_annee.id_annee=@id_annee";
            MySqlCommand cmd = new MySqlCommand(req, conn);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
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
                GDV_Gestion.Rows[0].Cells[0].Text = "Aucune faculte/institut n’a été affecté";
                GDV_Gestion.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
        }
        private void Facultes_Crees_In_Combo()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open(); 
                string req = "SELECT id_faculte, faculte FROM faculte where id_annee != @id_an";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@id_an",id_annee);
                MySqlDataReader dr = cmd.ExecuteReader();

                Departement_Combo.Items.Clear();
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = " Choisissez une faculté à ajouter";
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
            catch { }
        }



        //===================CREATION========================================================
        protected void GDV_Creation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = "";
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string requette = "SELECT COUNT(*) FROM faculte_par_annee WHERE id_faculte=@id_faculte";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_faculte", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "DELETE FROM faculte WHERE id_faculte=@id_faculte";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_faculte", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
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
            Facultes_Crees_In_GridView();
        }
        protected void GDV_Creation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Label_Success_Message.Text = "";
            Label_Error_Message.Text = "";
            GDV_Creation.EditIndex = e.NewEditIndex;
            Facultes_Crees_In_GridView();
        }
        protected void GDV_Creation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = "";
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string fclte = "", typ = "", sgl = "";

                string req = "SELECT  faculte.faculte, faculte.type, faculte_type.type" +
                    " FROM faculte INNER JOIN faculte_type ON faculte.id_type = faculte_type.id_type" +
                    " WHERE faculte.id_faculte=@id_faculte";
                MySqlCommand cmdo = new MySqlCommand(req, conn);
                cmdo.Parameters.AddWithValue("@id_faculte", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                MySqlDataReader dr = cmdo.ExecuteReader();
                while (dr.Read())
                {
                    fclte = dr.GetString(0);
                    sgl = dr.GetString(1);
                    typ = dr.GetString(2);
                }
                dr.Close();

                string faculta = (GDV_Creation.Rows[e.RowIndex].FindControl("Departement_TextBox_Editing") as TextBox).Text.Trim();
                string sigle = (GDV_Creation.Rows[e.RowIndex].FindControl("Sigle_TextBox_Editing") as TextBox).Text.Trim();
                string typa = (GDV_Creation.Rows[e.RowIndex].FindControl("Type_DropDown_Editing") as DropDownList).SelectedValue;

                int id_typa = -1;
                if (typa == "Faculté")
                {
                    id_typa = 1;
                }
                else if (typa == "Institut")
                {
                    id_typa = 2;
                }
                else
                {
                    id_typa = -1;
                }

                if ((faculta.Length > 0 && (faculta.CompareTo(fclte) != 0)) || (sigle.Length > 0 && (sigle.CompareTo(sgl) != 0)) || (typa.CompareTo(typ) != 0))
                {
                    string rqt = "UPDATE faculte SET faculte=@faculte, type=@sigle, id_type=@id_type WHERE id_faculte=@id_faculte";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_faculte", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@faculte", faculta);
                    cmd.Parameters.AddWithValue("@sigle", sigle);
                    cmd.Parameters.AddWithValue("@id_type", id_typa);
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
            Facultes_Crees_In_GridView();
        }
        protected void GDV_Creation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Label_Success_Message.Text = "";
            Label_Error_Message.Text = "";
            GDV_Creation.EditIndex = -1;
            Facultes_Crees_In_GridView();
        }
        protected void AddNew_Button_Click(object sender, EventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string facultea = Departement_TextBox_Footer.Value;
                if (facultea.Length > 0)
                {
                    string requette = "SELECT COUNT(*) FROM faculte WHERE faculte=@faculte";
                    MySqlCommand cmda = new MySqlCommand(requette, conn);
                    cmda.Parameters.AddWithValue("@faculte", facultea);
                    int result = Convert.ToInt32(cmda.ExecuteScalar());

                    if (result == 0)
                    {
                        string rqt = "INSERT INTO faculte(faculte,type,id_type) values(@faculte,@sigle,@id_type)";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@faculte", facultea);
                        cmd.Parameters.AddWithValue("@sigle", Sigle_TextBox_Footer.Value);
                        cmd.Parameters.AddWithValue("@id_type", Convert.ToInt32(Type_DropDown.SelectedValue));
                        cmd.ExecuteNonQuery();
                        Label_Success_Message.Text = "Enregistrement reussi";
                        Label_Error_Message.Text = "";
                    }
                    else
                    {
                        Label_Success_Message.Text = "";
                        Label_Error_Message.Text = "Ce departement est deja creee";
                    }
                    conn.Close();
                    Departement_TextBox_Footer.Value = "";
                    Sigle_TextBox_Footer.Value = "";
                    Type_DropDown.SelectedValue = "-1";
                    Facultes_Crees_In_GridView();
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }


        //==================GESTION==========================================================
        protected void GDV_Gestion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string requette = "SELECT COUNT(*) FROM departement_par_faculte WHERE  id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_faculte", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "DELETE FROM faculte_par_annee WHERE id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_faculte", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
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
            Facultes_Affectes_In_GridView(id_annee);
        }
        protected void Insert_Button_Click(object sender, EventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string req = "SELECT COUNT(*) FROM faculte_par_annee WHERE id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(req, conn);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "INSERT INTO faculte_par_annee(id_faculte,id_annee) values(@id_faculte,@id_annee)";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Label_Success_Message.Text = "Enregistrement reussi ";
                }
                else
                {
                    Label_Error_Message.Text = "Cet element est deja creee";
                }
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
            Facultes_Affectes_In_GridView(id_annee);
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedItem.Value);
            annee = Annee_Combo.SelectedItem.Text;
            Departement_Combo.SelectedValue = "-1";

            Facultes_Affectes_In_GridView(id_annee);
        }

        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32(Departement_Combo.SelectedItem.Value);
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