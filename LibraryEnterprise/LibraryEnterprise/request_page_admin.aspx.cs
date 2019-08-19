/* File: request_page_admin.aspx
 * Author: Shawn Pudjowargono
 * 
 * Purpose:
 * Web form allowing employees to search, insert, update and delete records in the requests table. 
 * Uses Requests_keeper.cs to carry out CRUD operations.
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
    public partial class request_page_admin : System.Web.UI.Page
    {
        // Used to carry out CRUD functionality with requests database
        Request_keeper request_keeper = new Request_keeper();

        string select_query = "SELECT r.request_id AS 'ID', r.isbn AS 'ISBN', r.author AS 'Author', " +
                              "r.title AS 'Title', g.genre_name AS 'Genre', r.language AS 'Language', " +
                              "r.year AS 'Year'" +
                              "FROM requests r " +
                              "JOIN genres g ON r.genre_id = g.genre_id";

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
                request_keeper.get_gridview_data(select_query, gridview_books);
            }
        }

        /*
         * Delete button on click function
         */
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
                request_keeper.delete_request(request_id);
            }
            request_keeper.get_gridview_data(select_query, gridview_books);
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