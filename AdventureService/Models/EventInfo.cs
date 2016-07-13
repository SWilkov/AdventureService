using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Models
{
    public class EventInfo: EntityData
    {
        public DateTime Date { get; set; }       
        
        public int MaximumPlaces { get; set; }
        public int PlacesTaken { get; set; } = 0;
        public string AdventureEventId { get; set; }
        public AdventureEvent AdventureEvent { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}