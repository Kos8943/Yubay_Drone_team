using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;

namespace Yubay_Drone_team
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public static string TableTitle { get; set; } = "無人機管理"; 
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!LoginHelper.HasLogined())
            //{
            //    Response.Redirect("Login.aspx");
            //}
            this.TableName.InnerText = TableTitle;

            //取得session
            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
            //取得session的使用者權限
            string AccountLevel = loginInfo.UserName;

            this.LoginUserName.InnerText = $"你好,{AccountLevel}";
            


        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginHelper.Logout();

            Response.Redirect("Login.aspx");
        }
    }
}