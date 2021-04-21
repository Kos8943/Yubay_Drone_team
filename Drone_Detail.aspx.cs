using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yubay_Drone_team
{
    public partial class Drone_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //網頁轉跳至.aspx
            Response.Redirect("Drone_Create.aspx");
        }
    }
}