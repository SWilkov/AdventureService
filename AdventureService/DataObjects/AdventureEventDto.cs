using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.DataObjects
{
    public class AdventureEventDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }        
        public ICollection<EventInfoDto> EventInfos { get; set; }
        public ICollection<ExperienceExtraDto> ExperienceExtras { get; set; }
    }
}