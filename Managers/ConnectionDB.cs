using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Models;
using Yubay_Drone_team.Helpers;

namespace Yubay_Drone_team.Managers
{
    public class ConnectionDB:CreateHelper
    {
        public void Drone_Detail_Create(DroneMedel Model)
        {
            
            //使用的SQL語法
            string queryString = $@" INSERT INTO Drone_Detail (Drone_ID, Manufacturer, WeightLoad, Status, StopReason, Operator)
                                        VALUES (@Drone_ID, @Manufacturer, @WeightLoad, @Status, @StopReason, @Operator);";

            //建立連線

                    //開始連線

            //轉換成SQL可讀懂的語法
            //SqlCommand command = new SqlCommand(queryString, connection);
            //command.Parameters.AddWithValue("@Drone_ID", Drone_ID);
            //command.Parameters.AddWithValue("@Manufacturer", Manufacturer);
            //command.Parameters.AddWithValue("@WeightLoad", WeightLoad);
            //command.Parameters.AddWithValue("@Status", Status);
            //command.Parameters.AddWithValue("@StopReason", StopReason);
            //command.Parameters.AddWithValue("@Operator", Operator);
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
    }
}