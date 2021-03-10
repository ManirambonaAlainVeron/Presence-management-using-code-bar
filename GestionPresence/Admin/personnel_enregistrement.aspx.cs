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

namespace GestionPresence.Admin
{
    public partial class personnel_enregistrement : System.Web.UI.Page
    {
        public static int id_personnel = -1, id_personnel_type = -1, id_categorie, id_vacation = -1, id_diplome = -1, id_grade = -1, is_in_Fonction = 1, is_User = -1;
        string matricule, region, pays, categorie, niveau, personnel_type_sigle;
        public static int appartenance = 1;
        public static int id_selected_personne = -1, type = -1;

        MySqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Supprimer_Button.Visible =false;
                Enregistrer_Button.Text = "Enregistrer un nouveau personnel";
                Load_Personnel_Type();
                Load_Personnel_Categorie();
                Load_Personnel_Vacation();
                Load_Personnel_Diplome();
                Load_Personnel_Grade();
                Base.charger_GridView(sender, e, " select id_personnel, concat(nom,' ',prenom)as personnel ,matricule,email,telephone from personnel order by id_personnel DESC limit 1", GridPersonnel);
            }
        }

        private void Load_Personnel_Type()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                Personnel_Type_ComboBox.Items.Clear();

                string re = "SELECT id_personnel_type, type FROM personnel_type";
                MySqlCommand cmt = new MySqlCommand(re, conn);
                MySqlDataReader dr = cmt.ExecuteReader();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Type du personnel";
                Personnel_Type_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Personnel_Type_ComboBox.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Personnel_Categorie()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                Categorie_ComboBox.Items.Clear();

                string re = "SELECT id_categorie, categorie FROM personnel_categorie";
                MySqlCommand cmt = new MySqlCommand(re, conn);
                MySqlDataReader dr = cmt.ExecuteReader();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Categorie du personnel";
                Categorie_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Categorie_ComboBox.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Personnel_Vacation()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                Vacation_ComboBox.Items.Clear();

                string re = "SELECT id_vacation, vacation FROM personnel_vacation";
                MySqlCommand cmt = new MySqlCommand(re, conn);
                MySqlDataReader dr = cmt.ExecuteReader();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Vocation du personnel";
                Vacation_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Vacation_ComboBox.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Personnel_Grade()
        {
            clearMessages();
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string reqdgv = "SELECT id_grade, grade FROM personnel_grade";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                MySqlDataReader dr = cmt.ExecuteReader();

                Grade_ComboBox.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Grade";
                Grade_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Grade_ComboBox.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }
        private void Load_Personnel_Diplome()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string reqdgv = "SELECT id_diplome,diplome FROM personnel_diplome";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                MySqlDataReader dr = cmt.ExecuteReader();
                Diplome_ComboBox.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Diplôme";
                Diplome_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Diplome_ComboBox.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Count_Documents()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string reqdgv = "SELECT COUNT(id_document) FROM personnel_documents WHERE id_proprietaire=@id_proprietaire AND appartenance=@appartenance";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                cmt.Parameters.AddWithValue("@id_proprietaire", id_selected_personne);
                cmt.Parameters.AddWithValue("@appartenance", appartenance);
                int nbre_documents = Convert.ToInt32(cmt.ExecuteScalar());
                //Documents_Number_Label.Text = nbre_documents.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        //public void Search_Personnel()
        //{
        //    try
        //    {
        //        conn = new MySqlConnection(Authentification.MyString);
        //        conn.Open();

        //        string req = " SELECT personnel.id_personnel, personnel.nom, personnel.prenom, personnel.genre, personnel.etat_civil, personnel.date_naissance, personnel.region, personnel.pays,personnel.type_identite,personnel.numero_identite, personnel.telephone, personnel.email, personnel_diplome.id_diplome, personnel_grade.id_grade,  personnel_categorie.id_categorie, personnel_vacation.id_vacation, personnel_type.id_personnel_type, personnel.user, personnel.en_fonction" +
        //            " FROM personnel_grade INNER JOIN (personnel_diplome INNER JOIN (personnel_type INNER JOIN (personnel_vacation INNER JOIN (personnel_categorie INNER JOIN personnel ON personnel_categorie.id_categorie = personnel.id_categorie) ON personnel_vacation.id_vacation = personnel.id_vacation) ON personnel_type.id_personnel_type = personnel.id_personnel_type) ON personnel_diplome.id_diplome = personnel.id_diplome) ON personnel_grade.id_grade = personnel.id_grade" +
        //            " WHERE personnel.matricule=@matricule";
        //        MySqlCommand cm = new MySqlCommand(req, conn);
        //        cm.Parameters.AddWithValue("@matricule", MatriculeTextBox.Text);
        //        MySqlDataReader dr = cm.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            id_personnel = dr.GetInt32(0);
        //            id_selected_personne = id_personnel;
        //            NomTextBox.Value = dr.GetString(1);
        //            PrenomTextBox.Value = dr.GetString(2);
        //            Genre_ComboBox.SelectedValue = dr.GetString(3);
        //            EtatCivil_ComboBox.SelectedValue = dr.GetString(4);
        //            DateTime time = DateTime.Parse(dr.GetString(5));
        //            Naissance_DateTimerPicker.Value = dr.GetString(5);
        //            Region_ComboBox.SelectedValue = dr.GetString(6);
        //            Pays_TextBox.Value = dr.GetString(7);
        //            Identite_Type_ComboBox.SelectedValue = dr.GetString(8);
        //            Identite_TextBox.Value = dr.GetString(9);
        //            Phone_TextBox.Value = dr.GetString(10);
        //            Email_TextBox.Value = dr.GetString(11);
        //            Categorie_ComboBox.SelectedValue = dr.GetInt32(14).ToString();
        //            Vacation_ComboBox.SelectedValue = dr.GetInt32(15).ToString();
        //            Diplome_ComboBox.SelectedValue = dr.GetInt32(12).ToString();
        //            Grade_ComboBox.SelectedValue = dr.GetInt32(13).ToString();
        //            Personnel_Type_ComboBox.SelectedValue = dr.GetInt32(16).ToString();
        //            Enregistrer_Button.Text = "Cliquer ici pour modifier";
        //        }
        //        dr.Close();
        //        conn.Close();
        //        Label_Success_Message.Text = "Modification réussie";
        //        Label_Error_Message.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        private void Initialiser_Tout()
        {
            //MatriculeTextBox.Text = "";
            NomTextBox.Value = "";
            PrenomTextBox.Value = "";
            Phone_TextBox.Value = "";
            Email_TextBox.Value = "";
            Pays_TextBox.Value = "";
            Identite_TextBox.Value = "";
            Identite_Type_ComboBox.SelectedValue = "-1";
            Genre_ComboBox.SelectedValue = "-1";
            EtatCivil_ComboBox.SelectedValue = "-1";
            Region_ComboBox.SelectedValue = "-1";
            Diplome_ComboBox.SelectedValue = "-1";
            Grade_ComboBox.SelectedValue = "-1";
            Personnel_Type_ComboBox.SelectedValue = "-1";
            Categorie_ComboBox.SelectedValue = "-1";
            Load_Personnel_Type();
            Load_Personnel_Categorie();
            Load_Personnel_Vacation();
            Load_Personnel_Diplome();
            Load_Personnel_Grade();
            id_personnel_type = id_grade = id_diplome = id_vacation = id_personnel = -1;
            Naissance_DateTimerPicker.Value = "";
            Enregistrer_Button.Text = "Enregistrer un nouveau personnel";
            id_selected_personne = -1;
            
        }

        protected void Enregistrer_Button_Click(object sender, EventArgs e)
        {
            clearMessages();
            id_vacation = Convert.ToInt32(Vacation_ComboBox.SelectedValue);
            id_categorie = Convert.ToInt32(Categorie_ComboBox.SelectedValue);
            id_personnel_type = Convert.ToInt32(Personnel_Type_ComboBox.SelectedValue);
            id_grade = Convert.ToInt32(Grade_ComboBox.SelectedValue);
            id_diplome = Convert.ToInt32(Diplome_ComboBox.SelectedValue);
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                if (id_selected_personne == -1)
                {
                    if (NomTextBox.Value.Length > 0 && PrenomTextBox.Value.Length > 0 && Email_TextBox.Value.Length > 0)
                    {
                        string req = " SELECT COUNT(*) FROM personnel WHERE email= @email";
                        MySqlCommand cm = new MySqlCommand(req, conn);
                        cm.Parameters.AddWithValue("@email", Email_TextBox.Value);
                        int instances = Convert.ToInt32(cm.ExecuteScalar());
                        if (instances == 0)
                        {
                            string str = " INSERT INTO personnel(nom, prenom, date_naissance, genre, etat_civil,passport, telephone, email, " +
                                    " region, pays, id_grade, id_diplome, id_categorie, id_vacation, id_personnel_type, user, en_fonction) VALUES (@nom, @prenom, " +
                                    " @date_naissance, @genre, @etat_civil, @numero_identite, @telephone, @email, @region, @pays, @id_grade, @id_diplome, " +
                                    " @id_categorie, @id_vacation, @id_personnel_type, @user, @en_fonction)";

                            MySqlCommand cmd = new MySqlCommand(str, conn);
                            cmd.Parameters.AddWithValue("@nom", NomTextBox.Value);
                            cmd.Parameters.AddWithValue("@prenom", PrenomTextBox.Value);
                            cmd.Parameters.AddWithValue("@date_naissance", DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                            cmd.Parameters.AddWithValue("@genre", Genre_ComboBox.Text);
                            cmd.Parameters.AddWithValue("@etat_civil", EtatCivil_ComboBox.Text);
                            cmd.Parameters.AddWithValue("@type_identite", Identite_Type_ComboBox.Text);
                            cmd.Parameters.AddWithValue("@numero_identite", Identite_TextBox.Value); 
                            cmd.Parameters.AddWithValue("@telephone", Phone_TextBox.Value);
                            cmd.Parameters.AddWithValue("@email", Email_TextBox.Value);
                            cmd.Parameters.AddWithValue("@region", Region_ComboBox.Text);
                            cmd.Parameters.AddWithValue("@pays", Pays_TextBox.Value);
                            cmd.Parameters.AddWithValue("@id_grade", id_grade);
                            cmd.Parameters.AddWithValue("@id_diplome", id_diplome);
                            cmd.Parameters.AddWithValue("@id_categorie", id_categorie);
                            cmd.Parameters.AddWithValue("@id_vacation", id_vacation);
                            cmd.Parameters.AddWithValue("@id_personnel_type", id_personnel_type);
                            cmd.Parameters.AddWithValue("@user", is_User);
                            cmd.Parameters.AddWithValue("@en_fonction", is_in_Fonction);
                            cmd.ExecuteNonQuery();

                            id_personnel = Convert.ToInt32(cmd.LastInsertedId);
                            matricule = DateTime.Now.Year + "/" + (1000 + id_personnel);

                            string stra = "UPDATE personnel SET matricule=@matricule WHERE id_personnel=@id_personnel";
                            MySqlCommand cmdo = new MySqlCommand(stra, conn);
                            cmdo.Parameters.AddWithValue("@id_personnel", id_personnel);
                            cmdo.Parameters.AddWithValue("@matricule", matricule);
                            cmdo.ExecuteNonQuery();


                            int id_ann = 0;
                            string st = "SELECT id_annee FROM annee";
                            MySqlCommand cmdst = new MySqlCommand(st, conn);
                            MySqlDataReader dst = cmdst.ExecuteReader();
                            while (dst.Read())
                            {
                                id_ann = dst.GetInt32(0);
                            }
                            dst.Close();

                            string st1 = "INSERT INTO personnel_carriere(id_personnel,id_annee)VALUES(@id_personnel,@id_annee)";
                            MySqlCommand cmdst1 = new MySqlCommand(st1, conn);
                            cmdst1.Parameters.AddWithValue("@id_personnel", id_personnel);
                            cmdst1.Parameters.AddWithValue("@id_annee", id_ann);
                            cmdst1.ExecuteNonQuery();

                            string pers = "INSERT INTO personnel_abonne(id_personnel,date_debut_abonnement) VALUES(@id_personnel,@debut)";
                            MySqlCommand comand = new MySqlCommand(pers, conn);
                            comand.Parameters.AddWithValue("@id_personnel", id_personnel);
                            comand.Parameters.AddWithValue("@debut", DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day);
                            comand.ExecuteNonQuery();

                            string insert_persnel = "INSERT INTO personnel_fonction(id_personnel,debut_activite)VALUES(@id_personnel,@date)";
                            MySqlCommand cmd1 = new MySqlCommand(insert_persnel, conn);
                            cmd1.Parameters.AddWithValue("@id_personnel", id_personnel);
                            cmd1.Parameters.AddWithValue("@date", DateTime.Now);
                            cmd1.ExecuteNonQuery();

                            Initialiser_Tout();
                            //MatriculeTextBox.Text = matricule;
                            Label_Success_Message.Text = "Enregistrement reussi";
                        }
                        else
                        {
                            Label_Error_Message.Text = "Une personne avec cet email existe dans la BD";
                        }
                    }
                    else
                    {
                        Label_Error_Message.Text = "Nom, prenom et email doivent impérativement être complétés";
                    }
                }
                else
                {
                    string str = "UPDATE personnel SET nom = @nom, prenom = @prenom, date_naissance = @date_naissance, genre = @genre , etat_civil = @etat_civil,type_identite = @type_identite, numero_identite = @numero_identite, telephone = @telephone, email = @email , region = @region, pays = @pays, id_grade = @id_grade, id_diplome = @id_diplome,id_vacation = @id_vacation, id_personnel_type=@id_personnel_type  WHERE id_personnel =@id_personnel";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("@id_personnel", id_selected_personne);
                    cmd.Parameters.AddWithValue("@nom", NomTextBox.Value);
                    cmd.Parameters.AddWithValue("@prenom", PrenomTextBox.Value);
                    cmd.Parameters.AddWithValue("@genre", Genre_ComboBox.Text);
                    cmd.Parameters.AddWithValue("@date_naissance", DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    cmd.Parameters.AddWithValue("@etat_civil", EtatCivil_ComboBox.Text);
                    cmd.Parameters.AddWithValue("@numero_identite", Identite_TextBox.Value);
                    cmd.Parameters.AddWithValue("@type_identite", Identite_Type_ComboBox.Text);
                    cmd.Parameters.AddWithValue("@telephone", Phone_TextBox.Value);
                    cmd.Parameters.AddWithValue("@email", Email_TextBox.Value);
                    cmd.Parameters.AddWithValue("@region", Region_ComboBox.Text);
                    cmd.Parameters.AddWithValue("@pays", Pays_TextBox.Value);
                    cmd.Parameters.AddWithValue("@id_grade", id_grade);
                    cmd.Parameters.AddWithValue("@id_diplome", id_diplome);
                    cmd.Parameters.AddWithValue("@id_vacation", id_vacation);
                    cmd.Parameters.AddWithValue("@id_personnel_type", id_personnel_type);
                    cmd.ExecuteNonQuery();

                    string str1 = "UPDATE user_credentials SET username=@username WHERE id_personnel=@id_personnel AND user_type=@user_type";
                    MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                    cmd1.Parameters.AddWithValue("@id_personnel", id_selected_personne);
                    cmd1.Parameters.AddWithValue("@user_type", "personnel");
                    cmd1.Parameters.AddWithValue("@username", Email_TextBox.Value);
                    cmd1.ExecuteNonQuery();

                    Label_Success_Message.Text = "Vos modifications ont été prises en compte";
                    Initialiser_Tout();
                }
                conn.Close();
                
            }
            catch (Exception ex)
            {
                Label_Error_Message.Text = ex.Message;
            }
        }

        //protected void Search_Button_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (MatriculeTextBox.Text == "")
        //    {
        //        Label_Success_Message.Text = "";
        //        Label_Error_Message.Text = "Saisissez la matricule !";
        //    }
        //    else
        //    {
        //        //Response.Redirect("../_AdminSI/personnel_enregistrement.aspx");
        //        Search_Personnel();
        //    }
        //}

        protected void Initialiser_Button_Click(object sender, EventArgs e)
        {
            Initialiser_Tout();
        }

        protected void clearMessages()
        {
            Label_Error_Message.Text = "";
            Label_Success_Message.Text = "";
        }

        protected void Region_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string t = Region_ComboBox.Text;

            if (t == "Autre")
            {
                region = t;
                Pays_TextBox.Value = "";
            }
            else
            {
                string value = Region_ComboBox.Text;
                string[] op = value.Split('-');
                region = op[0];
                pays = op[1];
                Pays_TextBox.Value = pays;
            }
        }

        protected void GridPersonnel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridPersonnel.DataKeys[e.RowIndex].Value.ToString());
            MySqlConnection c = new MySqlConnection(Authentification.MyString);
            string requete = "delete from personnel where id_personnel =" + id + "";
            c.Open();
            MySqlCommand data = new MySqlCommand(requete, c);
            data.ExecuteNonQuery();
            c.Close();
            Base.charger_GridView(sender, e, " select id_personnel, concat(nom,' ',prenom)as personnel ,matricule,email,telephone from personnel order by id_personnel DESC limit 1", GridPersonnel);

            MatriculeTextbox.Text = " ";
        }

        protected void Search_Button_Click(object sender, ImageClickEventArgs e)
        {
            if (MatriculeTextbox.Text == "")
            {
                Label_Error_Message.Text = "Saisissez la matricule !";
                return;
            }
            else
            {
                Base.charger_GridView(sender, e, " select id_personnel, concat(nom,' ',prenom)as personnel ,matricule,email,telephone from personnel where matricule='"+MatriculeTextbox.Text.Trim()+"'", GridPersonnel);

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Base.charger_GridView(sender, e, " select id_personnel, concat(nom,' ',prenom)as personnel ,matricule,email,telephone from personnel order by id_personnel DESC limit 1", GridPersonnel);

        }


        protected void GridPersonnel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
                int id = Convert.ToInt32(GridPersonnel.DataKeys[GridPersonnel.SelectedIndex].Value);
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string req = " SELECT personnel.id_personnel, personnel.nom, personnel.prenom, personnel.genre, personnel.etat_civil, personnel.date_naissance, personnel.region, personnel.pays,personnel.type_identite,personnel.numero_identite, personnel.telephone, personnel.email, personnel_diplome.id_diplome, personnel_grade.id_grade,  personnel_categorie.id_categorie, personnel_vacation.id_vacation, personnel_type.id_personnel_type, personnel.user, personnel.en_fonction" +
                    " FROM personnel_grade INNER JOIN (personnel_diplome INNER JOIN (personnel_type INNER JOIN (personnel_vacation INNER JOIN (personnel_categorie INNER JOIN personnel ON personnel_categorie.id_categorie = personnel.id_categorie) ON personnel_vacation.id_vacation = personnel.id_vacation) ON personnel_type.id_personnel_type = personnel.id_personnel_type) ON personnel_diplome.id_diplome = personnel.id_diplome) ON personnel_grade.id_grade = personnel.id_grade" +
                    " WHERE personnel.id_personnel=@id_personnel";
                MySqlCommand cm = new MySqlCommand(req, conn);
                cm.Parameters.AddWithValue("@id_personnel", id);
                MySqlDataReader dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    id_personnel = dr.GetInt32(0);
                    id_selected_personne = id_personnel;
                    NomTextBox.Value = dr.GetString(1);
                    PrenomTextBox.Value = dr.GetString(2);
                    Genre_ComboBox.SelectedValue = dr.GetString(3);
                    EtatCivil_ComboBox.SelectedValue = dr.GetString(4);
                    DateTime time = DateTime.Parse(dr.GetString(5));
                    Naissance_DateTimerPicker.Value = dr.GetString(5);
                    Region_ComboBox.SelectedValue = dr.GetString(6);
                    Pays_TextBox.Value = dr.GetString(7);
                    Identite_Type_ComboBox.SelectedValue = dr.GetString(8);
                    Identite_TextBox.Value = dr.GetString(9);
                    Phone_TextBox.Value = dr.GetString(10);
                    Email_TextBox.Value = dr.GetString(11);
                    Categorie_ComboBox.SelectedValue = dr.GetInt32(14).ToString();
                    Vacation_ComboBox.SelectedValue = dr.GetInt32(15).ToString();
                    Diplome_ComboBox.SelectedValue = dr.GetInt32(12).ToString();
                    Grade_ComboBox.SelectedValue = dr.GetInt32(13).ToString();
                    Personnel_Type_ComboBox.SelectedValue = dr.GetInt32(16).ToString();
                    Enregistrer_Button.Text = "Cliquer ici pour modifier";
                }
                dr.Close();
                conn.Close();
                Label_Success_Message.Text = "Modification réussie";
                Label_Error_Message.Text = "";
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
}