using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team.Managers
{
    public class DroneManager
    {
        public static DataTable ReadDroneDetail()
        {
            {

                //建立連線資料庫的字串變數Catalog=Drone的Drone為資料庫名稱
                string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";

                //使用的SQL語法
                string queryString = $@" SELECT * FROM Drone_Detail ORDER BY Sid ASC;";

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

                        dt.Load(reader);

                        //把值塞進空表

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
        }
    }
}