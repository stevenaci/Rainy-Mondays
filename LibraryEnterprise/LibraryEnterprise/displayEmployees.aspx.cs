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
    public partial class displayEmployees : System.Web.UI.Page
    {
        // Used to carry out CRUD functionality with employees table
        Employee_keeper employee_keeper = new Employee_keeper();

        string select_query = "SELECT * from employees";

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
                employee_keeper.get_gridview_data(select_query, gridview_books);
            }
        }

        /*
         * OnClick function for search button
         */
        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_fn.Text.Trim() == "" && tb_ln.Text.Trim() == "" && tb_email.Text.Trim() == "" && tb_phone.Text.Trim() == "" && tb_address.Text.Trim() == "" && tb_balance.Text.Trim() == "")
            {
                // display all employees if textboxes are empty
                employee_keeper.get_gridview_data(select_query, gridview_books);
            }
            else
            {
                string fn = tb_fn.Text.ToString().Trim();
                string ln = tb_ln.Text.ToString().Trim();
                string email = tb_email.Text.ToString().Trim();
                string phone = tb_phone.Text.ToString().Trim();
                string address = tb_address.Text.ToString().Trim();
                string role = tb_balance.Text.ToString().Trim();
                select_query += " WHERE ";

                bool multiple_conditions = false;

                multiple_conditions = add_where_conditions("first_name", fn, multiple_conditions);
                multiple_conditions = add_where_conditions("last_name", ln, multiple_conditions);
                multiple_conditions = add_where_conditions("email", email, multiple_conditions);
                multiple_conditions = add_where_conditions("phone", phone, multiple_conditions);
                multiple_conditions = add_where_conditions("address", address, multiple_conditions);
                multiple_conditions = add_where_conditions("role", role, multiple_conditions);


                employee_keeper.get_gridview_data(select_query, gridview_books);
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
                tb_language3.Text == "" || tb_year3.Text == "" || tb_year3.Text == "" || tb_password.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }

            else
            {
                string first_name = tb_isbm3.Text.ToString().Trim();
                string last_name = tb_author3.Text.ToString().Trim();
                string email = tb_title3.Text.ToString().Trim();
                string password = tb_password.Text.ToString().Trim();
                string phone = dd_genre.Text.ToString().Trim();
                string address = tb_language3.Text.ToString().Trim();
                string role = tb_year3.Text.ToString().Trim();

                bool duplicate_email = check_duplicate_email(email);
                if (!duplicate_email)
                {
                    employee_keeper.add_employee(first_name, last_name, email, password, phone, address, role);
                }
                else
                {
                    System.Diagnostics.Debug.Write("Email already exists");
                }
            }
            employee_keeper.get_gridview_data(select_query, gridview_books);
        }

        /*
         * Update Button onclick
         * 
         */
        protected void btn_update_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.Write("!!!!!!!!!!!!!!!!!!!!!");
            if (tb_modify_id.Text == "" || tb_isbm3.Text == "" || tb_author3.Text == "" || tb_title3.Text == "" ||
                                tb_language3.Text == "" || tb_year3.Text == "" || tb_password.Text == "")
            {
                // ERROR: EMPTY FIELD DETECTED
                System.Diagnostics.Debug.Write("ERROR: EMPTY FIELD DETECTED");
            }
            else
            {
                int employee_id = Convert.ToInt32(tb_modify_id.Text.ToString().Trim());
                string first_name = tb_isbm3.Text.ToString().Trim();
                string last_name = tb_author3.Text.ToString().Trim();
                string email = tb_title3.Text.ToString().Trim();
                string password = tb_password.Text.ToString().Trim();
                string phone = dd_genre.Text.ToString().Trim();
                string address = tb_language3.Text.ToString().Trim();
                string role = tb_year3.Text.ToString().Trim();

                //  System.Diagnostics.Debug.Write("ERROR: INSIDE UPDATE");
                employee_keeper.update_employee(employee_id, first_name, last_name, email, password, phone, address, role);
            }
            employee_keeper.get_gridview_data(select_query, gridview_books);
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
                int employee_id = Convert.ToInt32(tb_delete_id.Text.ToString().Trim());
                if (Convert.ToInt32(Session["employee_id"]) == employee_id)
                {
                    redirect_to_error_page("ERROR 403", "You can not delete your own account", "displayEmployees.aspx");
                }
                else
                {
                    employee_keeper.delete_employee(employee_id);
                }
            }
            employee_keeper.get_gridview_data(select_query, gridview_books);
        }

        /*
        * Used to convert string employee_name into int employee_id for updating and inserting
        * to employee table
        */
        protected int get_employee_id(string first_name)
        {
            int employee_id = -1;
            try
            {
                string query = "SELECT employee_id FROM employees WHERE first_name = @first_name;";
                System.Diagnostics.Debug.Write(query);

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
                        employee_id = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close(); // Close reader
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
            return employee_id;
        }

        /*
         * Checks for duplicate employee based on email when adding or updating
         */
        protected bool check_duplicate_email(string email)
        {
            bool duplicate_found = false;
            try
            {
                string query = "SELECT email FROM employees WHERE email = @email;";
                System.Diagnostics.Debug.Write(query);

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

        /*
         * Fills update textbox fields with data pulled based on patron_id
         */
        protected void btn_pull_info_Click(object sender, EventArgs e)
        {
            if (tb_modify_id.Text != "")
            {
                int employee_id = Convert.ToInt32(tb_modify_id.Text.ToString());
                pull_employee_info(employee_id);
            }
        }

        /*
         * pull info functionality
         */
        protected void pull_employee_info(int employee_id)
        {
            try
            {
                string query = "SELECT * FROM employees WHERE employee_id = @employee_id;";
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@employee_id", employee_id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tb_isbm3.Text = reader[1].ToString();
                            tb_author3.Text = reader[2].ToString();
                            tb_title3.Text = reader[3].ToString();
                            tb_password.Text = reader[4].ToString();
                            dd_genre.Text = reader[5].ToString();
                            tb_language3.Text = reader[6].ToString();
                            tb_year3.Text = reader[7].ToString();
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
    }
}