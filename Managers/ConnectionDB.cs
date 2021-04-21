using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Managers
{
    public class ConnectionDB
    {
        public void Drone_Detail_Create(string Drone_ID, string Manufacturer, string WeightLoad, string Status, string StopReason, string Operator)
        {
            //建立資料庫字串變數
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";
            //使用的SQL語法
            string queryString = $@" INSERT INTO Drone_Detail (Drone_ID, Manufacturer, WeightLoad, Status, StopReason, Operator)
                                        VALUES (@Drone_ID, @Manufacturer, @WeightLoad, @Status, @StopReason, @Operator);";

            //建立連線
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //轉換成SQL可讀懂的語法
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Drone_ID", Drone_ID);
                command.Parameters.AddWithValue("@Manufacturer", Manufacturer);
                command.Parameters.AddWithValue("@WeightLoad", WeightLoad);
                command.Parameters.AddWithValue("@Status", Status);
                command.Parameters.AddWithValue("@StopReason", StopReason);
                command.Parameters.AddWithValue("@Operator", Operator);

                try
                {
                    //開始連線
                    connection.Open();
                   
                    //關閉資料庫連線
                    connection.Close();
                    //回傳dt
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    

                }

            }
        }
    }
}