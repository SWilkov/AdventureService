using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using AdventureService.Models;
using System.Web.Http.Controllers;
using AdventureService.DataObjects;
using AutoMapper;
using AdventureService.Helpers;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace AdventureService.Controllers
{
    [MobileAppController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private AdventureContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            context = new AdventureContext();
            base.Initialize(controllerContext);
        }

        [HttpGet]
        // GET api/Customer
        public IHttpActionResult Get()
        {
            IQueryable<Customer> customers = null;

            try
            {
                customers = context.Customers
                                   .Include(c => c.HealthProblems)
                                   .Include(c => c.BookedEvents);

                if (customers == null)
                    return NotFound();
                                
                var result = MapperConfig.AdventureMapper.Map<List<CustomerDto>>(customers);
                return Ok(result);
            }
            catch(Exception e)
            {
                // TODO: log error to database
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] CustomerDto data)
        {
            if (data == null)
                return NotFound();

            var healthProblems = new List<HealthProblem>();
            var eventInfos = new List<EventInfo>();         

            if (data.HealthProblems != null)
            {
               foreach(var healthProblem in data.HealthProblems)
                {
                    var dbHp = context.HealthProblems.FirstOrDefault(hp => hp.Id == healthProblem.Id);
                    if (dbHp != null)
                        healthProblems.Add(dbHp);
                }
            }
             
            if (data.BookedEvents != null)
            {
                foreach(var info in data.BookedEvents)
                {
                    var dbei = context.Infos.FirstOrDefault(ei => ei.Id == info.Id);
                    if (dbei != null)
                    {
                        dbei.PlacesTaken++;
                        eventInfos.Add(dbei);
                    }
                }
            }           

            var customer = MapperConfig.AdventureMapper.Map<Customer>(data);
            if (customer == null)
                return BadRequest("Mapping failed for Customer");

            customer.HealthProblems = healthProblems;
            customer.BookedEvents = eventInfos;

            context.Customers.Add(customer);

            try
            {
                await context.SaveChangesAsync();
                var result = Helpers.MapperConfig.AdventureMapper.Map<Customer, CustomerDto>(customer, data);
                return Created("customer", data);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }            
        }
    }
}
