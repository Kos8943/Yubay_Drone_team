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
            string queryString = $@" SELECT * FROM Drone_Detail;";

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
        #region 刪除無人機資料的Method
        public static void DelectDroneDetail(string Sid)
        {

            //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

            //使用的SQL語法
            string queryString = $@"DELETE FROM Drone_Detail WHERE Sid = @Sid";
            //DELETE FROM TestTable_1 WHERE ID



            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //轉譯成SQL看得懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);

                //將值丟進相對應的位子
                command.Parameters.AddWithValue("@Sid", Sid);



                try
                {
                    //開始連線
                    connection.Open();

                    //受影響的資料筆數(沒有使用)
                    int totalChangRows = command.ExecuteNonQuery();
                    //HttpContext.Current.Response.Write("Total chang" + totalChangRows + " Rows.");
                    //Console.WriteLine("Total chang" + totalChangRows + " Rows.");

                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex);
                    //Console.WriteLine(ex);
                }

            }
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
        public DataTable ReadUserAccount()
        {

            string queryString = $@" SELECT * FROM UserAccount;";

            List<SqlParameter> dbParameters = new List<SqlParameter>();

            var dt = this.GetDataTable(queryString, dbParameters);

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


        public int GetUserLevel(int Sid)
        {
            string queryString = $@" SELECT AccountLevel FROM UserAccount WHERE Sid = @Sid;";

            List<SqlParameter> dbParameters = new List<SqlParameter>() 
            {
                new SqlParameter("@Sid", Sid),
            };

            
            var dt =Convert.ToInt32( this.GetScale(queryString, dbParameters));

            return dt;
        }
    }

}