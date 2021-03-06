using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Yubay_Drone_team.Models;
using Yubay_Drone_team.Helpers;

namespace Yubay_Drone_team.Managers
{
    public class ConnectionDB : CreateHelper
    {
        #region 新增無人機功能
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

        #endregion

        #region 讀取無人機的Method
        public DataTable Read_Drone_Detail(string Drone_ID)
        {

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Drone_Detail WHERE Drone_ID = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Drone_ID),
                };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;

      

        }
        #endregion


        #region 修改無人機功能
        public void Drone_Detail_Update(DroneMedel Model ,int Sid)
        {

            //使用的SQL語法
          
            string queryString = $@"UPDATE Drone_Detail SET  
                 Drone_ID = @Drone_ID, Manufacturer = @Manufacturer, WeightLoad = @WeightLoad, Status = @Status, 
                 StopReason= @StopReason, Operator = @Operator , Updater = @Updater, UpdateDate = @UpdateDate Where Sid = @Sid";



            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Drone_ID", Model.Drone_ID),
                   new SqlParameter("@Manufacturer",Model.Manufacturer),
                   new SqlParameter("@WeightLoad",Model.WeightLoad),
                   new SqlParameter("@Status",Model.Status),
                   new SqlParameter("@StopReason", Model.StopReason),
                   new SqlParameter("@Operator", Model.Operator),
                   new SqlParameter("@Sid",Model.Sid),
                   new SqlParameter("@Updater", Model.Updater),
                   new SqlParameter("@UpdateDate", DateTime.Now)

                };

            this.ExecuteNonQuery(queryString, parameters);

        }
        #endregion

        #region 查詢無人機資料的Method
        public DataTable ReadDroneDetail(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {                                   //總筆數        //搜尋條件         //關鍵字              //當前點選頁數           //一頁幾筆資料
            string keyWordSearchString;
            //如果搜尋條件、關鍵字不是空值或是空白
            if (!string.IsNullOrWhiteSpace(wantSearch) && !string.IsNullOrWhiteSpace(searchKeyWord))
            {
                //去找輸入搜尋條件的值
                keyWordSearchString = $"{wantSearch} Like @{wantSearch} AND";
            }
            else
            {
                //就不做搜尋
                keyWordSearchString = string.Empty;
            }

           

            string queryString =$@" SELECT TOP 10 * FROM
                                   (SELECT *,ROW_NUMBER() OVER(ORDER BY [Sid]) AS ROWSID FROM Drone_Detail WHERE {keyWordSearchString} (IsDelete IS NULL OR IsDelete = 'false') )
                                    a WHERE ROWSID > {pageSize * (currentPage - 1)};";

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM Drone_Detail
                    WHERE {keyWordSearchString} (IsDelete IS NULL OR IsDelete = 'false');";


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

        #region 刪除無人機資料的Method
        public void DelectDroneDetail(int Sid, string UserName)
        {

            //使用的SQL語法

            string queryString = $@"UPDATE Drone_Detail SET Deleter=@Deleter, DeleteDate= @DeleteDate , IsDelete = 'true' Where Sid = @Sid";



            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid),
                   new SqlParameter("@Deleter", $"{UserName}"),
                   new SqlParameter("@DeleteDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);


        }
        #endregion


        #region 讀取無人機管理
        public DataTable Select_DroneDetail(string sid)
        {
            ////建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            //string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Drone_Detail Where Sid=@Sid ORDER BY Sid ";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", sid)
                };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;

         
        }
        #endregion


        #region 讀取管理者 

        public DataTable ReadUserAccount(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {                                   //總筆數        //搜尋條件         //關鍵字              //當前點選頁數           //一頁幾筆資料             

            string keyWordSearchString;
            //如果搜尋條件、關鍵字不是空值或是空白
            if (!string.IsNullOrWhiteSpace(wantSearch) && !string.IsNullOrWhiteSpace(searchKeyWord))
            {
                //去找輸入搜尋條件的值
                keyWordSearchString = $"{wantSearch} Like @{wantSearch} AND";
            }
            else
            {
                //就不做搜尋
                keyWordSearchString = string.Empty;
            }

            //string queryString = $@" SELECT TOP 10 * FROM 
            //                            (SELECT *,ROW_NUMBER() OVER (ORDER BY [Sid] ASC) AS ROWSID FROM UserAccount)
            //                            a WHERE ROWSID > {pageSize * (currentPage - 1)} AND SuperAccount = 'False' AND (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";


            string queryString = $@"SELECT TOP 10 * FROM
                                        (SELECT *, ROW_NUMBER() OVER(ORDER BY[Sid] ASC) AS ROWSID
                                        FROM UserAccount
                                        WHERE  {keyWordSearchString}　SuperAccount = 0 AND IsDelete IS NULL OR IsDelete = 'false') a 
                                        WHERE ROWSID > {pageSize * (currentPage - 1)} AND SuperAccount = 'False';";
            

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM UserAccount
                    WHERE {keyWordSearchString} SuperAccount = 0 AND IsDelete IS NULL ;";


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
        public DataTable DropDownListRead()
        {
            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            //string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@" SELECT * FROM UserAccount WHERE Deleter IS NULL;";

            List<SqlParameter> parameters = new List<SqlParameter>();

            var dt = this.GetDataTable(queryString, parameters);

            return dt;

      
        }
        #endregion

        #region 刪除使用者帳號

        public void DeleteUserAccount(int Sid, string Account, string UserName)
        {

            string queryString = $@"UPDATE UserAccount SET Account = @Account, Deleter = @Deleter, DeleteDate = @DeleteDate, IsDelete = 'true' Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Sid", Sid),
                   new SqlParameter("@Account", $"{Account}_Deleted_{Sid}"),
                   new SqlParameter("@Deleter", $"{UserName}"),
                   new SqlParameter("@DeleteDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);

        }
        #endregion

        #region 讀取客戶資料
      
        public DataTable ReadCustomerDetail(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {                                   //總筆數        //搜尋條件         //關鍵字              //當前點選頁數           //一頁幾筆資料             

                string keyWordSearchString;
                //如果搜尋條件、關鍵字不是空值或是空白
                if (!string.IsNullOrWhiteSpace(wantSearch) && !string.IsNullOrWhiteSpace(searchKeyWord))
                {
                    //去找輸入搜尋條件的值
                    keyWordSearchString = $"{wantSearch} Like @{wantSearch} AND";
                }
                else
                {
                    //就不做搜尋
                    keyWordSearchString = string.Empty;
                }

              

                string queryString = $@"SELECT TOP 10 * FROM
                                        (SELECT *, ROW_NUMBER() OVER(ORDER BY[Sid] ASC) AS ROWSID
                                        FROM Customer
                                        WHERE  {keyWordSearchString}　SuperAccount = 0 AND IsDelete IS NULL OR IsDelete = 'false') a 
                                        WHERE ROWSID > {pageSize * (currentPage - 1)} AND SuperAccount = 'False';";


                string countQuery =
                    $@" SELECT 
                        COUNT(Sid)
                    FROM Customer
                    WHERE {keyWordSearchString} SuperAccount = 0 AND IsDelete IS NULL ;";


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

        #region 讀取出勤紀錄 

        public DataTable ReadDestination(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {                                   //總筆數        //搜尋條件         //關鍵字              //當前點選頁數           //一頁幾筆資料             

            string keyWordSearchString;
            //如果搜尋條件、關鍵字不是空值或是空白
            if (!string.IsNullOrWhiteSpace(wantSearch) && !string.IsNullOrWhiteSpace(searchKeyWord))
            {
                //去找輸入搜尋條件的值
                keyWordSearchString = $" {wantSearch} Like @{wantSearch} AND";
            }
            else
            {
                //就不做搜尋
                keyWordSearchString = string.Empty;
            }

            string queryString = $@" SELECT TOP 10 * FROM 
                                        (SELECT Sid, [Date], Staff, Drone_ID, Battery_Count, Customer_Name, Customer_Phone, Customer_Address, Customer_Sid, Remarks, Pesticide, Pesticide_Date, IsDelete,ROW_NUMBER() OVER (ORDER BY [Sid]) AS ROWSID FROM Destination WHERE {keyWordSearchString} (IsDelete IS NULL OR IsDelete = 'false') )
                                        a WHERE ROWSID > {pageSize * (currentPage - 1)}  ;";

        



            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM Destination
                    WHERE  {keyWordSearchString} (IsDelete IS NULL OR IsDelete = 'false');";


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


        #region 刪除無人機出勤紀錄
        public void DeleteDestination(int Sid, string UserName)
        {

            string queryString = $@"UPDATE Destination SET Deleter = @Deleter, DeleteDate = @DeleteDate, IsDelete = 'true' Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Sid", Sid),
                   new SqlParameter("@Deleter", $"{UserName}"),
                   new SqlParameter("@DeleteDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);

        }
        #endregion


        #region 讀取單筆客戶地址跟電話

        public DataTable ReadSingleCustomer(int Sid)
        {
            string queryString = $@" SELECT [Name], Address, Phone  FROM Customer Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion

        #region 讀取全部無人機ID
        public DataTable ReadDrone_ID_Only()
        {
            string queryString = $@" SELECT Drone_ID FROM Drone_Detail WHERE (IsDelete IS NULL OR IsDelete = 'false')";

            List<SqlParameter> parameters = new List<SqlParameter>();


            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion

        #region 讀取客戶名單

        public DataTable ReadAllCustomerName()
        {
            string queryString = $@" SELECT [Name], Sid FROM Customer WHERE (IsDelete IS NULL OR IsDelete = 'false');";

            List<SqlParameter> parameters = new List<SqlParameter>();


            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion

        #region 讀取單筆出勤紀錄
        public DataTable ReadSingleDestination(int Sid)
        {
            string queryString = $@" SELECT Sid, [Date], Staff, Drone_ID, Battery_Count, Customer_Name, Customer_Phone, Customer_Address, Pesticide, Pesticide_Date, Customer_Sid, Remarks FROM Destination Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion

        #region 新增出勤紀錄

        public void CreateDestination(DestinationModel model)
        {
            //使用的SQL語法
            string queryString = $@" INSERT INTO Destination (Date, Staff, Drone_ID, Battery_Count, Customer_Name, Customer_Phone, Customer_Address, Customer_Sid, Pesticide, Pesticide_Date, Remarks)

                                        VALUES (@Date, @Staff, @Drone_ID, @Battery_Count, @Customer_Name, @Customer_Phone, @Customer_Address, @Customer_Sid, @Pesticide, @Pesticide_Date, @Remarks);";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Date", model.Date),
                   new SqlParameter("@Staff",model.Staff),
                   new SqlParameter("@Drone_ID",model.Drone_ID),
                   new SqlParameter("@Battery_Count",model.Battery_Count),
                   new SqlParameter("@Customer_Name", model.Customer_Name),
                   new SqlParameter("@Customer_Phone",model.Customer_Phone),
                   new SqlParameter("@Customer_Address",model.Customer_Address),
                   new SqlParameter("@Customer_Sid",model.Customer_Sid),
                   new SqlParameter("@Pesticide",model.Pesticide),
                   new SqlParameter("@Pesticide_Date",model.Pesticide_Date),
                   new SqlParameter("@Remarks",model.Remarks)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 修改出勤紀錄
        public void UpdateDestination(DestinationModel model, int sid)
        {
            string queryString = $@"UPDATE Destination SET [Date] = @Date, Staff = @Staff, Drone_ID = @Drone_ID, Battery_Count = @Battery_Count, Customer_Name = @Customer_Name, Customer_Phone = @Customer_Phone, Customer_Address = @Customer_Address, Pesticide = @Pesticide, Pesticide_Date = @Pesticide_Date, Remarks = @Remarks, Updater = @Updater, UpdateDate = @UpdateDate Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", sid),
                   new SqlParameter("@Date", model.Date),
                   new SqlParameter("@Staff", model.Staff),
                   new SqlParameter("@Drone_ID", model.Drone_ID),
                   new SqlParameter("@Battery_Count", model.Battery_Count),
                   new SqlParameter("@Customer_Name", model.Customer_Name),
                   new SqlParameter("@Customer_Phone", model.Customer_Phone),
                   new SqlParameter("@Customer_Address", model.Customer_Address),
                   new SqlParameter("@Pesticide", model.Pesticide),
                   new SqlParameter("@Pesticide_Date", model.Pesticide_Date),
                   new SqlParameter("@Remarks", model.Remarks),
                   new SqlParameter("@Updater", model.Updater),
                   new SqlParameter("@UpdateDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 確認User帳號重複

        public int CheckUserAccount(string account)
        {
            string queryString = $@" SELECT Account FROM UserAccount Where Account = @Account;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Account", account)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            int accuontCount = data.Rows.Count;
            return accuontCount;
        }
        #endregion

        #region 新增客戶資料管理

        public void CreateCustomer(CustomerModel model)
        {
            string queryString = $@" INSERT INTO Customer (Name, Address, Phone, Crop, Area, Farm_Address)

                                        VALUES (@Name, @Address, @Phone, @Crop, @Area, @Farm_Address);";
            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Name", model.Name),
                   new SqlParameter("@Address",model.Address),
                   new SqlParameter("@Phone",model.Phone),
                   new SqlParameter("@Crop",model.Crop),
                   new SqlParameter("@Area", model.Area),
                   new SqlParameter("@Farm_Address",model.Farm_Address),

                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 修改客戶資料
        public void UpdateCustomer(CustomerModel model, int sid)
        {
            string queryString = $@"UPDATE Customer 
                                SET Name = @Name, Address = @Address, Phone= @Phone, Crop = @Crop, Area = @Area ,Farm_Address=@Farm_Address , Updater = @Updater, UpdateDate = @UpdateDate 
                                Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", sid),
                   new SqlParameter("@Name", model.Name),
                   new SqlParameter("@Address", model.Address),
                   new SqlParameter("@Phone", model.Phone),
                   new SqlParameter("@Crop", model.Crop),
                   new SqlParameter("@Area", model.Area),
                   new SqlParameter("@Farm_Address", model.Farm_Address),
                   new SqlParameter("@Updater", model.Updater),
                   new SqlParameter("@UpdateDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 刪除客戶資料
        public void DeleteCustomer(CustomerModel Model)
        {

            string queryString =
                $@"UPDATE Customer 
                   SET  Name= @Name,  Deleter = @Deleter, DeleteDate = @DeleteDate, IsDelete = 'true' 
                   Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Sid", Model.Sid),
                   new SqlParameter("@Deleter", Model.Deleter),
                   new SqlParameter("@Name", Model.Name),
                   new SqlParameter("@DeleteDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);

        }
        #endregion

        #region 查詢客戶資料
        public DataTable Select_Customer(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {
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
                                        (SELECT *,ROW_NUMBER() OVER (ORDER BY [Sid] ASC) AS ROWSID FROM Customer)
                                        a WHERE ROWSID > {pageSize * (currentPage - 1)} AND (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM Customer
                    WHERE IsDelete IS NULL {keyWordSearchString};";


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

        #region 查詢無人機維修紀錄的Method
        public DataTable ReadFixed(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {                             //總筆數        //搜尋條件         //關鍵字              //當前點選頁數       //一頁幾筆資料
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
                                        (SELECT *,ROW_NUMBER() OVER (ORDER BY [Sid] ASC) AS ROWSID FROM Fixed)
                                        a WHERE ROWSID > {pageSize * (currentPage - 1)} AND (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM Fixed
                    WHERE (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";


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

        #region 讀取單筆無人機維修紀錄
        public DataTable ReadSingleFixed(int Sid)
        {
            string queryString = $@" SELECT Sid , Drone_ID, FixChange, StopDate, SendDate, FixVendor, StopReason, Remarks FROM Fixed Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion

        #region 新增維修紀錄

        public void CreateFixed(FixedModel model)
        {
            //使用的SQL語法
            string queryString = $@" INSERT INTO Fixed (Drone_ID, FixChange, StopDate,SendDate, FixVendor, StopReason, Remarks)

                                        VALUES (@Drone_ID, @FixChange, @StopDate, @SendDate, @FixVendor, @StopReason, @Remarks);";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Drone_ID",model.Drone_ID),
                   new SqlParameter("@FixChange",model.FixChange),
                   new SqlParameter("@StopDate", model.StopDate),
                   new SqlParameter("@SendDate",model.SendDate),
                   new SqlParameter("@FixVendor",model.FixVendor),
                   new SqlParameter("@StopReason",model.StopReason),
                   new SqlParameter("@Remarks",model.Remarks)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 修改維修紀錄
        public void UpdateFixed(FixedModel model, int sid)
        {
            string queryString = $@"UPDATE Fixed SET [Drone_ID] = @Drone_ID, FixChange = @FixChange, StopDate = @StopDate, SendDate = @SendDate, FixVendor = @FixVendor, StopReason = @StopReason, Remarks = @Remarks, Updater = @Updater, UpdateDate = @UpdateDate Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", sid),
                   new SqlParameter("@Drone_ID",model.Drone_ID),
                   new SqlParameter("@FixChange",model.FixChange),
                   new SqlParameter("@StopDate", model.StopDate),
                   new SqlParameter("@SendDate",model.SendDate),
                   new SqlParameter("@FixVendor",model.FixVendor),
                   new SqlParameter("@StopReason",model.StopReason),
                   new SqlParameter("@Remarks", model.Remarks),
                   new SqlParameter("@Updater", model.Updater),
                   new SqlParameter("@UpdateDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 讀取電池管理的Method
        public DataTable ReadBattery(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
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
                                        (SELECT *,ROW_NUMBER() OVER (ORDER BY [Sid] ASC) AS ROWSID FROM Battery)
                                        a WHERE ROWSID > {pageSize * (currentPage - 1)} AND SuperAccount = 'False' AND (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM Battery
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

        #region  刪除電池管理資料
        public void DeleteBattery(BatteryModel Model)
        {

            string queryString =
                $@"UPDATE Battery 
                   SET   Battery_ID = @Battery_ID , Deleter = @Deleter, DeleteDate = @DeleteDate, IsDelete = 'true' 
                   Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                   new SqlParameter("@Sid", Model.Sid),
                   new SqlParameter("@Deleter", Model.Deleter),
                   new SqlParameter("@Battery_ID", $"{Model.Battery_ID}_Deleted_{Model.Sid}"),
                   new SqlParameter("@DeleteDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);

        }

        #endregion

        #region 查詢電池管理的Method
        public DataTable SearchBattery(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
        {
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
                                        (SELECT *,ROW_NUMBER() OVER (ORDER BY [Sid] ASC) AS ROWSID FROM Battery)
                                        a WHERE ROWSID > {pageSize * (currentPage - 1)} AND (IsDelete IS NULL OR IsDelete = 'false') {keyWordSearchString};";

            string countQuery =
                $@" SELECT 
                        COUNT(Sid)
                    FROM Battery
                    WHERE IsDelete IS NULL {keyWordSearchString};";


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

        #region 刪除無人機維修紀錄的Method
        public void DelectFixed(FixedModel Model)
        {

            //使用的SQL語法

            string queryString = $@"UPDATE Fixed SET Deleter=@Deleter, DeleteDate= @DeleteDate , IsDelete = 'true' Where Sid = @Sid";



            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Model.Sid),
                   new SqlParameter("@Deleter", Model.Deleter),
                   new SqlParameter("@DeleteDate",DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);


        }
        #endregion

        #region 新增電池

        public void CreateBattery(BatteryModel model)
        {
            //使用的SQL語法
            string queryString = $@" INSERT INTO Battery (Battery_ID, status, stopReason)

                                        VALUES (@Battery_ID, @status, @stopReason);";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Battery_ID",model.Battery_ID),
                   new SqlParameter("@status",model.status),
                   new SqlParameter("@stopReason", model.stopReason)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 修改電池
        public void UpdateBattery(BatteryModel model, int sid)
        {
            string queryString = $@"UPDATE Battery SET [Battery_ID] = @Battery_ID, status = @status, stopReason = @stopReason, Updater = @Updater, UpdateDate = @UpdateDate Where Sid = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", sid),
                   new SqlParameter("@Battery_ID",model.Battery_ID),
                   new SqlParameter("@status",model.status),
                   new SqlParameter("@stopReason", model.stopReason),
                   new SqlParameter("@Updater", model.Updater),
                   new SqlParameter("@UpdateDate", DateTime.Now)
                };

            this.ExecuteNonQuery(queryString, parameters);
        }
        #endregion

        #region 查詢電池是否重複
        public DataTable BatteryID_Checker(string Battery_ID)
        {

            //使用的SQL語法
            string queryString = $@" SELECT * FROM Battery WHERE Battery_ID = @Sid";

            List<SqlParameter> parameters = new List<SqlParameter>() 
            {
                new SqlParameter("@Sid", Battery_ID)
            };

            var dt = this.GetDataTable(queryString, parameters);

            return dt;
            //string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";
            ////建立連線
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, connection);
            //    command.Parameters.AddWithValue("@Sid", Battery_ID);

            //    try
            //    {
            //        //開始連線
            //        connection.Open();

            //        //從資料庫中讀取資料
            //        SqlDataReader reader = command.ExecuteReader();

            //        //在記憶體中創新的空表
            //        DataTable dt = new DataTable();

            //        //把值塞進空表
            //        dt.Load(reader);

            //        reader.Close();

            //        //回傳dt
            //        return dt;
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        return null;
            //    }
            //}

        }
        #endregion

        #region 讀取單筆電池
        public DataTable ReadSingleBattery(int Sid)
        {
            string queryString = $@" SELECT Sid , Battery_ID, status, stopReason FROM Battery Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion

        #region 讀取單筆客戶資料
        public DataTable ReadSingleCustomer_Detail(int Sid)
        {
            string queryString = $@" SELECT * FROM Customer Where Sid = @Sid;";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Sid", Sid)
                };

            DataTable data = this.GetDataTable(queryString, parameters);
            return data;
        }
        #endregion
    }
}





