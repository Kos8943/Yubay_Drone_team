using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Customer;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Managers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team
{
    public partial class Customer_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "客戶資料管理";

            string currentPage = Request.QueryString["Page"];
            string SearchType = Request.QueryString["SearchField"];
            string SearchKeyWord = Request.QueryString[$"WantSearch"];

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
                CustomerAccountManager DBbase = new CustomerAccountManager();
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
                int TotalSize;
                DataTable dt = DBbase.ReadCustomerDetail(out TotalSize, SearchType, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
                this.SaveInserVal();
            }
        }

        #region 客戶資料查詢的頁面

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //想搜尋的欄位
            string SearchField = this.DropDownListSearch.SelectedValue;

            //搜尋的值
            string KeyWord = this.textKeyWord.Text;

            string currentPage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            //如果搜尋欄位有值的話,把搜尋欄位跟搜尋值都放進URL,否則只放頁數
            if (!string.IsNullOrWhiteSpace(SearchField) && !string.IsNullOrWhiteSpace(KeyWord))
            {
                Response.Redirect($"Customer_Detail.aspx?Page={currentPage}&WantSearch={KeyWord}&SearchField={SearchField}");
            }
            else
            {
                Response.Redirect($"Customer_Detail.aspx?Page={currentPage}");
            }

        }
        #endregion

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customer_Create.aspx");
        }

        private void SaveInserVal()
        {

            string SearchType = Request.QueryString["SearchField"]; //從網址上面抓下拉式選單裡所選的搜尋條件
            string SearchKeyWord = Request.QueryString[$"WantSearch"];//將搜尋的條件丟進去使用者輸入的關鍵字

            //如果前面兩個東西都是空值進入判斷式
            if (!string.IsNullOrWhiteSpace(SearchType) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchType;
                this.textKeyWord.Text = SearchKeyWord;
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            CustomerModel Model = new CustomerModel();        
            ConnectionDB DBbase = new ConnectionDB();
            CustomerAccountManager DBbaseCustomer = new CustomerAccountManager();
            Model.Sid = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].Trim());


            if ("DeleteItem" == cmdName)
            {
                Model.Name = e.CommandArgument.ToString().Split(',')[1].Trim();
                //查看權限檢查是否可以作刪除動作
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                var username = loginInfo.UserName;
                Model.Deleter = username.ToString();
                DBbase.DeleteCustomer(Model);
             
            }
            if ("UpDateItem" == cmdName)
            {
                string targetUrl = "~/Customer_Create.aspx?Sid=" + Model.Sid;
                Response.Redirect(targetUrl);
            }

            string currentPage = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            int TotalSize;
            DataTable dt = DBbaseCustomer.ReadCustomerDetail(out TotalSize, "", "", Convert.ToInt32(currentPage));
            ChangePages.TotalSize = TotalSize;
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
        }
    }
}