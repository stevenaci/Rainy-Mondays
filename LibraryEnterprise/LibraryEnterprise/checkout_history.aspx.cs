/* File: checkout_history.aspx
 * Author: Shawn Pudjowargono
 * 
 * Purpose:
 * Web form allowing employees to search records in the checkouts table.
 * Uses Checkout_keeper.cs to carry out CRUD operations.
 */

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
        // Used to carry out CRUD functionality with checkouts table
        Checkout_keeper checkout_keeper = new Checkout_keeper();

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
                checkout_keeper.get_gridview_data(select_query, gridview_books);
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
                checkout_keeper.get_gridview_data(select_query, gridview_books);
            }
            else
            {
                string email = tb_email.Text.ToString().Trim();
                string isbn = tb_isbn.Text.ToString().Trim();
                select_query += " WHERE ";

                bool multiple_conditions = false;

                multiple_conditions = add_where_conditions("email", email, multiple_conditions);
                multiple_conditions = add_where_conditions("isbn", isbn, multiple_conditions);

                checkout_keeper.get_gridview_data(select_query, gridview_books);
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