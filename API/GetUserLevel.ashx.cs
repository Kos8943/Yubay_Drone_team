using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Managers;

namespace Yubay_Drone_team.API
{
    /// <summary>
    /// GetUserLevel 的摘要描述
    /// </summary>
    public class GetUserLevel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string Sid = context.Request.Form["Sid"];

            if (String.IsNullOrWhiteSpace(Sid))
            {
                context.Response.StatusCode = 400;
                context.Response.Write(" Sid is required.");

                return;
            }
            else
            {
                ConnectionDB DB= new ConnectionDB();

                var levle = DB.GetUserLevel(Convert.ToInt32(Sid));

                string retText = Newtonsoft.Json.JsonConvert.SerializeObject(levle);
                context.Response.ContentType = "text/json";
                context.Response.Write(retText);
            }




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