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
    public partial class UserAccount_Create : System.Web.UI.Page
    {
        protected void Page_init(object sender, EventArgs e)
        {
            
            string querryString = Request.QueryString["Sid"]; //取得網址上的內容並存成字串
            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
            UserLevel AccountLevel = loginInfo.AccountLevel;
            
            if(AccountLevel.ToString() == "Supervisor")
            {
                this.DeleteData_Authority.Enabled = true;
            }
            else
            {
                this.DeleteData_Authority.Enabled = false;
            }

            if (string.IsNullOrEmpty(querryString))
            {
                return;
            }
            else
            {
                int Sid;
                if(Int32.TryParse(querryString, out Sid))
                {
                    if(Sid < 2)
                    {
                        Response.Redirect("UserAccount_Create.aspx");
                    }

                    ConnectionDB ConnectionDB = new ConnectionDB();
                    DataTable dt = ConnectionDB.ReadSingleUserAccount(Sid);

                    this.PlaceHolderCreateMode.Visible = false;
                    this.PlaceHolderUpdateMode.Visible = true;
                    this.UserAccountTittle.Text = "修改使用者資料";
                    this.Btn_Create.Text = "修改";

                    this.Text_Account.Text = dt.Rows[0]["Account"].ToString();
                    this.Text_UserName.Text = dt.Rows[0]["UserName"].ToString();
                    this.Text_Account.Enabled = false;

                    if((int)dt.Rows[0]["AccountLevel"] == 2)
                    {
                        this.DeleteData_Authority.Checked = true;
                    }
                    else
                    {
                        this.DeleteData_Authority.Checked = false;
                    }
                }
                else
                {
                    Response.Redirect("UserAccount.aspx");
                }
               
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.FindControl("ChangePages").Visible = false;
            Main.TableTitle = string.Empty;
        }


        protected void Btn_Create_Click(object sender, EventArgs e)
        {
            string querryString = Request.QueryString["Sid"];
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);
            AccountModel model = new AccountModel();
            ConnectionDB ConnectionDB = new ConnectionDB();
            
            string userName = this.Text_UserName.Text;

            model.AccountLevel = AccountLevelCheckBoxIsChecked();
            //bool CheckBoxAccountLevel = this.DeleteData_Authority.Checked;

            //新增模式
            if (string.IsNullOrWhiteSpace(querryString) && !tryParseSid)
            {
                string account = this.Text_Account.Text;
                string password = this.Text_Password.Text;
                string checkPassword = this.Text_CheckPassword.Text;
                

                if (string.IsNullOrWhiteSpace(account))
                {
                    model.Account = account;
                }
                else
                {
                    return;
                }

                if (password == checkPassword)
                {
                    model.Password = password;
                }
                else
                {
                    return;
                }

                //if (CheckBoxAccountLevel)
                //{
                //    model.AccountLevel = 2;
                //}
                //else
                //{
                //    model.AccountLevel = 1;
                //}

                if (string.IsNullOrWhiteSpace(userName))
                {
                    model.UserName = userName;
                }
                else
                {
                    return;
                }
                
                ConnectionDB.CreateUserAccount(model);
            }
            else//修改模式
            {
                string oldPassword = this.Text_OldPassword.Text;

                bool checkOldPasword = ConnectionDB.checkOldPassword(Sid, oldPassword);

                if (checkOldPasword)
                {
                    string newPassword = this.Text_NewPassword.Text;
                    string newPasswordCheck = this.Text_NewPasswordCheck.Text;

                    if(string.Compare(newPassword, newPasswordCheck) == 0)
                    {
                        LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                        string AccountUserName = loginInfo.UserName;
                        model.UserName = userName;
                        model.Password = newPassword;
                        model.Updater = AccountUserName;

                        
                    }
                }
            }
            

            

            //string querryString = Request.QueryString["Sid"];

            //DroneMedel model = new DroneMedel();
            //model.Drone_ID = this.Text_Number.Text;
            //model.Manufacturer = this.Text_Manufacturer.Text;
            //model.WeightLoad = this.Text_Weight.Text;
            //model.Status = this.DropDownList_Status.SelectedValue;
            //model.StopReason = this.Text_Deactive.Text;
            //model.Operator = this.DropDownList_Operator.SelectedValue;

            //ConnectionDB ConnectionDB = new ConnectionDB();


            //if (this.Text_Number.Text != string.Empty && this.Text_Manufacturer.Text != string.Empty && this.Text_Weight.Text != string.Empty &&
            //    this.DropDownList_Status.Text != string.Empty &&
            //    this.DropDownList_Operator.Text != string.Empty)
            //{

            //    if (string.IsNullOrEmpty(querryString))
            //    {


            //        ConnectionDB.Drone_Detail_Create(model);

            //        this.Label1.Visible = true;

            //    }
            //    else
            //    {
            //        model.Sid = Convert.ToInt32(querryString);
            //        ConnectionDB.Drone_Detail_Update(model);

            //        this.Label1.Text = "修改成功!";
            //        this.Label1.Visible = true;

            //    }


            //}
            //else
            //{
            //    this.Label1.Text = "不可空白或輸入重複";
            //    this.Label1.Visible = true;
            //    return;

            //}

        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("User_Account.aspx");
        }

        private int AccountLevelCheckBoxIsChecked()
        {
            bool CheckBoxAccountLevel = this.DeleteData_Authority.Checked;

            if (CheckBoxAccountLevel)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}