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
    public partial class Battery_Create : System.Web.UI.Page
    {

        protected void Page_init(object sender, EventArgs e)
        {
            //取得網址上的內容並存成字串 
            string querryString = Request.QueryString["Sid"];

            ConnectionDB connectionDB = new ConnectionDB();

            ////判定QueryString["Sid"],空值得話進入新增模式,否則把相應值放進欄位
            if (string.IsNullOrEmpty(querryString))
            {
                return;
            }
            else
            {
                int Sid;
                //檢查QueryString["Sid"]是否為正常值進入修改模式,不是的話轉跳回列表頁
                if (Int32.TryParse(querryString, out Sid))
                {
                    //把值放進相應欄位
                    DataTable dt = connectionDB.ReadSingleBattery(Sid);


                    this.Text_Battery_ID.Text = dt.Rows[0]["Battery_ID"].ToString();
                    this.Text_Status.Text = dt.Rows[0]["status"].ToString();
                    this.Text_StopReason.Text = dt.Rows[0]["stopReason"].ToString();

                    this.BatteryTittle.Text = "修改無人機維修紀錄";
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
            BatteryModel model = new BatteryModel();
            ConnectionDB ConnectionDB = new ConnectionDB();

            //設定欄位變數
            string battery_ID = this.Text_Battery_ID.Text;
            string status = this.Text_Status.Text;

            
            //判定battery_ID是否為空值
            if (!string.IsNullOrEmpty(battery_ID))
            {
                model.Battery_ID = battery_ID;
            }
            else
            {
                this.ltMsg.Text = "請輸入電池編號";
                this.ltMsg.Visible = true;
                return;
            }

            //判定status是否為空值
            if (!string.IsNullOrEmpty(status))
            {
                model.status = status;
            }
            else
            {
                this.ltMsg.Text = "請輸入使用狀況";
                this.ltMsg.Visible = true;
                return;
            }

            //抓取不須判定的值
            string stopReason = this.Text_StopReason.Text;
            model.stopReason = stopReason;

            //檢查SID值是否正確
            string querryString = Request.QueryString["Sid"];
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);

            //新增模式
            if (string.IsNullOrWhiteSpace(querryString) && !tryParseSid)
            {
                //判定battery_ID是否重複輸入
                DataTable IDdt = ConnectionDB.BatteryID_Checker(battery_ID);
                if (IDdt.Rows.Count != 0)
                {
                    this.ltMsg.Text = "已重複輸入電池編號";
                    this.ltMsg.Visible = true;

                    return;
                }
                ConnectionDB.CreateBattery(model);
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
                ConnectionDB.UpdateBattery(model, Sid);
                this.ltMsg.Text = "修改成功";
                this.ltMsg.Visible = true;
                return;
            }
        }

        protected void Btm_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Battery.aspx");
        }
    }
}