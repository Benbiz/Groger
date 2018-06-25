using AutoMapper;
using Groger.DAL;
using Groger.DTO.ShoppingList;
using Groger.Entity;
using Groger.Entity.Shopping;
using Groger.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers.ShoppingList
{
    [Authorize]
    [RoutePrefix("api/models/shoppinglists")]
    public class ShoppingModelListsController : BaseApiController
    {
        public ShoppingModelListsController()
        {

        }

        public ShoppingModelListsController(IUnitOfWork uow)
            : base(uow)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<GetShoppingListModelDTO>))]
        public IHttpActionResult GetShoppingModelLists([FromUri] RestQueryParams<GetShoppingListModelDTO> param = null)
        {
            var lists = Mapper.Map<IEnumerable<GetShoppingListModelDTO>>(UserRecord.ShoppingListModels);

            return Ok(lists);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetShoppingListModel")]
        [ResponseType(typeof(GetShoppingListModelDTO))]
        public IHttpActionResult GetShoppingModelList(int id)
        {
            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return NotFound();

            return Ok(Mapper.Map<GetShoppingListModelDTO>(entity));
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(GetShoppingListModelDTO))]
        public IHttpActionResult PostShoppingList(NewShoppingListModelDTO list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new ShoppingModelList()
            {
                Name = list.Name,
                Description = list.Description,
                User = UserRecord
            };

            UnitOfWork.ShoppingModelListRepository.Insert(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("GetShoppingListModel", new { id = entity.Id }, Mapper.Map<GetShoppingListModelDTO>(entity));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingList(int id, ShoppingListModelDTO list)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return NotFound();

            entity.Name = list.Name;
            entity.Description = list.Description;

            UnitOfWork.ShoppingModelListRepository.Update(entity);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return InternalServerError(new Exception(string.Format("Failed to update cluster {0}", entity.Id), e));
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingListModelDTO))]
        public IHttpActionResult DeleteShoppingList(int id)
        {
            ShoppingModelList entity = UserRecord.ShoppingListModels.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return NotFound();

            UnitOfWork.ShoppingModelListRepository.Delete(id);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetShoppingListModelDTO>(entity));
        }
    }
}
