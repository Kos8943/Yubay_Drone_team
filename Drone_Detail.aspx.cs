using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Managers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team
{
    public partial class Drone_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "無人機管理";

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
                DataTable dt = DBbase.ReadDroneDetail(out TotalSize, SearchType, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
                this.SaveInserVal();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //網頁轉跳至.aspx
            Response.Redirect("Drone_Create.aspx");
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
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
                string targetUrl = "~/Drone_Create.aspx?Sid=" + Model.Sid;

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string WantSearch = this.DropDownListSearch.SelectedValue;
            string KeyWord = this.textKeyWord.Text;

            string currentPage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            if (!string.IsNullOrWhiteSpace(WantSearch) && !string.IsNullOrWhiteSpace(KeyWord))
            {
                Response.Redirect($"Drone_Detail.aspx?Page={currentPage}&{WantSearch}={KeyWord}&SearchType={WantSearch}");
            }
            else
            {
                Response.Redirect($"Drone_Detail.aspx?Page={currentPage}");
            }

            //DataTable dt = ConnectionDB.KeyWordSearchDroneDestination(WantSearch, KeyWord);
            //this.repInvoice.DataSource = dt;
            //this.repInvoice.DataBind();
        }

        private void SaveInserVal()
        {
            string SearchType = Request.QueryString["SearchType"];
            string SearchKeyWord = Request.QueryString[$"{SearchType}"];

            if (!string.IsNullOrWhiteSpace(SearchType) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchType;
                this.textKeyWord.Text = SearchKeyWord;
            }
        }
    }
}