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
            string SearchType = Request.QueryString["SearchType"];
            string SearchKeyWord = Request.QueryString[$"{SearchType}"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            if (string.IsNullOrWhiteSpace(SearchType) || string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                SearchType = string.Empty;
                SearchKeyWord = string.Empty;
            }
            else
            {
                ChangePages.SearchType = SearchType;
                ChangePages.SearchKeyWord = SearchKeyWord;
            }


            if (!IsPostBack)
            {
                ConnectionDB DBbase = new ConnectionDB();
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
                int TotalSize;
                DataTable dt = DBbase.ReadUserAccount(out TotalSize, SearchType, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
                this.SaveInserVal();
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

            ConnectionDB DBbase = new ConnectionDB();

            if ("DeleItem" == cmdName)
            {
                DBbase.DeleteUserAccount( Convert.ToInt32(cmdArgu));
            }
            if ("UpDateItem" == cmdName)
            {
                string targetUrl = "~/UserAccount_Create.aspx?Sid=" + cmdArgu;

                Response.Redirect(targetUrl);
            }

            string currentPage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }


            int TotalSize;
            DataTable dt = DBbase.ReadUserAccount(out TotalSize, "", "", Convert.ToInt32(currentPage));
            ChangePages.TotalSize = TotalSize;
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string WantSearch = this.DropDownListSearch.SelectedValue;
            string KeyWord = this.textKeyWord.Text;
            ConnectionDB DBbase = new ConnectionDB();

            string currentPage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            if(!string.IsNullOrWhiteSpace(WantSearch) && !string.IsNullOrWhiteSpace(KeyWord))
            {
                Response.Redirect($"User_Account.aspx?Page={currentPage}&{WantSearch}={KeyWord}&SearchType={WantSearch}");
            }
            else
            {
                Response.Redirect($"User_Account.aspx?Page={currentPage}");
            }  

        }

        private void SaveInserVal()
        {
            string SearchType = Request.QueryString["SearchType"];
            string SearchKeyWord = Request.QueryString[$"{SearchType}"];

            if(!string.IsNullOrWhiteSpace(SearchType) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchType;
                this.textKeyWord.Text = SearchKeyWord;
            }
        }
    }
}