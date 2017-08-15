using AutoMapper;
using Groger.Entity;
using System.Linq;

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
                    .ForMember(dto => dto.FirstGroceryName, conf => conf.MapFrom(ol => ol.Groceries.Count() != 0 ? ol.Groceries.First().Name : ""));

                cfg.CreateMap<Grocery, GetGroceryDTO>()
                    .ForMember(DTO => DTO.Category, conf => conf.MapFrom(ol => ol.Category.Name));

                cfg.CreateMap<Cluster, ClusterDTO>();

                cfg.CreateMap<Category, GetCategoryDTO>();
                cfg.CreateMap<Category, CategoryDTO>();

                cfg.CreateMap<ApplicationUser, UserDTO>();
            });
        }
    }
}
