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
    public partial class modify_books : System.Web.UI.Page
    {
        string select_query = "SELECT b.book_id AS 'Book ID', b.isbn AS 'ISBN', b.author AS 'Author', " +
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
         * 
         */
        private void get_gridview_data(string query)
        {
            System.Diagnostics.Debug.Write(query);
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (DataTable data_table = new DataTable())
            {
                command.Connection = connection;
                adapter.SelectCommand = command;
                data_table.PrimaryKey = new DataColumn[] { data_table.Columns["genre_id"] };
                adapter.Fill(data_table);
                
                gridview_books.DataSource = data_table;
                gridview_books.DataBind();
            }
        }

        /*
         * Shawn:
         * Changes headers of table for Book table
         */
        private void set_gridview_headers_books()
        {
            gridview_books.HeaderRow.Cells[0].Text = "Book ID";
            gridview_books.HeaderRow.Cells[1].Text = "ISBN";
            gridview_books.HeaderRow.Cells[2].Text = "Author";
            gridview_books.HeaderRow.Cells[3].Text = "Book Title";
            gridview_books.HeaderRow.Cells[4].Text = "Genre";
            gridview_books.HeaderRow.Cells[5].Text = "Language";
            gridview_books.HeaderRow.Cells[6].Text = "Year";
            gridview_books.HeaderRow.Cells[7].Text = "Quantity";
        }

        /*
         * Shawn:
         * OnClick function for search button
         */
        public void btn_search_Click(object sender, EventArgs e)
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

                if (isbn != "")
                {
                    if (multiple_conditions)
                    {
                        select_query += " AND ";
                    }
                    select_query += "isbn LIKE '%" + isbn + "%' ";
                    multiple_conditions = true;
                }

                if (author != "")
                {
                    if (multiple_conditions)
                    {
                        select_query += " AND ";
                    }
                    select_query += "author LIKE '%" + author + "%' ";
                    multiple_conditions = true;
                }

                if (title != "")
                {
                    if (multiple_conditions)
                    {
                        select_query += " AND ";
                    }
                    select_query += "title LIKE '%" + title + "%' ";
                    multiple_conditions = true;
                }

                if (year != "")
                {
                    if (multiple_conditions)
                    {
                        select_query += " AND ";
                    }
                    select_query += "year = " + year + " ";
                    multiple_conditions = true;
                }

                get_gridview_data(select_query);
            }
        }

        //public void add_to_where(string variable)
    }
}


 
 
 