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

        private void get_gridview_data(string query)
        {
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            {
                using (SqlCommand command = new SqlCommand(query))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        command.Connection = connection;
                        adapter.SelectCommand = command;
                        using (DataTable data_table = new DataTable())
                        {
                            adapter.Fill(data_table);
                            gridview_books.DataSource = data_table;
                            gridview_books.DataBind();
                            set_header_names();
                        }
                    }
                }
            }
        }

        private void set_header_names()
        {
            gridview_books.HeaderRow.Cells[0].Text = "Book ID";
            gridview_books.HeaderRow.Cells[1].Text = "ISBN";
            gridview_books.HeaderRow.Cells[2].Text = "Author";
            gridview_books.HeaderRow.Cells[3].Text = "Book Title";
            gridview_books.HeaderRow.Cells[4].Text = "Genre";
            gridview_books.HeaderRow.Cells[5].Text = "Language";
            gridview_books.HeaderRow.Cells[6].Text = "Year";
        }

        public void btn_search_Click(object sender, EventArgs e)
        {
            if (!(tb_isbn.Text == "" || tb_author.Text == "" || tb_title.Text == "" || tb_genre.Text == "" || tb_year.Text == ""))
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