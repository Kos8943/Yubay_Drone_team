﻿using System;
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

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customer_Create.aspx");
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        private void SaveInserVal()
        {

            string SearchType = Request.QueryString["SearchType"]; //從網址上面抓下拉式選單裡所選的搜尋條件
            string SearchKeyWord = Request.QueryString[$"{SearchType}"];//將搜尋的條件丟進去使用者輸入的關鍵字

            //如果前面兩個東西都是空值進入判斷式
            if (!string.IsNullOrWhiteSpace(SearchType) && !string.IsNullOrWhiteSpace(SearchKeyWord))
            {
                this.DropDownListSearch.SelectedValue = SearchType;
                this.textKeyWord.Text = SearchKeyWord;
            }
        }

        protected void repInvoice_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {

            string cmdName = e.CommandName;
            //string cmdArgu = e.CommandArgument.ToString().Split(',')[0].Trim();
            CustomerModel Model = new CustomerModel();
            Model.Sid = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].Trim());

            ConnectionDB DBbase = new ConnectionDB();

            if ("DeleItem" == cmdName)
            {
                Model.Name = e.CommandArgument.ToString().Split(',')[1].Trim();
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                var username = loginInfo.UserName;
                Model.Deleter = username.ToString();
                //DBbase.DelectDroneDetail(Model);
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
    }
}