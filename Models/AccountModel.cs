using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class AccountModel
    {
        public int Sid { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public bool SuperAccount { get; set; }

        public string UserName { get; set; }

        public int AccountLevel { get; set; }

        public string Updater { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsDelete { get; set; }

        public string Deleter { get; set; }

        public DateTime DeleteDate { get; set; }
 
    }
}