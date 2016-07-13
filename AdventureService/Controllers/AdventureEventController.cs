using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Linq;
using System.Data.Entity;
using AdventureService.Models;
using System.Web.Http.Controllers;
using System.Collections.Generic;
using System;
using AdventureService.DataObjects;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace AdventureService.Controllers
{
    /// <summary>
    /// The main class for an Adventure Trip eg Skydiving
    /// </summary>
    [MobileAppController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdventureEventController : ApiController
    {
        private AdventureContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            context = new AdventureContext();
            base.Initialize(controllerContext);
        }

        [HttpGet]
        // GET api/AdventureEvent
        public IHttpActionResult Get()
        {
            IQueryable<AdventureEvent> aevents = null;

            try
            {
                aevents = context.AdventureEvents
                                 .Include(e => e.EventInfos.Select(ei => ei.Customers))
                                 .Include(e => e.ExperienceExtras)
                                 .Include(e => e.Location);                                 

                if (aevents == null)
                    return NotFound();

                var results = Helpers.MapperConfig.AdventureMapper.Map<List<AdventureEventDto>>(aevents);
                return Ok(results);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/adventureEvent/{name}")]
        public IHttpActionResult Get(string name)
        {
            AdventureEvent aevent = null;

            try
            {
                aevent = context.AdventureEvents
                                
                                .Include(e => e.EventInfos.Select(ei => ei.Customers))
                                .Include(e => e.ExperienceExtras)
                                .Include(e => e.Location)
                                .First(e => e.Name.ToLower() == name.ToLower());

                if (aevent == null)
                    return NotFound();

                var result = Helpers.MapperConfig.AdventureMapper.Map<AdventureEventDto>(aevent);
                return Ok(result);
                       
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }        
    }
}
