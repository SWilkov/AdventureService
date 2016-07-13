using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Models
{
    public class AdventureEvent : EntityData
    {
        public string Name { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }       
        public string LocationId { get; set; }
        public Location Location { get; set; }        
        public ICollection<EventInfo> EventInfos { get; set; }       
        public ICollection<ExperienceExtra> ExperienceExtras { get; set; }

    }
}