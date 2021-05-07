using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Models;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team
{
    public partial class Destination_Create : System.Web.UI.Page
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

            //抓取客戶名稱並放進下拉選單
            DataTable ddlCustonerName = connectionDB.ReadAllCustomerName();
            this.DropDownList_Customer_Name.DataSource = ddlCustonerName;
            this.DropDownList_Customer_Name.DataTextField = "Name";
            this.DropDownList_Customer_Name.DataValueField = "Sid";         
            this.DropDownList_Customer_Name.DataBind();
            this.DropDownList_Customer_Name.Items.Insert(0, new ListItem("選擇客戶姓名", "0"));


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
                    DataTable dt = connectionDB.ReadSingleDestination(Sid);
                    this.Text_Date.Text = dt.Rows[0]["Date"].ToString();
                    this.Text_Staff.Text = dt.Rows[0]["Staff"].ToString();
                    this.DropDownList_Drone.SelectedValue = dt.Rows[0]["Drone_ID"].ToString();
                    this.Text_Battery.Text = dt.Rows[0]["Battery_Count"].ToString();
                    this.Text_Remarks.Text = dt.Rows[0]["Remarks"].ToString();
                    this.DropDownList_Customer_Name.SelectedValue = dt.Rows[0]["Customer_Name"].ToString();
                    this.Text_Phone.Text = dt.Rows[0]["Customer_Phone"].ToString();
                    this.Text_Address.Text = dt.Rows[0]["Customer_Address"].ToString();
                    this.Text_Pesticide.Text = dt.Rows[0]["Pesticide"].ToString();
                    this.Text_Pesticide_Date.Text = dt.Rows[0]["Pesticide_Date"].ToString();

                    this.UserAccountTittle.Text = "修改無人機出勤紀錄";
                    this.Btn_Create.Text = "修改";
                }
                else
                {
                    //如果修改URL的QueryString["Sid"],為不正確的值則轉跳
                    Response.Redirect("Destination.aspx");
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
            

            DestinationModel model = new DestinationModel();
            ConnectionDB ConnectionDB = new ConnectionDB();

            //設定欄位變數
            string date = this.Text_Date.Text;
            string staff = this.Text_Staff.Text;
            string useDrone_ID = this.DropDownList_Drone.SelectedValue;
            string carryBattery = this.Text_Battery.Text;                 
            string customerPhone = this.Text_Phone.Text;
            string customerAddress = this.Text_Address.Text;


            //判定date是否為空值
            if (!string.IsNullOrWhiteSpace(date))
            {
                model.Date = date;
            }
            else
            {
                this.ltMsg.Text = "請選擇日期";
                this.ltMsg.Visible = true;
                return;
            }

            //判定staff是否為空值
            if (!string.IsNullOrWhiteSpace(staff))
            {
                model.Staff = staff;
            }
            else
            {
                this.ltMsg.Text = "請輸入出勤人員";
                this.ltMsg.Visible = true;
                return;
            }

            //判定useDrone_ID是否為空值
            if (!string.IsNullOrWhiteSpace(useDrone_ID))
            {
                model.Drone_ID = useDrone_ID;
            }
            else
            {
                this.ltMsg.Text = "請選擇使用無人機";
                this.ltMsg.Visible = true;
                return;
            }

            //判定carryBattery是否為空值
            if (!string.IsNullOrWhiteSpace(carryBattery))
            {
                model.Battery_Count = carryBattery;
            }
            else
            {
                this.ltMsg.Text = "請輸入攜帶電池數量";
                this.ltMsg.Visible = true;
                return;
            }

            //抓取客戶名稱選單的值
            string DropDownList_Customer_Name_Value = this.DropDownList_Customer_Name.SelectedValue;
            int customerSid;
            //檢查客戶名稱選單的值是否為正常值
            bool trySearchCustomerBySid = Int32.TryParse(DropDownList_Customer_Name_Value, out customerSid);

            
            if (trySearchCustomerBySid)
            {
                //如果選到的值是0,代表是沒選擇
                if(customerSid == 0)
                {
                    this.ltMsg.Text = "請選擇客戶姓名";
                    this.ltMsg.Visible = true;
                    return;
                }

                //用選到的值撈出客戶名稱
                DataTable customerName = ConnectionDB.ReadSingleCustomer(customerSid);

                //放進準備回傳的model裡
                model.Customer_Name = customerName.Rows[0]["Name"].ToString();
                model.Customer_Sid = Convert.ToInt32(this.DropDownList_Customer_Name.SelectedValue);
            }

            //判定customerPhone是否為空值
            if (!string.IsNullOrWhiteSpace(customerPhone))
            {
                model.Customer_Phone = customerPhone;
            }
            else
            {
                this.ltMsg.Text = "請輸入客戶電話";
                this.ltMsg.Visible = true;
                return;
            }

            //判定customerAddress是否為空值
            if (!string.IsNullOrWhiteSpace(customerAddress))
            {
                model.Customer_Address = customerAddress;
            }
            else
            {
                this.ltMsg.Text = "請輸入客戶地址";
                this.ltMsg.Visible = true;
                return;
            }

            //抓取不須判定的值
            string pesticide = this.Text_Pesticide.Text;
            string pesticideDate = this.Text_Pesticide_Date.Text;
            string Remarks = this.Text_Remarks.Text;

            model.Pesticide = pesticide;
            model.Pesticide_Date = pesticideDate;
            model.Remarks = Remarks;

            //檢查SID值是否正確
            string querryString = Request.QueryString["Sid"];
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);

           
                //新增模式
                if (string.IsNullOrWhiteSpace(querryString) && !tryParseSid)
                {
                    ConnectionDB.CreateDestination(model);
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
                    ConnectionDB.UpdateDestination(model, Sid);
                    this.ltMsg.Text = "修改成功";
                    this.ltMsg.Visible = true;
                    return;
                }
           
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Destination.aspx");
        }

        
    }
}