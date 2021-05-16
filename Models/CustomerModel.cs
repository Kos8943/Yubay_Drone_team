using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class CustomerModel
    {
        public int Sid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Crop { get; set; }
        public string Area { get; set; }
        public string Farm_Address { get; set; }
        public string Updater { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public string Deleter { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}