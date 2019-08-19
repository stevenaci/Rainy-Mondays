/* File: patronhomepage.aspx
 * Author: Steven Carino
 * 
 * Purpose:
 * Web form that acts as the homepage for patrons, displaying their checkout history as well as
 * allowing them to search for books or add book requests (links to other pages).
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
    public partial class patronhomepage : System.Web.UI.Page
    {
        // Used to carry out CRUD functionality with books table
        Checkout_keeper checkout_keeper = new Checkout_keeper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["patron_id"] == null)
            {
                if (Session["employee_id"] != null)
                {
                    redirect_to_error_page("403 ERROR", "Please log in to an patron account to continue.", "homepage.aspx");
                }
                else
                {
                    redirect_to_error_page("403 ERROR", "Please log in to an patron account to continue.", "patronloginpage.aspx");
                }
            }

            lbl1.Text = "Welcome: " + Session["patron_name"];
            int p_id = Convert.ToInt32(Session["patron_id"]);

            // load all checked out books
            string query = "select isbn, title, author" +
                            " from dbo.books b" +
                            " inner join" +
                            " dbo.checkouts c" +
                            " on b.book_id = c.book_id" +
                            " where c.date_in IS NULL" +
                            " and c.patron_id = @p_id;";

            checkout_keeper.set_grid_view(query, p_id, gv1);

            // load all overdue books
            query = "select isbn, title, author" +
                " from dbo.books b" +
                " inner join" +
                " dbo.checkouts c" +
                " on b.book_id = c.book_id" +
                " where c.date_in IS NULL" +
                " and DATEDIFF(day, c.date_out, CURRENT_TIMESTAMP) > 14" +
                " and c.patron_id = @p_id;";

            checkout_keeper.set_grid_view(query, p_id, gv2);

            lblbal.Text = Session["balance"].ToString();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // 
            Response.Redirect("modify_books.aspx");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
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