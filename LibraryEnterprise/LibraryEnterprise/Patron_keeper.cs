/* File: Patron_keeper.cs
 * Author: Scott Ritchie
 * 
 * Purpose:
 * Carries out the business logic for CRUD operations for the employees table
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
     * Class to handle business logic for SELECT, INSERT, UPDATE and DELETE functionality
     * for patrons table
     */
    public class Patron_keeper : System.Web.UI.Page
    {

        /*
         * Fills data grid view with data from query (SELECT)
         */
        public void get_gridview_data(string query, GridView gridview)
        {
            try
            {
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                using (DataTable data_table = new DataTable())
                {
                    data_table.PrimaryKey = new DataColumn[] { data_table.Columns["patrons_id"] };
                    adapter.Fill(data_table);
                    gridview.DataSource = data_table;
                    gridview.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }


        /*
         * Add Record to Patron Table (INSERT)
         */
        public void add_patron(string first_name, string last_name, string email, 
                               string password, string phone, string address,
                               double account_balance)
        {
            try
            {
                string query = "INSERT INTO patrons VALUES" +
                                "(@first_name, @last_name, @email, @password, @phone, @address, @account_balance);";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@account_balance", account_balance);
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
        * Update to Patron Table (UPDATE)
        */
        public void update_patron(int patron_id, string first_name, string last_name, 
                                  string email, string password, string phone, 
                                  string address, double account_balance)
        {
            try
            {
                string query = "UPDATE patrons SET " +
                                "first_name = @first_name, " +
                                "last_name = @last_name, " +
                                "email = @email, " +
                                "password = @password, " +
                                "phone = @phone, " +
                                "address = @address, " +
                                "account_balance = @account_balance " +
                                "WHERE patron_id = @patron_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@patron_id", patron_id);
                    command.Parameters.AddWithValue("@first_name", first_name);
                    command.Parameters.AddWithValue("@last_name", last_name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@account_balance", account_balance);
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
         * Deletes patron record from patron table based on patron_id (DELETE)
         */
        public void delete_patron(int patron_id)
        {
            try
            {
                string query = "DELETE FROM patrons WHERE patron_id = @patron_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                using (DataTable data_table = new DataTable())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@patron_id", patron_id);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                redirect_to_error_page("ERROR", "Cannot delete this record as this patron currently has something checked out.",
                    "display_patrons.aspx");
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