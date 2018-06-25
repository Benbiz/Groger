using AutoMapper;
using Groger.DAL;
using Groger.DTO.ShoppingList.ShoppingItem;
using Groger.Entity;
using Groger.Entity.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers.ShoppingList
{
    [Authorize]
    [RoutePrefix("api/models/shoppinglists/{listId:int}/shoppingitems")]
    public class ShoppingModelItemsController : BaseApiController
    {
        public ShoppingModelItemsController()
        {

        }

        public ShoppingModelItemsController(IUnitOfWork uow)
            : base(uow)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<GetShoppingItemModelDTO>))]
        public IHttpActionResult GetShoppingItems(int listId)
        {
            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == listId);
            if (entity == null)
                return NotFound();
            

            return Ok(Mapper.Map<IEnumerable<GetShoppingItemModelDTO>>(entity.ShoppingModelItems));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetShoppingItemModel")]
        [ResponseType(typeof(GetShoppingItemModelDTO))]
        public IHttpActionResult GetShoppingItem(int listId, int id)
        {
            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == listId);
            if (entity == null)
                return NotFound();

            ShoppingModelItem item = entity.ShoppingModelItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            return Ok(Mapper.Map<GetShoppingItemModelDTO>(item));
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(GetShoppingItemModelDTO))]
        public IHttpActionResult PostShoppingItem(int listId, ShoppingItemModelDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == listId);
            if (list == null)
                return NotFound();

            var grocery = UnitOfWork.GroceryRepository.GetByID(item.GroceryId);
            if (grocery == null)
                return BadRequest("Grocery not found");

            var entity = new ShoppingModelItem()
            {
                GroceryId = grocery.Id,
                Comment = item.Comment,
                ToBuy = item.ToBuy
            };

            list.ShoppingModelItems.Add(entity);
            UnitOfWork.ShoppingModelListRepository.Update(list);
            UnitOfWork.Save();


            return CreatedAtRoute("GetShoppingItemModel", new { id = entity.Id }, Mapper.Map<GetShoppingItemModelDTO>(entity));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingItemDTO))]
        public IHttpActionResult PutShoppingItem(int listId, int id, ShoppingItemModelDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == listId);
            if (entity == null)
                return NotFound();

            ShoppingModelItem itemEntity = entity.ShoppingModelItems.FirstOrDefault(x => x.Id == id);
            if (itemEntity == null)
                return NotFound();

            var grocery = UnitOfWork.GroceryRepository.GetByID(item.GroceryId);
            if (grocery == null)
                return BadRequest("Grocery not found");


            itemEntity.Comment = item.Comment;
            itemEntity.Grocery = grocery;
            itemEntity.ToBuy = item.ToBuy;

            UnitOfWork.ShoppingModelItemRepository.Update(itemEntity);
            UnitOfWork.Save();

           
            return CreatedAtRoute("GetShoppingItemModel", new { id = entity.Id }, Mapper.Map<GetShoppingItemModelDTO>(itemEntity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingItemModelDTO))]
        public IHttpActionResult DeleteShoppingItem(int listId, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == listId);
            if (entity == null)
                return NotFound();

            ShoppingModelItem itemEntity = entity.ShoppingModelItems.FirstOrDefault(x => x.Id == id);
            if (itemEntity == null)
                return NotFound();


            UnitOfWork.ShoppingModelItemRepository.Delete(itemEntity);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetShoppingItemModelDTO>(itemEntity));
        }
    }
}
