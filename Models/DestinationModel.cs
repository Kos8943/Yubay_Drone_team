using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Models
{
    public class DestinationModel
    {
        public int Sid { get; set; }

        public string Date { get; set; }

        public string Staff { get; set; }

        public string Drone_ID { get; set; }

        public string Battery_Count { get; set; }

        public string Customer_Name { get; set; }

        public string Customer_Phone { get; set; }

        public string Customer_Address { get; set; }

        public int Customer_Sid { get; set; }

        public string Remarks { get; set; }

        public string Pesticide { get; set; }

        public string Pesticide_Date { get; set; }

        public string Updater { get; set; }

        public string UpdateDate { get; set; }

        public bool IsDelete { get; set; }

        public string Deleter { get; set; }

        public string DeleteDate { get; set; }
      
    }
}