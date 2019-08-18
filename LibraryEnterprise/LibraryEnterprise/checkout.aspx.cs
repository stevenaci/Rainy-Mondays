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
    public partial class checkout : System.Web.UI.Page
    {
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
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            populateUser();
            populateBook();
        }

        bool populateUser()
        {
            string uf1 = "";
            string uf2 = "";
            uf1 = tb1.Text;
            uf2 = tb2.Text;

            string query = "select * from patrons";
            if (!(uf1 == "") || !(uf2 == ""))
            {
                query += " where";
            }
            if (!(uf1 == ""))
            {
                query += " first_name LIKE '%" + uf1 + "%'";
            }
            if (!(uf2 == ""))
            {
                query += " last_name LIKE '%" + uf2 + "%'";
            }

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable data_table = new DataTable();
                data_table.Load(reader);

                ddpatrons.DataSource = data_table;
                ddpatrons.DataTextField = "email";
                ddpatrons.DataValueField = "patron_id";
                ddpatrons.DataBind();

                reader.Close();
                command.Dispose();
                connection.Close();
            }
            return true;
        }
        bool populateBook()
        {
            string uf1 = "";
            string uf2 = "";
            uf1 = tb3.Text;
            uf2 = tb4.Text;

            string query = "select * from books";
            if (!(uf1 == "") || !(uf2 == ""))
            {
                query += " where";
            }
            if (!(uf1 == ""))
            {
                query += " ISBN LIKE '%" + uf1 + "%'";
            }
            if (!(uf2 == ""))
            {
                query += " title LIKE '%" + uf2 + "%' and quantity > 0";
            }

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable data_table = new DataTable();
                data_table.Load(reader);

                ddbooks.DataSource = data_table;
                ddbooks.DataTextField = "title";
                ddbooks.DataValueField = "book_id";
                ddbooks.DataBind();

                reader.Close();
                command.Dispose();
                connection.Close();
            }
            return true;
        }

        protected void btncheck_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO checkouts VALUES(@patron_id, @book_id, @date_out, @date_in)";

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@patron_id", ddpatrons.SelectedValue);
                command.Parameters.AddWithValue("@book_id", ddbooks.SelectedValue);
                command.Parameters.AddWithValue("@date_out", DateTime.Now);
                command.Parameters.AddWithValue("@date_in", DBNull.Value);
                command.Parameters[3].IsNullable = true;

                command.ExecuteScalar();

                debug.Text = "Book Checked out for " + ddpatrons.SelectedItem + ": " + ddbooks.SelectedItem;
            }

            query = "UPDATE BOOKS set quantity=(quantity-1) where book_id=@book_id";
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@book_id", ddbooks.SelectedValue);
                command.ExecuteNonQuery();
            }
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