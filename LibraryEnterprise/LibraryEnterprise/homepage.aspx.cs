using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryEnterprise
{
    public partial class homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnemploy_Click(object sender, EventArgs e)
        {
            Response.Redirect("displayEmployees.aspx");
        }

        protected void btnpatron_Click(object sender, EventArgs e)
        {
            Response.Redirect("display_patrons.aspx");
        }

        protected void btncheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }

        protected void btncheckin_Click(object sender, EventArgs e)
        {

            Response.Redirect("checkin.aspx");
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {

        }

        protected void addpatron_Click(object sender, EventArgs e)
        {
            Response.Redirect("registration.aspx");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

        }
    }
}