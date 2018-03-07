using AutoMapper;
using Groger.DTO.Grocery;
using Groger.DTO.ShoppingList;
using Groger.DTO.ShoppingList.ShoppingItem;
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
                    .ForMember(DTO => DTO.GroceriesQuantity, conf => conf.MapFrom(ol => ol.ClusterGroceries.Count()));

                cfg.CreateMap<ClusterGrocery, GetGroceryDTO>()
                    .ForMember(DTO => DTO.Id, conf => conf.MapFrom(ol => ol.Grocery.Id))
                    .ForMember(DTO => DTO.Name, conf => conf.MapFrom(ol => ol.Grocery.Name))
                    .ForMember(DTO => DTO.Description, conf => conf.MapFrom(ol => ol.Grocery.Description))
                    .ForMember(DTO => DTO.Picture, conf => conf.MapFrom(ol => ol.Grocery.Picture))
                    .ForMember(DTO => DTO.Category, conf => conf.MapFrom(ol => ol.Grocery.Category.Name));

                cfg.CreateMap<Entity.Grocery, GrocerySearchResultDTO>()
                    .ForMember(DTO => DTO.Category, conf => conf.MapFrom(ol => ol.Category.Name));

                cfg.CreateMap<Cluster, ClusterDTO>();

                cfg.CreateMap<Category, GetCategoryDTO>();
                cfg.CreateMap<Category, CategoryDTO>();

                cfg.CreateMap<ApplicationUser, UserDTO>();

                cfg.CreateMap<Entity.Shopping.ShoppingList, GetShoppingListDTO>()
                    .ForMember(DTO => DTO.Products, conf => conf.MapFrom(ol => ol.ShoppingItems.Count()));

                cfg.CreateMap<Entity.Shopping.ShoppingItem, GetShoppingItemDTO>();
            });
        }
    }
}
