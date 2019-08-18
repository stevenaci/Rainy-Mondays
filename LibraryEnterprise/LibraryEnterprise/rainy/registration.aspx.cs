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
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool emailExists = false;
            string query = "SELECT * FROM patrons WHERE email='"+email.Text+"'";

            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = query;
                SqlDataReader rdr = command.ExecuteReader();
                if (rdr.HasRows)
                {
                    emailExists = true;
                }
            }


                if (!emailExists)
                {
                    query = "INSERT INTO patrons VALUES(@first_name,@last_name,@email,@password,@phone,@address,@account_balance)";
                
                    using (SqlConnection connection = new SqlConnection(con_string))
                    using (SqlCommand command = new SqlCommand(query))
                    {
                        Decimal balance = 0.00M;
                        connection.Open();
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@first_name", fname.Text);
                        command.Parameters.AddWithValue("@last_name", lname.Text);
                        command.Parameters.AddWithValue("@email", email.Text);
                        command.Parameters.AddWithValue("@password", password.Text);
                        command.Parameters.AddWithValue("@phone", phone.Text);
                        command.Parameters.AddWithValue("@address", address.Text);
                        command.Parameters.AddWithValue("@account_balance", balance);
                        command.ExecuteNonQuery();
                        debug.Text = "Patron Added: Email: " + email.Text;
                    }
                }
                else { debug.Text = "Email already exists"; }
            
        }
    }
}