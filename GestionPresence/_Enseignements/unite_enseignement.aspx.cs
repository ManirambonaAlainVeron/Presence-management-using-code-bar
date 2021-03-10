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
    public partial class unite_enseignement : System.Web.UI.Page
    {
        MySqlConnection conn;

        public static int id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1, id_semestre = -1, id_ue = -1, idf = -1, idp = -1;
        public static string codeue, nomUE, idue;

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
                item0.Text = "";
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
                item0.Text = "";
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

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "";
                ClasseCombo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
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
        private void Load_Semestres()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string reqdgv = "SELECT DISTINCT id_semestre, semestre FROM semestre";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                MySqlDataReader dr = cmt.ExecuteReader();
                SemestreCombo.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "";
                SemestreCombo.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    SemestreCombo.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
            }
        }
        private void Unite_Par_Classe()
        {
            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            string sql = "select unite.id_unite, unite,code_unite,SUM(credits) as credit  from unite inner join cours "
                       + " where  unite.id_classe=@idc and unite.id_departement=@id_departement and unite.id_faculte=@id_faculte and unite.id_annee=@id_annee "
                       + " and cours.id_unite=unite.id_unite  and unite.id_faculte=cours.id_faculte GROUP BY (unite.id_unite);";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idc", id_classe);
            cmd.Parameters.AddWithValue("@id_departement", id_departement);
            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
            cmd.Parameters.AddWithValue("@id_annee", id_annee);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);

            GDV_Creation.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GDV_Creation.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GDV_Creation.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

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
                GDV_Creation.Rows[0].Cells[0].Text = "Aucune UE déjà créée";
                GDV_Creation.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

            conn.Close();
        }
        private void Unite_Par_Semestre(int id_annee, int id_faculte, int id_departement, int id_classe, int id_semestre)
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string sql = "SELECT id_unite, unite,code_unite FROM unite" +
                            " WHERE id_annee=@id_annee AND id_faculte=@id_faculte AND id_departement=@id_departement AND id_classe=@id_classe AND id_semestre=@id_semestre" +
                           " ORDER BY unite";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id_semestre", id_semestre);
                cmd.Parameters.AddWithValue("@id_classe", id_classe);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                da.Fill(dtable);

                GDV_Creation.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GDV_Creation.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

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
                    GDV_Creation.Rows[0].Cells[0].Text = "Aucune UE créée dans ce semestre";
                    GDV_Creation.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
        }
        protected void ExitButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/_Enseignements/emptyEnseignements.aspx");
        }
        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            Load_Faculte();
        }
        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedValue);
            Load_Departements();
        }
        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_departement = Convert.ToInt32(Departement_Combo.SelectedValue);
            Load_Classe();
        }
        protected void ClasseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32(ClasseCombo.SelectedValue);
            Load_Semestres();
        }
        protected void SemestreCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_semestre = Convert.ToInt32(SemestreCombo.SelectedValue);
            Unite_Par_Semestre(id_annee, id_faculte, id_departement, id_classe, id_semestre);
        }

        private void Delete_UE()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                if (id_ue != -1)
                {
                    string requette = "select count(id_cours) from cours where id_unite=@id_ue";
                    MySqlCommand cmd = new MySqlCommand(requette, conn);
                    cmd.CommandText = requette;
                    cmd.Parameters.AddWithValue("@id_ue", id_ue);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result == 0)
                    {
                        string rqt = "DELETE FROM unite WHERE id_unite=@id_ue";
                        MySqlCommand cm = new MySqlCommand(rqt, conn);
                        cm.Parameters.AddWithValue("@id_ue", id_ue);
                        cm.ExecuteNonQuery();
                        Response.Write("<script>alert('Operation réussie !')</script>");
                        return;
                    }
                    else
                    {
                        Response.Write("<script>alert('Assurez-vous d avoir supprimé tous les éléments liés à cet élément !')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Veuillez choisir l élément à supprimer !')</script>");
                    return;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec !')</script>");
                return;
            }
        }

        //=============================CREATION==============================================
        protected void GDV_Creation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    MySqlConnection conn = new MySqlConnection(LoginForm.MyString);
                    conn.Open();

                    string nom_ue = (GDV_Creation.FooterRow.FindControl("NomUE_TextBox_Footer") as TextBox).Text.Trim();
                    string code_ue = (GDV_Creation.FooterRow.FindControl("CodeUE_TextBox_Footer") as TextBox).Text.Trim();
                    if (nom_ue.Length > 0 && code_ue.Length > 0 && id_semestre != -1)
                    {
                        String requette = "SELECT COUNT(*) FROM unite WHERE (code_unite=@code OR unite=@ue) AND id_classe=@idc AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                        MySqlCommand cmd = new MySqlCommand(requette, conn);
                        cmd.Parameters.AddWithValue("@ue", nom_ue);
                        cmd.Parameters.AddWithValue("@code", code_ue);
                        cmd.Parameters.AddWithValue("@idc", id_classe);
                        cmd.Parameters.AddWithValue("@id_departement", id_departement);
                        cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmd.Parameters.AddWithValue("@id_annee", id_annee);
                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        if (result == 0)
                        {
                            string rqt = "INSERT INTO unite(unite,code_unite, id_semestre, id_classe, id_departement, id_faculte, id_annee) VALUES (@uee, @code,@idsem, @idclasse,@id_depart,@id_fac, @id_an)";
                            MySqlCommand cm = new MySqlCommand(rqt, conn);
                            cm.Parameters.AddWithValue("@uee", nom_ue);
                            cm.Parameters.AddWithValue("@code", code_ue);
                            cm.Parameters.AddWithValue("@idsem", id_semestre);
                            cm.Parameters.AddWithValue("@idclasse", id_classe);
                            cm.Parameters.AddWithValue("@id_depart", id_departement);
                            cm.Parameters.AddWithValue("@id_fac", id_faculte);
                            cm.Parameters.AddWithValue("@id_an", id_annee);
                            cm.ExecuteNonQuery();
                            Label_Success_Message.Text = "Operation reussie avec succès";
                            Label_Error_Message.Text = "";
                            conn.Close();
                        }
                        else
                        {
                            Label_Success_Message.Text = "";
                            Label_Error_Message.Text = "Cette UE ou ce code se trouve deja dans la BD. Verifier et reessayer!";
                        }
                    }
                    else
                    {
                        Label_Success_Message.Text = "";
                        Label_Error_Message.Text = "Veuillez respecter les consignes, Compléter tous les champs nécessaires!";
                    }
                }
            }
            catch (Exception ex)
            {
                Label_Success_Message.Text = "";
                Label_Error_Message.Text = ex.Message;
            }
            Unite_Par_Semestre(id_annee, id_faculte, id_departement, id_classe, id_semestre);
        }
        protected void GDV_Creation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GDV_Creation.EditIndex = e.NewEditIndex;
            Unite_Par_Semestre(id_annee, id_faculte, id_departement, id_classe, id_semestre);
        }
        protected void GDV_Creation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();

                string unite = (GDV_Creation.Rows[e.RowIndex].FindControl("nom_TextBox") as TextBox).Text.Trim();
                string code_unite = (GDV_Creation.Rows[e.RowIndex].FindControl("code_TextBox") as TextBox).Text.Trim();

                string requette = "SELECT COUNT(*) FROM unite WHERE unite=@unite AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@unite", unite);
                cmda.Parameters.AddWithValue("@id_classe", id_classe);
                cmda.Parameters.AddWithValue("@id_departement", id_departement);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "UPDATE unite SET unite=@unite, code_unite=@code_unite WHERE id_unite=@id_unite AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_unite", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@unite", unite);
                    cmd.Parameters.AddWithValue("@code_unite", code_unite);
                    cmd.Parameters.AddWithValue("@id_classe", id_classe);
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    GDV_Creation.EditIndex = -1;
                    Unite_Par_Semestre(id_annee, id_faculte, id_departement, id_classe, id_semestre);

                }
                else
                {
                    Label_Success_Message.Text = "";
                    Label_Error_Message.Text = "Il y a une erreur. Veuillez bien verifier!";
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
            GDV_Creation.EditIndex = -1;
            Unite_Par_Semestre(id_annee, id_faculte, id_departement, id_classe, id_semestre);
        }
        protected void GDV_Creation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string requette = "SELECT COUNT(*) FROM cours WHERE id_unite=@id_unite AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                MySqlCommand cmda = new MySqlCommand(requette, conn);
                cmda.Parameters.AddWithValue("@id_unite", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                cmda.Parameters.AddWithValue("@id_classe", id_classe);
                cmda.Parameters.AddWithValue("@id_departement", id_departement);
                cmda.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmda.Parameters.AddWithValue("@id_annee", id_annee);
                int result = Convert.ToInt32(cmda.ExecuteScalar());
                if (result == 0)
                {
                    string rqt = "DELETE FROM unite WHERE id_unite=@id_unite AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@id_unite", Convert.ToInt32(GDV_Creation.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.Parameters.AddWithValue("@id_classe", id_classe);
                    cmd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmd.Parameters.AddWithValue("@id_annee", id_annee);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Label_Success_Message.Text = "UE supprimée avec succès";
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
            Unite_Par_Semestre(id_annee, id_faculte, id_departement, id_classe, id_semestre);
        }

        private void refresh_method()
        {
            Annee_Combo.SelectedValue = "-1";
            Departement_Combo.SelectedValue = "-1";
            Faculte_Combo.SelectedValue = "-1";
            ClasseCombo.SelectedValue = "-1";
            SemestreCombo.SelectedValue = "-1";
        }

        protected void Refresh_Button_Click(object sender, EventArgs e)
        {
            refresh_method();
        }


        /*protected void AjouterButton_Click(object sender, EventArgs e)
        {

            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                if (UETextBox.Value != "" && Code_UE_TextBox.Value != "" && id_semestre != -1 )
                {
                    if (id_ue == -1)
                    {
                        String requette = "select count(*) from unite where unite=@ue and id_classe=@idc and id_departement=@id_departement and id_faculte=@id_faculte and id_annee=@id_annee";
                        MySqlCommand cmd = new MySqlCommand(requette, conn);
                        cmd.Parameters.AddWithValue("@ue", UETextBox.Value);
                        cmd.Parameters.AddWithValue("@idc", id_classe);
                        cmd.Parameters.AddWithValue("@id_departement", id_departement);
                        cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmd.Parameters.AddWithValue("@id_annee", id_annee);

                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        if (result == 0)
                        {
                            string rqt = "INSERT INTO unite(unite,code_unite, id_semestre, id_classe, id_departement, id_faculte, id_annee) VALUES (@uee, @code,@idsem, @idclasse,@id_depart,@id_fac, @id_an)";
                            MySqlCommand cm = new MySqlCommand(rqt, conn);
                            cm.Parameters.AddWithValue("@uee", UETextBox.Value);
                            cm.Parameters.AddWithValue("@code", Code_UE_TextBox.Value);
                            cm.Parameters.AddWithValue("@idsem", id_semestre);
                            cm.Parameters.AddWithValue("@idclasse", id_classe);
                            cm.Parameters.AddWithValue("@id_depart", id_departement);
                            cm.Parameters.AddWithValue("@id_fac", id_faculte);
                            cm.Parameters.AddWithValue("@id_an", id_annee);

                            if(cm.ExecuteNonQuery()==1)
                                Response.Write("<script>alert('Enregistrement reussi !')</script>");
                            else
                                Response.Write("<script>alert('Echec !')</script>");
                           
                        }
                        else
                        {
                            Response.Write("<script>alert('Assurez-vous que cette unité n existe pas dans la base de données!')</script>");
                            return;
                        }
                    }
                    else
                    {
                        string rqt = "UPDATE unite SET unite=@ueee, code_unite=@code, id_semestre=@id_semestre WHERE id_unite=@id_unite";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@ueee", UETextBox.Value);
                        cmd.Parameters.AddWithValue("@code", Code_UE_TextBox.Value);
                        cmd.Parameters.AddWithValue("@id_unite", id_ue);
                        cmd.Parameters.AddWithValue("@id_semestre", id_semestre);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Enregistrement reussi!')</script>");
                        return;
                   
                    }
                }
                else
                {
                    Response.Write("<script>alert('Assurez-vous d avoir complété tous les champs!')</script>");
                    return;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec !')</script>");
                return;
            }
            Unite_Par_Semestre();
            refresh_method();
        }*/


    }
}