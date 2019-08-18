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
    public partial class loginpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlog_Click(object sender, EventArgs e)
        {
            try
            {
                string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = connection.CreateCommand())
                {
                    string query = "SELECT * FROM employees where email = @email and password = @password";
                    command.CommandText = query;
                    connection.Open();
                    command.Parameters.AddWithValue("@email", tbuser.Text);
                    command.Parameters.AddWithValue("@password", tbpass.Text);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Session["employee_id"] = reader[0].ToString();
                        }
                    }

                    if (Session["employee_id"] != null)
                    {
                        Response.Redirect("homepage.aspx");
                    }
                    else
                    {
                        lbldebug.Text = "Invalid Login";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }
    }
}