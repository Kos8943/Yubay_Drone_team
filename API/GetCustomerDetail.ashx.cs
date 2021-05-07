using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team.API
{
    /// <summary>
    /// GetCustomerDetail 的摘要描述
    /// </summary>
    public class GetCustomerDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var jsonSid = context.Request["Sid"];
            
            int Sid;
            if(!Int32.TryParse(jsonSid, out Sid))
            {
                return;
            }
            ConnectionDB connectionDB = new ConnectionDB();
            DataTable dt = connectionDB.ReadSingleCustomer(Sid);

            string customerName = dt.Rows[0]["Name"].ToString();
            string customerPhone = dt.Rows[0]["Phone"].ToString();
            context.Response.ContentType = "application/json";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}