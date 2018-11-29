using AutoMapper;
using EventApp.Core.Domain;
using EventApp.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventApp.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDto>()
                    .ForMember(x => x.TicketsCount, m => m.MapFrom(p => p.Tickets.Count()));
            })
            .CreateMapper();

            return mapper;
        }
    }
}
