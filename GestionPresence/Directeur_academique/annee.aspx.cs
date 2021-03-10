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
    public partial class annee : System.Web.UI.Page
    {
        public static string end_previous_year, start_current_year, end_current_year, exist_years, dateouv, dateclot;
        public static string etat_annee, date_debut, date_fin;
        public static int id_annee_gestion = -1;
        public static int id_annee_creer = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Operations();
            }
        }

        private void Load_Created_Years()
        {
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();

            string req = "SELECT id_annee,annee,debut,fin,etat_annee FROM annee ";
            MySqlDataAdapter da = new MySqlDataAdapter(req, conn);
            DataTable dtable = new DataTable();
            da.Fill(dtable);

            GDV_Creation.RowStyle.HorizontalAlign = HorizontalAlign.Center;
            if (dtable.Rows.Count > 0)
            {
                exist_years = "Yes";
                GDV_Creation.DataSource = dtable;
                GDV_Creation.DataBind();

                DataRow lastRow = (DataRow)dtable.Rows[dtable.Rows.Count - 1];
                end_previous_year = lastRow["fin"].ToString().Trim();

                GDV_Creation.FooterRow.Cells[1].Text = end_previous_year;
                GDV_Creation.FooterRow.Cells[2].Text = (Convert.ToInt32(end_previous_year) + 1).ToString();

                start_current_year = GDV_Creation.FooterRow.Cells[1].Text;
                end_current_year = GDV_Creation.FooterRow.Cells[2].Text;
            }
            else
            {
                exist_years = "No";
                dtable.Rows.Add(dtable.NewRow());
                GDV_Creation.DataSource = dtable;
                GDV_Creation.DataBind();

                GDV_Creation.Rows[0].Cells.Clear();
                GDV_Creation.Rows[0].Cells.Add(new TableCell());
                GDV_Creation.Rows[0].Cells[0].ColumnSpan = dtable.Columns.Count - 1;
                GDV_Creation.Rows[0].Cells[0].Text = "Aucune année académique n’a été créée à cette date";
                GDV_Creation.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
        }
        public void Load_State_Years()
        {
            MySqlConnection conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string req = "SELECT id_annee,annee,etat_annee,date, date_ouverture,date_cloture FROM annee ";
            MySqlDataAdapter da = new MySqlDataAdapter(req, conn);
            DataTable dtable = new DataTable();
            da.Fill(dtable);

            GDV_Gestion.RowStyle.HorizontalAlign = HorizontalAlign.Center;
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
                GDV_Gestion.Rows[0].Cells[0].Text = "Aucune année académique n’a été créée à cette date";
                GDV_Gestion.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            conn.Close();
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
            item.Text = "Création d’une année académique";
            Operation_Combo.Items.Add(item);

            ListItem item1 = new ListItem();
            item1.Value = "1";
            item1.Text = "Gestion des années académiques";
            Operation_Combo.Items.Add(item1);
        }
        protected void Operation_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_Error_Message.Text="";
            Label_Success_Message.Text = "";
            MyMultiView.ActiveViewIndex = Convert.ToInt32(Operation_Combo.SelectedValue);
            int id_operation = MyMultiView.ActiveViewIndex;
            switch (id_operation)
            {
                case 0: Load_Created_Years(); ;
                    break;
                case 1: Load_State_Years();
                    break;
                default:
                    break;
            }
        }

        //===================CREATION========================================================
        protected void GDV_Creation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string rqt = "DELETE FROM annee WHERE id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_annee", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                conn.Close();

                Load_Created_Years();
                Label_Success_Message.Text = "Enregistrement supprimé avec succès";
                Label_Error_Message.Text = "";
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void GDV_Creation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GDV_Creation.EditIndex = e.NewEditIndex;
            Load_Created_Years();
        }
        protected void GDV_Creation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string depart = (GDV_Creation.Rows[e.RowIndex].FindControl("Debut_TextBox") as TextBox).Text.Trim();
                string arrive = (GDV_Creation.Rows[e.RowIndex].FindControl("Fin_TextBox") as TextBox).Text.Trim();
                if (depart.Length > 0 && arrive.Length > 0 && Convert.ToInt32(arrive) == (Convert.ToInt32(depart) + 1))
                {
                    string year = depart + "-" + arrive;
                    string rqt = "UPDATE annee SET annee=@annee, debut=@debut, fin=@fin WHERE id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_annee", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@annee", year);
                    cmd.Parameters.AddWithValue("@debut", Convert.ToInt32(depart));
                    cmd.Parameters.AddWithValue("@fin", Convert.ToInt32(arrive));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    GDV_Creation.EditIndex = -1;
                    Load_Created_Years();
                    Label_Success_Message.Text = "Modification réussie";
                    Label_Error_Message.Text = "";
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void GDV_Creation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                clear_meassage();
                if (e.CommandName.Equals("AddNew"))
                {
                    MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                    conn.Open();
                    if (exist_years == "No")
                    {
                        start_current_year = (GDV_Creation.FooterRow.FindControl("Debut_Editing_TextBox") as TextBox).Text.Trim();
                        end_current_year = (GDV_Creation.FooterRow.FindControl("Fin_Editing_TextBox") as TextBox).Text.Trim();
                    }
                    if (start_current_year.Length > 0 && end_current_year.Length > 0)
                    {
                        string requette = "SELECT COUNT(*) FROM annee WHERE annee=@annee";
                        MySqlCommand cmda = new MySqlCommand(requette, conn);

                        string t = start_current_year + "-" + end_current_year;
                        cmda.Parameters.AddWithValue("@annee", t);
                        int result = Convert.ToInt32(cmda.ExecuteScalar());
                        if (result == 0)
                        {
                            string rqt = "INSERT INTO annee(annee,debut,fin,etat_annee) values(@annee,@debut,@fin,@etat_annee)";
                            MySqlCommand cmd = new MySqlCommand(rqt, conn);
                            cmd.Parameters.AddWithValue("@annee", t);
                            cmd.Parameters.AddWithValue("@debut", Convert.ToInt32(start_current_year));
                            cmd.Parameters.AddWithValue("@fin", Convert.ToInt32(end_current_year));
                            cmd.Parameters.AddWithValue("@etat_annee", "En attente");
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                        Load_Created_Years();
                        Label_Success_Message.Text = "Enregistrement reussi";
                        Label_Error_Message.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void GDV_Creation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clear_meassage();
            GDV_Creation.EditIndex = -1;
            Load_Created_Years();
        }

        //==================GESTION==========================================================
        protected void GDV_Gestion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            clear_meassage();
            GDV_Gestion.EditIndex = e.NewEditIndex;
            Load_State_Years();
        }
        protected void GDV_Gestion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clear_meassage();
            GDV_Gestion.EditIndex = -1;
            Load_State_Years();
        }
        protected void GDV_Gestion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                clear_meassage();
                MySqlConnection conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string date = "", date_ouverture = "", date_cloture = "", etat = "", ouverture = "";

                string req = "SELECT etat_annee, date_ouverture FROM annee WHERE id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(req, conn);
                cmda.Parameters.AddWithValue("@id_annee", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
                MySqlDataReader dra = cmda.ExecuteReader();
                if (dra.Read())
                {
                    etat_annee = dra.GetString(0);
                    ouverture = dra.GetString(1);
                }
                dra.Close();

                switch ((GDV_Gestion.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim())
                {
                    case "En attente":
                        etat = (GDV_Gestion.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim();
                        date = (GDV_Gestion.Rows[e.RowIndex].FindControl("Date_TextBox") as TextBox).Text.Trim();
                        date_ouverture = "";
                        date_cloture = "";
                        break;
                    case "Encours":
                        etat = (GDV_Gestion.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim();
                        date = (GDV_Gestion.Rows[e.RowIndex].FindControl("Date_TextBox") as TextBox).Text.Trim();
                        date_ouverture = (GDV_Gestion.Rows[e.RowIndex].FindControl("Date_TextBox") as TextBox).Text.Trim();
                        date_cloture = "";
                        break;
                    case "Clôturée":
                        etat = (GDV_Gestion.Rows[e.RowIndex].FindControl("Operation_DropDown") as DropDownList).Text.Trim();
                        date = (GDV_Gestion.Rows[e.RowIndex].FindControl("Date_TextBox") as TextBox).Text.Trim();
                        date_ouverture = ouverture;
                        date_cloture = (GDV_Gestion.Rows[e.RowIndex].FindControl("Date_TextBox") as TextBox).Text.Trim();
                        break;
                    default:
                        break;
                }
                string rqt = "UPDATE annee SET etat_annee= @etat_annee, date=@date, date_ouverture=@date_ouverture, date_cloture=@date_cloture WHERE id_annee=@id_annee";
                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                cmd.Parameters.AddWithValue("@id_annee", Convert.ToInt32(GDV_Gestion.DataKeys[e.RowIndex].Value.ToString()));
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@date_ouverture", date_ouverture);
                cmd.Parameters.AddWithValue("@date_cloture", date_cloture);
                cmd.Parameters.AddWithValue("@etat_annee", etat);
                cmd.ExecuteNonQuery();

                GDV_Gestion.EditIndex = -1;
                Load_State_Years();
                Label_Success_Message.Text = "Modification réussie";
                conn.Close();
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