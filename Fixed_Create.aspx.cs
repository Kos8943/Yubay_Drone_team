using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;
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
                    this.DropDownList_Drone.Enabled = false;
                    //把值放進相應欄位
                    DataTable dt = connectionDB.ReadSingleFixed(Sid);
                    DateTime stopDate = (DateTime)dt.Rows[0]["StopDate"];
                    DateTime sendDate = (DateTime)dt.Rows[0]["SendDate"];


                    this.DropDownList_Drone.SelectedValue = dt.Rows[0]["Drone_ID"].ToString();
                    this.Text_FixChange.Text = dt.Rows[0]["FixChange"].ToString();
                    this.Text_StopDate.Text = stopDate.ToString("yyyy-MM-dd");
                    this.Text_SendDate.Text = sendDate.ToString("yyyy-MM-dd");
                    this.Text_FixVendor.Text = dt.Rows[0]["FixVendor"].ToString();
                    this.Text_StopReason.Text = dt.Rows[0]["StopReason"].ToString();
                    this.Text_Remarks.Text = dt.Rows[0]["Remarks"].ToString();

                    this.UserAccountTittle.Text = "修改無人機維修紀錄";
                    this.Btn_Create.Text = "修改";
                }
                else
                {
                    //如果修改URL的QueryString["Sid"],為不正確的值則轉跳
                    Response.Redirect("Fixed.aspx");

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
            ConnectionDB ConnectionDB = new ConnectionDB();

            //設定欄位變數
            string useDrone_ID = this.DropDownList_Drone.SelectedValue;
            string fixChange = this.Text_FixChange.Text;
            string stopDate = this.Text_StopDate.Text;
            string fixVendor = this.Text_FixVendor.Text;
            string stopReason = this.Text_StopReason.Text;



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

            //抓取不須判定的值
            DateTime sendDate = Convert.ToDateTime(this.Text_SendDate.Text);
            string remarks = this.Text_Remarks.Text;

            model.SendDate = sendDate;
            model.Remarks = remarks;

            //檢查SID值是否正確
            string querryString = Request.QueryString["Sid"];
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);

            //新增模式
            if (string.IsNullOrWhiteSpace(querryString) && !tryParseSid)
            {
                ConnectionDB.CreateFixed(model);
                this.ltMsg.Text = "新增成功";
                this.ltMsg.Visible = true;
                return;

            }
            else//修改模式
            {

                //取得session
                LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                //取得session的使用者權限
                string UserName = loginInfo.UserName;
                model.Updater = UserName;
                ConnectionDB.UpdateFixed(model, Sid);
                this.ltMsg.Text = "修改成功";
                this.ltMsg.Visible = true;
                return;
            }
        }
        protected void Btm_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Fixed.aspx");
        }
    }
}