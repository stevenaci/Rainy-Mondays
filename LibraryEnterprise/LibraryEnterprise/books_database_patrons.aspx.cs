/* File: books_database_patrons.aspx
 * Author: Shawn Pudjowargono
 * 
 * Purpose:
 * Web form allowing patrons to search records in the books table. 
 * Uses Book_keeper.cs to carry out CRUD operations.
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
    public partial class books_database_patrons : System.Web.UI.Page
    {

        // Used to carry out CRUD functionality with books table
        Book_keeper book_keeper = new Book_keeper();

        // Select query for data grid view
        string select_query = "SELECT b.book_id AS 'ID', b.isbn AS 'ISBN', b.author AS 'Author', " +
                              "b.title AS 'Title', g.genre_name AS 'Genre', b.language AS 'Language', " +
                              "b.year AS 'Year', b.quantity AS 'Quantity' " +
                              "FROM books b " +
                              "JOIN genres g ON b.genre_id = g.genre_id";

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

            if (!IsPostBack)
            {
                book_keeper.get_gridview_data(select_query, gridview_books);
            }
        }

        /*
         * OnClick function for search button
         */
        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_isbn.Text == "" && tb_author.Text == "" && tb_title.Text == "" && tb_year.Text == "")
            {
                // display all books if textboxes are empty
                book_keeper.get_gridview_data(select_query, gridview_books);
            }
            else
            {
                string isbn = tb_isbn.Text.ToString().Trim();
                string author = tb_author.Text.ToString().Trim();
                string title = tb_title.Text.ToString().Trim();
                string year = tb_year.Text.ToString().Trim();
                select_query += " WHERE ";

                bool multiple_conditions = false;

                multiple_conditions = add_where_conditions("isbn", isbn, multiple_conditions);
                multiple_conditions = add_where_conditions("author", author, multiple_conditions);
                multiple_conditions = add_where_conditions("title", title, multiple_conditions);
                multiple_conditions = add_where_conditions("year", year, multiple_conditions);

                book_keeper.get_gridview_data(select_query, gridview_books);
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