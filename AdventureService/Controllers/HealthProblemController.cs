using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Web.Http.Cors;
using System.Linq;
using AdventureService.Models;
using System.Web.Http.Controllers;
using AdventureService.DataObjects;
using System.Collections.Generic;
using System;

namespace AdventureService.Controllers
{
    [MobileAppController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HealthProblemController : ApiController
    {
        private AdventureContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            context = new AdventureContext();
            base.Initialize(controllerContext);
        }

        // GET api/HealthProblem
        [HttpGet]
        public IHttpActionResult Get()
        {
            IQueryable<HealthProblem> healthProblems;

            try
            {
                healthProblems = context.HealthProblems;

                if (healthProblems == null)
                    return NotFound();

                var results = Helpers.MapperConfig.AdventureMapper.Map<List<HealthProblemDto>>(healthProblems);
                return Ok(results);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
