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
    public partial class prestations : System.Web.UI.Page
    {
        
        MySqlConnection conn;

        int debut, fin, cptd = -1, cptf = -1;
        public static int id_departement, id_classe, id_cours = -1, operation = -1, has_depaartement = -1;
        public static int id_horaire = -1;
        public static int id_horai = -1;
        public static string code, dat, nom_classe, nom_departement, nom_faculte, nom_cours, date1, date2, jr, perio;
        public static string periode;
        public static string etat_classe = "", etat_annee = "", annee = "";
        public static int largeur = 0, hauteur = 0;
        public static int id_faculte = -1, id_prestation = -1, idprst = -1, id_annee = -1;
        public static string[] Horaire_ID;
        public static string[] Horaire_Date;
        public static string[] Horaire_Jour;
        public static string[] HoraireID;
        public static string[] HorairePeriode;
        public static string[] HoraireDate;
        public static string[] HoraireJour;

        public static LinkButton[] AM;

        public static int id_operation = -1;
        prestations prst;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Annee();
                //mpe.Show();
            }
            Load_Planifications_Par_Cours();
        }

        protected void ExitButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/_Enseignements/emptyEnseignements.aspx");
        }

        private void Load_Annee()
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
                    item.Text= dr.GetString(1);
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
        private void Load_Departements()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string reqdgv = "SELECT DISTINCT user_departement.id_departement, departement.departement" +
                    " FROM (departement_par_faculte INNER JOIN departement ON departement_par_faculte.id_departement = departement.id_departement) INNER JOIN user_departement ON departement.id_departement = user_departement.id_departement" +
                    " WHERE departement_par_faculte.id_faculte=@id_facultee AND departement_par_faculte.id_annee=@id_annee";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                cmt.Parameters.AddWithValue("@id_annee", id_annee);
                cmt.Parameters.AddWithValue("@id_facultee", id_faculte);
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
        private void Load_Classe(int idannee, int idfaculte, int iddepartement)
        {
            int id_annee = idannee;
            int id_faculte = idfaculte;
            int id_departement = iddepartement;

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
                //item.string_value_1 = dr.GetString(2);
                ClasseCombo.Items.Add(item);
            }
            dr.Close();
            conn.Close();
        }
        private void Find_Cours()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string re = "SELECT DISTINCT cours.id_cours, cours.cours" +
                    " FROM horaire INNER JOIN cours ON horaire.id_cours = cours.id_cours" +
                    " WHERE horaire.id_annee=@id_annee AND horaire.id_faculte=@id_faculte AND horaire.id_departement=@id_departement AND horaire.id_classe=@idcl";
                MySqlCommand cm = new MySqlCommand(re, conn);
                cm.Parameters.AddWithValue("@idcl", id_classe);
                cm.Parameters.AddWithValue("@id_departement", id_departement);
                cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                cm.Parameters.AddWithValue("@id_annee", id_annee);
                MySqlDataReader drg = cm.ExecuteReader();
                Cours_ComboBox.Items.Clear();

                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez le cours";
                Cours_ComboBox.Items.Add(item0);

                while (drg.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = drg.GetInt32(0).ToString();
                    item.Text = drg.GetString(1);
                    Cours_ComboBox.Items.Add(item);
                }
                drg.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Operations()
        {
            Operation_Combo.Items.Clear();

            ListItem item0 = new ListItem();
            item0.Value = "-1";
            item0.Text = "";
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
        private void Load_Presations_Validees()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                //Jour_Prestation_ListView.Items.Clear();
                    string ra = "SELECT prestation.id_prestation, horaire.jour, horaire.date, prestation.heureD, prestation.heureF, horaire.periode" +
                    " FROM (cours INNER JOIN horaire ON cours.id_cours = horaire.id_cours) INNER JOIN prestation ON horaire.id_horaire = prestation.id_horaire" +
                    " WHERE  cours.id_cours=@id_cours";
                    MySqlCommand cmd = new MySqlCommand(ra, conn);
                    cmd.Parameters.AddWithValue("@id_cours", id_cours);

                    MySqlDataReader dr = cmd.ExecuteReader();

                        Jour_Prestation_ListView.DataSource = dr;
                        Jour_Prestation_ListView.DataBind();
                  
                    dr.Close();
              
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void Load_Prestations_Details()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                String requette = "SELECT id_prestation_details, pointAborde FROM prestation_details WHERE id_prestation=@idp";
                MySqlCommand cmd = new MySqlCommand(requette, conn);
                cmd.Parameters.AddWithValue("@idp", id_prestation);
                MySqlDataReader dr = cmd.ExecuteReader();
                Matiere_Enseigne_ListView.DataSource = dr;
                Matiere_Enseigne_ListView.DataBind();
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            { }
        }
        public void Load_Planifications_Par_Cours()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();

                string r = "SELECT COUNT(id_horaire) FROM horaire WHERE id_cours=@idcours AND id_classe=@idcl AND id_departement=@id_departement";
                MySqlCommand cmm = new MySqlCommand(r, conn);
                cmm.Parameters.AddWithValue("@idcl", id_classe);
                cmm.Parameters.AddWithValue("@idcours", id_cours);
                cmm.Parameters.AddWithValue("@id_departement", id_departement);
                int length = Convert.ToInt32(cmm.ExecuteScalar());

                HoraireID = new string[length];
                HorairePeriode = new string[length];
                HoraireDate = new string[length];
                HoraireJour = new string[length];

                string re = "SELECT id_horaire, date, jour, periode FROM horaire WHERE id_cours=@idcours AND id_classe=@idcl AND id_departement=@id_departement ORDER BY date";
                MySqlCommand cm = new MySqlCommand(re, conn);
                cm.Parameters.AddWithValue("@idcl", id_classe);
                cm.Parameters.AddWithValue("@idcours", id_cours);
                cm.Parameters.AddWithValue("@id_departement", id_departement);
                MySqlDataReader drg = cm.ExecuteReader();
                int i = 0;
                while (drg.Read())
                {
                    HoraireID[i] = string.Format("{0}", drg.GetInt32(0).ToString());
                    HoraireDate[i] = string.Format("{0}", drg.GetString(1));
                    HoraireJour[i] = string.Format("{0}", drg.GetString(2));
                    HorairePeriode[i] = string.Format("{0}", drg.GetString(3));
                    i++;
                }
                drg.Close();
                //Generer Boutons
                AM = new LinkButton[length];
                for (int u = 0; u < length; u++)
                {
                    AM[u] = new LinkButton();
                    AM[u].Click += new EventHandler(btn_Click);
                }
                Horaire_Panel.Controls.Clear();

                
                for (int k = 0; k < length; k++)
                {
                    int j = 0;
                    int col = 0, lign = 1;
                    int y = 0;

                    foreach (LinkButton bn in AM)
                    {
                        bn.Attributes.Add("Style", System.Drawing.ContentAlignment.TopLeft.ToString());
                        bn.Width = 120;
                        bn.Height = 60;
                        largeur = 360; //bn.Width * 3;
                        if (col % 3 == 0 && col != 0)
                        {
                            y += 60;
                            col = 0;
                            //bn.Location = new System.Drawing.Point(col * 120, y);
                            col++;
                            lign++;
                        }
                        else
                        {
                            //bn.Location = new System.Drawing.Point(col * 120, y);
                            col++;
                        }
                        //bn.FlatStyle = FlatStyle.Flat;
                        bn.BackColor = System.Drawing.Color.Azure;
                        bn.Visible = true;
                        bn.BorderStyle = BorderStyle.Solid;
                        bn.BorderColor = System.Drawing.Color.White;
                        hauteur = 60 * lign;// Convert.ToInt32(bn.Height) * lign;
                        Horaire_Panel.Width = largeur;
                        Horaire_Panel.Height = hauteur;
                        Horaire_Panel.Controls.Add(bn);
                        j++;
                    }
                }
                
                for (int h = 0; h < length; h++)
                {
                    AM[h].ID = HoraireJour[h] + "_" + HoraireDate[h] + "_" + HorairePeriode[h] + "_" + HoraireID[h];
                    
                    string value = AM[h].ID;
                    string[] op = value.Split('_');
                    id_horai = Convert.ToInt32(op[3]);

                    string ra = "SELECT COUNT(id_prestation) FROM prestation  WHERE id_horaire=@id_horaire";
                    MySqlCommand cmma = new MySqlCommand(ra, conn);
                    cmma.Parameters.AddWithValue("@id_horaire", id_horai);
                    int nbre_prestation = Convert.ToInt32(cmma.ExecuteScalar());

                    if (nbre_prestation == 0)
                    {
                        AM[h].Text = HoraireJour[h] + "_" + HorairePeriode[h] + "<br/>" + HoraireDate[h] + "<br/>NbreH: -";
                        AM[h].BackColor = System.Drawing.Color.MintCream;
                    }
                    else
                    {
                        string req = "SELECT id_prestation,heureD,heureF FROM prestation WHERE id_horaire=@id_horaire";
                        MySqlCommand cmq = new MySqlCommand(req, conn);
                        cmq.Parameters.AddWithValue("@id_horaire", id_horai);
                        MySqlDataReader drq = cmq.ExecuteReader();
                        int heured = 0, mind = 0, heuref = 0, minf = 0, min_prest = 0;
                        string nbre_heure = "";
                        while (drq.Read())
                        {
                            string value1 = drq.GetString(1);
                            string[] op1 = value1.Split('h');
                            heured = Convert.ToInt32(op1[0]);
                            mind = Convert.ToInt32(op1[1]);

                            string value2 = drq.GetString(2);
                            string[] op2 = value2.Split('h');
                            heuref = Convert.ToInt32(op2[0]);
                            minf = Convert.ToInt32(op2[1]);

                            min_prest = (heuref * 60 + minf) - (heured * 60 + mind);
                            nbre_heure = min_prest / 60 + "h" + min_prest % 60;
                            AM[h].Text = HoraireJour[h] + "_" + HorairePeriode[h] + "<br/>" + HoraireDate[h] + "<br/>NbreH: " + nbre_heure;
                            AM[h].BackColor = System.Drawing.Color.PaleTurquoise;
                        }
                        drq.Close();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Debut_TextBox.Visible = false;
            Fin_TextBox.Visible = false;
            Enregistre_Btn.Visible = false;
            Debut_TextBox.Value = ""; Fin_TextBox.Value = "";
            Operation_ComboBox.SelectedValue = "-1";

            mpe.Show();
            LinkButton btnSender = (LinkButton)sender;
            code = btnSender.ID;
            string value = btnSender.ID;
            string[] op = value.Split('_');
            id_horaire = Convert.ToInt32(op[3]);

            jours_tt.Text = btnSender.Text;
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_annee = Convert.ToInt32((Annee_Combo.SelectedItem).Value);
            annee = (Annee_Combo.SelectedItem).Text;

            Load_Faculte();
        }

        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_faculte = Convert.ToInt32((Faculte_Combo.SelectedItem as ListItem).Value);
            nom_faculte = (Faculte_Combo.SelectedItem as ListItem).Text;

            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();
            //MessageBox.Show(id_annee + "  " + id_faculte);
            string sql0 = "SELECT COUNT(*) FROM departement_par_faculte WHERE id_annee=@id_annee AND id_faculte=@id_faculte";
            MySqlCommand cmd0 = new MySqlCommand(sql0, conn);
            cmd0.Parameters.AddWithValue("@id_annee", id_annee);
            cmd0.Parameters.AddWithValue("@id_faculte", id_faculte);
            has_depaartement = Convert.ToInt32(cmd0.ExecuteScalar());
            conn.Close();

            Load_Departements();

        }

        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_departement = Convert.ToInt32((Departement_Combo.SelectedItem as ListItem).Value);
            nom_departement = (Departement_Combo.SelectedItem as ListItem).Text;
            Load_Classe(id_annee, id_faculte, id_departement);
        }

        protected void ClasseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32((ClasseCombo.SelectedItem as ListItem).Value);
            nom_classe = (ClasseCombo.SelectedItem as ListItem).Text;
            //etat_classe = (ClasseCombo.SelectedItem as Class_Container_Items_Manager).string_value_1;
            Find_Cours();
        }

        protected void Cours_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_cours = Convert.ToInt32((Cours_ComboBox.SelectedItem as ListItem).Value);
            nom_cours = (Cours_ComboBox.SelectedItem as ListItem).Text;
            Load_Planifications_Par_Cours();
            Load_Presations_Validees();
            Load_Operations();
        }

        protected void Jour_Prestation_ListView_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            Jour_Prestation_ListView.SelectedIndex = e.NewSelectedIndex;
        }

        protected void grdv_ligne_CheckedChanged(object sender, EventArgs e)
        {
            int id_row_index = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;

            CheckBox ckb = (CheckBox)Jour_Prestation_ListView.Rows[id_row_index].FindControl("grdv_ligne");

            if (ckb.Checked == true)
            {
                id_prestation = Convert.ToInt32(Jour_Prestation_ListView.DataKeys[id_row_index].Value.ToString());
                Load_Prestations_Details();
            }
            else { 
            
            
            }
        }

        protected void Print_Button_Click(object sender, ImageClickEventArgs e)
        {
            Class_Reports rap = new Class_Reports();
            //Response.Write("<script>alert(' yoooooooooooooo ')</script>");
            Response.Write("<script>alert('" + id_annee + " ," + annee + " ," + id_faculte + " ," + nom_faculte + " ," + has_depaartement + " ," + id_departement + " ," + nom_departement + " ," + id_classe + " ," + nom_classe + " ," + id_cours + " ," + nom_cours + "')</script>");
            rap.prestations_history(id_annee, annee, id_faculte, nom_faculte, has_depaartement, id_departement, nom_departement, id_classe, nom_classe, id_cours, nom_cours);
        
        }

        protected void Enregistre_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();

                if (id_operation == -1)
                {
                    Response.Write("<script>alert('Il est possible que vous n ayez pas choisi l operatioin')</script>");
                }
                else
                {
                    if (id_operation == 0)
                    {
                        string rqt = "DELETE FROM prestation WHERE id_horaire=@id_horaire";
                        MySqlCommand cmd = new MySqlCommand(rqt, conn);
                        cmd.Parameters.AddWithValue("@id_horaire", id_horaire);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Operation reussie')</script>");
                    }
                    else
                    {
                        if (Debut_TextBox.Value != "" && Fin_TextBox.Value != "")
                        {
                            int debut_heure, debut_min, fin_heure, fin_min, minute_prestees;
                            string heure_preste;

                            string value1 = Debut_TextBox.Value;
                            string[] op = value1.Split('h');
                            debut_heure = Convert.ToInt32(op[0]);
                            debut_min = Convert.ToInt32(op[1]);

                            string value2 = Fin_TextBox.Value;
                            string[] op1 = value2.Split('h');
                            fin_heure = Convert.ToInt32(op1[0]);
                            fin_min = Convert.ToInt32(op1[1]);

                            minute_prestees = (fin_heure * 60 + fin_min) - (debut_heure * 60 + debut_min);
                            if (minute_prestees > 0)
                            {
                                heure_preste = minute_prestees / 60 + " h " + minute_prestees % 60;
                                string requette = "SELECT COUNT(*) FROM prestation WHERE id_horaire=@id_horaire";
                                MySqlCommand com = new MySqlCommand(requette, conn);
                                com.Parameters.AddWithValue("@id_horaire", prestations.id_horaire);
                                Int16 result = Convert.ToInt16(com.ExecuteScalar());
                                if (result == 0)
                                {
                                    string rqt = "INSERT INTO prestation (heureD, heureF,id_horaire) VALUES (@heureD, @heureF,@id_horaire)";
                                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                                    cmd.Parameters.AddWithValue("@heureD", debut_heure + "h" + debut_min);
                                    cmd.Parameters.AddWithValue("@heureF", fin_heure + "h" + fin_min);
                                    cmd.Parameters.AddWithValue("@id_horaire", prestations.id_horaire);
                                    cmd.ExecuteNonQuery();
                                    Response.Write("<script>alert('Operation reussie')</script>");
                                }
                                else
                                {
                                    string rqt = "UPDATE prestation SET heureD=@heureD,heureF=@heureF WHERE id_horaire=@id_horaire";
                                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                                    cmd.Parameters.AddWithValue("@heureD", debut_heure + "h" + debut_min);
                                    cmd.Parameters.AddWithValue("@heureF", fin_heure + "h" + fin_min);
                                    cmd.Parameters.AddWithValue("@id_horaire", prestations.id_horaire);
                                    cmd.ExecuteNonQuery();
                                    Response.Write("<script>alert('Operation reussie')</script>");
                                }
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Remplicer tous les données')</script>");
                        }
                    }
                }
                conn.Close();
                Load_Planifications_Par_Cours();
                
                //Close();
            }
            catch (Exception ex)
            {
            }
        }

        protected void Operation_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_operation = Convert.ToInt32( Operation_ComboBox.SelectedValue);
            mpe.Show();
            if ( id_operation == 1)
            {
                Debut_TextBox.Visible = true;
                Fin_TextBox.Visible = true;
                Enregistre_Btn.Visible = true;
                exit.Text = "Annuler";
            }
            else if (id_operation == 0)
            {
                Enregistre_Btn.Visible = true;
                exit.Text = "Annuler";
            } 
        }

        protected void Matiere_Enseigne_ListView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string rqt = "DELETE FROM prestation_details WHERE id_prestation_details=@idprestation";
                MySqlCommand cm = new MySqlCommand(rqt, conn);
                cm.Parameters.AddWithValue("@idprestation", Convert.ToInt32(Matiere_Enseigne_ListView.DataKeys[e.RowIndex].Value.ToString()));
                cm.ExecuteNonQuery();
                conn.Close();

                Load_Prestations_Details();
            }
            catch (Exception ex)
            {
            }
        }

        protected void Matiere_Enseigne_ListView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Matiere_Enseigne_ListView.EditIndex = -1;
            Load_Prestations_Details();
        }

        protected void Matiere_Enseigne_ListView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Matiere_Enseigne_ListView.EditIndex = e.NewEditIndex;
            Load_Prestations_Details();
            
        }

        protected void Matiere_Enseigne_ListView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                string pointAborde = (Matiere_Enseigne_ListView.Rows[e.RowIndex].FindControl("point_aborde") as TextBox).Text.Trim();
                
                if (pointAborde.Trim() == "")
                {
                    Response.Write("<script>confirm('Remplicer le champ')</script>");
                    conn.Close();
                    return;
                }
                    string rqt = "UPDATE prestation_details SET pointAborde=@pointAborde WHERE id_prestation_details=@id_prestation_details";
                    MySqlCommand cm = new MySqlCommand(rqt, conn);
                    cm.Parameters.AddWithValue("@pointAborde", pointAborde);
                    cm.Parameters.AddWithValue("@id_prestation_details", Convert.ToInt32(Matiere_Enseigne_ListView.DataKeys[e.RowIndex].Value.ToString()));
                    cm.ExecuteNonQuery();
                    pointAborde = "";
                conn.Close();
                Matiere_Enseigne_ListView.EditIndex = -1;
                Load_Prestations_Details();
            }
            catch (Exception ex)
            {
                Response.Write("<script>confirm('Echec, verifier si vous avez de la connection.')</script>");
            }
        }

        protected void Matiere_Enseigne_ListView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
             try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    conn = new MySqlConnection(LoginForm.MyString);
                    conn.Open();
                    string pointAborde = (Matiere_Enseigne_ListView.FooterRow.FindControl("nouveauPoint") as TextBox).Text.Trim();
                    if (pointAborde.Trim() == "")
                    {
                        Response.Write("<script>confirm('Remplicer le champ')</script>");
                        conn.Close();
                        return;
                    }

                    string rqt = "INSERT INTO prestation_details(pointAborde, id_prestation) VALUES (@pointAborde, @idprestation)";
                    MySqlCommand cm = new MySqlCommand(rqt, conn);
                    cm.Parameters.AddWithValue("@pointAborde", pointAborde);
                    cm.Parameters.AddWithValue("@idprestation", id_prestation);
                    cm.ExecuteNonQuery();
                    pointAborde = "";
                    idprst = -1;

                    conn.Close();
                    Load_Prestations_Details();
                }
            }
             catch (Exception ex)
             {
             }
        }

        protected void Operation_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyMultiView.ActiveViewIndex = Convert.ToInt32(Operation_Combo.SelectedValue);
            int id_operation = MyMultiView.ActiveViewIndex;
            switch (id_operation)
            {
                case 0: Load_Planifications_Par_Cours();
                    break;
                case 1: Load_Presations_Validees();
                    break;
                default:
                    break;
            }
        }

    }
}