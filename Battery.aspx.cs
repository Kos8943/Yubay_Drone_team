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
    public partial class Battery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "電池管理";

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
                //先創一個記憶體空間存class再呼叫method去拿要使用的method
                CustomerAccountManager DBbase = new CustomerAccountManager();
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
                int TotalSize;
                
                DataTable dt = DBbase.ReadBatteryDetail(out TotalSize, SearchType, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
                //進階搜尋條件值
                this.SaveInserVal();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("Battery_Create.aspx");
        }

    

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
                 Response.Redirect($"Battery.aspx?Page={currentPage}&WantSearch={KeyWord}&SearchField={SearchField}");
            }
            else
            {
                Response.Redirect($"Battery.aspx?Page={currentPage}");
            }

        }


        #region 保留輸入及進階搜尋條件的值

        private void SaveInserVal()
        {
            //抓取Url上得值
            string SearchType = Request.QueryString["SearchType"];
            string SearchKeyWord = Request.QueryString[$"WantSearch"];

            //判斷是否有進階搜尋,有的話把值放進搜尋欄位
            if (!string.IsNullOrWhiteSpace(SearchType) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchType;
                this.textKeyWord.Text = SearchKeyWord;
            }
        }
        #endregion

        protected void repInvoice_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string cmdName = e.CommandName;
     
            BatteryModel Model = new BatteryModel();
            ConnectionDB DBbase = new ConnectionDB();
            Model.Sid = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].Trim());
            CustomerAccountManager DBbaseBattery = new CustomerAccountManager();

            if ("DeleItem" == cmdName)
            {
                Model.Battery_ID = e.CommandArgument.ToString().Split(',')[1].Trim();
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                var username = loginInfo.UserName;
                Model.Deleter = username.ToString();
                DBbase.DeleteBattery(Model);
               
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
            DataTable dt = DBbaseBattery.ReadBatteryDetail(out TotalSize, "", "", Convert.ToInt32(currentPage));
            ChangePages.TotalSize = TotalSize;
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
        }
    }
}