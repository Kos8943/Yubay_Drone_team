using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team.Managers
{
    public class AccountManager
    {

        public AccountModel GetAccount(string Account)
        {
            string connectionString = "Data Source=localhost\\SQLExpress;Initial Catalog=Yubay_Drone; Integrated Security=true";
            string queryString =
                $@" SELECT * FROM UserAccount
                    WHERE Account = @Account
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Account", Account);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    AccountModel model = null;

                    while (reader.Read())
                    {
                        model = new AccountModel();
                        model.Sid = (int)reader["Sid"];
                        model.Account = (string)reader["Account"];
                        model.Password = (string)reader["Password"];
                        model.AccountLevel = (int)reader["AccountLevel"];
                        model.UserName = (string)reader["UserName"];
                    }

                    reader.Close();

                    return model;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }



            //AccountModel model = null;
            //return model;

        }

    }
}