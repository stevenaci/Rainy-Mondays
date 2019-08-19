using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LibraryEnterprise
{
    /*
     * Class to handle business logic for SELECT, INSERT, UPDATE and DELETE functionality
     * for checkout table
     */
    public class Checkout_keeper : System.Web.UI.Page
    {
        public void set_grid_view(string query, int patron_id, GridView gridview)
        {
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@p_id", patron_id);
                SqlDataReader rdr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);

                gridview.DataSource = dt;
                gridview.DataBind();
            }
        }
    }
}