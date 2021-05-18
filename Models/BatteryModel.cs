using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class BatteryModel
    {

        public int Sid { get; set; }
        public string Battery_ID { get; set; }
        public string status { get; set; }
        public string stopReason { get; set; }
        public string Updater { get; set; }   
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public string Deleter { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}