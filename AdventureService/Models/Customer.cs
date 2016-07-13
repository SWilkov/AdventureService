using AdventureService.Helpers;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Models
{
    public class Customer : EntityData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public ICollection<EventInfo> BookedEvents { get; set; }
        public ICollection<HealthProblem> HealthProblems { get; set; }
    }
}