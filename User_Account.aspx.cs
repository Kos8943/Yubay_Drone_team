using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team
{
    public partial class User_Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "使用者管理";

            string currentPage = Request.QueryString["Page"];  

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            if (!IsPostBack)
            {
                ConnectionDB ReadUserAccountTable = new ConnectionDB();
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
                int TotalSize;
                DataTable dt = ReadUserAccountTable.ReadUserAccount(out TotalSize, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //網頁轉跳至.aspx
            Response.Redirect("UserAccount_Create.aspx");
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            string cmdArgu = e.CommandArgument.ToString();

            if ("DeleItem" == cmdName)
            {
                ConnectionDB.DelectDroneDetail(cmdArgu);             
            }
            if ("UpDateItem" == cmdName)
            {
                string targetUrl = "~/UserAccount_Create.aspx?Sid=" + cmdArgu;

                Response.Redirect(targetUrl);
            }

            ConnectionDB ReadUserAccountTable = new ConnectionDB();

            //DataTable dt = ReadUserAccountTable.ReadUserAccount();
            //this.repInvoice.DataSource = dt;
            //this.repInvoice.DataBind();
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