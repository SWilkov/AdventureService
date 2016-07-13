using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.DataObjects
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }
        public ICollection<EventInfoDto> BookedEvents { get; set; }
        public ICollection<HealthProblemDto> HealthProblems { get; set; }
    }
}