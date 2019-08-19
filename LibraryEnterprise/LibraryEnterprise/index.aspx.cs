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
    public partial class index : System.Web.UI.Page
    {
        string con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            // create time keeper
            // and find the difference between dates
            Time_keeper tk = new Time_keeper();
            // update all patron balances
            tk.update_fees();

            // update the lastaccessed date
            tk.update_date();
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            Response.Redirect("loginpage.aspx");

        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            Response.Redirect("patronloginpage.aspx");
        }
    }
}