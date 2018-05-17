using AutoMapper;
using Groger.DAL;
using Groger.DTO.ShoppingList.ShoppingItem;
using Groger.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers.ShoppingList
{
    [Authorize]
    [RoutePrefix("api/clusters/{clusterId:int}/shoppinglists/{listId:int}/shoppingitems")]
    public class ShoppingItemsController : BaseApiController
    {
        public ShoppingItemsController()
        {

        }

        public ShoppingItemsController(IUnitOfWork uow)
            : base(uow)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(GetShoppingItemDTO))]
        public IHttpActionResult GetShoppingItems(int clusterId, int listId)
        {
            Cluster entity = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (entity == null)
                return NotFound();
            else if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var list = entity.ShoppingLists.FirstOrDefault(x => x.Id == listId);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IEnumerable<GetShoppingItemDTO>>(list.ShoppingItems));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetShoppingItem")]
        [ResponseType(typeof(GetShoppingItemDTO))]
        public IHttpActionResult GetShoppingItem(int clusterId, int listId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.ShoppingLists.FirstOrDefault(x => x.Id == listId);
            if (entity == null)
            {
                return NotFound();
            }

            var item = entity.ShoppingItems.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<GetShoppingItemDTO>(item));
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(GetShoppingItemDTO))]
        public IHttpActionResult PostShoppingItem(int clusterId, int listId, ShoppingItemDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var list = cluster.ShoppingLists.FirstOrDefault(x => x.Id == listId);
            if (list == null)
            {
                return NotFound();
            }
            // Unable to modify a validated shopping list
            else if (list.Validated == true)
            {
                return BadRequest("Unable to modify a validated shopping list");
            }

            Grocery grocery = UnitOfWork.GroceryRepository.GetByID(item.GroceryId);
            if (grocery == null)
            {
                return BadRequest("Grocery not found");
            }

            Entity.Shopping.ShoppingItem entity = new Entity.Shopping.ShoppingItem()
            {
                AddDate = DateTime.Now,
                Brought = item.Brought,
                ToBuy = item.ToBuy,
                Validated = false,
                ValidatedDate = null,
                Grocery = grocery,
                GroceryId = grocery.Id
            };
            entity.LastUpdate = entity.AddDate;

            list.ShoppingItems.Add(entity);

            UnitOfWork.ShoppingListRepository.Update(list);
            UnitOfWork.Save();

            return CreatedAtRoute("GetShoppingItem", new { clusterId = clusterId, listId = list.Id, id = entity.Id }, Mapper.Map<GetShoppingItemDTO>(entity));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingItemDTO))]
        public IHttpActionResult PutShoppingItem(int clusterId, int listId, int id, ShoppingItemDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var list = cluster.ShoppingLists.FirstOrDefault(x => x.Id == listId);
            if (list == null)
            {
                return NotFound();
            }
            // Unable to modify a validated shopping list
            else if (list.Validated == true)
            {
                return BadRequest("Unable to modify a validated shopping list");
            }

            Grocery grocery = UnitOfWork.GroceryRepository.GetByID(item.GroceryId);
            if (grocery == null)
            {
                return BadRequest("Grocery not found");
            }
            var entity = list.ShoppingItems.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            // Unable to modify a validated shopping list
            else if (entity.Validated == true)
            {
                return BadRequest("Unable to modify a validated shopping item");
            }

            entity.Brought = item.Brought;
            entity.ToBuy = item.ToBuy;
            if (entity.Validated == false && item.Validated == true)
                entity.ValidatedDate = DateTime.Now;
            entity.Validated = item.Validated;
            entity.LastUpdate = DateTime.Now;
            entity.Grocery = grocery;
            entity.GroceryId = grocery.Id;

            UnitOfWork.ShoppingItemRepository.Update(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("GetShoppingItem", new { clusterId = clusterId, listId = list.Id, id = entity.Id }, Mapper.Map<GetShoppingItemDTO>(entity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingItemDTO))]
        public IHttpActionResult DeleteShoppingItem(int clusterId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var list = cluster.ShoppingLists.FirstOrDefault(x => x.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            // Unable to modify a validated shopping list
            else if (list.Validated == true)
            {
                return BadRequest("Unable to modify a validated shopping list");
            }

            var entity = list.ShoppingItems.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            UnitOfWork.ShoppingItemRepository.Delete(entity);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetShoppingItemDTO>(entity));
        }
    }
}
