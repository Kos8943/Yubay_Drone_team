using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team
{
    public partial class Drone_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = ConnectionDB.ReadDroneDetail();
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //網頁轉跳至.aspx
            Response.Redirect("Drone_Create.aspx");
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            string cmdArgu = e.CommandArgument.ToString();

            if ("DeleItem" == cmdName)
            {
            ConnectionDB.DelectDroneDetail(cmdArgu);
            }
        }
    }
}