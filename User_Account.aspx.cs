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

            //抓取Url上得值
            string currentPage = Request.QueryString["Page"];
            string SearchField = Request.QueryString["SearchField"];
            string SearchKeyWord = Request.QueryString[$"WantSearch"];

            //預設在第一頁
            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            //判斷是否有進階搜尋,有的話取值,沒有的話清空
            if (string.IsNullOrWhiteSpace(SearchField) || string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                SearchField = string.Empty;
                SearchKeyWord = string.Empty;
            }
            else
            {
                ChangePages.SearchType = SearchField;
                ChangePages.SearchKeyWord = SearchKeyWord;
            }


            if (!IsPostBack)
            {
                ConnectionDB DBbase = new ConnectionDB();

                //取消搜尋欄位的Enter鍵盤事件
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");

                //從資料庫撈資料
                int TotalSize;               
                DataTable dt = DBbase.ReadUserAccount(out TotalSize, SearchField, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
                this.SaveInserVal();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //按下新增按鈕轉跳至新增帳號頁面
            Response.Redirect("UserAccount_Create.aspx");
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            //抓取按鈕CommandName的值
            string cmdName = e.CommandName;

            //抓取按鈕CommandArgument的值,
            //因CommandArgument的值為Sid及UserAccount組成故用Split(',')做切割
            string cmdArguSid = e.CommandArgument.ToString().Split(',')[0].Trim();
            

            ConnectionDB DBbase = new ConnectionDB();

            if ("UpDateItem" == cmdName)
            {
                //準備轉跳至修改使用者頁面,並將SID值用URL帶過去
                string targetUrl = "~/UserAccount_Create.aspx?Sid=" + cmdArguSid;

                Response.Redirect(targetUrl);
            }

            if ("DeleItem" == cmdName)
            {
                //取得session
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                //取得session的使用者名稱
                string UserName = loginInfo.UserName;

                //抓取按鈕CommandArgument的Accuont的值
                string cmdArguAccount = e.CommandArgument.ToString().Split(',')[1].Trim();

                //把值放進Method進行刪除
                DBbase.DeleteUserAccount(Convert.ToInt32(cmdArguSid), cmdArguAccount, UserName);
            }

            
            string currentPage = Request.QueryString["Page"];
            string SearchField = Request.QueryString["SearchField"];
            string SearchKeyWord = Request.QueryString[$"WantSearch"];

            if (string.IsNullOrWhiteSpace(currentPage))
            {
                currentPage = "1";
            }

            //判斷是否有進階搜尋,有的話取值,沒有的話清空
            if (string.IsNullOrWhiteSpace(SearchField) || string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                SearchField = string.Empty;
                SearchKeyWord = string.Empty;
            }
            else
            {
                ChangePages.SearchType = SearchField;
                ChangePages.SearchKeyWord = SearchKeyWord;
            }

            //資料刪除後重新Rander至畫面上
            int TotalSize;
            DataTable dt = DBbase.ReadUserAccount(out TotalSize, SearchField, SearchKeyWord, Convert.ToInt32(currentPage));
            ChangePages.TotalSize = TotalSize;
            this.repInvoice.DataSource = dt;
            this.repInvoice.DataBind();
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
            if(!string.IsNullOrWhiteSpace(SearchField) && !string.IsNullOrWhiteSpace(KeyWord))
            {
                Response.Redirect($"User_Account.aspx?Page={currentPage}&WantSearch={KeyWord}&SearchField={SearchField}");
            }
            else
            {
                Response.Redirect($"User_Account.aspx?Page={currentPage}");
            }  

        }

        #region 保留輸入及進階搜尋條件的值

        private void SaveInserVal()
        {
            //抓取Url上得值
            string SearchField = Request.QueryString["SearchField"];
            string SearchKeyWord = Request.QueryString["WantSearch"];

            //判斷是否有進階搜尋,有的話把值放進搜尋欄位
            if (!string.IsNullOrWhiteSpace(SearchField) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchField;
                this.textKeyWord.Text = SearchKeyWord;
            }
        } 
        #endregion
    }
}