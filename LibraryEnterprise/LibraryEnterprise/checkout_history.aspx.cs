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
    public partial class checkout_history : System.Web.UI.Page
    {
        // The default select query for retrieving checkout data
        string select_query = "SELECT p.email AS 'Patron Email', b.isbn AS 'ISBN', " +
                              "c.date_out AS 'Date Out', c.date_in AS 'Date In' " +
                              "FROM checkouts c " +
                              "JOIN patrons p ON c.patron_id = p.patron_id " +
                              "JOIN books b ON c.book_id = b.book_id ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["employee_id"] == null)
            {
                if (Session["patron_id"] != null)
                {
                    redirect_to_error_page("403 ERROR", "Employee access only.", "patronhomepage.aspx");
                }
                else
                {
                    redirect_to_error_page("403 ERROR", "Employee access only.", "loginpage.aspx");
                }
            }


            if (!IsPostBack)
            {
                get_gridview_data(select_query);
            }
        }

        /*
         * Retrieve data from any table and display in gridview object in HTML
         * Simple pass a SELECT query into this function and data will display
         */
        private void get_gridview_data(string query)
        {
            try
            { 
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                using (DataTable data_table = new DataTable())
                {
                    data_table.PrimaryKey = new DataColumn[] { data_table.Columns["patron_id"] };
                    adapter.Fill(data_table);
                    gridview_books.DataSource = data_table;
                    gridview_books.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
         * OnClick function for search button
         */
        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_email.Text == "" && tb_isbn.Text == "")
            {
                // display all books if textboxes are empty
                get_gridview_data(select_query);
            }
            else
            {
                string email = tb_email.Text.ToString().Trim();
                string isbn = tb_isbn.Text.ToString().Trim();
                select_query += " WHERE ";

                bool multiple_conditions = false;

                multiple_conditions = add_where_conditions("email", email, multiple_conditions);
                multiple_conditions = add_where_conditions("isbn", isbn, multiple_conditions);

                get_gridview_data(select_query);
            }
        }

        /*
         * Adds a sequence of where conditions to a SELECT query
         * After the first condition is added, each subsequent will be preceeded by 'AND'
         */
        protected bool add_where_conditions(string column, string value, bool multiple_conditions)
        {
            if (value != "")
            {
                if (multiple_conditions)
                {
                    select_query += " AND ";
                }
                select_query += column + " LIKE '%" + value + "%' ";
                multiple_conditions = true;
            }
            return multiple_conditions;
        }

        /*
         * Redirect function
         */
        protected void redirect_to_error_page(string error_title, string error_message, string redirect_URL)
        {
            Session["error_title"] = error_title;
            Session["error_message"] = error_message;
            Session["redirect_URL"] = redirect_URL;
            Server.Transfer("error_page.aspx");
        }
    }
}