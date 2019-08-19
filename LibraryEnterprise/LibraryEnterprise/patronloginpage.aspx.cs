/* File: patronloginpage.aspx
 * Author: Steven Carino
 * 
 * Purpose:
 * Web form that allows patrons to login. Redirects to the patronhomepage upon successful login,
 * setting Session variables to keep track of login status.
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
    public partial class patronloginpage : System.Web.UI.Page
    {
        string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(con_string))
                using (SqlCommand command = connection.CreateCommand())
                {
                    string query = "SELECT * FROM patrons where email = @email and password = @password";
                    command.CommandText = query;
                    connection.Open();

                    command.Parameters.AddWithValue("@email", tb1.Text);

                    command.Parameters.AddWithValue("@password", tb2.Text);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Session["patron_id"] = reader[0].ToString();
                            Session["patron_name"] = reader[1].ToString() + " " + reader[2].ToString();
                            Session["balance"] = reader.GetDecimal(reader.GetOrdinal("account_balance"));
                        }
                    }

                    if (Session["patron_id"] != null)
                    {
                        Response.Redirect("patronhomepage.aspx");
                    }
                    else
                    {
                        lbldebug.Text = "Invalid Login. Register to make an account";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString());
            }
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            Response.Redirect("registration.aspx");
        }
    }
}