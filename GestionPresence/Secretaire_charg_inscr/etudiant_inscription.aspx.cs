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
using System.Drawing.Imaging;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;

using System.ComponentModel;
using System.Threading.Tasks;
using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;
//using IronBarCode;

namespace GestionPresence.Secretaire_charg_inscr
{
    public partial class etudiant_inscription : System.Web.UI.Page
    {
        MySqlConnection conn;

        //===================FONT_DEFINING======================================//
        public static iTextSharp.text.pdf.BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

        iTextSharp.text.Font font_bold_8_blue = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE);
        iTextSharp.text.Font font_bold_8_black = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        iTextSharp.text.Font font_normal_7_black = new iTextSharp.text.Font(bfTimes, 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
        iTextSharp.text.Font font_underlined_9_bold = new iTextSharp.text.Font(bfTimes, 9, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK);

        iTextSharp.text.Font font_normal_12 = FontFactory.GetFont("Lucida Sans", 12, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_11 = FontFactory.GetFont("Lucida Sans", 11, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_10 = FontFactory.GetFont("Lucida Sans", 10, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_9 = FontFactory.GetFont("Lucida Sans", 9, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_8 = FontFactory.GetFont("Lucida Sans", 8, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_7 = FontFactory.GetFont("Lucida Sans", 7, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_6 = FontFactory.GetFont("Lucida Sans", 6, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_5 = FontFactory.GetFont("Lucida Sans", 5, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font font_normal_4 = FontFactory.GetFont("Lucida Sans", 4, iTextSharp.text.Font.NORMAL);


        iTextSharp.text.Font font_bold_12 = FontFactory.GetFont("Lucida Sans", 12, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_11 = FontFactory.GetFont("Lucida Sans", 11, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_10 = FontFactory.GetFont("Lucida Sans", 10, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_9 = FontFactory.GetFont("Lucida Sans", 9, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_8 = FontFactory.GetFont("Lucida Sans", 8, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_7 = FontFactory.GetFont("Lucida Sans", 7, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_6 = FontFactory.GetFont("Lucida Sans", 6, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_5 = FontFactory.GetFont("Lucida Sans", 5, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font font_bold_4 = FontFactory.GetFont("Lucida Sans", 4, iTextSharp.text.Font.BOLD);



        iTextSharp.text.Font font_normal_underlined_10 = FontFactory.GetFont("Lucida Sans", 10, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE);
        iTextSharp.text.Font font_normal_underlined_9 = FontFactory.GetFont("Lucida Sans", 9, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE);
        iTextSharp.text.Font font_normal_underlined_8 = FontFactory.GetFont("Lucida Sans", 8, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE);

        iTextSharp.text.Font font_bold_underlined_10 = FontFactory.GetFont("Lucida Sans", 10, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE);
        iTextSharp.text.Font font_bold_underlined_9 = FontFactory.GetFont("Lucida Sans", 9, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE);
        iTextSharp.text.Font font_bold_underlined_8 = FontFactory.GetFont("Lucida Sans", 8, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE);

        public static int id_annee = -1, id_faculte = -1, id_departement = -1, id_classe = -1, id_semestre = -1, id_etudiant = -1, id_session, id_inscription=0;
        int id = 0, id_matri, duree;
        public static string valeur;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load_Annee();
                Base.charger_GridView(sender, e, "select id_inscription,num_inscription,date_inscription,annee.annee,faculte.faculte,classe.Classe,"
                +" faculte.faculte,departement.departement from etudiant_inscription inner join annee on etudiant_inscription.id_annee"
                +" =annee.id_annee INNER JOIN faculte on etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON etudiant_inscription.id_departement"
                + " =departement.id_departement INNER JOIN classe ON etudiant_inscription.id_classe = classe.id_classe ORDER BY id_inscription DESC LIMIT 20", Grid_Inscription);
            }
        }

        public string Generate_numero_inscription()
        {
            /*------------------- NUMERO D' INSCRIPTION -----------------------------------*/
            string num_inscript = "";
            conn = new MySqlConnection(Authentification.MyString);
            conn.Open();
            string sqtd = "SELECT num_inscription FROM etudiant_inscription WHERE id_etudiant=@id_etudiant AND id_session=@id_session AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
            MySqlCommand cmtd = new MySqlCommand(sqtd, conn);
            cmtd.Parameters.AddWithValue("@id_etudiant", id_etudiant);
            cmtd.Parameters.AddWithValue("@id_session", id_session);
            cmtd.Parameters.AddWithValue("@id_classe", id_classe);
            cmtd.Parameters.AddWithValue("@id_departement", id_departement);
            cmtd.Parameters.AddWithValue("@id_faculte", id_faculte);
            cmtd.Parameters.AddWithValue("@id_annee", id_annee);
            MySqlDataReader dr = cmtd.ExecuteReader();

            if (dr.Read())
            {
                num_inscript = dr.GetString(0);
                dr.Close();
                conn.Close();
                return num_inscript;
            }
            else
            {
                dr.Close();
                string req = "SELECT COUNT(*) FROM etudiant_inscription WHERE id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee AND id_session=@id_session";
                MySqlCommand cmd = new MySqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@id_session", id_session);
                cmd.Parameters.AddWithValue("@id_classe", id_classe);
                cmd.Parameters.AddWithValue("@id_departement", id_departement);
                cmd.Parameters.AddWithValue("@id_faculte", id_faculte);
                cmd.Parameters.AddWithValue("@id_annee", id_annee);
                int resu = Convert.ToInt32(cmd.ExecuteScalar());
                num_inscript = id_annee + "/" + id_faculte + "/" + id_departement + "/" + id_classe + "/" + (resu + 1);
                conn.Close();
                return num_inscript;
            }

            /*------------------------------------------------------*/

        }

        public void Generate_Codebar()
        {
            string barCode = Generate_numero_inscription();
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    System.Drawing.Font oFont = new System.Drawing.Font("IDAutomationHC39M", 24);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(System.Drawing.Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(System.Drawing.Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    ViewState["ImageBase64"] = Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                  
                }
                PlaceHolder1.Controls.Add(imgBarCode);
            }
        }


        protected void ExportToPDF()
        {
            //===============================EXPORT TO PDF=================================================

            if (ViewState["ImageBase64"] != null)
            {
                try
                {
                DateTime dt = DateTime.Now;
                int year = dt.Year;
                int mois = dt.Month;
                int jour = dt.Day;
                int heure = dt.Hour;
                int min = dt.Minute;
                int sec = dt.Month;
                string date = jour + "-" + mois + "-" + year + "_à_" + heure + "h" + min + "min" + sec + "sec";

                string base64 = (string)ViewState["ImageBase64"];
                byte[] imagebytes = Convert.FromBase64String(base64);
                iTextSharp.text.Image image_barcode = iTextSharp.text.Image.GetInstance(imagebytes);
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {

                    string dossier = "Carte_etudiant";
                    string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dossier);
                    if (!System.IO.Directory.Exists(path))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        catch (IOException ie)
                        {
                            Response.Write("<script>alert('Echec de creation d un dossier')</script>");
                            return;
                        }
                        catch (Exception e)
                        {
                            Response.Write("<script>alert('Echec de creation d un dossier')</script>");
                            return;
                        }
                    }






                    

                    MySqlConnection con = new MySqlConnection(Authentification.MyString);
                    con.Open();

                    iTextSharp.text.Document doc= new iTextSharp.text.Document(PageSize.A4, 88f, 88f, 10f, 10f);
                    string docname = "carte_" + date + ".pdf";
                    string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Carte_etudiant", docname);
                    FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None);
                    PdfWriter wri = PdfWriter.GetInstance(doc, fs);
          
                    doc.Open();

                    iTextSharp.text.Paragraph txt_t = new iTextSharp.text.Paragraph(" ", font_bold_8_black);
                    txt_t.Alignment = Element.ALIGN_CENTER;
                    txt_t.SpacingBefore = 10;
                    txt_t.SpacingAfter = 10;
                    doc.Add(txt_t);

                    PdfPCell cell = new PdfPCell(new Phrase("", font_bold_8));
                    PdfPTable taba = new PdfPTable(5);//Creer une table de 5 colonnes
                    float[] coltaba = { 10, 14, 16, 15,10 };//parametrer les domensions(largeur) de chacune des collonnes
                    taba.SetWidths(coltaba);


                    cell = new PdfPCell(new Phrase("", font_bold_8_black));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidth = 0;
                    cell.BorderWidthRight = 2;
                    cell.Rowspan=11;
                    taba.AddCell(cell);


                    string req0 = "select photo,nom,prenom,naissance_pays,naissance_date,annee,type,departement,classe from etudiant"
                                  + " inner join etudiant_inscription ON etudiant.id_etudiant = etudiant_inscription.id_etudiant"
                                   + " INNER JOIN annee ON etudiant_inscription.id_annee = annee.id_annee INNER JOIN faculte ON "
                                    + " etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON "
                                    + " etudiant_inscription.id_departement = departement.id_departement INNER JOIN classe on "
                                    + " etudiant_inscription.id_classe = classe.id_classe WHERE matricule = '" + valeur + "' ORDER BY etudiant.id_etudiant DESC LIMIT 1";

                    MySqlCommand cmd = new MySqlCommand(req0, con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cell = new PdfPCell(new Phrase("Nom", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        cell.BorderWidthTop = 2;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": "+dr.GetString(1), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        cell.BorderWidthTop = 2;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" CARTE D'ETUDIANT", font_bold_8_blue));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        cell.BorderWidthTop = 2;
                        cell.Rowspan = 3;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("", font_bold_8_black));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.Rowspan = 11;
                        cell.BorderWidthLeft = 2;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Prenom", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(2), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Nationalite", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(3), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Date de naissance", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(4), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(Server.MapPath(dr.GetString(0)));
                        PNG.ScaleToFit(80, 95);

                        cell = new PdfPCell(PNG);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.Rowspan = 7;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("A-A", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(5), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Faculte", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(6), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Departement", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(7), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Classe", font_bold_7));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(": " + dr.GetString(8), font_normal_8));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        image_barcode.ScaleToFit(350, 20);
                        cell = new PdfPCell(image_barcode);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BorderWidth = 0;
                        cell.Colspan = 2;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        taba.AddCell(cell);

                        cell = new PdfPCell(new Phrase(".", font_bold_8_black));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.BorderWidth = 0;
                        cell.Colspan = 3;
                        cell.BackgroundColor = BaseColor.ORANGE;
                        cell.BorderWidthBottom = 2;
                        taba.AddCell(cell);
                    }
                    con.Close();
                    doc.Add(taba);
                  
                    doc.Close();
                    System.Diagnostics.Process.Start(outputFile);
                    
                }
                }
                catch (IOException ex)
                {
                    Response.Write("<script LANGUAGE='JavaScript'>alert('Echec il existe un fichier de meme nom ouvertee !!')</script>");
                    return;
                }
            }
        }


        private void Load_Annee()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                String reqdgv = "SELECT id_annee, annee FROM annee WHERE etat_annee=@etat_annee";
                MySqlCommand cmd = new MySqlCommand(reqdgv, conn);
                cmd.Parameters.AddWithValue("@etat_annee", "Encours");
                MySqlDataReader dr = cmd.ExecuteReader();

                Annee_Combo.Items.Clear();

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez A-A";
                Annee_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string reqdgv = "SELECT faculte_par_annee.id_faculte, faculte.faculte" +
                    " FROM faculte INNER JOIN faculte_par_annee ON faculte.id_faculte = faculte_par_annee.id_faculte" +
                    " WHERE faculte_par_annee.id_annee=@idaa";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                cmt.Parameters.AddWithValue("@idaa", id_annee);
                MySqlDataReader dr = cmt.ExecuteReader();
                Faculte_Combo.Items.Clear();

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez la faculte";
                Faculte_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisissez le departement";
                Departement_Combo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisir la classe";
                ClasseCombo.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
        private void Load_Session()
        {
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();
                string reqdgv = "SELECT id_session, session  FROM session";
                MySqlCommand cmt = new MySqlCommand(reqdgv, conn);
                MySqlDataReader dr = cmt.ExecuteReader();
                Type_Incription_ComboBox.Items.Clear();

                System.Web.UI.WebControls.ListItem item0 = new System.Web.UI.WebControls.ListItem();
                item0.Value = "-1";
                item0.Text = "Choisir la session";
                Type_Incription_ComboBox.Items.Add(item0);

                while (dr.Read())
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                    item.Value = dr.GetInt32(0).ToString();
                    item.Text = dr.GetString(1);
                    Type_Incription_ComboBox.Items.Add(item);
                }
                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Echec de chargement des session')</script>");
                return;
            }
        }
        private void Load_Student()
        {
            Nom_Prenom_Label.Visible = true;
            try
            {
                conn = new MySqlConnection(Authentification.MyString);
                conn.Open();

                string reqh = " SELECT DISTINCT id_etudiant, nom,prenom FROM etudiant WHERE matricule=@matricule";
                MySqlCommand cmh = new MySqlCommand(reqh, conn);
                cmh.Parameters.AddWithValue("@matricule", Matricule_TextBox.Text);
                MySqlDataReader dkh = cmh.ExecuteReader();
                while (dkh.Read())
                {
                    id_etudiant = dkh.GetInt32(0);
                    Nom_Prenom_Label.Text = dkh.GetString(2) + " " + dkh.GetString(1);

                }
                dkh.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Etudiant introuvable !')</script>");
                return;
            }
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
            Load_Session();
        }

        protected void Type_Incription_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Enregistrer_Button_Click(object sender, ImageClickEventArgs e)
        {
            if (Annee_Combo.SelectedValue == "-1" || Faculte_Combo.SelectedValue == "-1" || Departement_Combo.SelectedValue == "-1" || ClasseCombo.SelectedValue == "-1" || Type_Incription_ComboBox.SelectedValue == "-1" || Matricule_TextBox.Text == "")
            {
                Response.Write("<script>alert('Saisissez toutes les informations necessaires pour inscriver un etudiant!')</script>");
                return;
            }
            else
            {
            //    try
            //    {
                    conn = new MySqlConnection(Authentification.MyString);
                    conn.Open();
                    string sqtd = "SELECT COUNT(*) FROM etudiant_inscription WHERE id_etudiant=@id_etudiant AND id_session=@id_session AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee";
                    MySqlCommand cmtd = new MySqlCommand(sqtd, conn);
                    cmtd.Parameters.AddWithValue("@id_etudiant", id_etudiant);
                    cmtd.Parameters.AddWithValue("@id_session", id_session);
                    cmtd.Parameters.AddWithValue("@id_classe", id_classe);
                    cmtd.Parameters.AddWithValue("@id_departement", id_departement);
                    cmtd.Parameters.AddWithValue("@id_faculte", id_faculte);
                    cmtd.Parameters.AddWithValue("@id_annee", id_annee);
                    int resultd = Convert.ToInt32(cmtd.ExecuteScalar());

                    if (resultd == 0)
                    {
                        conn.Close();
                        string num_inscript = Generate_numero_inscription();
                        conn.Open();
                        string sqte = "INSERT INTO etudiant_inscription (date_inscription,id_etudiant,id_session,id_classe,id_departement,id_faculte,id_annee,redoublement,change_filiere,num_inscription,status) VALUES (@date_inscription,@id_etudiant,@id_session,@id_classe,@id_departement,@id_faculte,@id_annee,@redoublement,@change_filiere,@num_inscription,@status)";
                        MySqlCommand cmte = new MySqlCommand(sqte, conn);
                        cmte.Parameters.AddWithValue("@date_inscription", DateTime.Today.Date.ToString("dd/MM/yyyy"));
                        cmte.Parameters.AddWithValue("@id_etudiant", id_etudiant);
                        cmte.Parameters.AddWithValue("@id_session", id_session);
                        cmte.Parameters.AddWithValue("@id_classe", id_classe);
                        cmte.Parameters.AddWithValue("@id_departement", id_departement);
                        cmte.Parameters.AddWithValue("@id_faculte", id_faculte);
                        cmte.Parameters.AddWithValue("@id_annee", id_annee);
                        cmte.Parameters.AddWithValue("@redoublement", "NON");
                        cmte.Parameters.AddWithValue("@change_filiere", "NON");
                        cmte.Parameters.AddWithValue("@num_inscription", num_inscript);
                        cmte.Parameters.AddWithValue("@status", 1);
                        cmte.ExecuteNonQuery();
                        id_inscription = Convert.ToInt32(cmte.LastInsertedId);
                        conn.Close();
                        Response.Write("<script>alert('Inscription reussie !')</script>");
                        Base.charger_GridView(sender, e, "select id_inscription,num_inscription,date_inscription,annee.annee,faculte.faculte,classe.Classe,"
                        + " faculte.faculte,departement.departement from etudiant_inscription inner join annee on etudiant_inscription.id_annee"
                        + " =annee.id_annee INNER JOIN faculte on etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON etudiant_inscription.id_departement"
                        + " =departement.id_departement INNER JOIN classe ON etudiant_inscription.id_classe = classe.id_classe ORDER BY id_inscription DESC LIMIT 20", Grid_Inscription);
                        Annee_Combo.SelectedValue = "-1";
                        Faculte_Combo.SelectedValue = "-1";
                        Departement_Combo.SelectedValue = "-1";
                        ClasseCombo.SelectedValue = "-1";
                        Type_Incription_ComboBox.SelectedValue = "-1";
                        Matricule_TextBox.Text = "";
                        Nom_Prenom_Label.Visible = false;
                        conn.Close();
                        ExportToPDF();
                        return;

                    }
                    else
                    {
                        //string sqte = "UPDATE etudiant_inscription SET status=@status WHERE id_etudiant=@id_etudiant AND id_session=@id_session AND id_classe=@id_classe AND id_departement=@id_departement AND id_faculte=@id_faculte AND id_annee=@id_annee ";
                        //MySqlCommand cmte = new MySqlCommand(sqte, conn);
                        //cmte.Parameters.AddWithValue("@id_etudiant", id_etudiant);
                        //cmte.Parameters.AddWithValue("@id_session", id_session);
                        //cmte.Parameters.AddWithValue("@id_classe", id_classe);
                        //cmte.Parameters.AddWithValue("@id_departement", id_departement);
                        //cmte.Parameters.AddWithValue("@id_faculte", id_faculte);
                        //cmte.Parameters.AddWithValue("@id_annee", id_annee);
                        //cmte.Parameters.AddWithValue("@date_inscription", DateTime.Today.Date.ToString("dd/MM/yyyy"));
                        //cmte.Parameters.AddWithValue("@status", 1);
                        //cmte.ExecuteNonQuery();
                        //id_inscription = Convert.ToInt32(cmte.LastInsertedId);
                        //Response.Write("<script>alert('modifier reussie !')</script>");
                        //Base.charger_GridView(sender, e, "select id_inscription,num_inscription,date_inscription,annee.annee,faculte.faculte,classe.Classe,"
                        //+ " faculte.faculte,departement.departement from etudiant_inscription inner join annee on etudiant_inscription.id_annee"
                        //+ " =annee.id_annee INNER JOIN faculte on etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON etudiant_inscription.id_departement"
                        //+ " =departement.id_departement INNER JOIN classe ON etudiant_inscription.id_classe = classe.id_classe ORDER BY id_inscription DESC LIMIT 20", Grid_Inscription);
                        //Annee_Combo.SelectedValue = "-1";
                        //Faculte_Combo.SelectedValue = "-1";
                        //Departement_Combo.SelectedValue = "-1";
                        //ClasseCombo.SelectedValue = "-1";
                        //Type_Incription_ComboBox.SelectedValue = "-1";
                        //Matricule_TextBox.Text = "";
                        //Nom_Prenom_Label.Visible = false;
                        //conn.Close();
                        ExportToPDF();
                    }

                //}
                //catch (Exception ex)
                //{
                //    Response.Write("<script>alert('Echec !')</script>");
                //    return;
                //}
                
                
            }

        }

        protected void search_btn_Click(object sender, ImageClickEventArgs e)
        {
            if (num_text.Text == "")
            {
                Response.Write("<script>alert('Ecrivez le numero d inscription à chercher')</script>");
                return;
            }
            else
            {
                Base.charger_GridView(sender, e, "select id_inscription,num_inscription,date_inscription,annee.annee,faculte.faculte,classe.Classe,"
                    + " faculte.faculte,departement.departement from etudiant_inscription inner join annee on etudiant_inscription.id_annee"
                    + " =annee.id_annee INNER JOIN faculte on etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON etudiant_inscription.id_departement"
                    + " =departement.id_departement INNER JOIN classe ON etudiant_inscription.id_classe = classe.id_classe where num_inscription='" + num_text.Text + "'", Grid_Inscription);
            }
        }

        protected void Grid_Inscription_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(Grid_Inscription.DataKeys[e.RowIndex].Value.ToString());
            MySqlConnection c = new MySqlConnection(Authentification.MyString);
            string requete = "delete from etudiant_inscription where id_inscription =" + id + "";
            c.Open();
            MySqlCommand data = new MySqlCommand(requete, c);
            data.ExecuteNonQuery();
            c.Close();

            Base.charger_GridView(sender, e, "select id_inscription,num_inscription,date_inscription,annee.annee,faculte.faculte,classe.Classe,"
                + " faculte.faculte,departement.departement from etudiant_inscription inner join annee on etudiant_inscription.id_annee"
                + " =annee.id_annee INNER JOIN faculte on etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON etudiant_inscription.id_departement"
                + " =departement.id_departement INNER JOIN classe ON etudiant_inscription.id_classe = classe.id_classe ORDER BY id_inscription DESC LIMIT 20", Grid_Inscription);
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            num_text.Text = "";
            Base.charger_GridView(sender, e, "select id_inscription,num_inscription,date_inscription,annee.annee,faculte.faculte,classe.Classe,"
                + " faculte.faculte,departement.departement from etudiant_inscription inner join annee on etudiant_inscription.id_annee"
                + " =annee.id_annee INNER JOIN faculte on etudiant_inscription.id_faculte = faculte.id_faculte INNER JOIN departement ON etudiant_inscription.id_departement"
                + " =departement.id_departement INNER JOIN classe ON etudiant_inscription.id_classe = classe.id_classe ORDER BY id_inscription DESC LIMIT 20", Grid_Inscription);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Annee_Combo.SelectedValue == "-1" || Faculte_Combo.SelectedValue == "-1" || Departement_Combo.SelectedValue == "-1" || ClasseCombo.SelectedValue == "-1" || Type_Incription_ComboBox.SelectedValue == "-1" || Matricule_TextBox.Text.Trim() == "")
            {
                Response.Write("<script>alert('Saisissez toutes les informations necessaires !')</script>");
                return;
            }
            else
            {
                valeur = Matricule_TextBox.Text.Trim();
                Load_Student();
                // Load_documents();
                Generate_Codebar();
            }
        }

    }
}