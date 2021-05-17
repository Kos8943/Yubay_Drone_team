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
    public partial class CustomerCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataTable dt = ConnectionDB.DropDownListRead();
            //DropDownList_Operator.DataSource = dt;
            //DropDownList_Operator.DataTextField = "UserName";
            //this.DropDownList_Operator.DataValueField = "UserName";
            //DropDownList_Operator.DataBind();


            //string querryString = Request.QueryString["Sid"]; //取得網址上的內容並存成字串
            //if (string.IsNullOrEmpty(querryString))
            //{
            //    return;

            //}


            //DataTable data = ConnectionDB.UpdateOnlyoneDroneDetail(querryString);

            //this.Text_Number.Text = data.Rows[0]["Drone_ID"].ToString();
            //this.Text_Manufacturer.Text = data.Rows[0]["Manufacturer"].ToString();
            //this.Text_Weight.Text = data.Rows[0]["WeightLoad"].ToString();
            //this.DropDownList_Status.SelectedValue = data.Rows[0]["Status"].ToString();
            //this.Text_Deactive.Text = data.Rows[0]["StopReason"].ToString();
            //this.DropDownList_Operator.SelectedValue = data.Rows[0]["operator"].ToString();

            //this.CreateDrone.Text = "修改無人機";
            //this.Btn_Create.Text = "修改";

        }

        protected void BtnCustomer_Click(object sender, EventArgs e)
        {

        }
    }
}