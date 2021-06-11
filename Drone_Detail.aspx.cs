using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team
{
    public partial class Drone_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "無人機管理";

            //抓取Url上的值
            string currentPage = Request.QueryString["Page"];
            //抓取下拉式選單DropDownListSearch的值
            string SearchField = Request.QueryString["SearchField"];
            //抓取對話方塊textKeyWord的值
            string SearchKeyWord = Request.QueryString["WantSearch"];

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
            //頁面是第一次加載時，所要執行的事件。
            if (!IsPostBack)
            {
                //取消搜尋欄位的Enter鍵盤事件
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");

                //從資料庫撈資料
                int TotalSize;      //總筆數
                ConnectionDB DBbase = new ConnectionDB();
                DataTable dt = DBbase.ReadDroneDetail(out TotalSize, SearchField, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
                //保留輸入及進階搜尋條件的值
                this.SaveInserVal();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //按下新增按鈕轉跳至新增無人機頁面
            Response.Redirect("Drone_Create.aspx");
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            //抓取按鈕CommandName的值
            string cmdName = e.CommandName;
            //抓取按鈕CommandArgument的值,
            string cmdArguSid = e.CommandArgument.ToString();

            ConnectionDB DBbase = new ConnectionDB();

            //依照CommandName帶過來的值判斷要帶路修改或是刪除
            if ("UpDateItem" == cmdName)
            {
                //準備轉跳至修改無人機管理頁面,並將SID值用URL帶過去
                string targetUrl = "~/Drone_Create.aspx?Sid=" + cmdArguSid;

                Response.Redirect(targetUrl);
            }

            if ("DeleItem" == cmdName)
            {
                //取得session
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                //取得session的使用者名稱
                string UserName = loginInfo.UserName;

                //把值放進Method進行刪除
                DBbase.DelectDroneDetail(Convert.ToInt32(cmdArguSid), UserName);

            }
            //抓取Url上的值
            string currentPage = Request.QueryString["Page"];
            //抓取下拉式選單DropDownListSearch的值
            string SearchField = Request.QueryString["SearchField"];
            //抓取對話方塊textKeyWord的值
            string SearchKeyWord = Request.QueryString["WantSearch"];

            

            //資料刪除後重新Rander至畫面上
            int TotalSize;
            DataTable dt = DBbase.ReadDroneDetail(out TotalSize, SearchField, SearchKeyWord, Convert.ToInt32(currentPage));
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
            if (!string.IsNullOrWhiteSpace(SearchField) && !string.IsNullOrWhiteSpace(KeyWord))
            {
                Response.Redirect($"Drone_Detail.aspx?Page={currentPage}&WantSearch={KeyWord}&SearchField={SearchField}");
            }
            else
            {
                Response.Redirect($"Drone_Detail.aspx?Page={currentPage}");
            }

        
        }

        #region 保留輸入及進階搜尋條件的值

        private void SaveInserVal()
        {

            //換頁的時候搜索條件的值不消失
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