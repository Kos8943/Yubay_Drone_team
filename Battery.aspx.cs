using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Managers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team
{
    public partial class Battery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        protected void repInvoice_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            //string cmdArgu = e.CommandArgument.ToString().Split(',')[0].Trim();
            DroneMedel Model = new DroneMedel();
            ConnectionDB DBbase = new ConnectionDB();
            Model.Sid = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].Trim());


            if ("DeleItem" == cmdName)
            {
                Model.Drone_ID = e.CommandArgument.ToString().Split(',')[1].Trim();
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                var username = loginInfo.UserName;
                Model.Deleter = username.ToString();
                DBbase.DelectDroneDetail(Model);
                //DataTable dt = ConnectionDB.ReadDroneDetail();
                //this.repInvoice.DataSource = dt;
                //this.repInvoice.DataBind();
            }
            if ("UpDateItem" == cmdName)
            {
                string targetUrl = "~/Battery_Create.aspx?Sid=" + Model.Sid;
                Response.Redirect(targetUrl);
            }

            string currentPage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            int TotalSize;
            DataTable dt = DBbase.ReadDroneDetail(out TotalSize, "", "", Convert.ToInt32(currentPage));
            ChangePages.TotalSize = TotalSize;
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
        }
    }
}