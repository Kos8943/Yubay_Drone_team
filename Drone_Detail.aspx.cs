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

            //抓取Url上得值
            string currentPage = Request.QueryString["Page"];
            string SearchField = Request.QueryString["SearchField"];
            string SearchKeyWord = Request.QueryString["WantSearch"];

            //string SearchKeyWord = Request.QueryString  [$"{SearchType}"];

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
                //取消搜尋欄位的Enter鍵盤事件
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");

                //從資料庫撈資料
                int TotalSize;
                ConnectionDB DBbase = new ConnectionDB();
                DataTable dt = DBbase.ReadDroneDetail(out TotalSize, SearchField, SearchKeyWord, Convert.ToInt32(currentPage));
                ChangePages.TotalSize = TotalSize;
                this.repInvoice.DataSource = dt;
                this.repInvoice.DataBind();
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
            //因CommandArgument的值為Sid及Drone_ID組成故用Split(',')做切割
            DroneMedel Model = new DroneMedel();
            ConnectionDB DBbase = new ConnectionDB();
            Model.Sid = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].Trim());


            if ("DeleItem" == cmdName)
            {
                Model.Drone_ID = e.CommandArgument.ToString().Split(',')[1].Trim();
                //取得session
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                //取得session的使用者名稱
                string username = loginInfo.UserName;

                //取得的使用者名稱代入Deleter
                Model.Deleter = username.ToString();

                //把值放進Method進行刪除
                DBbase.DelectDroneDetail(Model);
        
            }
            if ("UpDateItem" == cmdName)
            {
                //準備轉跳至修改使用者頁面,並將SID值用URL帶過去
                string targetUrl = "~/Drone_Create.aspx?Sid=" + Model.Sid;

                Response.Redirect(targetUrl);
            }

            string currentPage = Request.QueryString["Page"];
            string SearchField = Request.QueryString["SearchField"];
            string SearchKeyWord = Request.QueryString["WantSearch"];

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
            //抓取Url上得值
            string SearchType = Request.QueryString["SearchType"];
            string SearchKeyWord = Request.QueryString["WantSearch"];

            //判斷是否有進階搜尋,有的話把值放進搜尋欄位
            if (!string.IsNullOrWhiteSpace(SearchType) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchType;
                this.textKeyWord.Text = SearchKeyWord;
            }
        }
        #endregion
    }
}