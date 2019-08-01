﻿using System;
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

        /*
        * Shawn:
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
                delete_book(book_id);
            }
        }

        /*
        * Shawn:
        * Update Button onclick
        * 
        * TODO:
        * Updating with the same isbn will throw an error. Must check if the ISBN is unchanged!
        */
        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (tb_modify_id.Text == "" || tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" || tb_genre3.Text == "" ||
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
                string genre = tb_genre3.Text.ToString().Trim();
                string language = tb_language3.Text.ToString().Trim();
                int year = Convert.ToInt32(tb_year3.Text.ToString().Trim());
                int quantity = Convert.ToInt32(tb_quantity3.Text.ToString().Trim());

                int genre_id = get_genre_id(genre);
                bool duplicate_isbn = check_duplicate_isbn(isbn);
                if (genre_id >= 0 && !duplicate_isbn)
                {
                    update_book(book_id, isbn, author, title, genre_id, language, year, quantity);
                }
                else
                {
                    System.Diagnostics.Debug.Write("Genre not found");
                }


            }
        }

        /*
        * Shawn:
        * Add Button onclick
        */
        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" || tb_genre3.Text == "" ||
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
                string genre = tb_genre3.Text.ToString().Trim();
                string language = tb_language3.Text.ToString().Trim();
                int year = Convert.ToInt32(tb_year3.Text.ToString().Trim());
                int quantity = Convert.ToInt32(tb_quantity3.Text.ToString().Trim());

                int genre_id = get_genre_id(genre);
                bool duplicate_isbn = check_duplicate_isbn(isbn);
                if (genre_id >= 0 && !duplicate_isbn)
                {
                    add_book(isbn, author, title, genre_id, language, year, quantity);
                }
                else
                {

                    System.Diagnostics.Debug.Write("Genre not found");

                }


            }
        }

        /*
        * Shawn:
        * Add Record to Book Table. Before adding, calls get_genre_id to get the genre_id associated with
        * the genre_name before adding to table
        * 
        * TODO:
        *  Check for duplicate ISBMs
        */
        protected void add_book(string isbm, string author, string title, int genre_id, string language,
                                int year, int quantity)
        {

            string query = "INSERT INTO books VALUES" +
                            "(@isbm, @author, @title, @genre_id, @language, @year, @quantity);";
            System.Diagnostics.Debug.Write(query);

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
            get_gridview_data(select_query);
        }


        /*
        * Shawn:
        * Update to Book Table. Before updating, calls get_genre_id to get the genre_id associated with
        * the genre_name before adding to table
        * 
        * TODO:
        *  Check for duplicate ISBMs
        */
        protected void update_book(int book_id, string isbn, string author, string title, int genre_id, string language,
                        int year, int quantity)
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
            System.Diagnostics.Debug.Write(query);

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
            get_gridview_data(select_query);
        }

        /*
         * Deletes book record from books table based on book_id
         */
        protected void delete_book(int book_id)
        {
            string query = "DELETE FROM books WHERE book_id = @book_id;";
            System.Diagnostics.Debug.Write(query);

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
            get_gridview_data(select_query);
        }

        /*
        * Shawn:
        * Used to convert string genre_name into int genre_id for updating and inserting
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

        /*
         * Shawn:
         * Checks for duplicate ISBN when adding or updating
         * 
         */
        protected bool check_duplicate_isbn(string isbn)
        {
            bool duplicate_found = false;
            //System.Diagnostics.Debug.Write(genre_name);
            string query = "SELECT isbn FROM books WHERE isbn = @isbn;";
            System.Diagnostics.Debug.Write(query);

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@isbn", isbn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    System.Diagnostics.Debug.Write("DUPLICATE FOUND!");
                    duplicate_found = true;
                }
                reader.Close(); // Close reader
                connection.Close();
            }
            return duplicate_found;
        }

        /*
         * Shawn:
         * For updating, checks if the ISBN is the same as before
         */
        protected bool check_same_isbn(string isbn)
        {
            bool isbm = false;
            return isbm;
        }
    }
}



 
 
 