using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.DataObjects
{
    public class EventInfoDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool Available { get; set; } = true;
        public int MaximumPlaces { get; set; } = 0;
        public int PlacesTaken { get; set; } = 0;
        public ICollection<CustomerDto> Customers { get; set; }
        public int PlacesLeft { get; set; } = 0;
    }
}