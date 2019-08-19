/* File: request_page.aspx
 * Author: Shawn Pudjowargono
 * 
 * Purpose:
 * Web form allowing patrons to insert records in the requests table. 
 * Uses Request_keeper.cs to carry out CRUD operations.
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
    public partial class request_page : System.Web.UI.Page
    {
        // Used to carry out CRUD functionality with requests database
        Request_keeper request_keeper = new Request_keeper();

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
                request_keeper.get_genre_data(dd_genre);
            }
        }

        /*
         * Add button click function
         */
        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_isbn.Text == "" || tb_author.Text == "" || tb_title.Text == "" ||
                tb_language.Text == "" || tb_year.Text == "")
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

                request_keeper.add_request(patron_id, isbn, author, title, genre_id, language, year);
            }

            redirect_to_error_page("SUCCESS", "You have successfully requested a book. Click below to return.", "request_page.aspx");
        }

        /*
         * Used to convert string genre_name into int genre_id
         */
        protected int get_genre_id(string genre_name)
        {
            int genre_id = -1;
            try
            {
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
            return genre_id;
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