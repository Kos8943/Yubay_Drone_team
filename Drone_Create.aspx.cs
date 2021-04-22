﻿using System;
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
        protected void Page_init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            string querryString = Request.QueryString["Sid"];

            if (string.IsNullOrEmpty(querryString))
            {
                if (this.Text_Number.Text != string.Empty && this.Text_Manufacturer.Text != string.Empty && this.Text_Weight.Text != string.Empty &&
                this.DropDownList_Status.Text != string.Empty &&
                this.DropDownList_Operator.Text != string.Empty)
                {
                    DroneMedel model = new DroneMedel();
                    model.Drone_ID = this.Text_Number.Text;
                    model.Manufacturer = this.Text_Manufacturer.Text;
                    model.WeightLoad = this.Text_Weight.Text;
                    model.Status = this.DropDownList_Status.SelectedValue;
                    model.StopReason = this.Text_Deactive.Text;
                    model.Operator = this.DropDownList_Operator.SelectedValue;



                    ConnectionDB ConnectionDB = new ConnectionDB();

                    ConnectionDB.Drone_Detail_Create(model);

                    this.Literal1.Visible = true;
                }
                else
                {
                    this.Literal1.Text = "不可空白或輸入重複";
                    this.Literal1.Visible = true;
                    return;

                }

            }
            else
            {
                this.Literal1.Text = "修改成功!";
                this.Literal1.Visible = true;
                return;
            }

        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Drone_Detail.aspx");

        }




    }
}