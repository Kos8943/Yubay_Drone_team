using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Managers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team
{
    public partial class Drone_Create : System.Web.UI.Page
    {
        protected void Page_init(object sender, EventArgs e)
        {
            string querryString = Request.QueryString["Sid"];
            if (string.IsNullOrEmpty(querryString))
            {
                return;

            }

            DataTable data = ConnectionDB.UpdateOnlyoneDroneDetail(querryString);

            this.Text_Number.Text = data.Rows[0]["Drone_ID"].ToString();
            this.Text_Manufacturer.Text = data.Rows[0]["Manufacturer"].ToString();
            this.Text_Weight.Text = data.Rows[0]["WeightLoad"].ToString();
            this.DropDownList_Status.SelectedValue = data.Rows[0]["Status"].ToString();
            this.Text_Deactive.Text = data.Rows[0]["StopReason"].ToString();
            this.DropDownList_Operator.SelectedValue = data.Rows[0]["operator"].ToString();

            this.CreateDrone.Text = "修改無人機";
            this.Btn_Create.Text = "修改";

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = ConnectionDB.DropDownListRead();
            DropDownList_Operator.DataSource = dt;
            DropDownList_Operator.DataTextField = "Account";
            DropDownList_Operator.DataBind();

        }


        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            string querryString = Request.QueryString["Sid"];

            DroneMedel model = new DroneMedel();
            model.Drone_ID = this.Text_Number.Text;
            model.Manufacturer = this.Text_Manufacturer.Text;
            model.WeightLoad = this.Text_Weight.Text;
            model.Status = this.DropDownList_Status.SelectedValue;
            model.StopReason = this.Text_Deactive.Text;
            model.Operator = this.DropDownList_Operator.SelectedValue;

            ConnectionDB ConnectionDB = new ConnectionDB();


            if (this.Text_Number.Text != string.Empty && this.Text_Manufacturer.Text != string.Empty && this.Text_Weight.Text != string.Empty &&
                this.DropDownList_Status.Text != string.Empty &&
                this.DropDownList_Operator.Text != string.Empty)
            {

                if (string.IsNullOrEmpty(querryString))
                {
                    

                    ConnectionDB.Drone_Detail_Create(model);

                    this.Label1.Visible = true;

                }
                else
                {
                    model.Sid = Convert.ToInt32(querryString);
                    ConnectionDB.Drone_Detail_Update(model);

                    this.Label1.Text = "修改成功!";
                    this.Label1.Visible = true;
                    
                }

                
            }
            else
            {
                this.Label1.Text = "不可空白或輸入重複";
                this.Label1.Visible = true;
                return;

            }

        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Drone_Detail.aspx");

        }




    }
}