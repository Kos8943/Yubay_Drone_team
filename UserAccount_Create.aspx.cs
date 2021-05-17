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
            //取得session
            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
            //取得session的使用者權限
            UserLevel AccountLevel = loginInfo.AccountLevel;
            
            //判定使用者權限,不足的話把ckeckbox鎖住
            if(AccountLevel.ToString() == "Supervisor")
            {
                this.DeleteData_Authority.Enabled = true;
            }
            else
            {
                this.DeleteData_Authority.Enabled = false;
            }

            //判定QueryString["Sid"],空值得話進入新增模式,否則把相應值放進欄位
            if (string.IsNullOrEmpty(querryString))
            {
                return;
            }
            else
            {
                int Sid;
                //檢查QueryString["Sid"]是否為正常值
                if (Int32.TryParse(querryString, out Sid))
                {
                    if(Sid < 2)
                    {
                        Response.Redirect("UserAccount_Create.aspx");
                    }

                    //用QueryString["Sid"]進資料庫撈出欲修改資料
                    ConnectionDB ConnectionDB = new ConnectionDB();
                    DataTable dt = ConnectionDB.ReadSingleUserAccount(Sid);

                    //切換新增/修改的PlaceHolder
                    this.PlaceHolderCreateMode.Visible = false;
                    this.PlaceHolderUpdateMode.Visible = true;
                    this.UserAccountTittle.Text = "修改使用者資料";
                    this.Btn_Create.Text = "修改";

                    //把值丟進欄位
                    this.Text_Account.Text = dt.Rows[0]["Account"].ToString();
                    this.Text_UserName.Text = dt.Rows[0]["UserName"].ToString();

                    //把帳號欄位改為不可輸入
                    this.Text_Account.Enabled = false;

                    //檢查使用者權限,不足的話把CheckBox鎖住
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
                    //如果修改URL的QueryString["Sid"],為不正確的值則轉跳
                    Response.Redirect("UserAccount.aspx");
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
            //檢查SID值是否正確
            string querryString = Request.QueryString["Sid"];
            int Sid;
            bool tryParseSid = Int32.TryParse(querryString, out Sid);

            AccountModel model = new AccountModel();
            ConnectionDB ConnectionDB = new ConnectionDB();
            
            //抓取名稱欄位的值
            string userName = this.Text_UserName.Text;

            //抓到CheckBox有沒有勾選
            model.AccountLevel = AccountLevelCheckBoxIsChecked();
            //bool CheckBoxAccountLevel = this.DeleteData_Authority.Checked;

            //新增模式
            if (string.IsNullOrWhiteSpace(querryString) && !tryParseSid)
            {
                //設定欄位變數
                string account = this.Text_Account.Text;
                string password = this.Text_Password.Text;
                string checkPassword = this.Text_CheckPassword.Text;
                

                //檢查帳號欄位是否為空值
                if (!string.IsNullOrWhiteSpace(account))
                {
                    //檢查新增的帳號是否重複,不是的話將值塞進Model
                    if (ConnectionDB.CheckUserAccount(account) == 0)
                    {
                        model.Account = account;
                    }
                    else
                    {
                        this.ltMsg.Text = "帳號重複";
                        this.ltMsg.Visible = true;
                        return;
                    }
                    
                }
                else
                {
                    this.ltMsg.Text = "請輸入帳號";
                    this.ltMsg.Visible = true;
                    return;
                }

                //檢查密碼欄位是否為空值
                if (!string.IsNullOrWhiteSpace(password))
                {
                    //檢查密碼欄位是否與密碼確認欄位一致,是的話將值塞進Model
                    if (string.Compare(password, checkPassword) == 0)
                    {
                        model.Password = password;
                    }
                    else
                    {
                        this.ltMsg.Text = "密碼與確認密碼不一致";
                        this.ltMsg.Visible = true;
                        return;
                    }
                }
                else
                {
                    this.ltMsg.Text = "請輸入密碼";
                    this.ltMsg.Visible = true;
                    return;
                }


                //檢查userName是否為空值
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    model.UserName = userName;
                }
                else
                {
                    this.ltMsg.Text = "請輸入使用者名稱";
                    this.ltMsg.Visible = true;
                    return;
                }
                
                //新增進資料庫
                ConnectionDB.CreateUserAccount(model);
                this.ltMsg.Text = "新增成功";
                this.ltMsg.Visible = true;
            }
            else//修改模式
            {
               
                //檢查userName是否為空值
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    model.UserName = userName;
                }
                else
                {
                    this.ltMsg.Text = "請輸入使用者名稱";
                    this.ltMsg.Visible = true;
                    return;
                }

                //抓取舊密碼欄位
                string oldPassword = this.Text_OldPassword.Text;

                //確認舊密碼是否正確
                bool checkOldPasword = ConnectionDB.checkOldPassword(Sid, oldPassword);

                //舊密碼正確的話進入判斷
                if (checkOldPasword)
                {
                    //抓取新密碼與新密碼確認的值
                    string newPassword = this.Text_NewPassword.Text;
                    string newPasswordCheck = this.Text_NewPasswordCheck.Text;

                    //檢查新密碼與新密碼欄位是否為空值
                    if (!string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(newPasswordCheck))
                    {
                        //檢查新密碼與新密碼確認的值是否一致,是的話將值塞進Model
                        if (string.Compare(newPassword, newPasswordCheck) == 0)
                        {
                            LoginInfo loginInfo = HttpContext.Current.Session["IsLogined"] as LoginInfo;
                            string AccountUserName = loginInfo.UserName;
                            model.UserName = userName;
                            model.Password = newPassword;
                            model.Updater = AccountUserName;

                            //將直寫進資料庫
                            ConnectionDB.UpdateAccount(model, Sid);

                            this.ltMsg.Text = "修改成功";
                            this.ltMsg.Visible = true;
                            return;
                        }
                        else
                        {
                            this.ltMsg.Text = "新密碼與新密碼不一致";
                            this.ltMsg.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        //如果新密碼為空值
                        if (string.IsNullOrWhiteSpace(newPassword))
                        {
                            this.ltMsg.Text = "請輸入新密碼";
                            this.ltMsg.Visible = true;
                            return;
                        }

                        //如果確認新密碼為空值
                        if (string.IsNullOrWhiteSpace(newPasswordCheck))
                        {
                            this.ltMsg.Text = "請輸入確認新密碼";
                            this.ltMsg.Visible = true;
                            return;
                        }

                        
                    }
                    
                }
                else
                {
                    this.ltMsg.Text = "舊密碼錯誤";
                    this.ltMsg.Visible = true;
                    return;
                }
            }
            
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            //按取消按鈕會轉跳至查詢頁面
            Response.Redirect("User_Account.aspx");
        }

        //確認刪除資料權限CheckBox是否勾選
        private int AccountLevelCheckBoxIsChecked()
        {
            bool CheckBoxAccountLevel = this.DeleteData_Authority.Checked;

            //確認刪除資料的CheckBox有沒有被勾選,並回傳相對應的使用者等級
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