using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team
{
    public partial class Customer_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main.TableTitle = "客戶資料管理";
            if (!IsPostBack)
            {
                textKeyWord.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
                DataTable dt = ConnectionDB.ReadCustomerDetail();
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();

            }

        }
        
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
                Response.Redirect("Customer_Create.aspx");
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}