using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Models
{
    public class HealthProblem : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}