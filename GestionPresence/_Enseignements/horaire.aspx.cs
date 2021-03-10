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
using System.Globalization;
using System.Diagnostics.CodeAnalysis;


namespace BMDSysWeb._Enseignements
{
    public partial class horaire : System.Web.UI.Page
    {
        MySqlConnection conn;

        public static int i, week_number = 0, id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1, id_semestre = -1, idh = -1, id_operation=-1,ids = -1,id_cours = -1;
        int cpt = 0;
        public static int id_salle = -1;
        public static string sal = "";
        public static string etat_annee;
        public static string code, dat, nom_classe, nom_departement, date1, date2, jr, perio;
        public static string[] dates, days_week;
        
        public static string lu, mar, mer, jeu, ve, sam, dim, lun, mard, merc, jeud, ven, same, dima;

        public static int capac = -1;
        public static string cours = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Annee();
            }

            Find_Week_Dates(0);

            if (id_classe != -1)
            {
                Generer_Boutons_AM();
                Generer_Boutons_PM();
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
                item0.Text = "Choisissez A-A";
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
                Response.Write("<script>alert('Echec de chargement des annees academique')</script>");
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
                item0.Text = "Choisissez la faculte";
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
                Response.Write("<script>alert('Echec de chargement des facultes')</script>");
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
                item0.Text = "Choisissez le departement";
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
                Response.Write("<script>alert('Echec de chargement des departements')</script>");
                return;
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
                Classe_ComboBox.Items.Clear();
                ListItem item0 = new ListItem();
                item0.Value = "-1";
                item0.Text = "Choisir la classe";
                Classe_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    ListItem item = new ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Classe_ComboBox.Items.Add(item);
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

        private void Find_Week_Dates(int c)
        {
            if (c == 0)
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                System.Globalization.Calendar cal = dfi.Calendar;
                week_number = cal.GetWeekOfYear(DateTime.Today, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                DateTime dt = new DateTime(DateTime.Today.Year, 1, 1);
                int days = (week_number - 1) * 7;
                DateTime dt1 = dt.AddDays(days);
                DayOfWeek dow = dt1.DayOfWeek;
                DateTime dimanche_date = dt1.AddDays(-(int)dow);
                DateTime Lundi_date, Mardi_date, Mercredi_date, Jeudi_date, Vendredi_date, Samedi_date, Dimanche_date;
                Lundi_date = dimanche_date.AddDays(1);
                Lundi_Date_Label.Text = Lundi_date.ToString("dd-MM-yyyy");
                lun = "Lundi";

                Mardi_date = dimanche_date.AddDays(2);
                Mardi_Date_Label.Text = Mardi_date.ToString("dd-MM-yyyy");
                mard = "Mardi";

                Mercredi_date = dimanche_date.AddDays(3);
                Mercredi_Date_Label.Text = Mercredi_date.ToString("dd-MM-yyyy");
                merc = "Mercredi";

                Jeudi_date = dimanche_date.AddDays(4);
                Jeudi_Date_Label.Text = Jeudi_date.ToString("dd-MM-yyyy");
                jeud = "Jeudi";

                Vendredi_date = dimanche_date.AddDays(5);
                Vendredi_Date_Label.Text = Vendredi_date.ToString("dd-MM-yyyy");
                ven = "Vendredi";


                Samedi_date = dimanche_date.AddDays(6);
                Samedi_Date_Label.Text = Samedi_date.ToString("dd-MM-yyyy");
                same = "Samedi";


                Dimanche_date = dimanche_date.AddDays(7);
                Dimanche_Date_Label.Text = Dimanche_date.ToString("dd-MM-yyyy");
                dima = "Dimanche";
                same = "Samedi";
                ven = "Vendredi";
                jeud = "Jeudi";
                merc = "Mercredi";
                mard = "Mardi";
                lun = "Lundi";

                lu = Lundi_Date_Label.Text;
                mar = Mardi_Date_Label.Text;
                mer = Mercredi_Date_Label.Text;
                jeu = Jeudi_Date_Label.Text;
                ve = Vendredi_Date_Label.Text;
                sam = Samedi_Date_Label.Text;
                dim = Dimanche_Date_Label.Text;
                dates = new string[7] { lu, mar, mer, jeu, ve, sam, dim };
                days_week = new string[7] { lun, mard, merc, jeud, ven, same, dima };
            }
            else
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                System.Globalization.Calendar cal = dfi.Calendar;
                week_number = cal.GetWeekOfYear(DateTime.Today, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                DateTime dt = new DateTime(DateTime.Today.Year, 1, 1);
                int days = (week_number - 1) * 7;
                DateTime dt1 = dt.AddDays(days);
                DayOfWeek dow = dt1.DayOfWeek;
                DateTime dimanche_date = dt1.AddDays(-(int)dow);
                DateTime Lundi_date, Mardi_date, Mercredi_date, Jeudi_date, Vendredi_date, Samedi_date, Dimanche_date;
                Lundi_date = dimanche_date.AddDays(c + 1);
                Lundi_Date_Label.Text = Lundi_date.ToString("dd-MM-yyyy");

                Mardi_date = dimanche_date.AddDays((c + 2));
                Mardi_Date_Label.Text = Mardi_date.ToString("dd-MM-yyyy");

                Mercredi_date = dimanche_date.AddDays((c + 3));
                Mercredi_Date_Label.Text = Mercredi_date.ToString("dd-MM-yyyy");

                Jeudi_date = dimanche_date.AddDays((c + 4));
                Jeudi_Date_Label.Text = Jeudi_date.ToString("dd-MM-yyyy");

                Vendredi_date = dimanche_date.AddDays((c + 5));
                Vendredi_Date_Label.Text = Vendredi_date.ToString("dd-MM-yyyy");

                Samedi_date = dimanche_date.AddDays((c + 6));
                Samedi_Date_Label.Text = Samedi_date.ToString("dd-MM-yyyy");

                Dimanche_date = dimanche_date.AddDays((c + 7));
                Dimanche_Date_Label.Text = Dimanche_date.ToString("dd-MM-yyyy");
                lu = Lundi_Date_Label.Text;
                mar = Mardi_Date_Label.Text;
                mer = Mercredi_Date_Label.Text;
                jeu = Jeudi_Date_Label.Text;
                ve = Vendredi_Date_Label.Text;
                sam = Samedi_Date_Label.Text;
                dim = Dimanche_Date_Label.Text;
                dates = new string[7] { lu, mar, mer, jeu, ve, sam, dim };
                days_week = new string[7] { lun, mard, merc, jeud, ven, same, dima };
            }
        }

        protected void ExitButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/_Enseignements/emptyEnseignements.aspx");
        }

        protected void Annee_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Faculte_Combo.SelectedValue = "-1";
            Departement_Combo.SelectedValue = "-1";
            Classe_ComboBox.SelectedValue = "-1";
            id_annee = Convert.ToInt32(Annee_Combo.SelectedValue);
            Load_Faculte();
        }

        protected void Faculte_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Departement_Combo.SelectedValue = "-1";
            Classe_ComboBox.SelectedValue = "-1";
            id_faculte = Convert.ToInt32(Faculte_Combo.SelectedValue);
            Load_Departements();
        }

        protected void Departement_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            id_departement = Convert.ToInt32(Departement_Combo.SelectedValue);
            nom_departement = (Departement_Combo.SelectedItem).Text;
            Load_Classe();
        }

        protected void Classe_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_classe = Convert.ToInt32(Classe_ComboBox.SelectedValue);
            nom_classe = (Classe_ComboBox.SelectedItem).Text + "_" + id_classe;
            Find_Week_Dates(0);

            Generer_Boutons_AM();
            Generer_Boutons_PM();
            
        }
        public static int itype;

        public static LinkButton[] AM;

        private void Generer_Boutons_AM()
        {
            AM = new LinkButton[7];
            
            for (int u = 0; u < 7; u++)
            {
                AM[u] = new LinkButton();
                AM[u].Click += new EventHandler(btn_Click);
            }

            Horaire_Panel_AM.Controls.Clear();

            for (int k = 0; k < 7; k++)
            {
                int i = 0;
                foreach (LinkButton bn in AM)
                {
                    bn.Width = 300;
                    bn.Height = 60;
                    bn.BorderStyle = BorderStyle.Solid;
                    bn.BackColor = System.Drawing.Color.White;
                    bn.Visible = true;
                    Horaire_Panel_AM.Controls.Add(bn);
                    i++;
                }
            }
            for (int d = 0; d < 7; d++)
            {
                AM[d].ID = nom_classe + "_AM_" + id_departement + "_" + dates[d] + "_" + days_week[d] + "_" + id_faculte + "_" + id_annee;
                
                //try
                //{
                    conn = new MySqlConnection(LoginForm.MyString);
                    conn.Open();

                    idh = -1;
                    string ra = "SELECT id_horaire FROM horaire WHERE code=@code";
                    MySqlCommand cma = new MySqlCommand(ra, conn);
                    cma.Parameters.AddWithValue("@code", AM[d].ID);
                    MySqlDataReader dtr = cma.ExecuteReader();
                    if (dtr.Read())
                    {
                        idh = dtr.GetInt32(0);
                    }
                    dtr.Close();

                    string re = "SELECT cours.cours, horaire.id_horaire_type, salle.nom_salle, personnel.nom, personnel.prenom" +
                       " FROM (salle INNER JOIN salle_occupation ON salle.id_salle = salle_occupation.id_salle) INNER JOIN ((personnel INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON personnel.id_personnel = attribution_cours.id_personnel) INNER JOIN horaire ON cours.id_cours = horaire.id_cours) ON salle_occupation.id_horaire = horaire.id_horaire" +
                       " WHERE horaire.id_horaire=@id_horaire";
                    MySqlCommand cm = new MySqlCommand(re, conn);
                    cm.Parameters.AddWithValue("@id_horaire", idh);
                    MySqlDataReader dr = cm.ExecuteReader();
                    string cours = "", nomp = "", salle = "";
                    while (dr.Read())
                    {
                        cours = dr.GetString(0);
                        itype = dr.GetInt32(1);
                        salle = dr.GetString(2);
                        nomp = dr.GetString(4) + "" + dr.GetString(3);
                    }
                    dr.Close();
                    if (cours == "")
                    {
                        AM[d].Text = cours;
                        AM[d].BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        switch (itype)
                        {
                            case 1:
                                AM[d].Text = cours + "<br/>Salle:" + salle + " <br/>Enseignant: " + nomp;
                                AM[d].BackColor = System.Drawing.Color.MintCream;
                                break;
                            case 2:
                                AM[d].Text = "Cours: " + cours + "<br/>EXAMEN 1ere SESSION <br/> Salle: " + salle;
                                AM[d].BackColor = System.Drawing.Color.MintCream;
                                break;
                            case 3:
                                AM[d].Text = "Cours: " + cours + "<br/>EXAMEN 2eme SESSION <br/> Salle: " + salle;
                                AM[d].BackColor = System.Drawing.Color.MintCream;
                                break;
                            default:
                                break;
                        }
                    }
                    conn.Close();
                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.Message);
                //}
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Operation_ComboBox.SelectedValue = "-1";
            cours_Combo.SelectedValue = "-1";
            salle_Combo.SelectedValue = "-1";

            LinkButton btnSender = (LinkButton)sender;
            code = btnSender.ID;
            conn = new MySqlConnection(LoginForm.MyString);
            conn.Open();

            string ra = "SELECT id_horaire FROM horaire WHERE code=@code";
            MySqlCommand cma = new MySqlCommand(ra, conn);
            cma.Parameters.AddWithValue("@code", code);
            MySqlDataReader dtr = cma.ExecuteReader();
            if (dtr.Read())
            {
                idh = dtr.GetInt32(0);
            }
            dtr.Close();
            conn.Close();

            string value = btnSender.ID;
            string[] op = value.Split('_');
            op[1].All(char.IsDigit);

            if (op[1].All(char.IsDigit))
            {
                id_classe = Convert.ToInt32(op[1]);
                id_departement = Convert.ToInt32(op[3]);
                jr = op[5];
                dat = op[4];
                perio = op[2];
            }
            else
            {
                id_classe = 0;
                id_departement = 0;
                jr = op[4];
                dat = op[3];
                perio = op[2];
            }
            hc.Show();
            
        }

        public static LinkButton[] PM;
        public void Generer_Boutons_PM()
        {
            
            PM = new LinkButton[7];
            for (int u = 0; u < 7; u++)
            {
                PM[u] = new LinkButton();
                PM[u].Click += new EventHandler(btn_Click);
            }

            Horaire_Panel_PM.Controls.Clear();
            
            for (int k = 0; k < 7; k++)
            {
                i = 0;
                foreach (LinkButton bn in PM)
                {
                    bn.Width = 300;
                    bn.Height = 60;
                    bn.BorderStyle = BorderStyle.Solid;
                    bn.BackColor = System.Drawing.Color.White;
                    bn.Visible = true;
                    Horaire_Panel_PM.Controls.Add(bn);
                    i++;
                }
            }
            for (int d = 0; d < 7; d++)
            {
                PM[d].ID = nom_classe + "_PM_" + id_departement + "_" + dates[d] + "_" + days_week[d] + "_" + id_faculte + "_" + id_annee;
                try
                {
                    Class_conString conn = new Class_conString();
                    conn.con.Open();

                    idh = -1;
                    string ra = "SELECT id_horaire FROM horaire WHERE code=@code";
                    MySqlCommand cma = new MySqlCommand(ra, conn.con);
                    cma.Parameters.AddWithValue("@code", PM[d].ID);
                    MySqlDataReader dtr = cma.ExecuteReader();
                    if (dtr.Read())
                    {
                        idh = dtr.GetInt32(0);
                    }
                    dtr.Close();

                    string re = "SELECT cours.cours, horaire.id_horaire_type, salle.nom_salle, personnel.nom, personnel.prenom" +
                       " FROM (salle INNER JOIN salle_occupation ON salle.id_salle = salle_occupation.id_salle) INNER JOIN ((personnel INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON personnel.id_personnel = attribution_cours.id_personnel) INNER JOIN horaire ON cours.id_cours = horaire.id_cours) ON salle_occupation.id_horaire = horaire.id_horaire" +
                       " WHERE horaire.id_horaire=@id_horaire";
                    MySqlCommand cm = new MySqlCommand(re, conn.con);
                    cm.Parameters.AddWithValue("@id_horaire", idh);
                    MySqlDataReader dr = cm.ExecuteReader();
                    string cours = "", nomp = "", salle = "";
                    while (dr.Read())
                    {
                        cours = dr.GetString(0);
                        itype = dr.GetInt32(1);
                        salle = dr.GetString(2);
                        nomp = dr.GetString(4) + " " + dr.GetString(3);
                    }
                    dr.Close();
                    if (cours == "")
                    {
                        PM[d].Text = cours;
                        PM[d].BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        switch (itype)
                        {
                            case 1:
                                PM[d].Text = cours + "<br/>Salle:" + salle + " <br/>Enseignant: " + nomp;
                                PM[d].BackColor = System.Drawing.Color.MintCream;
                                break;
                            case 2:
                                PM[d].Text = "Cours: " + cours + "<br/>EXAMEN 1ere SESSION<br/>Salle: " + salle;
                                PM[d].BackColor = System.Drawing.Color.MintCream;
                                break;
                            case 3:
                                PM[d].Text = "Cours: " + cours + "<br/>EXAMEN 2eme SESSION<br/>Salle: " + salle;
                                PM[d].BackColor = System.Drawing.Color.MintCream;
                                break;
                            default:
                                break;
                        }
                    }
                    conn.con.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        protected void Preview_Button_Click(object sender, EventArgs e)
        {
            cpt = cpt - 7;
            Find_Week_Dates(cpt);
            Generer_Boutons_AM();
            Generer_Boutons_PM();
        }

        protected void Inition_Click(object sender, EventArgs e)
        {
            cpt = 0;
            Find_Week_Dates(cpt);
            Generer_Boutons_PM();
            Generer_Boutons_AM();
        }

        protected void Next_Button_Click(object sender, EventArgs e)
        {
            cpt = cpt + 7;
            Find_Week_Dates(cpt);
            Generer_Boutons_AM();
            Generer_Boutons_PM();
        }

        protected void Enregistre_Btn_Click(object sender, EventArgs e)
        {

            Operation_ComboBox.SelectedValue = "-1";
            cours_Combo.SelectedValue = "-1";
            salle_Combo.SelectedValue = "-1";

            try
            {
                 conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();

                if (id_operation == -1)
                {
                    Response.Write("<script>alert('Veillez choisir une opération ')</script>");
                    hc.Show();
                    return;
                }

                if (id_operation == 0)
                {
                    string rqt = "DELETE FROM horaire WHERE code=@code";
                    MySqlCommand cmd = new MySqlCommand(rqt, conn);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.ExecuteNonQuery();

                    string rqt1 = "DELETE FROM salle_occupation WHERE id_horaire=@id_horaire";
                    MySqlCommand cmd1 = new MySqlCommand(rqt1, conn);
                    cmd1.Parameters.AddWithValue("@id_horaire", idh);
                    cmd1.ExecuteNonQuery();

                    //MessageBox.Show("Opération réussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (id_cours != -1 && id_operation != -1 )
                {
                    if ( id_operation==2 || id_operation == 3 )
                    {
                        string req = "SELECT COUNT(*) FROM horaire WHERE id_horaire_type=@id_horaire_type AND id_cours=@id_cours";
                        MySqlCommand cma = new MySqlCommand(req, conn);
                        cma.Parameters.AddWithValue("@id_cours", id_cours);
                        cma.Parameters.AddWithValue("@id_horaire_type", 2);
                        int exist_exam = Convert.ToInt32(cma.ExecuteScalar());
                        if (exist_exam == 0)
                        {
                            string requette = "SELECT COUNT(*) FROM horaire WHERE code=@code";
                            MySqlCommand com = new MySqlCommand(requette,conn);
                            com.Parameters.AddWithValue("@code", code);
                            int result = Convert.ToInt32(com.ExecuteScalar());
                            if (result == 0)
                            {
                                string rqt = "INSERT INTO horaire(code,date,jour, periode,id_cours,id_classe,id_departement,id_faculte,id_annee,id_horaire_type) VALUES (@code,@date,@jour, @periode,@id_cours,@id_classe,@id_departement,@id_faculte,@id_annee,@id_horaire_type)";
                                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                                cmd.Parameters.AddWithValue("@code", code);
                                cmd.Parameters.AddWithValue("@date", dat);
                                cmd.Parameters.AddWithValue("@jour", jr);
                                cmd.Parameters.AddWithValue("@periode", perio);
                                cmd.Parameters.AddWithValue("@id_cours", id_cours);
                                cmd.Parameters.AddWithValue("@id_classe", id_classe);
                                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                                cmd.Parameters.AddWithValue("@id_horaire_type", id_operation);
                                cmd.ExecuteNonQuery();
                                int idhor = Convert.ToInt32(cmd.LastInsertedId);

                                string rqta = "INSERT INTO salle_occupation(id_salle, id_horaire, date) VALUES (@id_salle, @id_horaire, @date)";
                                MySqlCommand cmda = new MySqlCommand(rqta, conn);
                                cmda.Parameters.AddWithValue("@id_salle", id_salle);
                                cmda.Parameters.AddWithValue("@id_horaire", idhor);
                                cmda.Parameters.AddWithValue("@date", dat + "_" +perio);
                                cmda.ExecuteNonQuery();

                                //MessageBox.Show("Operation reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string rqt = "UPDATE horaire SET date=@dat,jour=@jour,periode=@periode,id_cours=@idcours,id_classe=@idcla,id_departement=@id_departement,id_faculte=@id_faculte,id_annee=@id_annee, id_horaire_type=@id_horaire_type WHERE code =@code";
                                MySqlCommand cmd = new MySqlCommand(rqt, conn);
                                cmd.Parameters.AddWithValue("@dat",dat);
                                cmd.Parameters.AddWithValue("@jour", jr);
                                cmd.Parameters.AddWithValue("@periode", perio);
                                cmd.Parameters.AddWithValue("@idcours", id_cours);
                                cmd.Parameters.AddWithValue("@idcla", id_classe);
                                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                                cmd.Parameters.AddWithValue("@code", code);
                                cmd.Parameters.AddWithValue("@id_horaire_type", id_operation);
                                cmd.ExecuteNonQuery();

                                string rqta = "UPDATE salle_occupation SET id_salle=@id_salle, date=@date WHERE id_horaire=@id_horaire";
                                MySqlCommand cmda = new MySqlCommand(rqta, conn);
                                cmda.Parameters.AddWithValue("@id_salle",id_salle);
                                cmda.Parameters.AddWithValue("@id_horaire", idh);
                                cmda.Parameters.AddWithValue("@date", dat + "_" + perio);
                                cmda.ExecuteNonQuery();

                                //MessageBox.Show("Operation reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {

                           // MessageBox.Show("L’examen pour ce cours est déjà programmé. Soit supprimer d’abord \n le créneau où il est programmé et reprogrammer par la suite", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        string requette = "SELECT COUNT(*) FROM horaire WHERE code=@code";
                        MySqlCommand com = new MySqlCommand(requette, conn);
                        com.Parameters.AddWithValue("@code", code);
                        Int16 result = Convert.ToInt16(com.ExecuteScalar());
                        if (result == 0)
                        {
                            string rqt = "INSERT INTO horaire(code,date,jour, periode,id_cours,id_classe,id_departement,id_faculte,id_annee,id_horaire_type) VALUES (@code,@date,@jour, @periode,@id_cours,@id_classe,@id_departement,@id_faculte,@id_annee,@id_horaire_type)";
                            MySqlCommand cmd = new MySqlCommand(rqt, conn);
                            cmd.Parameters.AddWithValue("@code", code);
                            cmd.Parameters.AddWithValue("@date", dat);
                            cmd.Parameters.AddWithValue("@jour", jr);
                            cmd.Parameters.AddWithValue("@periode", perio);
                            cmd.Parameters.AddWithValue("@id_cours", id_cours);
                            cmd.Parameters.AddWithValue("@id_classe", id_classe);
                            cmd.Parameters.AddWithValue("@id_departement", id_departement);
                            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                            cmd.Parameters.AddWithValue("@id_annee", id_annee);
                            cmd.Parameters.AddWithValue("@id_horaire_type", id_operation);
                            cmd.ExecuteNonQuery();
                            int idhor = Convert.ToInt32(cmd.LastInsertedId);

                            string rqta = "INSERT INTO salle_occupation(id_salle, id_horaire, date) VALUES (@id_salle, @id_horaire, @date)";
                            MySqlCommand cmda = new MySqlCommand(rqta, conn);
                            cmda.Parameters.AddWithValue("@id_salle", id_salle);

                            cmda.Parameters.AddWithValue("@id_horaire", idhor);
                            cmda.Parameters.AddWithValue("@date", dat + "_" + perio);
                            cmda.ExecuteNonQuery();

                           // MessageBox.Show("Operation reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string rqt = "UPDATE horaire SET date=@dat,jour=@jour,periode=@periode,id_cours=@idcours,id_classe=@idcla,id_departement=@id_departement,id_faculte=@id_faculte,id_annee=@id_annee, id_horaire_type=@id_horaire_type WHERE code =@code";
                            MySqlCommand cmd = new MySqlCommand(rqt, conn);
                            cmd.Parameters.AddWithValue("@dat", dat);
                            cmd.Parameters.AddWithValue("@jour", jr);
                            cmd.Parameters.AddWithValue("@periode", perio);
                            cmd.Parameters.AddWithValue("@idcours", id_cours);
                            cmd.Parameters.AddWithValue("@idcla", id_classe);
                            cmd.Parameters.AddWithValue("@id_departement", id_departement);
                            cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                            cmd.Parameters.AddWithValue("@id_annee", id_annee);
                            cmd.Parameters.AddWithValue("@code", code);
                            cmd.Parameters.AddWithValue("@id_horaire_type", id_operation);
                            cmd.ExecuteNonQuery();

                            string rqta = "UPDATE salle_occupation SET id_salle=@id_salle, date=@date WHERE id_horaire=@id_horaire";
                            MySqlCommand cmda = new MySqlCommand(rqta, conn);
                            cmda.Parameters.AddWithValue("@id_salle", id_salle);
                            cmda.Parameters.AddWithValue("@id_horaire", idh);
                            cmda.Parameters.AddWithValue("@date",dat + "_" + perio);
                            cmda.ExecuteNonQuery();

                            //MessageBox.Show("Operation reussie", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    //ids = -1;
                    id_cours = -1;
                    id_operation = -1;
                }
                conn.Close();

                Generer_Boutons_AM();
                Generer_Boutons_PM();
            }
            catch (Exception ex)
            {
            }
        }

        protected void Operation_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_operation = Convert.ToInt32( Operation_ComboBox.SelectedValue);
            Find_Available_ECUE();
            Find_AvailableSalle();
            hc.Show();
        }

        protected void cours_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = cours_Combo.SelectedValue;
            string[] op = value.Split('_');
            id_cours = Convert.ToInt32(op[2]);
            cours = op[0];

            hc.Show();
        }

        protected void salle_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = salle_Combo.SelectedValue;
            string[] op = value.Split('_');
            id_salle = Convert.ToInt32(op[2]);
            sal = op[0];
            capac = Convert.ToInt32(op[1]);

            hc.Show();
        }

        static string[] CoursID, CoursNom, CoursVH, CoursVP, coursProID, coursProNom, coursProVH;

        private void Find_Available_ECUE()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();
                if (id_operation == 1)
                {
                    string r = "SELECT COUNT(DISTINCT cours.id_cours)" +
                    " FROM unite INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON unite.id_unite = cours.id_unite" +
                    " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee AND attribution_cours.etat=@etat";
                    MySqlCommand cmr = new MySqlCommand(r, conn);
                    cmr.Parameters.AddWithValue("@idcl", id_classe);
                    cmr.Parameters.AddWithValue("@id_departement",id_departement);
                    cmr.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmr.Parameters.AddWithValue("@id_annee", id_annee);
                    cmr.Parameters.AddWithValue("@etat", "Encours");
                    int length = Convert.ToInt32(cmr.ExecuteScalar());
                    if (length > 0)
                    {
                        CoursID = new string[length];
                        CoursNom = new string[length];
                        CoursVH = new string[length];
                        CoursVP = new string[length];

                        string re = "SELECT cours.id_cours, cours.cours, cours.credits" +
                            " FROM unite INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON unite.id_unite = cours.id_unite" +
                            " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee AND attribution_cours.etat=@etat";
                        MySqlCommand cm = new MySqlCommand(re, conn);
                        cm.Parameters.AddWithValue("@idcl", id_classe);
                        cm.Parameters.AddWithValue("@id_departement",id_departement);
                        cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cm.Parameters.AddWithValue("@id_annee", id_annee);
                        cm.Parameters.AddWithValue("@etat", "Encours");
                        MySqlDataReader drg = cm.ExecuteReader();
                        int i = 0;
                        while (drg.Read())
                        {
                            CoursID[i] = string.Format("{0}", drg.GetInt32(0).ToString());
                            CoursNom[i] = string.Format("{0}", drg.GetString(1));
                            CoursVH[i] = string.Format("{0}", drg.GetInt32(2) * 15 + "h");
                            i++;
                        }
                        drg.Close();

                        for (int ecue = 0; ecue < length; ecue++)
                        {
                            string sqr0 = "SELECT COUNT(prestation.id_prestation)" +
                                " FROM (cours INNER JOIN horaire ON cours.id_cours = horaire.id_cours) INNER JOIN prestation ON horaire.id_horaire = prestation.id_horaire" +
                                " WHERE cours.id_cours=@id_cours";
                            MySqlCommand cmq0 = new MySqlCommand(sqr0, conn);
                            cmq0.Parameters.AddWithValue("@id_cours", Convert.ToInt32(CoursID[ecue]));
                            int nbrep = Convert.ToInt32(cmq0.ExecuteScalar());
                            if (nbrep == 0)
                            {
                                CoursVP[ecue] = string.Format("{0}", "Zero heure");
                            }
                            else
                            {
                                int volume_preste = 0;
                                string heure_preste = "";
                                string sqr = "SELECT prestation.heureD, prestation.heureF" +
                                " FROM (cours INNER JOIN horaire ON cours.id_cours = horaire.id_cours) INNER JOIN prestation ON horaire.id_horaire = prestation.id_horaire" +
                                " WHERE cours.id_cours=@id_cours";
                                MySqlCommand cmq = new MySqlCommand(sqr, conn);
                                cmq.Parameters.AddWithValue("@id_cours", Convert.ToInt32(CoursID[ecue]));
                                MySqlDataReader drq = cmq.ExecuteReader();
                                while (drq.Read())
                                {
                                    int debut_heure, debut_min, fin_heure, fin_min, minute_prestees, volume, restant;
                                    //string heure_preste, heure_cumulee, heure_restant;

                                    string value1 = drq.GetString(0);
                                    string[] op = value1.Split('h');
                                    debut_heure = Convert.ToInt32(op[0]);
                                    debut_min = Convert.ToInt32(op[1]);

                                    string value2 = drq.GetString(1);
                                    string[] op1 = value2.Split('h');
                                    fin_heure = Convert.ToInt32(op1[0]);
                                    fin_min = Convert.ToInt32(op1[1]);

                                    minute_prestees = (fin_heure * 60 + fin_min) - (debut_heure * 60 + debut_min);
                                    volume_preste += minute_prestees;
                                }
                                drq.Close();

                                int min = volume_preste % 60;
                                if (min < 10)
                                {
                                    heure_preste = volume_preste / 60 + "h0" + min + "min";
                                }
                                else
                                {
                                    heure_preste = volume_preste / 60 + "h" + volume_preste % 60 + "min";
                                }

                                CoursVP[ecue] = string.Format("{0}", heure_preste);
                            }
                        }

                        cours_Combo.Items.Clear();
                        
                        ListItem item0 = new ListItem();
                        item0.Value = "-1";
                        item0.Text = "";
                        cours_Combo.Items.Add(item0);

                        for (int u = 0; u < length; u++)
                        {
                            ListItem item = new ListItem();
                            item.Value = CoursNom[u] + "_" + CoursVH[u] + "_" + CoursID[u];
                            item.Text = CoursNom[u] + " VH :" + CoursVH[u] + " VP :" + CoursVP[u];
                            cours_Combo.Items.Add(item);
                        }

                        
                    }
                }
                else if (id_operation == 2)
                {
                    string r = "SELECT COUNT(DISTINCT cours.id_cours)" +
                    " FROM unite INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON unite.id_unite = cours.id_unite" +
                    " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee AND attribution_cours.etat=@etat";
                    MySqlCommand cmr = new MySqlCommand(r, conn);
                    cmr.Parameters.AddWithValue("@idcl", id_classe);
                    cmr.Parameters.AddWithValue("@id_departement", id_departement);
                    cmr.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmr.Parameters.AddWithValue("@id_annee", id_annee);
                    cmr.Parameters.AddWithValue("@etat", "Encours");
                    int length = Convert.ToInt32(cmr.ExecuteScalar());
                    if (length > 0)
                    {
                        CoursID = new string[length];
                        CoursNom = new string[length];
                        CoursVH = new string[length];
                        CoursVP = new string[length];

                        coursProID = new string[length];
                        coursProNom = new string[length];
                        coursProVH = new string[length];

                        string re = "SELECT cours.id_cours, cours.cours, cours.credits" +
                            " FROM unite INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON unite.id_unite = cours.id_unite" +
                            " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee AND attribution_cours.etat=@etat";
                        MySqlCommand cm = new MySqlCommand(re, conn);
                        cm.Parameters.AddWithValue("@idcl", id_classe);
                        cm.Parameters.AddWithValue("@id_departement", id_departement);
                        cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cm.Parameters.AddWithValue("@id_annee", id_annee);
                        cm.Parameters.AddWithValue("@etat", "Encours");
                        MySqlDataReader drg = cm.ExecuteReader();
                        int i = 0;
                        while (drg.Read())
                        {
                            CoursID[i] = string.Format("{0}", drg.GetInt32(0).ToString());
                            CoursNom[i] = string.Format("{0}", drg.GetString(1));
                            CoursVH[i] = string.Format("{0}", drg.GetInt32(2) * 15 + "h");
                            i++;
                           }
                        drg.Close();
                        int ecue_exam = 0;
                        int id_tp = 0;
                        for (int ecue = 0; ecue < length; ecue++)
                        {
                            string sqr0 = "SELECT COUNT(*) FROM horaire WHERE id_cours=@id_cours AND id_horaire_type=@id_horaire_type";
                            MySqlCommand cmq0 = new MySqlCommand(sqr0, conn);
                            cmq0.Parameters.AddWithValue("@id_cours", Convert.ToInt32(CoursID[ecue]));
                            cmq0.Parameters.AddWithValue("@id_horaire_type", 2);
                            int exist_exam = Convert.ToInt32(cmq0.ExecuteScalar());
                            
                            if (exist_exam == 0)
                            {
                                coursProID[ecue_exam] = string.Format("{0}", CoursID[ecue]);
                                coursProNom[ecue_exam] = string.Format("{0}", CoursNom[ecue]);
                                coursProVH[ecue_exam] = string.Format("{0}", CoursVH[ecue]);

                                ecue_exam++;
                            }
                        }

                        cours_Combo.Items.Clear();
                        
                        ListItem item0 = new ListItem();
                        item0.Value = "-1";
                        item0.Text = "";
                        cours_Combo.Items.Add(item0);

                        for (int u = 0; u < length; u++)
                        {
                            ListItem item = new ListItem();
                            item.Value = coursProNom[u] + "_" + coursProVH[u] + "_" + coursProID[u];
                            item.Text = coursProNom[u] + " VH :" + coursProVH[u];

                            cours_Combo.Items.Add(item);
                        }
                    }
                    
                }
                else if (id_operation == 3)
                {
                    string r = "SELECT COUNT(DISTINCT etudiant_note.id_cours)" +
                    " FROM ((unite INNER JOIN cours ON unite.id_unite = cours.id_unite) INNER JOIN etudiant_note ON cours.id_cours = etudiant_note.id_cours) INNER JOIN etudiant_note_complement ON cours.id_cours = etudiant_note_complement.id_cours" +
                    " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee AND etudiant_note.id_session=@id_session AND etudiant_note_complement.etat_validation=@etat_validation AND etudiant_note.id_etudiant>@id_etudiant";
                    MySqlCommand cmr = new MySqlCommand(r, conn);
                    cmr.Parameters.AddWithValue("@idcl", id_classe);
                    cmr.Parameters.AddWithValue("@id_departement", id_departement);
                    cmr.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmr.Parameters.AddWithValue("@id_annee", id_annee);
                    cmr.Parameters.AddWithValue("@id_session", 2);
                    cmr.Parameters.AddWithValue("@etat_validation", "Non");
                    cmr.Parameters.AddWithValue("@id_etudiant", 0);
                    int length = Convert.ToInt32(cmr.ExecuteScalar());
                    if (length > 0)
                    {
                        CoursID = new string[length];
                        CoursNom = new string[length];
                        CoursVH = new string[length];
                        CoursVP = new string[length];

                        coursProID = new string[length];
                        coursProNom = new string[length];
                        coursProVH = new string[length];

                        string re = "SELECT DISTINCT etudiant_note.id_cours, cours.cours, cours.credits" +
                            " FROM unite INNER JOIN (cours INNER JOIN attribution_cours ON cours.id_cours = attribution_cours.id_cours) ON unite.id_unite = cours.id_unite" +
                            " WHERE unite.id_classe=@idcl AND unite.id_departement=@id_departement AND unite.id_faculte=@id_faculte AND unite.id_annee=@id_annee AND attribution_cours.etat=@etat";
                        MySqlCommand cm = new MySqlCommand(re, conn);
                        cm.Parameters.AddWithValue("@idcl", id_classe);
                        cm.Parameters.AddWithValue("@id_departement", id_departement);
                        cm.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cm.Parameters.AddWithValue("@id_annee", id_annee);
                        cm.Parameters.AddWithValue("@id_session", 2);
                        cm.Parameters.AddWithValue("@etat_validation", "Non");
                        cm.Parameters.AddWithValue("@id_etudiant", 0);
                        MySqlDataReader drg = cm.ExecuteReader();
                        int i = 0;
                        while (drg.Read())
                        {
                            CoursID[i] = string.Format("{0}", drg.GetInt32(0).ToString());
                            CoursNom[i] = string.Format("{0}", drg.GetString(1));
                            CoursVH[i] = string.Format("{0}", drg.GetInt32(2) * 15 + "h");
                            i++;
                        }
                        drg.Close();
                        int ecue_exam = 0;
                        int id_tp = 0;
                        for (int ecue = 0; ecue < length; ecue++)
                        {
                            string sqr0 = "SELECT COUNT(*) FROM horaire WHERE id_cours=@id_cours AND id_horaire_type=@id_horaire_type";
                            MySqlCommand cmq0 = new MySqlCommand(sqr0, conn);
                            cmq0.Parameters.AddWithValue("@id_cours", Convert.ToInt32(CoursID[ecue]));
                            cmq0.Parameters.AddWithValue("@id_horaire_type", 3);
                            int exist_exam = Convert.ToInt32(cmq0.ExecuteScalar());
                            if (exist_exam == 0)
                            {
                                coursProID[ecue_exam] = string.Format("{0}", CoursID[ecue]);
                                coursProNom[ecue_exam] = string.Format("{0}", CoursNom[ecue]);
                                coursProVH[ecue_exam] = string.Format("{0}", CoursNom[ecue]);
                                ecue_exam++;
                            }
                        }
                        
                        cours_Combo.Items.Clear();
                        
                        ListItem item0 = new ListItem();
                        item0.Value = "-1";
                        item0.Text = "";
                        cours_Combo.Items.Add(item0);

                        for (int u = 0; u < length; u++)
                        {
                            ListItem item = new ListItem();
                            item.Value = coursProNom[u] + "_" + coursProVH[u] + "_" + coursProID[u];
                            item.Text = coursProNom[u] + " VH :" + coursProVH[u];

                            cours_Combo.Items.Add(item);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        string[] Salle_ID, Salle_Nom, Salle_Capacite, Salle_Occupee_ID;
        private void Find_AvailableSalle()
        {
            try
            {
                conn = new MySqlConnection(LoginForm.MyString);
                conn.Open();


                string r = "SELECT COUNT(*) FROM salle_occupation WHERE date=@date";
                MySqlCommand cmr = new MySqlCommand(r, conn);
                cmr.Parameters.AddWithValue("@date", horaire.dat + "_" + horaire.perio);
                int exist = Convert.ToInt32(cmr.ExecuteScalar());
                if (exist == 0)
                {
                    string ra = "SELECT COUNT(id_salle) FROM salle";
                    MySqlCommand cmm = new MySqlCommand(ra, conn);
                    int nbre_salle_disponible = Convert.ToInt32(cmm.ExecuteScalar());

                    Salle_ID = new string[nbre_salle_disponible];
                    Salle_Nom = new string[nbre_salle_disponible];
                    Salle_Capacite = new string[nbre_salle_disponible];
                    string re = "SELECT id_salle, nom_salle, capacite FROM salle";
                    MySqlCommand cm = new MySqlCommand(re, conn);
                    MySqlDataReader drg = cm.ExecuteReader();
                    int i = 0;
                    while (drg.Read())
                    {
                        Salle_ID[i] = string.Format("{0}", drg.GetInt32(0).ToString());
                        Salle_Nom[i] = string.Format("{0}", drg.GetString(1));
                        Salle_Capacite[i] = string.Format("{0}", drg.GetInt32(2).ToString());
                        i++;
                    }
                    drg.Close();

                    salle_Combo.Items.Clear();

                    ListItem item0 = new ListItem();
                    item0.Value = "-1";
                    item0.Text = "";
                    salle_Combo.Items.Add(item0);

                    for (int u = 0; u < nbre_salle_disponible; u++)
                    {
                        ListItem item = new ListItem();
                        item.Value = Salle_Nom[u] + "_" + Salle_Capacite[u] + "_" + Salle_ID[u];
                        item.Text = Salle_Nom[u] + " Capacite :" + Salle_Capacite[u];
                        salle_Combo.Items.Add(item);
                    }
                }
                else
                {
                    string[] Salle_Dispo_ID, Salle_Dispo_Nom, Salle_Dispo_Capacite;

                    string ra = "SELECT COUNT(id_salle) FROM salle";
                    MySqlCommand cmm = new MySqlCommand(ra, conn);
                    int nbre_salle = Convert.ToInt32(cmm.ExecuteScalar());

                    Salle_ID = new string[nbre_salle];
                    Salle_Nom = new string[nbre_salle];
                    Salle_Capacite = new string[nbre_salle];


                    string re = "SELECT id_salle, nom_salle, capacite FROM salle";
                    MySqlCommand cm = new MySqlCommand(re, conn);
                    MySqlDataReader drg = cm.ExecuteReader();
                    int i = 0;
                    while (drg.Read())
                    {
                        Salle_ID[i] = string.Format("{0}", drg.GetInt32(0).ToString());
                        Salle_Nom[i] = string.Format("{0}", drg.GetString(1));
                        Salle_Capacite[i] = string.Format("{0}", drg.GetInt32(2).ToString());
                        i++;
                    }
                    drg.Close();

                    string ri = "SELECT COUNT(DISTINCT id_salle) FROM salle_occupation WHERE date=@date";
                    MySqlCommand ci = new MySqlCommand(ri, conn);
                    ci.Parameters.AddWithValue("@date", horaire.dat + "_" + horaire.perio);
                    int nbre_salle_occupe = Convert.ToInt32(ci.ExecuteScalar());
                    int nbre_salle_dispo = nbre_salle - nbre_salle_occupe;

                    Salle_Occupee_ID = new string[nbre_salle_occupe];
                    Salle_Dispo_ID = new string[nbre_salle_dispo];
                    Salle_Dispo_Nom = new string[nbre_salle_dispo];
                    Salle_Dispo_Capacite = new string[nbre_salle_dispo];


                    string ru = "SELECT DISTINCT id_salle FROM salle_occupation WHERE date=@date";
                    MySqlCommand cu = new MySqlCommand(ru, conn);
                    cu.Parameters.AddWithValue("@date", horaire.dat + "_" + horaire.perio);
                    MySqlDataReader du = cu.ExecuteReader();
                    i = 0;
                    while (du.Read())
                    {
                        Salle_Occupee_ID[i] = string.Format("{0}", du.GetInt32(0).ToString());
                        i++;
                    }
                    du.Close();

                    int s_dispo = 0, cpt = 0, s_disp = 0;

                    for (int idsalle = 0; idsalle < nbre_salle; idsalle++)
                    {
                        cpt = 0;
                        for (int s_occupe = 0; s_occupe < nbre_salle_occupe; s_occupe++)
                        {
                            if (Salle_ID[idsalle] == Salle_Occupee_ID[s_occupe])
                            {
                                cpt++;
                            }
                        }

                        if (cpt == 0)
                        {
                            Salle_Dispo_ID[s_disp] = string.Format("{0}", Salle_ID[idsalle]);
                            Salle_Dispo_Nom[s_disp] = string.Format("{0}", Salle_Nom[idsalle]);
                            Salle_Dispo_Capacite[s_disp] = string.Format("{0}", Salle_Capacite[idsalle]);
                            s_disp++;
                        }
                        
                    }

                    salle_Combo.Items.Clear();

                    ListItem item0 = new ListItem();
                    item0.Value = "-1";
                    item0.Text = "";
                    salle_Combo.Items.Add(item0);

                    for (int u = 0; u < s_disp; u++)
                    {
                        ListItem item = new ListItem();
                        item.Value = Salle_Dispo_Nom[u] + "_" + Salle_Dispo_Capacite[u] + "_" + Salle_Dispo_ID[u];
                        item.Text = Salle_Dispo_Nom[u] + "  Capacite :" + Salle_Dispo_Capacite[u];
                        salle_Combo.Items.Add(item);
                    }

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        protected void exit_Click(object sender, EventArgs e)
        {
            Operation_ComboBox.SelectedValue = "-1";
            cours_Combo.SelectedValue = "-1";
            salle_Combo.SelectedValue = "-1";
        }
    }
}