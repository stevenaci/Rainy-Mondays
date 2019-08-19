/* File: homepage.aspx
 * Author: Steven Carino
 * 
 * Purpose:
 * Web form that acts as the homepage for employees, giving them a wide range of functionality through
 * links to various other web forms.
 */

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

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void redirect_to_error_page(string error_title, string error_message, string redirect_URL)
        {
            Session["error_title"] = error_title;
            Session["error_message"] = error_message;
            Session["redirect_URL"] = redirect_URL;
            Server.Transfer("error_page.aspx");
        }

        protected void btncheckout_history_Click(object sender, EventArgs e)
        {

        }
    }
}