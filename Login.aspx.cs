using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;

namespace Yubay_Drone_team
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Account = this.TextAccount.Text;

            string Password = this.TextPassword.Text;

            if( string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password))
            {
                this.ltErrorMsg.Text = "請輸入帳號密碼";

                this.ltErrorMsg.Visible = true;

                return;
            }

            if(LoginHelper.TryLogin(Account, Password))
            {
                Response.Redirect("User_Account.aspx");
            }
            else
            {
                this.ltErrorMsg.Text = "帳號或密碼錯誤";

                this.ltErrorMsg.Visible = true;

                return;
            }
        }
    }
}