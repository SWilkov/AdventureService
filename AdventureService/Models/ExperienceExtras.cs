using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Models
{
    public class ExperienceExtra : EntityData
    {
        public string Content { get; set; }
        public int Order { get; set; }
        public string AdventureEventId { get; set; }
        public AdventureEvent AdventureEvent { get; set; }
    }
}