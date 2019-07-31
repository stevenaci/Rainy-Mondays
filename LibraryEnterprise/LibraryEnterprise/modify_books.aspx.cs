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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                get_gridview_data("SELECT * FROM books");
            }
        }

        /*
         * Retrieve data from any table and display in gridview_books object in HTML
         * Simple pass a SELECT query into this function and data will display
         */
        private void get_gridview_data(string query)
        {
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (DataTable data_table = new DataTable())
            {
                command.Connection = connection;
                adapter.SelectCommand = command;
                adapter.Fill(data_table);
                gridview_books.DataSource = data_table;
                gridview_books.DataBind();
                set_gridview_headers_books();
            }
        }

        /*
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
        }

        /*
         * OnClick function for search button
         */
        public void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_isbn.Text == "" && tb_author.Text == "" && tb_title.Text == "" && tb_genre.Text == "" && tb_year.Text == "")
            {
                // display all books if textboxes are empty
                get_gridview_data("SELECT * FROM books");
            }
            else
            {
                string isbn = tb_isbn.Text.ToString();
                string author = tb_author.Text.ToString();
                string title = tb_title.Text.ToString();
                string genre = tb_genre.Text.ToString();
                string year = tb_year.Text.ToString();
            }
        }
    }
}