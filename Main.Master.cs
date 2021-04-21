using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;

namespace Yubay_Drone_team
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!LoginHelper.HasLogined())
            //{
            //    Response.Redirect("Login.aspx");
            //}
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginHelper.Logout();

            Response.Redirect("Login.aspx");
        }
    }
}