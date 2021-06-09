using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Models;

namespace Yubay_Drone_team.Managers
{
    public class AccountManager : CreateHelper
    {

        public AccountModel GetAccount(string Account)
        {
            
            string queryString =
                $@" SELECT * FROM UserAccount
                    WHERE Account = @Account
                ";

            List<SqlParameter> parameters = new List<SqlParameter>()

                {
                   new SqlParameter("@Account", Account)
                };

            var dt = this.GetDataTable(queryString, parameters);

            AccountModel model = new AccountModel();

            if (dt.Rows.Count > 0)
            {

                if (!Convert.IsDBNull(dt.Rows[0]["Sid"]))
                {
                    model.Sid = (int)dt.Rows[0]["Sid"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Account"]))
                {
                    model.Account = (string)dt.Rows[0]["Account"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["Password"]))
                {
                    model.Password = (string)dt.Rows[0]["Password"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["AccountLevel"]))
                {
                    model.AccountLevel = (int)dt.Rows[0]["AccountLevel"];
                }

                if (!Convert.IsDBNull(dt.Rows[0]["UserName"]))
                {
                    model.UserName = (string)dt.Rows[0]["UserName"];
                }
            }
            return model;

  
        }

    }
}