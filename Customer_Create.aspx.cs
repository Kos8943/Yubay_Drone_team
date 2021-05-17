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
    public partial class CustomerCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataTable dt = ConnectionDB.DropDownListRead();
            //DropDownList_Operator.DataSource = dt;
            //DropDownList_Operator.DataTextField = "UserName";
            //this.DropDownList_Operator.DataValueField = "UserName";
            //DropDownList_Operator.DataBind();


            string querryString = Request.QueryString["Sid"]; //取得網址上的內容並存成字串
            if (string.IsNullOrEmpty(querryString))
            {
                return;

            }

            //修改客戶資料管理
            DataTable data = ConnectionDB.UpdateOnlyoneCustomer(querryString);

            this.Text_Name.Text = data.Rows[0]["Name"].ToString();
            this.Text_Address.Text = data.Rows[0]["Address"].ToString();
            this.Text_Phone.Text = data.Rows[0]["Phone"].ToString();
            this.Text_Crop.Text = data.Rows[0]["Crop"].ToString();
            this.Text_Area.Text = data.Rows[0]["Area"].ToString();
            this.Text_Farm_Address.Text = data.Rows[0]["Farm_Address"].ToString();

            this.CreateCustomer.Text = "修改客戶資料";
            this.Btn_Create.Text = "修改";

        }

        protected void BtnCustomer_Click(object sender, EventArgs e)
        {

        }

        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            string querryString = Request.QueryString["Sid"];
            CustomerModel model = new CustomerModel();
                       //轉型,強制型別轉換
            model.Sid =Convert.ToInt32(querryString);
            model.Name = this.Text_Name.Text;
            model.Address = this.Text_Address.Text;
            model.Phone = this.Text_Phone.Text;
            model.Crop = this.Text_Crop.Text;
            model.Area = this.Text_Area.Text;
     

            ConnectionDB ConnectionDB = new ConnectionDB();


            //if判斷式是否重複或修改是否成功

            if (this.Text_Name.Text != string.Empty && this.Text_Address.Text != string.Empty && this.Text_Phone.Text != string.Empty
                && this.Text_Crop.Text != string.Empty && this.Text_Area.Text != string.Empty && this.Text_Farm_Address.Text != string.Empty
                ) 
                
            {

                if (string.IsNullOrEmpty(querryString))
                {


                    //DataTable IDdt = ConnectionDB.ID_Checker(model.Drone_ID);
                    //if (IDdt.Rows.Count != 0)
                    //{
                    //    this.Label1.Text = "已重複輸入";
                    //    this.Label1.Visible = true;

                    //    return;
                    //}
                    ConnectionDB.CreateCustomer(model);

                    this.Label1.Visible = true;

                }
                else
                {
                    model.Sid = Convert.ToInt32(querryString);
                    ConnectionDB.UpdateCustomer(model,model.Sid);
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

        //轉跳取消頁面
        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Drone_Detail.aspx");
        }
    }
}