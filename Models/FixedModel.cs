using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class FixedModel
    {
        public int Sid { get; set; }

        public string Drone_ID { get; set; }

        public string FixChange { get; set; }

        public string Updater { get; set; }

        public DateTime StopDate { get; set; }

        public string SendDate { get; set; }

        public string FixVendor { get; set; }

        public string StopReason { get; set; }

        public string Remarks { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsDelete { get; set; }

        public string Deleter { get; set; }

        public DateTime DeleteDate { get; set; }
    }
}