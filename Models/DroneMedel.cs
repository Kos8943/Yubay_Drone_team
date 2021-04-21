using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class DroneMedel
    {
        public int Sid { get; set; }

        public string Drone_ID { get; set; }

        public string WeightLoad { get; set; }
        public string Status { get; set; }
        public string StopReason { get; set; }
        public string Operator { get; set; }
        public string Updater { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public string Delete { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}