using AutoMapper;
using Groger.DAL;
using Groger.DTO.ShoppingList;
using Groger.Entity;
using Groger.Entity.Shopping;
using Groger.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers.ShoppingList
{
    [Authorize]
    [RoutePrefix("api/clusters/{clusterId:int}/shoppinglists")]
    public class ShoppingListsController : BaseApiController
    {
        public ShoppingListsController()
        {

        }

        public ShoppingListsController(IUnitOfWork uow)
            : base(uow)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(GetShoppingListDTO))]
        public IHttpActionResult GetShoppingLists(int clusterId, [FromUri] RestQueryParams<GetShoppingListDTO> param = null)
        {
            Cluster entity = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (entity == null)
                return NotFound();
            else if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            IEnumerable<Entity.Shopping.ShoppingList> groceries = entity.ShoppingLists;
            if (param != null)
            {
                groceries = entity.ShoppingLists.Where(x => param.IsOk(Mapper.Map<GetShoppingListDTO>(x)));
            }

            return Ok(Mapper.Map<IEnumerable<GetShoppingListDTO>>(entity.ShoppingLists));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetShoppingList")]
        [ResponseType(typeof(GetShoppingListDTO))]
        public IHttpActionResult GetShoppingList(int clusterId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.ShoppingLists.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<GetShoppingListDTO>(entity));
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(GetShoppingListDTO))]
        public IHttpActionResult PostShoppingList(int clusterId, NewShoppingListDTO list)
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


            Entity.Shopping.ShoppingList entity = new Entity.Shopping.ShoppingList()
            {
                Name = list.Name,
                Description = list.Description,
                CreateDate = DateTime.Now,
                Validated = false,
                ValidatedDate = null
            };

            cluster.ShoppingLists.Add(entity);

            UnitOfWork.ClusterRepository.Update(cluster);
            UnitOfWork.Save();

            return CreatedAtRoute("GetShoppingList", new { clusterId = clusterId, id = entity.Id }, Mapper.Map<GetShoppingListDTO>(entity));
        }

        [HttpPost]
        [Route("{modelId:int}")]
        [ResponseType(typeof(GetShoppingListDTO))]
        public IHttpActionResult PostShoppingListFromModel(int clusterId, int modelId)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            ShoppingModelList model = UnitOfWork.ShoppingModelListRepository.GetByID(modelId);
            if (model == null)
                return NotFound();

            Entity.Shopping.ShoppingList entity = new Entity.Shopping.ShoppingList()
            {
                Name = model.Name,
                Description = model.Description,
                CreateDate = DateTime.Now,
                Validated = false,
                ValidatedDate = null
            };

            cluster.ShoppingLists.Add(entity);

            UnitOfWork.ClusterRepository.Update(cluster);
            UnitOfWork.Save();

            entity.ShoppingItems = new List<ShoppingItem>();

            foreach (ShoppingModelItem item in model.ShoppingModelItems)
            {
                ShoppingItem newItem = new ShoppingItem()
                {
                    AddDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Brought = 0,
                    ToBuy = item.ToBuy,
                    Validated = false,
                    Comment  = item.Comment,
                    ValidatedDate = null,
                    Grocery = item.Grocery,
                    GroceryId = item.Grocery.Id
                };
                entity.ShoppingItems.Add(newItem);
            }

            UnitOfWork.ShoppingListRepository.Update(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("GetShoppingList", new { clusterId = clusterId, id = entity.Id }, Mapper.Map<GetShoppingListDTO>(entity));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingListDTO))]
        public IHttpActionResult PutShoppingList(int clusterId, int id, ShoppingListDTO list)
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

            var entity = cluster.ShoppingLists.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            // Unable to modify a validated shopping list
            else if (entity.Validated == true)
            {
                return BadRequest("Unable to modify a validated shopping list");
            }

            entity.Name = list.Name;
            entity.Description = list.Description;
            if (list.Validated == true && entity.Validated == false)
            {
                entity.ValidatedDate = DateTime.Now;
                // When validated, cluster is filled
                ValidateList(cluster, entity);
            }
            entity.Validated = list.Validated;

            UnitOfWork.ShoppingListRepository.Update(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("GetShoppingList", new { clusterId = clusterId, id = entity.Id }, Mapper.Map<GetShoppingListDTO>(entity));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(GetShoppingListDTO))]
        public IHttpActionResult DeleteShoppingList(int clusterId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.ShoppingLists.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            UnitOfWork.ShoppingListRepository.Delete(entity);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetShoppingListDTO>(entity));
        }

        private void ValidateList(Cluster cluster, Entity.Shopping.ShoppingList list)
        {
            foreach(Entity.Shopping.ShoppingItem item in list.ShoppingItems)
            {
                // On verifie si le produit est dans le cluster
                var clusterGrocery = cluster.ClusterGroceries.FirstOrDefault(x => x.Grocery.Id == item.GroceryId);
                if (clusterGrocery != null)
                {
                    clusterGrocery.Quantity += item.Brought;
                    UnitOfWork.ClusterGroceriesRepository.Update(clusterGrocery);
                }
                // Sinon on l'ajoute
                else
                {
                    var grocery = UnitOfWork.GroceryRepository.GetByID(item.GroceryId);
                    if (grocery != null)
                    {
                        clusterGrocery = new ClusterGrocery()
                        {
                            Cluster = cluster,
                            Grocery = grocery,
                            Quantity = item.Brought,
                            UpdateTime = DateTime.Now
                        };
                        UnitOfWork.ClusterGroceriesRepository.Insert(clusterGrocery);
                        UnitOfWork.Save();
                    }
                }

            }
        }
    }
}
