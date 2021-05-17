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
    public partial class Fixed_Create : System.Web.UI.Page
    {
        protected void Page_init(object sender, EventArgs e)
        {
            //取得網址上的內容並存成字串 
            string querryString = Request.QueryString["Sid"];

            ConnectionDB connectionDB = new ConnectionDB();
            //抓取無人機ID並放進下拉選單
            DataTable ddlDrone_ID = connectionDB.ReadDrone_ID_Only();
            this.DropDownList_Drone.DataSource = ddlDrone_ID;
            this.DropDownList_Drone.DataTextField = "Drone_ID";
            this.DropDownList_Drone.DataValueField = "Drone_ID";
            this.DropDownList_Drone.DataBind();

            ////判定QueryString["Sid"],空值得話進入新增模式,否則把相應值放進欄位
            if (string.IsNullOrEmpty(querryString))
            {
                return;
            }
            else
            {
                int Sid;
                //檢查QueryString["Sid"]是否為正常值,不是的話轉跳回列表頁
                if (Int32.TryParse(querryString, out Sid))
                {
                    //把值放進相應欄位
                    DataTable dt = connectionDB.ReadSingleFixed(Sid);
                    DateTime StopDate;

                    this.DropDownList_Drone.SelectedValue = dt.Rows[0]["Drone_ID"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //隱藏換頁功能
            this.Master.FindControl("ChangePages").Visible = false;

            //設定TableName
            Main.TableTitle = string.Empty;
        }

        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            FixedModel model = new FixedModel();
            ConnectionDB connectionDB = new ConnectionDB();

            //設定欄位變數
            string useDrone_ID = this.DropDownList_Drone.SelectedValue;
            string fixChange = this.Text_FixChange.Text;
            string stopDate = this.Text_StopDate.Text;
            string fixVendor = this.Text_FixVendor.Text;
            string stopReason = this.Text_StopDate.Text;

            //抓取不須判定的值
            string sendDate = this.Text_SendDate.Text;
            string remarks = this.Text_Remarks.Text;


            //判定useDrone_ID是否為空值
            if (!string.IsNullOrEmpty(useDrone_ID))
            {
                model.Drone_ID = useDrone_ID;
            }
            else
            {
                this.ltMsg.Text = "請選擇使用無人機";
                this.ltMsg.Visible = true;
                return;
            }

            //判定fixChange是否為空值
            if (!string.IsNullOrEmpty(fixChange))
            {
                model.FixChange = fixChange;
            }
            else
            {
                this.ltMsg.Text = "請選擇更換零件";
                this.ltMsg.Visible = true;
                return;
            }
            //判定stopDate是否為空值
            if (!string.IsNullOrEmpty(stopDate))
            {
                model.StopDate = Convert.ToDateTime(stopDate);
            }
            else
            {
                this.ltMsg.Text = "請選擇故障日期";
                this.ltMsg.Visible = true;
                return;
            }
            //判定fixVendor是否為空值
            if (!string.IsNullOrEmpty(fixVendor))
            {
                model.FixVendor = fixVendor;
            }
            else
            {
                this.ltMsg.Text = "請輸入維修廠商";
                this.ltMsg.Visible = true;
                return;
            }
            //判定stopReason是否為空值
            if (!string.IsNullOrEmpty(stopReason))
            {
                model.StopReason = stopReason;
            }
            else
            {
                this.ltMsg.Text = "請輸入故障原因";
                this.ltMsg.Visible = true;
                return;
            }

        }

        protected void Btm_Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}