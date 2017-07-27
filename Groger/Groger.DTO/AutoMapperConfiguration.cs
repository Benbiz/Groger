using AutoMapper;
using System;
using System.Linq;
using Groger.Entity;

namespace Groger.DTO
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Cluster, GetClusterDTO>()
                    .ForMember(DTO => DTO.GroceriesQuantity, conf => conf.MapFrom(ol => ol.Groceries.Count()))
                    .ForMember(dto => dto.FirstGroceryName, conf =>conf.MapFrom(ol => ol.Groceries.Count() != 0 ? ol.Groceries.First().Name : ""));
                cfg.CreateMap<Grocery, GetGroceryDTO>();
                cfg.CreateMap<Cluster, ClusterDTO>();
                cfg.CreateMap<ApplicationUser, UserDTO>();
            } );
        }
    }
}
