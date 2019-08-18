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
    public partial class patronhomepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl1.Text = "Welcome: " + Session["patron_id"];
            
            string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

            int p_id = 0;
            // load all checked out books
            string query = "select isbn, title, author" +
                            " from dbo.books b" +
                            " inner join" +
                            " dbo.checkouts c" +
                            " on b.book_id = c.book_id" +
                            " where c.date_in IS NULL" +
                            " and c.patron_id = @p_id;";

            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@p_id", p_id);
                SqlDataReader rdr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);

                gv1.DataSource = dt;
                gv1.DataBind();
            }

            query = "select isbn, title, author" +
                " from dbo.books b" +
                " inner join" +
                " dbo.checkouts c" +
                " on b.book_id = c.book_id" +
                " where c.date_in IS NULL" +
                " and DATEDIFF(day, c.date_out, CURRENT_TIMESTAMP) > 14"+
                " and c.patron_id = @p_id;";

            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@p_id", p_id);
                SqlDataReader rdr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                gv2.DataSource = dt;
                gv2.DataBind();
            }
            lblbal.Text = Session["balance"].ToString();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // 
            Response.Redirect("modify_books.aspx");
        }
    }
}