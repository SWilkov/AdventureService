using AdventureService.DataObjects;
using AdventureService.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureService.Helpers
{
    public static class MapperConfig
    {
        public static IMapper AdventureMapper { get; private set; }

        public static void RegisterMappers()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>()                    
                    .ForMember(dto => dto.HealthProblems, map => map.MapFrom(c => c.HealthProblems));
                                        
                cfg.CreateMap<CustomerDto, Customer>()
                    .ForMember(c => c.Id, map => map.MapFrom(dto => dto.Id == null ? Guid.NewGuid().ToString() : dto.Id));

                cfg.CreateMap<HealthProblem, HealthProblemDto>()
                    .ForMember(dto => dto.Id, map => map.MapFrom(hp => hp.Id))
                    .ForMember(dto => dto.Name, map => map.MapFrom(hp => hp.Name))
                    .ForMember(dto => dto.Description, map => map.MapFrom(hp => hp.Description));
                cfg.CreateMap<HealthProblemDto, HealthProblem>()
                    ;

                cfg.CreateMap<EventInfo, EventInfoDto>()
                    .ForMember(dto => dto.PlacesLeft, map => map.MapFrom(ei => ei.MaximumPlaces - ei.PlacesTaken))
                    .ForMember(dto => dto.Available,
                        map => map.MapFrom(ei => ei.PlacesTaken == ei.MaximumPlaces ? false : true))
                    .ForMember(dto => dto.Customers, map => map.Ignore());

                cfg.CreateMap<EventInfoDto, EventInfo>()
                .ForMember(ei => ei.AdventureEvent, map => map.Ignore());                

                cfg.CreateMap<ExperienceExtra, ExperienceExtraDto>();
                    
                cfg.CreateMap<Location, LocationDto>();    

                cfg.CreateMap<AdventureEvent, AdventureEventDto>()                    
                    .ForMember(dto => dto.EventInfos, map => map.MapFrom(ae => ae.EventInfos))
                    .ForMember(dto => dto.ExperienceExtras, map => map.MapFrom(ae => ae.ExperienceExtras))
                    .ForMember(dto => dto.Location, map => map.MapFrom(ae => ae.Location));
            });

            AdventureMapper = config.CreateMapper();
        }
    }
}