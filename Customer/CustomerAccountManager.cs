using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team.Customer
{
    public class CustomerAccountManager
    {
        #region 讀取Customer資料
        public DataTable ReadCustomerDetail(out int TotalSize, string wantSearch, string searchKeyWord, int currentPage = 1, int pageSize = 10)
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
        public DataTable GetDataTable(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = GetConnectionString();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);
                command.Parameters.AddRange(parameters.ToArray());

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    return dt;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public object GetScale(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = GetConnectionString();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);

                List<SqlParameter> parameters2 = new List<SqlParameter>();
                foreach (var item in parameters)
                {
                    parameters2.Add(new SqlParameter(item.ParameterName, item.Value));
                }

                command.Parameters.AddRange(parameters2.ToArray());

                try
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        private string GetConnectionString()
        {
            var manage = System.Configuration.ConfigurationManager.ConnectionStrings["systemDataBase"];

            if (manage == null)
                return string.Empty;
            else
                return manage.ConnectionString;
        }

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

        public int ExecuteNonQuery(string dbCommand, List<SqlParameter> parameters)
        {
            string connectionString = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommand, connection);

                List<SqlParameter> parameters2 = new List<SqlParameter>();
                foreach (var item in parameters)
                {
                    parameters2.Add(new SqlParameter(item.ParameterName, item.Value));

                }

                command.Parameters.AddRange(parameters2.ToArray());

                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                command.Transaction = sqlTransaction;

                try
                {
                    int totalChange = command.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    return totalChange;

                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();

                    throw;
                }

            }




        }
    }
}