using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using AdventureService.Models;
using System.Web.Http.Controllers;
using System.Linq;
using System.Data.Entity;
using System;
using System.Threading.Tasks;
using AdventureService.DataObjects;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace AdventureService.Controllers
{
    [MobileAppController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventInfoController : ApiController
    {
        private AdventureContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            context = new AdventureContext();
            base.Initialize(controllerContext);
        }

        [HttpGet]

        // GET api/EventInfo
        public IHttpActionResult Get()
        {
            IQueryable<EventInfo> eventInfos = null;

            eventInfos = context.Infos;

            if (eventInfos == null)
                return NotFound();

            try
            {
                var result = Helpers.MapperConfig.AdventureMapper.Map<List<EventInfoDto>>(eventInfos);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }

        public async Task<IHttpActionResult> AddCustomerToEventInfo(string id, [FromUri] string customerId)
        {           
        
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("CustomerId cannot be null");

            var customer = context.Customers.FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
                return NotFound();

            var eventInfo = context.Infos
                             .FirstOrDefault(ei => ei.Id == id);
            if (eventInfo == null)
                return BadRequest("cannot add customer to null EventInfo");

            var existingCustomer = eventInfo.Customers.FirstOrDefault(c => c.Id == customerId);
            if (existingCustomer != null)
                return BadRequest("Customer already booked in");

            eventInfo.Customers.Add(customer);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    
    }
}
