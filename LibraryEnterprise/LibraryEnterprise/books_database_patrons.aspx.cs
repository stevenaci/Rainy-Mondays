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
        // The default select query for retrieving book data
        // Searching will concatenate a WHERE condition at the end
        string select_query = "SELECT b.book_id AS 'ID', b.isbn AS 'ISBN', b.author AS 'Author', " +
                              "b.title AS 'Title', g.genre_name AS 'Genre', b.language AS 'Language', " +
                              "b.year AS 'Year', b.quantity AS 'Quantity' " +
                              "FROM books b " +
                              "JOIN genres g ON b.genre_id = g.genre_id";

        protected void Page_Load(object sender, EventArgs e)
        {
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

        /*
         * Shawn:
         * OnClick function for search button
         */
        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_isbn.Text == "" && tb_author.Text == "" && tb_title.Text == "" && tb_year.Text == "")
            {
                // display all books if textboxes are empty
                get_gridview_data(select_query);
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

                get_gridview_data(select_query);
            }
        }

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
    }
}