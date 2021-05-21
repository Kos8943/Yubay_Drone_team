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
    public partial class Drone_Create : System.Web.UI.Page
    {
        protected void Page_init(object sender, EventArgs e)
        {
            //抓取負責人員並放進下拉選單
            DataTable dt = ConnectionDB.DropDownListRead();
            DropDownList_Operator.DataSource = dt;
            DropDownList_Operator.DataTextField = "UserName";
            this.DropDownList_Operator.DataValueField = "UserName";
            DropDownList_Operator.DataBind();


            string querryString = Request.QueryString["Sid"]; //取得網址上的內容並存成字串
            if (string.IsNullOrEmpty(querryString))
            {
                return;

            }

            DataTable data = ConnectionDB.Select_DroneDetail(querryString);

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
            //隱藏換頁功能
            this.Master.FindControl("ChangePages").Visible = false;

            //設定TableName
            Main.TableTitle = string.Empty;

        }


        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            string querryString = Request.QueryString["Sid"];
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);

            DroneMedel model = new DroneMedel();
            model.Drone_ID = this.Text_Number.Text;
            model.Manufacturer = this.Text_Manufacturer.Text;
            model.WeightLoad = this.Text_Weight.Text;
            model.Status = this.DropDownList_Status.SelectedValue;
            model.StopReason = this.Text_Deactive.Text;
            model.Operator = this.DropDownList_Operator.SelectedValue;

            ConnectionDB ConnectionDB = new ConnectionDB();

            //判斷有幾個不能夠是空值

            if (this.Text_Number.Text != string.Empty && this.Text_Manufacturer.Text != string.Empty && this.Text_Weight.Text != string.Empty &&
                this.DropDownList_Status.Text != string.Empty && this.DropDownList_Operator.Text != string.Empty)     
            {
                if (string.IsNullOrEmpty(querryString))
                {

                    
                    DataTable IDdt = ConnectionDB.Read_Drone_Detail(model.Drone_ID);
                    if (IDdt.Rows.Count != 0)
                    {
                        this.Label1.Text = "已重複輸入";
                        this.Label1.Visible = true;

                        return;
                    }
                    ConnectionDB.Drone_Detail_Create(model);

                    this.Label1.Visible = true;

                }
                else
                {

                    //取得session
                    LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                    //取得session的使用者權限
                    string UserName = loginInfo.UserName;
                    model.Updater = UserName;
                    model.Sid = Convert.ToInt32(querryString);
 
                    ConnectionDB.Drone_Detail_Update(model,Sid);

                    this.Label1.Text = "修改成功!";

                    this.Label1.Visible = true;
                    
                }

                
            }
            else
            {
                this.Label1.Text = "除了停用原因之外其餘不可空白";
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