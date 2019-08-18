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
    public partial class request_page_admin : System.Web.UI.Page
    {

        string select_query = "SELECT r.request_id AS 'ID', r.isbn AS 'ISBN', r.author AS 'Author', " +
                              "r.title AS 'Title', g.genre_name AS 'Genre', r.language AS 'Language', " +
                              "r.year AS 'Year'" +
                              "FROM requests r " +
                              "JOIN genres g ON r.genre_id = g.genre_id";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["employee_id"] == null)
            {
                redirect_to_error_page("403 ERROR", "Please log in to an employee account to continue.", "loginpage.aspx");
            }

            if (!IsPostBack)
            {
                get_gridview_data(select_query);
            }
        }

        /*
* Shawn:
* Retrieve data from any table and display in gridview_books object in HTML
* Simple pass a SELECT query into this function and data will display
*/
        private void get_gridview_data(string query)
        {
            System.Diagnostics.Debug.Write(query);
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            using (DataTable data_table = new DataTable())
            {
                data_table.PrimaryKey = new DataColumn[] { data_table.Columns["genre_id"] };
                adapter.Fill(data_table);
                gridview_books.DataSource = data_table;
                gridview_books.DataBind();
            }
        }


        protected void btn_delete_Click(object sender, EventArgs e)
        {
            if (tb_request_id.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {
                int request_id = Convert.ToInt32(tb_request_id.Text.ToString().Trim());
                delete_request(request_id);
            }
        }

        protected void delete_request(int request_id)
        {
            string query = "DELETE FROM requests WHERE request_id = @request_id;";
            System.Diagnostics.Debug.Write(query);

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (DataTable data_table = new DataTable())
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@request_id", request_id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            get_gridview_data(select_query);
        }

        protected void redirect_to_error_page(string error_title, string error_message, string redirect_URL)
        {
            Session["error_title"] = error_title;
            Session["error_message"] = error_message;
            Session["redirect_URL"] = redirect_URL;
            Server.Transfer("error_page.aspx");
        }
    }
}