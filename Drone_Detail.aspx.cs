using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;
using Yubay_Drone_team.Managers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team
{
    public partial class Drone_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "無人機管理";

            if (!IsPostBack)
            {
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
                DataTable dt = ConnectionDB.ReadDroneDetail();
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //網頁轉跳至.aspx
            Response.Redirect("Drone_Create.aspx");
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName;
            DroneMedel cmdArgu = new DroneMedel();
             cmdArgu = (DroneMedel)e.CommandArgument;
            ConnectionDB CDB = new ConnectionDB();
            if ("DeleItem" == cmdName)
            {
                CDB.DelectDroneDetail(cmdArgu);

                DataTable dt = ConnectionDB.ReadDroneDetail();
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
            }
            if ("UpDateItem" == cmdName)
            {
                string targetUrl = "~/Drone_Create.aspx?Sid=" + cmdArgu;

                Response.Redirect(targetUrl);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string WantSearch = this.DropDownListSearch.SelectedValue;
            string KeyWord = this.textKeyWord.Text;

            DataTable dt = ConnectionDB.KeyWordSearchDroneDestination(WantSearch, KeyWord);
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
        }


    }
}