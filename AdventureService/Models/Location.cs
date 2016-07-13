using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Models
{
    public class Location : EntityData
    {
        public string HouseName { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string TownCity { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
    }
}