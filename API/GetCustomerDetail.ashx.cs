using Newtonsoft.Json;
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
        public class CustomerDetail
        {
            public string Address { get; set; }

            public string Phone { get; set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var jsonSid = context.Request["Sid"];
            
            int Sid;
            if(!Int32.TryParse(jsonSid, out Sid))
            {
                return;
            }

            if(Sid == 0)
            {
                return;
            }
            ConnectionDB connectionDB = new ConnectionDB();
            DataTable dt = connectionDB.ReadSingleCustomer(Sid);

            string customerAddress = dt.Rows[0]["Address"].ToString();
            string customerPhone = dt.Rows[0]["Phone"].ToString();

           CustomerDetail josnString = new CustomerDetail();

            josnString.Address = customerAddress;
            josnString.Phone = customerPhone;

            string strJson = JsonConvert.SerializeObject(josnString, Formatting.Indented);
            context.Response.ContentType = "application/json";
            context.Response.Write(strJson);
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