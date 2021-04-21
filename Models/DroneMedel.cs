using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class DroneMedel
    {
        public int Number { get; set; }

        public string Manufacturer { get; set; }

        public string Weight { get; set; }
        public string Status { get; set; }
        public string Deactive { get; set; }
        public string Person { get; set; }
    }
}