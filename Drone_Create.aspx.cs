using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     
        protected void Btn_Create_Click(object sender, EventArgs e)
        {

            DroneMedel model = new DroneMedel();
            model.Drone_ID = this.Text_Number.Text;
            model.Manufacturer = this.Text_Manufacturer.Text;
            model.WeightLoad = this.Text_Weight.Text;
            model.Status = this.DropDownList_Status.SelectedValue;
            model.StopReason = this.Text_Deactive.Text;
            model.Operator = this.DropDownList_Operator.SelectedValue;

            ConnectionDB ConnectionDB = new ConnectionDB();

            ConnectionDB.Drone_Detail_Create(model.Drone_ID, model.Manufacturer, model.WeightLoad, model.Status, model.StopReason, model.Operator);
        } 

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Drone_Detail.aspx");

        }

        
    }
}