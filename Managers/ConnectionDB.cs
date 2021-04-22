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
    }
}