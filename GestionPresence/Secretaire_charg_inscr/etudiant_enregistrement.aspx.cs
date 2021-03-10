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
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace GestionPresence.Secretaire_charg_inscr
{
    public partial class etudiant_enregistrement : System.Web.UI.Page
    {
        int select_user_for_doc = -1;
        string current_matr = "";
        int matr;
        string fileName_path, M;
        static string file_path;
        int fileName_size;
        public static int idetu = -1, etat_exetat = -1, cpt_etat_exetat = -1, etat_etudes = -1, cpt_etat_etudes = -1, id_hebergement = -1, id_boursier = -1, id_identite_type = -1;
        public static string region, pays, matricule = "", code_inscri = "", identite_type = "", photo_catch_url = "";

        MySqlConnection conn = new MySqlConnection(Authentification.MyString);

        protected void Page_Load(object sender, EventArgs e)
        {
            NaissanceDatePicker.TextMode = TextBoxMode.Date;
            if (!IsPostBack)
            {
                Load_Annee();
                Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);
            }

            if (FileUpload_rec.PostedFile != null && FileUpload_rec.PostedFile.ContentLength > 0)
            {
                UpLoadAndDisplay();

            }
        }


        //=============================PHOTO===================================

        private void UpLoadAndDisplay()
        {
            string imgName = FileUpload_rec.FileName;
            string imgPath = "~/Secretaire_charg_inscr/Photo_Upload/" + imgName;
            int imgSize = FileUpload_rec.PostedFile.ContentLength;
            if (FileUpload_rec.PostedFile != null && FileUpload_rec.PostedFile.FileName != "")
            {
                fileName_path = imgPath;
                file_path = "Photo_Upload/" + imgName;
                // Response.Write(Server.MapPath(fileName_path));
                photo_catch_url = Server.MapPath(fileName_path);
               // Response.Write(photo_catch_url);
                //photo.imgPat = Server.MapPath(fileName_path);
                fileName_size = imgSize;
                FileUpload_rec.SaveAs(Server.MapPath(imgPath));
                imagePic.ImageUrl = imgPath;
            }
        }
        private void Load_Annee()
        {
            try
            {
                conn.Open();
                String requette = "select annee from  annee WHERE etat_annee=@etat_annee";
                MySqlCommand com = new MySqlCommand(requette, conn);
                com.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    string value = dr.GetString(0);
                    string[] op = value.Split('-');
                    int an1 = Convert.ToInt32(op[0]);
                    int an2 = Convert.ToInt32(op[1]);
                    code_inscri = an1 % 2000 + "-" + an2 % 2000;
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        protected void Region_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string t = Region_ComboBox.Text;
            if (t == "Autre" || t == "")
            {
                region = t;
                PaysTextBox.Text = "";
                //PaysTextBox.ReadOnly = false;
            }
            else
            {
                string value = Region_ComboBox.Text;
                string[] op = value.Split('-');
                region = op[0];
                pays = op[1];
                PaysTextBox.Text = pays;
                //PaysTextBox.ReadOnly = true;
            }
        }

        protected void Identite_Type_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //id_identite_type=Convert.ToInt32(Identite_Type_ComboBox.SelectedValue);
            identite_type = Identite_Type_ComboBox.Text;
        }

        protected void Reussi_Exetat_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Reussi_Exetat_CheckBox.Checked == true)
            {
                etat_exetat = 1;
                Echoue_Exetat_CheckBox.Checked = false;
                if (cpt_etat_exetat == 0)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string update_user = "UPDATE etudiant SET reussite_exetat=@reussite_exetat WHERE matricule=@matricule";
                    MySqlCommand cmdii = new MySqlCommand(update_user, conn);
                    cmdii.Parameters.AddWithValue("@matricule", matricule);
                    cmdii.Parameters.AddWithValue("@reussite_exetat", etat_exetat);
                    cmdii.ExecuteNonQuery();

                }
            }
        }

        protected void Echoue_Exetat_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Echoue_Exetat_CheckBox.Checked == true)
            {
                etat_exetat = 0;
                Reussi_Exetat_CheckBox.Checked = false;
                if (cpt_etat_exetat == 0)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string update_user = "UPDATE etudiant SET reussite_exetat=@reussite_exetat WHERE matricule=@matricule";
                    MySqlCommand cmdii = new MySqlCommand(update_user, conn);
                    cmdii.Parameters.AddWithValue("@matricule", matricule);
                    cmdii.Parameters.AddWithValue("@reussite_exetat", etat_exetat);
                    cmdii.ExecuteNonQuery();
                }
            }
        }

        protected void Encours_ComboBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Encours_ComboBox.Checked == true)
            {
                etat_etudes = 1;
                Suspendu_CheckBox.Checked = false;
                if (cpt_etat_etudes == 0)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string update_user = "UPDATE etudiant SET etat_etudes=@etat_etudes WHERE matricule=@matricule";
                    MySqlCommand cmdii = new MySqlCommand(update_user, conn);
                    cmdii.Parameters.AddWithValue("@matricule", matricule);
                    cmdii.Parameters.AddWithValue("@etat_etudes", etat_etudes);
                    cmdii.ExecuteNonQuery();
                }
            }
        }

        protected void Suspendu_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Suspendu_CheckBox.Checked == true)
            {
                etat_etudes = 0;
                Encours_ComboBox.Checked = false;
                if (cpt_etat_etudes == 0)
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    string update_user = "UPDATE etudiant SET etat_etudes=@etat_etudes WHERE matricule=@matricule";
                    MySqlCommand cmdii = new MySqlCommand(update_user, conn);
                    cmdii.Parameters.AddWithValue("@matricule", matricule);
                    cmdii.Parameters.AddWithValue("@etat_etudes", etat_etudes);
                    cmdii.ExecuteNonQuery();
                }
            }
        }

        protected void Hebergement_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_hebergement = Convert.ToInt32(Hebergement_ComboBox.SelectedValue);
        }

        protected void Boursier_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_boursier = Convert.ToInt32(Boursier_ComboBox.SelectedValue);
        }

        private void Generate_Matricule()
        {
            string req = "SELECT COUNT(*) FROM etudiant WHERE code_inscri=@code";
            MySqlCommand com = new MySqlCommand(req, conn);
            com.Parameters.AddWithValue("@code", code_inscri);
            int nbre_inscr = Convert.ToInt32(com.ExecuteScalar());
            if (nbre_inscr == 0)
            {
                matricule = code_inscri + "/" + "0000" + 1;
            }
            else
            {

                string reqa = "SELECT matricule FROM etudiant WHERE code_inscri=@code ORDER BY id_etudiant DESC LIMIT 1";
                MySqlCommand coma = new MySqlCommand(reqa, conn);
                coma.Parameters.AddWithValue("@code", code_inscri);
                MySqlDataReader dr = coma.ExecuteReader();
                while (dr.Read())
                {
                    string value = dr.GetString(0);
                    string[] op = value.Split('/');
                    matr = Convert.ToInt32(op[1]);
                }
                dr.Close();

                matr = matr + 1;

                if (matr < 10)
                {
                    matricule = code_inscri + "/" + "0000" + matr;
                }
                else if ((matr >= 10) && (matr < 100))
                {
                    matricule = code_inscri + "/" + "000" + matr;
                }
                else if ((matr >= 100) && (matr < 1000))
                {
                    matricule = code_inscri + "/" + "00" + matr;
                }
                else if ((matr >= 1000) && (matr < 10000))
                {
                    matricule = code_inscri + "/" + "0" + matr;
                }
                else if (matr >= 10000)
                {
                    matricule = code_inscri + "/" + matr;
                }
            }
        }
        private void ViderLesChamps()
        {
            MatriculeTextbox.Text = "";
            NomTextBox.Text = "";
            PrenomTextBox.Text = "";
            Genre_ComboBox.SelectedValue = "-1";
            NaissanceDatePicker.Text = "";
            //DateNaissance.Text = dk.GetDateTime(3);
            PaysTextBox.Text = "";
            ProvinceTextBox.Text = "";
            CommuneTextBox.Text = "";
            ZoneTextBox.Text = "";
            SectionTextBox.Text = "";
            EtatCivil_ComboBox.SelectedValue = "-1";
            IdentiteNumberTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
            EtablissementTextbox.Text = "";
            PromotionTextbox.Text = "";
            NoteClasseTextbox.Text = "";
            NoteExetatTextBox.Text = "";
            NoteTestMedecineTextBox.Text = "";
            NoteSynthesetextBox.Text = "";
            Identite_Type_ComboBox.SelectedValue = "-1";
            Hebergement_ComboBox.SelectedValue = "-1";
            Boursier_ComboBox.SelectedValue = "-1";
            Hebergement_ComboBox.SelectedValue = "-1";
            Region_ComboBox.SelectedIndex = -1;
            Suspendu_CheckBox.Checked = false;
            Encours_ComboBox.Checked = false;
            Echoue_Exetat_CheckBox.Checked = false;
            Reussi_Exetat_CheckBox.Checked = false;
            //MatriculeTextbox.Text = "";
            NaissanceDatePicker.TextMode = TextBoxMode.Date;
            imagePic.ImageUrl = "~/Images/personne.png";

        }

        protected void Enregistrer_Button_Click(object sender, EventArgs e)
        {
            //try
            //{
                conn.Open();
                if (NomTextBox.Text != "" && PrenomTextBox.Text != "" && Region_ComboBox.SelectedValue != "-1" && PaysTextBox.Text != "" && Genre_ComboBox.SelectedValue != "-1" && EtatCivil_ComboBox.SelectedValue != "-1" && Identite_Type_ComboBox.SelectedValue != "-1" && IdentiteNumberTextBox.Text != "" && EmailTextBox.Text != "")
                {
                    string req = "SELECT COUNT(*) FROM etudiant WHERE nom=@nom AND prenom=@prenom AND naissance_date=@naissance_date AND naissance_pays=@pays AND email=@email";
                    MySqlCommand cmd = new MySqlCommand(req, conn);
                    cmd.Parameters.AddWithValue("@nom", NomTextBox.Text);
                    cmd.Parameters.AddWithValue("@prenom", PrenomTextBox.Text);
                    cmd.Parameters.AddWithValue("@naissance_date", NaissanceDatePicker.Text);
                    cmd.Parameters.AddWithValue("@pays", PaysTextBox.Text);
                    cmd.Parameters.AddWithValue("@email", EmailTextBox.Text);
                    int nbre_etu = Convert.ToInt32(cmd.ExecuteScalar());
                    if (nbre_etu == 0)
                    {
                        Generate_Matricule();

                        string str = "INSERT INTO etudiant(code_inscri, matricule, nom, prenom, genre, naissance_date, naissance_region, naissance_pays, naissance_province, naissance_commune," +
                            " naissance_zone, etat_civil, identite, telephone, email, etablissement, section, promotion, note_classe, note_exetat, note_synthese, note_medecine,reussite_exetat, hebergement, boursier,etat_etudes,photo)" +
                            " VALUES (@code_inscri, @matricule, @nom, @prenom, @genre, @naissance_date, @naissance_region, @naissance_pays, @naissance_province, @naissance_commune, @naissance_zone," +
                            " @etat_civil, @identite, @telephone, @email, @etablissement, @section, @promotion, @note_classe, @note_exetat, @note_synthese, @note_medecine, @reussite_exetat,@hebergement, @boursier,@etat_etudes,@photo)";
                        MySqlCommand cmda = new MySqlCommand(str, conn);
                        cmda.Parameters.AddWithValue("@code_inscri", code_inscri);
                        cmda.Parameters.AddWithValue("@matricule", matricule);
                        cmda.Parameters.AddWithValue("@nom", NomTextBox.Text);
                        cmda.Parameters.AddWithValue("@prenom", PrenomTextBox.Text);
                        cmda.Parameters.AddWithValue("@genre", Genre_ComboBox.Text);
                        cmda.Parameters.AddWithValue("@naissance_date", NaissanceDatePicker.Text);
                        cmda.Parameters.AddWithValue("@naissance_region", Region_ComboBox.Text);
                        cmda.Parameters.AddWithValue("@naissance_pays", PaysTextBox.Text);
                        cmda.Parameters.AddWithValue("@naissance_province", ProvinceTextBox.Text);
                        cmda.Parameters.AddWithValue("@naissance_commune", CommuneTextBox.Text);
                        cmda.Parameters.AddWithValue("@naissance_zone", ZoneTextBox.Text);
                        cmda.Parameters.AddWithValue("@etat_civil", EtatCivil_ComboBox.Text);
                        cmda.Parameters.AddWithValue("@identite", Identite_Type_ComboBox.Text + "-" + IdentiteNumberTextBox.Text);
                        cmda.Parameters.AddWithValue("@telephone", PhoneTextBox.Text);
                        cmda.Parameters.AddWithValue("@email", EmailTextBox.Text);
                        cmda.Parameters.AddWithValue("@etablissement", EtablissementTextbox.Text);
                        cmda.Parameters.AddWithValue("@section", SectionTextBox.Text);
                        cmda.Parameters.AddWithValue("@promotion", PromotionTextbox.Text);
                        cmda.Parameters.AddWithValue("@note_classe", NoteClasseTextbox.Text);
                        cmda.Parameters.AddWithValue("@note_exetat", NoteExetatTextBox.Text);
                        cmda.Parameters.AddWithValue("@note_synthese", NoteSynthesetextBox.Text);
                        cmda.Parameters.AddWithValue("@note_medecine", NoteTestMedecineTextBox.Text);
                        cmda.Parameters.AddWithValue("@reussite_exetat", etat_exetat);
                        cmda.Parameters.AddWithValue("@hebergement", id_hebergement);
                        cmda.Parameters.AddWithValue("@boursier", id_boursier);
                        cmda.Parameters.AddWithValue("@etat_etudes", etat_etudes);
                        cmda.Parameters.AddWithValue("@photo", file_path);
                        cmda.ExecuteNonQuery();
                        Response.Write("<script>alert('Enregistrement reussie !')</script>");

                        MatriculeTextbox.Text = matricule;

                        Class_Reports rp = new Class_Reports();
                        rp.etudiant_matricule_Generate_pdf(NomTextBox.Text.ToString(), PrenomTextBox.Text.ToString(), MatriculeTextbox.Text);

                        ViderLesChamps();
                        Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);

                    }
                    else
                    {
                        Response.Write("<script>alert('Etudiant existe deja !')</script>");
                        ViderLesChamps();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Chargez toutes les informations necessaire svp!')</script>");
                }
                conn.Close();
                cpt_etat_etudes = -1;
                cpt_etat_exetat = -1;
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert(' Echec, enregistrement echoué !')</script>");
            //    return;
            //}
        }

        public void Load_Etudiant(object sender, ImageClickEventArgs e)
        {

            try
            {
                conn.Open();

                cpt_etat_etudes = 0;
                cpt_etat_exetat = 0;
                int numberet = 0;

                NaissanceDatePicker.TextMode = TextBoxMode.SingleLine;
                string req = " SELECT nom, prenom, genre, naissance_date, naissance_region, naissance_pays, naissance_province, naissance_commune, naissance_zone, etat_civil, identite, telephone, email, etablissement, section, promotion, note_classe, note_exetat, note_synthese, note_medecine, reussite_exetat, hebergement, boursier, etat_etudes" +
                    " FROM etudiant WHERE matricule=@matricule";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@matricule", MatriculeTextbox.Text);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    NomTextBox.Text = dr.GetString(0);
                    PrenomTextBox.Text = dr.GetString(1);
                    Genre_ComboBox.SelectedValue = dr.GetString(2);
                    EtatCivil_ComboBox.SelectedValue = dr.GetString(9);

                    // DateTime time = DateTime.Parse(dr.GetString(3));

                    NaissanceDatePicker.Text = dr.GetString(3);

                    Region_ComboBox.SelectedValue = dr.GetString(4);
                    PaysTextBox.Text = dr.GetString(5);
                    ProvinceTextBox.Text = dr.GetString(6);
                    CommuneTextBox.Text = dr.GetString(7);
                    ZoneTextBox.Text = dr.GetString(8);

                    string identites = dr.GetString(10);
                    string[] op = identites.Split('-');
                    Identite_Type_ComboBox.SelectedValue = op[0];
                    IdentiteNumberTextBox.Text = op[1];


                    PhoneTextBox.Text = dr.GetString(11);
                    EmailTextBox.Text = dr.GetString(12);
                    EtablissementTextbox.Text = dr.GetString(13);
                    SectionTextBox.Text = dr.GetString(14);
                    PromotionTextbox.Text = dr.GetString(15);


                    NoteClasseTextbox.Text = dr.GetString(16);
                    NoteExetatTextBox.Text = dr.GetString(17);
                    NoteSynthesetextBox.Text = dr.GetString(18);
                    NoteTestMedecineTextBox.Text = dr.GetString(19);

                    etat_exetat = dr.GetInt32(20);

                    if (etat_exetat == 0)
                    {
                        Reussi_Exetat_CheckBox.Checked = false;
                        Echoue_Exetat_CheckBox.Checked = true;
                    }
                    else if (etat_exetat == 1)
                    {
                        Reussi_Exetat_CheckBox.Checked = true;
                        Echoue_Exetat_CheckBox.Checked = false;
                    }



                    //int selItem = Hebergement_ComboBox.Items.IndexOf(Hebergement_ComboBox.Items.FindByValue(dr.GetInt32(21).ToString()));
                    Hebergement_ComboBox.SelectedValue = dr.GetInt32(21).ToString();

                    Boursier_ComboBox.SelectedValue = dr.GetInt32(22).ToString();

                    //for (int i = 0; i < Boursier_ComboBox.Items.Count; ++i)
                    //{
                    //    if (Boursier_ComboBox.Items[i].Text == dr.GetInt32(22).ToString())
                    //    {
                    //        Boursier_ComboBox.SelectedItem.Text = Boursier_ComboBox.Items[i].Text;
                    //    }
                    //}


                    etat_etudes = dr.GetInt32(23);
                    if (etat_etudes == 0)
                    {
                        Encours_ComboBox.Checked = false;
                        Suspendu_CheckBox.Checked = true;
                    }
                    else if (etat_etudes == 1)
                    {
                        Encours_ComboBox.Checked = true;
                        Suspendu_CheckBox.Checked = false;
                    }

                    Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant where matricule ='" + MatriculeTextbox.Text.Trim() + "'", Gridetudiant);

                    /*select_user_for_doc = dr.GetInt16(25);
                    doc.Text = this.CountDoc(select_user_for_doc);

                    if (dr.GetString(24) != "")
                    {
                        PortraitPictureBox.Image = byteArrayToImage(dr["photo"] as byte[]);
                    }
                    else
                    {
                        PortraitPictureBox.Image = null;
                    }*/
                    numberet++;

                }
                else
                {
                    Response.Write("<script>alert('Cette matricule n existe pas dans la base des données !')</script>");
                    return;
                }
                dr.Close();
                Encours_ComboBox.Visible = true;
                Suspendu_CheckBox.Visible = true;
                Reussi_Exetat_CheckBox.Visible = true;
                Echoue_Exetat_CheckBox.Visible = true;
                conn.Close();

                cpt_etat_etudes = -1;
                cpt_etat_exetat = -1;
            }
            catch (Exception ex)
            {
                conn.Close();
                Response.Write("<script>alert('Echec de chargement !')</script>");
                return;
            }
        }

        protected void Search_Button_Click(object sender, ImageClickEventArgs e)
        {
            if (MatriculeTextbox.Text == "")
            {
                Response.Write("<script>alert('Saisissez la matricule !')</script>");
                return;
            }
            else
            {
                Load_Etudiant(sender, e);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            current_matr = "";
            ViderLesChamps();
            Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);
        }

        protected void bt_supprimer_Click(object sender, EventArgs e)
        {
            try
            {
                if (MatriculeTextbox.Text == "")
                {
                    Response.Write("<script>alert(' Chercher par matricule etudiant à supprimer !')</script>");
                    return;
                }
                else
                {
                    conn.Open();
                    string str = "delete from etudiant WHERE matricule=@matricule ";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("@matricule", MatriculeTextbox.Text);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert(' Suppression reussie !')</script>");
                    NaissanceDatePicker.TextMode = TextBoxMode.Date;
                    ViderLesChamps();
                    Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' Echec de la suppression, peut etre que l etudiant est liée avec d autre données !')</script>");
                return;
            }
        }

        protected void bt_modifier_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                if (MatriculeTextbox.Text == "")
                {
                    Response.Write("<script>alert('Chercher par matricule etudiant à modifier !')</script>");
                    return;
                }
                else
                {

                    string str = "UPDATE etudiant SET nom=@nom, prenom=@prenom, genre=@genre, naissance_date=@naissance_date, naissance_region=@naissance_region, naissance_pays=@naissance_pays, naissance_province=@naissance_province, naissance_commune=@naissance_commune, naissance_zone=@naissance_zone," +
                        " etat_civil=@etat_civil, identite=@identite, telephone=@telephone, email=@email, etablissement=@etablissement, section=@section, promotion=@promotion, note_classe=@note_classe, note_exetat=@note_exetat, note_synthese=@note_synthese, note_medecine=@note_medecine," +
                        " reussite_exetat=@reussite_exetat, hebergement=@hebergement, boursier=@boursier, etat_etudes=@etat_etudes,photo=@photo" +
                        " WHERE matricule=@matricule";
                    MySqlCommand cmdx = new MySqlCommand(str, conn);
                    cmdx.Parameters.AddWithValue("@matricule", MatriculeTextbox.Text);
                    cmdx.Parameters.AddWithValue("@nom", NomTextBox.Text);
                    cmdx.Parameters.AddWithValue("@prenom", PrenomTextBox.Text);
                    cmdx.Parameters.AddWithValue("@genre", Genre_ComboBox.Text);
                    cmdx.Parameters.AddWithValue("@naissance_date", NaissanceDatePicker.Text);
                    cmdx.Parameters.AddWithValue("@naissance_region", Region_ComboBox.Text);
                    cmdx.Parameters.AddWithValue("@naissance_pays", PaysTextBox.Text);
                    cmdx.Parameters.AddWithValue("@naissance_province", ProvinceTextBox.Text);
                    cmdx.Parameters.AddWithValue("@naissance_commune", CommuneTextBox.Text);
                    cmdx.Parameters.AddWithValue("@naissance_zone", ZoneTextBox.Text);
                    cmdx.Parameters.AddWithValue("@etat_civil", EtatCivil_ComboBox.Text);
                    cmdx.Parameters.AddWithValue("@identite", Identite_Type_ComboBox.Text + "-" + IdentiteNumberTextBox.Text);
                    cmdx.Parameters.AddWithValue("@telephone", PhoneTextBox.Text);
                    cmdx.Parameters.AddWithValue("@email", EmailTextBox.Text);
                    cmdx.Parameters.AddWithValue("@etablissement", EtablissementTextbox.Text);
                    cmdx.Parameters.AddWithValue("@section", SectionTextBox.Text);
                    cmdx.Parameters.AddWithValue("@promotion", PromotionTextbox.Text);
                    cmdx.Parameters.AddWithValue("@note_classe", NoteClasseTextbox.Text);
                    cmdx.Parameters.AddWithValue("@note_exetat", NoteExetatTextBox.Text);
                    cmdx.Parameters.AddWithValue("@note_synthese", NoteSynthesetextBox.Text);
                    cmdx.Parameters.AddWithValue("@note_medecine", NoteTestMedecineTextBox.Text);
                    cmdx.Parameters.AddWithValue("@reussite_exetat", etat_exetat);
                    cmdx.Parameters.AddWithValue("@hebergement", id_hebergement);
                    cmdx.Parameters.AddWithValue("@boursier", id_boursier);
                    cmdx.Parameters.AddWithValue("@etat_etudes", etat_etudes);
                    cmdx.Parameters.AddWithValue("@photo", file_path);
                    cmdx.ExecuteNonQuery();
                    Response.Write("<script>alert('Modification reussie !')</script>");
                    ViderLesChamps();
                    current_matr = "";
                    NaissanceDatePicker.TextMode = TextBoxMode.Date;
                    Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);

                }

                conn.Close();
                cpt_etat_etudes = -1;
                cpt_etat_exetat = -1;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' Echec de modification !')</script>");
                return;
            }
        }

        protected void Gridetudiant_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(Gridetudiant.DataKeys[e.RowIndex].Value.ToString());
            MySqlConnection c = new MySqlConnection(Authentification.MyString);
            string requete = "delete from etudiant where id_etudiant =" + id + "";
            c.Open();
            MySqlCommand data = new MySqlCommand(requete, c);
            data.ExecuteNonQuery();
            c.Close();

            Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant, matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);
            MatriculeTextbox.Text = " ";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Base.charger_GridView(sender, e, "SELECT id_etudiant, concat(nom,' ',prenom)as etudiant , matricule, telephone, naissance_pays, note_classe, note_exetat FROM etudiant ORDER BY id_etudiant DESC LIMIT 20", Gridetudiant);
            MatriculeTextbox.Text = "";
        }
    }
}