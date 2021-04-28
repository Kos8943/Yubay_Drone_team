using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yubay_Drone_team.Helpers;
using Yubay_Drone_team.Managers;
using System.Web.SessionState;

namespace Yubay_Drone_team.API
{
    /// <summary>
    /// GetUserLevel 的摘要描述
    /// </summary>
    public class GetUserLevel : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write(context.Request["IsLogined"]);

            var level = context.Session["IsLogined"];

            //string Sid = context.Request.Form["Sid"];

            //var GetSession = context.Request["IsLogined"];





            //var UserLevel = GetSession.AccountLevel.ToString();

            if (String.IsNullOrWhiteSpace("123"))
            {
                context.Response.StatusCode = 400;
                context.Response.Write(" Sid is required.");

                return;
            }
            else
            {
                //ConnectionDB DB= new ConnectionDB();

                //var levle = DB.GetUserLevel(Convert.ToInt32(Sid));

                string retText = Newtonsoft.Json.JsonConvert.SerializeObject(level);
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