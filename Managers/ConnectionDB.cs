﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Models;
using Yubay_Drone_team.Helpers;

namespace Yubay_Drone_team.Managers
{
    public class ConnectionDB : CreateHelper
    {
        public void Drone_Detail_Create(DroneMedel Model)
        {

            //使用的SQL語法
            string queryString = $@" INSERT INTO Drone_Detail (Drone_ID, Manufacturer, WeightLoad, Status, StopReason, Operator)
                                        VALUES (@Drone_ID, @Manufacturer, @WeightLoad, @Status, @StopReason, @Operator);";

            //建立連線


            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Drone_ID", Model.Drone_ID),
                   new SqlParameter("@Manufacturer",Model.Manufacturer),
                   new SqlParameter("@WeightLoad",Model.WeightLoad),
                   new SqlParameter("@Status",Model.Status),
                   new SqlParameter("@StopReason", Model.StopReason),
                   new SqlParameter("@Operator", Model.Operator)

                };

            this.ExecuteNonQuery(queryString, parameters);

        }
        public DataTable ID_Checker(string Drone_ID)
        {

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Drone_Detail WHERE Drone_ID = @Sid";
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";
            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Sid", Drone_ID);

                try
                {
                    //開始連線
                    connection.Open();

                    //從資料庫中讀取資料
                    SqlDataReader reader = command.ExecuteReader();

                    //在記憶體中創新的空表
                    DataTable dt = new DataTable();

                    //把值塞進空表
                    dt.Load(reader);

                    reader.Close();

                    //回傳dt
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }



        }



        public void Drone_Detail_Update(DroneMedel Model)
        {

            //使用的SQL語法
            //string queryString = $@" INSERT INTO Drone_Detail (Drone_ID, Manufacturer, WeightLoad, Status, StopReason, Operator)
            //                            VALUES (@Drone_ID, @Manufacturer, @WeightLoad, @Status, @StopReason, @Operator);";

            string queryString = $@"UPDATE Drone_Detail SET  
                 Drone_ID = @Drone_ID, Manufacturer = @Manufacturer, WeightLoad = @WeightLoad, Status = @Status, 
                 StopReason= @StopReason, Operator = @Operator Where Sid = @Sid";



            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Drone_ID", Model.Drone_ID),
                   new SqlParameter("@Manufacturer",Model.Manufacturer),
                   new SqlParameter("@WeightLoad",Model.WeightLoad),
                   new SqlParameter("@Status",Model.Status),
                   new SqlParameter("@StopReason", Model.StopReason),
                   new SqlParameter("@Operator", Model.Operator),
                   new SqlParameter("@Sid",Model.Sid)


                };

            this.ExecuteNonQuery(queryString, parameters);

        }


        #region 查詢無人機資料的Method
        public static DataTable ReadDroneDetail()
        {
            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Drone_Detail WHERE IsDelete IS NULL;";

            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //轉譯成SQL看得懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@NumberCol", "2");

                try
                {
                    //開始連線
                    connection.Open();

                    //從資料庫中讀取資料
                    SqlDataReader reader = command.ExecuteReader();

                    //在記憶體中創新的空表
                    DataTable dt = new DataTable();

                    //把值塞進空表
                    dt.Load(reader);


                    //關閉資料庫連線
                    reader.Close();

                    //回傳dt
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

                //finally
                //{
                //    connection.Close();
                //}
            }
        }
        #endregion
        #region 刪除無人機資料的Method
        public void DelectDroneDetail(DroneMedel Model)
        {

            //使用的SQL語法

            string queryString = $@"UPDATE Drone_Detail SET Drone_ID = @Drone_ID, Deleter=@Deleter, DeleteDate= @DeleteDate , IsDelete = 'true' Where Sid = @Sid";



            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Model.Sid),
                   new SqlParameter("@Deleter", Model.Deleter),
                   new SqlParameter("@Drone_ID", $"{Model.Drone_ID}_Deleted_{Model.Sid}"),
                   new SqlParameter("@DeleteDate",DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);


        }
        #endregion
        #region 關鍵字模糊查詢
        public static DataTable KeyWordSearchDroneDestination(string WantSearch, string KeyWord)
        {

            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Drone_Detail  WHERE {WantSearch} LIKE @KeyWord ORDER BY {WantSearch} ASC;";

            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //轉譯成SQL看得懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue($"@KeyWord", "%" + KeyWord + "%");


                try
                {
                    //開始連線
                    connection.Open();

                    //從資料庫中讀取資料
                    SqlDataReader reader = command.ExecuteReader();

                    //在記憶體中創新的空表
                    DataTable dt = new DataTable();

                    //把值塞進空表
                    dt.Load(reader);

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    Console.WriteLine(
                    //        "\t{0}\t{1}\t{2}",
                    //        dr["ID"],
                    //        dr["Birthday"],
                    //        dr["Name"]
                    //    );
                    //}

                    //關閉資料庫連線
                    reader.Close();

                    //回傳dt
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

                //finally
                //{
                //    connection.Close();
                //}
            }
        }
        #endregion
        public static DataTable UpdateOnlyoneDroneDetail(string sid)
        {
            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Drone_Detail Where Sid=@Sid;";

            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //轉譯成SQL看得懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Sid", sid);

                try
                {
                    //開始連線
                    connection.Open();

                    //從資料庫中讀取資料
                    SqlDataReader reader = command.ExecuteReader();

                    //在記憶體中創新的空表
                    DataTable dt = new DataTable();

                    //把值塞進空表
                    dt.Load(reader);

                    reader.Close();

                    //回傳dt
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }


            }
        }



        #region 讀取管理者 

        public DataTable ReadUserAccount(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {                                   //總筆數        //搜尋條件         //關鍵字              //當前點選頁數           //一頁幾筆資料             

            string keyWordSearchString;
            //如果搜尋條件、關鍵字不是空值或是空白
            if (!string.IsNullOrWhiteSpace(wantSearch) && !string.IsNullOrWhiteSpace(searchKeyWord))
            {
                //去找輸入搜尋條件的值
                keyWordSearchString = $"AND {wantSearch} Like @{wantSearch} ";
            }
            else
            {
                //就不做搜尋
                keyWordSearchString = string.Empty;
            }

            string queryString = $@" SELECT TOP 10 * FROM 
                                        (SELECT *,ROW_NUMBER() OVER (ORDER BY [Sid]) AS ROWSID FROM UserAccount)
                                        a WHERE ROWSID > {pageSize * (currentPage - 1)} AND SuperAccount = 'False' AND (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM UserAccount
                    WHERE SuperAccount = 'False' AND IsDelete IS NULL {keyWordSearchString};";


            List<SqlParameter> dbParameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(wantSearch) && !string.IsNullOrWhiteSpace(searchKeyWord))
            {
                dbParameters.Add(new SqlParameter($"@{wantSearch}", "%" + searchKeyWord + "%"));
            }

            var dt = this.GetDataTable(queryString, dbParameters);

            var dataCount = this.GetScale(countQuery, dbParameters) as int?;

            TotalSize = (dataCount.HasValue) ? dataCount.Value : 0;

            return dt;

        }
        #endregion


        #region 下拉式選單(負責人員)
        public static DataTable DropDownListRead()
        {
            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM UserAccount;";

            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //轉譯成SQL看得懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@NumberCol", "2");

                try
                {
                    //開始連線
                    connection.Open();

                    //從資料庫中讀取資料
                    SqlDataReader reader = command.ExecuteReader();

                    //在記憶體中創新的空表
                    DataTable dt = new DataTable();

                    //把值塞進空表
                    dt.Load(reader);
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    Console.WriteLine(
                    //        "\t{0}\t{1}\t{2}",
                    //        dr["ID"],
                    //        dr["Birthday"],
                    //        dr["Name"]
                    //    );
                    //}

                    //關閉資料庫連線
                    reader.Close();

                    //回傳dt
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

                //finally
                //{
                //    connection.Close();
                //}
            }
        }
        #endregion


        //public int GetUserLevel(int Sid)
        //{
        //    string queryString = $@" SELECT AccountLevel FROM UserAccount WHERE Sid = @Sid;";

        //    List<SqlParameter> dbParameters = new List<SqlParameter>() 
        //    {
        //        new SqlParameter("@Sid", Sid),
        //    };


        //    var dt =Convert.ToInt32( this.GetScale(queryString, dbParameters));

        //    return dt;
        //}

        #region 刪除使用者帳號(未完成)

        public void DeleteUserAccount(int Sid, string Account)
        {

            string queryString = $@"UPDATE UserAccount SET Account = @Account, IsDelete = 'true' Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Sid", Sid),
                   new SqlParameter("@Account", $"{Account}_Deleted_{Sid}")
                };

            this.ExecuteNonQuery(queryString, parameters);

        }
        #endregion
        public static DataTable ReadCustomerDetail()
        {
            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Customer WHERE Deleter IS NULL;";

            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //轉譯成SQL看得懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);


                try
                {
                    //開始連線
                    connection.Open();

                    //從資料庫中讀取資料
                    SqlDataReader reader = command.ExecuteReader();

                    //在記憶體中創新的空表
                    DataTable dt = new DataTable();

                    //把值塞進空表
                    dt.Load(reader);

                    //關閉資料庫連線
                    reader.Close();

                    //回傳dt
                    return dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

              
            }
        }

        #region 新增使用者帳號

        public void CreateUserAccount(AccountModel model)
        {
            //使用的SQL語法
            string queryString = $@" INSERT INTO UserAccount (Account, Password, SuperAccount, UserName, AccountLevel)
                                        VALUES (@Account, @Password, @SuperAccount, @UserName, @AccountLevel);";

            //建立連線


            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Account", model.Account),
                   new SqlParameter("@Password",model.Password),
                   new SqlParameter("@SuperAccount",model.SuperAccount),
                   new SqlParameter("@UserName",model.UserName),
                   new SqlParameter("@AccountLevel", model.AccountLevel)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 讀取單筆User帳號

        public DataTable ReadSingleUserAccount(int Sid)
        {
            string queryString = $@" SELECT Account, [Password], UserName, AccountLevel FROM UserAccount Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion


        #region 確認舊帳號
        public bool checkOldPassword(int Sid, string oldPassword)
        {
            string queryString = $@"SELECT [Password] FROM UserAccount Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);

            if (string.Compare(data.Rows[0]["Password"].ToString(), oldPassword) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion


        #region 修改帳號
        public void UpdateAccount(AccountModel model, int sid)
        {
            string queryString = $@"UPDATE UserAccount SET [Password] = @Password, UserName = @UserName, AccountLevel = @AccountLevel, Updater = @Updater, UpdateDate = @UpdateDate Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", sid),
                   new SqlParameter("@Password", model.Password),
                   new SqlParameter("@UserName", model.UserName),
                   new SqlParameter("@AccountLevel", model.AccountLevel),
                   new SqlParameter("@Updater", model.Updater),
                   new SqlParameter("@UpdateDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);
        } 
        #endregion
    }

}