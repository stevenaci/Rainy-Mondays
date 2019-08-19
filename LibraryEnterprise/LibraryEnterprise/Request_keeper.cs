/* File: Request_keeper.cs
 * Author: Shawn Pudjowargono
 * 
 * Purpose:
 * Carries out the business logic for CRUD operations for the requests table
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

    /*
     * Class to handle business logic for SELECT, INSERT and DELETE functionality
     * for requests table
     */
    public class Request_keeper : System.Web.UI.Page
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
         * Add Record to Requests Table (INSERT)
         */
        public void add_request(int patron_id, string isbm, string author, string title,
                                   int genre_id, string language, int year)
        {
            try
            {
                string query = "INSERT INTO requests VALUES" +
                                "(@patron_id, @isbm, @author, @title, @genre_id, @language, @year);";

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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
         * Delete a record from request table (DELETE)
         */
        public void delete_request(int request_id)
        {
            try
            {
                string query = "DELETE FROM requests WHERE request_id = @request_id;";
                System.Diagnostics.Debug.Write(query);

                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@request_id", request_id);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }
    }
}