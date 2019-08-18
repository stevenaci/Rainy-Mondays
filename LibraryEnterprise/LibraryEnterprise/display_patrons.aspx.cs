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
    public partial class display_patrons : System.Web.UI.Page
    {

        string select_query = "SELECT * from patrons";

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
                get_gridview_data(select_query);

            }
        }

        /*
         * Retrieve data from any table and display in gridview object in HTML
         * Simple pass a SELECT query into this function and data will display
         */
        private void get_gridview_data(string query)
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
         * OnClick function for search button
         */
        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_fn.Text == "" && tb_ln.Text == "" && tb_email.Text == "" && tb_phone.Text == "" && tb_address.Text == "" && tb_balance.Text == "" && TextBox1.Text == "")
            {
                // display all patrons if textboxes are empty
                get_gridview_data(select_query);
            }
            else
            {
                string fn = tb_fn.Text.ToString().Trim();
                string ln = tb_ln.Text.ToString().Trim();
                string email = tb_email.Text.ToString().Trim();
                string password = TextBox1.Text.ToString().Trim();
                string phone = tb_phone.Text.ToString().Trim();
                string address = tb_address.Text.ToString().Trim();
                string balance = tb_balance.Text.ToString().Trim();
                select_query += " WHERE ";

                bool multiple_conditions = false;

                multiple_conditions = add_where_conditions("first_name", fn, multiple_conditions);
                multiple_conditions = add_where_conditions("last_name", ln, multiple_conditions);
                multiple_conditions = add_where_conditions("email", email, multiple_conditions);
                multiple_conditions = add_where_conditions("password", password, multiple_conditions);
                multiple_conditions = add_where_conditions("phone", phone, multiple_conditions);
                multiple_conditions = add_where_conditions("address", address, multiple_conditions);
                multiple_conditions = add_where_conditions("account_balance", balance, multiple_conditions);

                get_gridview_data(select_query);
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
                int patron_id = Convert.ToInt32(tb_delete_id.Text.ToString().Trim());
                delete_patron(patron_id);
            }
        }

        /*
         * Update Button onclick
         * 
         */
        protected void btn_update_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.Write("!!!!!!!!!!!!!!!!!!!!!");
            if (tb_modify_id.Text == "" || tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" ||
                                tb_language3.Text == "" || tb_year3.Text == "" || TextBox2.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {
                int patron_id = Convert.ToInt32(tb_modify_id.Text.ToString().Trim());
                string first_name = tb_isbm3.Text.ToString().Trim();
                string last_name = tb_author3.Text.ToString().Trim();
                string email = tb_title3.Text.ToString().Trim();
                string password = TextBox2.Text.ToString().Trim();
                string phone = dd_genre.Text.ToString().Trim();
                string address = tb_language3.Text.ToString().Trim();
                double account_balance = Convert.ToDouble(tb_year3.Text.ToString().Trim());

                update_patron(patron_id, first_name, last_name, email, password, phone, address, account_balance);
            }
        }

        /*
         * Add Button onclick
         */
        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" ||
                tb_language3.Text == "" || tb_year3.Text == "" || tb_year3.Text == "" || TextBox2.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }

            else
            {
                string first_name = tb_isbm3.Text.ToString().Trim();
                string last_name = tb_author3.Text.ToString().Trim();
                string email = tb_title3.Text.ToString().Trim();
                string password = TextBox2.Text.ToString().Trim();
                string phone = dd_genre.Text.ToString().Trim();
                string address = tb_language3.Text.ToString().Trim();
                double account_balance = Convert.ToDouble(tb_year3.Text.ToString().Trim());

                bool duplicate_email = check_duplicate_email(email);
                if (!duplicate_email)
                {
                    add_patron(first_name, last_name, email, password, phone, address, account_balance);
                }
                else
                {
                    System.Diagnostics.Debug.Write("Email already exists");
                }
            }
        }

        /*
         * Add Record to Patron Table
         */
        protected void add_patron(string first_name, string last_name, string email, string password, string phone, string address,
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
                get_gridview_data(select_query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }


        /*
        * Update to Patron Table
        */
        protected void update_patron(int patron_id, string first_name, string last_name, string email, string password, string phone, string address,
                                double account_balance)
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
                get_gridview_data(select_query);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        /*
         * Deletes patron record from patron table based on patron_id
         */
        protected void delete_patron(int patron_id)
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
                get_gridview_data(select_query);
            }
            catch (Exception ex)
            {
                redirect_to_error_page("ERROR", "Cannot delete this record as this patron currently has something checked out.",
                    "display_patrons.aspx");
            }
        }

        /*
        * Used to convert string patron_name into int patron_id for updating and inserting
        * to patron table
        */
        protected int get_patron_id(string first_name)
        {
            int patron_id = -1;
            try
            {
                string query = "SELECT patron_id FROM patrons WHERE first_name = @first_name;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@first_name", first_name);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        patron_id = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close(); // Close reader
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
            return patron_id;
        }

        /*
         * Checks for duplicate patron based on email when adding or updating
         */
        protected bool check_duplicate_email(string email)
        {
            bool duplicate_found = false;
            try
            {
                string query = "SELECT email FROM patrons WHERE email = @email;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query))
                {
                    connection.Open();
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        System.Diagnostics.Debug.Write("DUPLICATE FOUND!");
                        duplicate_found = true;
                    }
                    reader.Close(); // Close reader
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
            return duplicate_found;
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





