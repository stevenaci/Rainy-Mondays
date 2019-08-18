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
    public partial class request_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["patron_id"] == null)
            {
                redirect_to_error_page("403 ERROR", "Please log in to an patron account to continue.", "loginpage.aspx");
            }

            if (!IsPostBack)
            {
                get_genre_data();
            }
            System.Diagnostics.Debug.Write("HELLLLOOOOO\n");
            System.Diagnostics.Debug.Write(Session["patron_id"].ToString());
        }

        /*
         * Sets data for genre dropdown
         *
         */
        private void get_genre_data()
        {
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand("SELECT genre_name FROM genres;"))
            {
                connection.Open();
                command.Connection = connection;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dd_genre.Items.Add(reader[0].ToString());
                    }
                }
                connection.Close();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_isbn.Text == "" || tb_author.Text == "" || tb_title.Text == "" ||
                tb_language.Text == "" || tb_year.Text == "" )
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {
                
                int patron_id = Convert.ToInt32(Session["patron_id"].ToString());
                string isbn = tb_isbn.Text.ToString().Trim();
                string author = tb_author.Text.ToString().Trim();
                string title = tb_title.Text.ToString().Trim();
                string genre = dd_genre.Text.ToString().Trim();
                string language = tb_language.Text.ToString().Trim();
                int year = Convert.ToInt32(tb_year.Text.ToString().Trim());

                int genre_id = get_genre_id(genre);

                add_request(patron_id, isbn, author, title, genre_id, language, year);
            }

            redirect_to_error_page("SUCCESS", "You have successfully requested a book. Click below to return.", "request_page.aspx");
        }

        /*
         * Shawn:
         * Add Record to Requests Table. Before adding, calls get_genre_id to get the genre_id associated with
         * the genre_name before adding to table
         */
        protected void add_request(int patron_id, string isbm, string author, string title, 
                                   int genre_id, string language, int year)
        {
            string query = "INSERT INTO requests VALUES" +
                            "(@patron_id, @isbm, @author, @title, @genre_id, @language, @year);";
            System.Diagnostics.Debug.Write(query);

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            using (DataTable data_table = new DataTable())
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@patron_id", patron_id);
                command.Parameters.AddWithValue("@isbm", isbm);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@genre_id", genre_id);
                command.Parameters.AddWithValue("@language", language);
                command.Parameters.AddWithValue("@year", year);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /*
         * Shawn:
         * Used to convert string genre_name into int genre_id
         * to books table
         */
        protected int get_genre_id(string genre_name)
        {
            int genre_id = -1;
            //System.Diagnostics.Debug.Write(genre_name);
            string query = "SELECT genre_id FROM genres WHERE genre_name = @genre_name;";
            System.Diagnostics.Debug.Write(query);

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@genre_name", genre_name);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genre_id = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close(); // Close reader
                connection.Close();
            }
            return genre_id;
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