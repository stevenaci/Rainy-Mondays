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
    /*
     * Class to handle business logic for SELECT, INSERT, UPDATE and DELETE functionality
     * for books table
     */
    public class Book_keeper : System.Web.UI.Page
    {

        /*
         * Fills data grid view with data from query (SELECT)
         */
        public void get_gridview_data(string query, GridView gridview_books)
        {
            try
            {
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
         * Sets data for genre dropdown (SELECT)
         */
        public void get_genre_data(DropDownList dd_genre)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
        * Add record to Book Table (INSERT)
        */
        public void add_book(string isbm, string author, string title, int genre_id, string language,
                                int year, int quantity)
        {
            try
            {
                string query = "INSERT INTO books VALUES" +
                                "(@isbm, @author, @title, @genre_id, @language, @year, @quantity);";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@isbm", isbm);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@genre_id", genre_id);
                    command.Parameters.AddWithValue("@language", language);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
        * Update to Book Table. Before updating, calls get_genre_id to get the genre_id associated with
        * the genre_name before adding to table (UPDATE)
        */
        public void update_book(int book_id, string isbn, string author, string title, int genre_id, string language,
                        int year, int quantity)
        {
            try
            {
                string query = "UPDATE books SET " +
                                "isbn = @isbn, " +
                                "author = @author, " +
                                "title = @title, " +
                                "genre_id = @genre_id, " +
                                "language = @language, " +
                                "year = @year, " +
                                "quantity = @quantity " +
                                "WHERE book_id = @book_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@isbn", isbn);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@genre_id", genre_id);
                    command.Parameters.AddWithValue("@language", language);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@book_id", book_id);
                    command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
         * Deletes book record from books table based on book_id (DELETE)
         */
        public void delete_book(int book_id)
        {
            try
            {
                string query = "DELETE FROM books WHERE book_id = @book_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@book_id", book_id);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                redirect_to_error_page("ERROR", "Cannot delete this record as this book is currently checked out.",
                    "books_database_employees.aspx");
            }
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