using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace GestionPresence
{
    public class Base
    {
        public static void charger_GridView(object sender, EventArgs e, string requete, GridView table)
        {
            MySqlConnection c = new MySqlConnection(Authentification.MyString);
            string requeste = requete;
            c.Open();
            MySqlCommand data = new MySqlCommand(requeste, c);
            MySqlDataReader r = data.ExecuteReader();
            table.DataSource = r;
            table.DataBind();
            c.Close();
        }
    }
}