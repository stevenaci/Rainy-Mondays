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
    public partial class books_database_employees : System.Web.UI.Page
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
                book_keeper.get_gridview_data(select_query, gridview_books);
                book_keeper.get_genre_data(dd_genre);
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
        * Add Button onclick
        */
        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" ||
                tb_language3.Text == "" || tb_year3.Text == "" || tb_quantity3.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {

                string isbn = tb_isbm3.Text.ToString().Trim();
                string author = tb_author3.Text.ToString().Trim();
                string title = tb_title3.Text.ToString().Trim();
                string genre = dd_genre.Text.ToString().Trim();
                string language = tb_language3.Text.ToString().Trim();
                int year = Convert.ToInt32(tb_year3.Text.ToString().Trim());
                int quantity = Convert.ToInt32(tb_quantity3.Text.ToString().Trim());

                int genre_id = get_genre_id(genre);
                if (genre_id >= 0)
                {
                    book_keeper.add_book(isbn, author, title, genre_id, language, year, quantity);
                }
                else
                {
                    System.Diagnostics.Debug.Write("Genre not found");
                }
            }
            book_keeper.get_gridview_data(select_query, gridview_books);
        }

        /*
         * Update Button onclick
         */
        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (tb_modify_id.Text == "" || tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" ||
                            tb_language3.Text == "" || tb_year3.Text == "" || tb_quantity3.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {
                int book_id = Convert.ToInt32(tb_modify_id.Text.ToString().Trim());
                string isbn = tb_isbm3.Text.ToString().Trim();
                string author = tb_author3.Text.ToString().Trim();
                string title = tb_title3.Text.ToString().Trim();
                string genre = dd_genre.Text.ToString().Trim();
                string language = tb_language3.Text.ToString().Trim();
                int year = Convert.ToInt32(tb_year3.Text.ToString().Trim());
                int quantity = Convert.ToInt32(tb_quantity3.Text.ToString().Trim());
                int genre_id = get_genre_id(genre);
                //bool duplicate_isbn = check_duplicate_isbn(isbn);
                //bool same_isbm = check_same_isbm(book_id, isbn);

                book_keeper.update_book(book_id, isbn, author, title, genre_id, language, year, quantity);
            }
            book_keeper.get_gridview_data(select_query, gridview_books);
        }

        /*
        * Delete Button onclick
        */
        protected void btn_delete_Click(object sender, EventArgs e)
        {

            if (tb_delete_id.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {
                int book_id = Convert.ToInt32(tb_delete_id.Text.ToString().Trim());
                book_keeper.delete_book(book_id);
            }
            book_keeper.get_gridview_data(select_query, gridview_books);
        }

        /*
        * Used to convert string genre_name into int genre_id for updating and inserting
        * to books table
        */
        protected int get_genre_id(string genre_name)
        {
            int genre_id = -1;
            try
            {
                string query = "SELECT genre_id FROM genres WHERE genre_name = @genre_name;";
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
         * Used to convert int genre_id into string genre_name for fill in text fields
         * for updating book
         */
        protected string get_genre_name(int genre_id)
        {
            string genre_name = "Test";
            try
            {
                string query = "SELECT genre_name FROM genres WHERE genre_id = @genre_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@genre_id", genre_id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        genre_name = reader[0].ToString();
                    }
                    reader.Close(); // Close reader
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
            return genre_name;
        }

        /*
         * Fills update textbox fields with data pulled based on book_id
         */
        protected void btn_pull_info_Click(object sender, EventArgs e)
        {
            if (tb_modify_id.Text != "")
            {
                int book_id = Convert.ToInt32(tb_modify_id.Text.ToString());
                pull_book_info(book_id);
            }
        }

        /*
         * Pull info functionality
         */
        protected void pull_book_info(int book_id)
        {
            try
            {
                string query = "SELECT * FROM books WHERE book_id = @book_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@book_id", book_id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tb_isbm3.Text = reader[1].ToString();
                            tb_author3.Text = reader[2].ToString();
                            tb_title3.Text = reader[3].ToString();
                            dd_genre.ClearSelection();
                            dd_genre.SelectedValue = get_genre_name(Convert.ToInt32(reader[4]));
                            tb_language3.Text = reader[5].ToString();
                            tb_year3.Text = reader[6].ToString();
                            tb_quantity3.Text = reader[7].ToString();

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