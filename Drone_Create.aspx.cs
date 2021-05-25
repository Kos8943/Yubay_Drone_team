using System;
using System.Data;
using System.Web;
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

            int number;
           
            //判斷ID是否為正確的型別
            if (querryString != null && !int.TryParse(querryString, out number))
            {
                Response.Write("<script>alert('此為無效ID')</script>");
                return;
            }

            //從資料庫抓資料顯示在畫面上
            DataTable data = ConnectionDB.Select_DroneDetail(querryString);

            //如果沒資料就跳回新增模式
            if (data == null)
            {
                return;
            }
            //如果資料庫無此型別ID則跳回新增模式
            if (data.Rows.Count <= 0)
            {
                Response.Write("<script>alert('此ID無資料')</script>");
                return;
            }

            //從資料庫抓值修改無人機資料
            if (int.TryParse(querryString, out number))
            {                           //取得這個資料表的資料列集合 //0為第一筆資料
                this.Text_Number.Text = data.Rows[0]["Drone_ID"].ToString();
                this.Text_Manufacturer.Text = data.Rows[0]["Manufacturer"].ToString();
                this.Text_Weight.Text = data.Rows[0]["WeightLoad"].ToString();
                this.DropDownList_Status.SelectedValue = data.Rows[0]["Status"].ToString();
                this.Text_Deactive.Text = data.Rows[0]["StopReason"].ToString();
                this.DropDownList_Operator.SelectedValue = data.Rows[0]["operator"].ToString();

                this.CreateDrone.Text = "修改無人機";
                this.Btn_Create.Text = "修改";
            }
        }


        //進入載入畫面 //因為是放在master上，但是沒有用到要隱藏
        protected void Page_Load(object sender, EventArgs e)
        {
            //隱藏換頁功能
            this.Master.FindControl("ChangePages").Visible = false;
            //設定TableName
            Main.TableTitle = string.Empty;

        }

        //新增按鈕事件
        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            //載入新增的畫面先做型別比對
            string querryString = Request.QueryString["Sid"];
            //比對QuerryString是不是數字
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);
            //給他Model去給使用者一個放資料的空間
            DroneMedel model = new DroneMedel();
            model.Drone_ID = this.Text_Number.Text;
            model.Manufacturer = this.Text_Manufacturer.Text;
            model.WeightLoad = this.Text_Weight.Text;
            model.Status = this.DropDownList_Status.SelectedValue;
            model.StopReason = this.Text_Deactive.Text;
            model.Operator = this.DropDownList_Operator.SelectedValue;

           

           

            //判斷有幾個不能夠是空值
            if (this.Text_Number.Text != string.Empty && 
                this.Text_Manufacturer.Text != string.Empty && 
                this.Text_Weight.Text != string.Empty) 
            {  
                ConnectionDB ConnectionDB = new ConnectionDB();
               
                if (string.IsNullOrEmpty(querryString))
                { 
                    //讀取資料庫欄位判斷是否重複輸入
                    DataTable IDdt = ConnectionDB.Read_Drone_Detail(model.Drone_ID);
                    if (IDdt.Rows.Count != 0)
                    {
                        this.Label1.Text = "已重複輸入";
                        this.Label1.Visible = true;

                        return;
                    }
                    ConnectionDB.Drone_Detail_Create(model);
                    //顯示已重複輸入
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
                    //在資料庫做修改
                    ConnectionDB.Drone_Detail_Update(model, Sid);

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

    }
}