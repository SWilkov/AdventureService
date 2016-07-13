using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using AdventureService.Models;
using Owin;
using System.Linq;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;

namespace AdventureService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            config.EnableCors();
           
            config.Formatters.JsonFormatter
                             .SerializerSettings
                             .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //Automapper setup
            Helpers.MapperConfig.RegisterMappers();

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new AdventureServiceInitializer());

            var migrator = new DbMigrator(new Migrations.Configuration());
            migrator.Update();

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class AdventureServiceInitializer : CreateDatabaseIfNotExists<AdventureContext>
    {
        protected override void Seed(AdventureContext context)
        {           

            // Populate our table with some data including
            // Chronic Health Problems
            // TODO: admin section on website to create/update/delete if needed
            var healthProblems = new List<HealthProblem>
            {
                new HealthProblem { Id = Guid.NewGuid().ToString(), Name = "None", Description = "" },
                new HealthProblem { Id = Guid.NewGuid().ToString(), Name = "Heart Attack", Description = "" },
                new HealthProblem { Id = Guid.NewGuid().ToString(), Name = "Stroke", Description = "" },
                new HealthProblem { Id = Guid.NewGuid().ToString(), Name = "Cancer", Description = "" },
                new HealthProblem { Id = Guid.NewGuid().ToString(), Name = "Depression", Description = "" },
                new HealthProblem { Id = Guid.NewGuid().ToString(), Name = "Diabetes", Description = "" }
            };

            foreach (var hp in healthProblems)
                context.Set<HealthProblem>().Add(hp);

            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Christian",
                    LastName = "Erikksen",
                    Age = 23,
                    Gender = "Male",
                    Weight = 84.0, 
                    HealthProblems = new List<HealthProblem>
                    {
                        healthProblems.FirstOrDefault(hp => hp.Name == "Stroke")
                    }
                }
            };

            foreach (var customer in customers)
            {
                context.Set<Customer>().Add(customer);
            }

            var eventInfos = new List<EventInfo>
            {
                new EventInfo
                        {
                            Id = Guid.NewGuid().ToString(),
                            MaximumPlaces = 6,
                            Date = DateTime.Now.AddDays(10)
                        },
                        new EventInfo
                        {
                            Id = Guid.NewGuid().ToString(),
                            MaximumPlaces = 1,
                            Date = DateTime.Now.AddDays(4),
                            PlacesTaken = 1,
                            Customers = new List<Customer>
                            {
                                customers.First()
                            }
                        },
                        new EventInfo
                        {
                            Id = Guid.NewGuid().ToString(),
                            MaximumPlaces = 6,
                            Date = DateTime.Now.AddDays(12)
                        },
                        new EventInfo
                        {
                            Id = Guid.NewGuid().ToString(),
                            MaximumPlaces = 10,
                            Date = DateTime.Now.AddDays(14)
                        },
                        new EventInfo
                        {
                            Id = Guid.NewGuid().ToString(),
                            MaximumPlaces = 6,
                            Date = DateTime.Now.AddDays(16)
                        }
            };

            foreach (var info in eventInfos)
                context.Set<EventInfo>().Add(info);

            var extras = new List<ExperienceExtra>
            {
                new ExperienceExtra
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "15 minutes instruction",
                            Order = 1
                        },
                        new ExperienceExtra
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "20 minute flight in tandem jump",
                            Order = 2
                        },
                        new ExperienceExtra
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "30 -50 second freefall",
                            Order = 3
                        },
                        new ExperienceExtra
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "5-6 minute ride in parachute after the free fall with great views",
                            Order = 4
                        },
                        new ExperienceExtra
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "Certificate Diploma",
                            Order = 5
                        },
                        new ExperienceExtra
                        {
                            Id = Guid.NewGuid().ToString(),
                            Content = "All equipment rental is included in the price",
                            Order = 6
                        }
            };

            foreach (var extra in extras)
                context.Set<ExperienceExtra>().Add(extra);

            var location = new Location
            {
                Id = Guid.NewGuid().ToString(),
                HouseName = "Tureby",
                Street1 = "Ejstrupholm",
                Street2 = "Odense",
                TownCity = "Vamdrup",
                Region = "Herning",
                Country = "Denmark",
                Postcode = ""
            };

            context.Set<Location>().Add(location);

            var adventureEvents = new List<AdventureEvent>
            {
                new AdventureEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sky Diving",
                    Headline = "An ultimate adrenaline experience as an experience gift?",
                    Description = "A parachute jump as a tandem jump is a fantastic experience that everyone should experience. To go along with an instructor, which means there is no need to spend time on theoretical and practical preparation",
                    LocationId = location.Id,
                    Location = location,                    
                    EventInfos = new List<EventInfo>(eventInfos),
                    ExperienceExtras = new List<ExperienceExtra>(extras)                  
                }
            };

            foreach (var eve in adventureEvents)
                context.Set<AdventureEvent>().Add(eve);

            base.Seed(context);
        }
    }
}

