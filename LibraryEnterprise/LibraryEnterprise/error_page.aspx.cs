using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryEnterprise
{
    public partial class error_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            error_title.InnerText = Session["error_title"].ToString();
            error_message.InnerText = Session["error_message"].ToString();
        }

        protected void btn_return_Click(object sender, EventArgs e)
        {
            //btn_return.PostBackUrl = Session["redirect_URL"].ToString();
            Server.Transfer(Session["redirect_URL"].ToString());
        }
    }
}