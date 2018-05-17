using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.DTO.Grocery;
using Groger.Entity;
using Groger.WebApi.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/clusters/{clusterId:int}/groceries")]
    public class ClusterGroceriesController : BaseApiController
    {
        public ClusterGroceriesController()
        {
        }

        public ClusterGroceriesController(IUnitOfWork unit)
            : base(unit)
        {
        }

        // GET: api/Grocery
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Grocery list", typeof(IEnumerable<GetGroceryDTO>))]
        public IHttpActionResult GetGroceries(int clusterId, [FromUri] RestQueryParams<GetGroceryDTO> param = null)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest();

            IEnumerable<ClusterGrocery> groceries = cluster.ClusterGroceries;
            if (param != null)
            {
                groceries = cluster.ClusterGroceries.Where(x => param.IsOk(Mapper.Map<GetGroceryDTO>(x)));
            }
            
            return Ok(Mapper.Map<IEnumerable<GetGroceryDTO>>(groceries).AsQueryable());
        }

        // GET: api/Grocery/5
        [HttpGet]
        [Route("{id:int}", Name = "GetGrocery")]
        [ResponseType(typeof(GetGroceryDTO))]
        public IHttpActionResult GetGrocery(int clusterId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.ClusterGroceries.FirstOrDefault(x => x.GroceryId == id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<GetGroceryDTO>(entity));
        }

        // PUT: api/Grocery/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrocery(int clusterId, int id, ClusterGroceryDTO grocery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.ClusterGroceries.FirstOrDefault(x => x.GroceryId == id);
            if (entity == null)
            {
                return NotFound();
            }


            entity.Name = grocery.Name;
            entity.Quantity = grocery.Quantity;
            entity.UpdateTime = DateTime.Now;

            UnitOfWork.ClusterGroceriesRepository.Update(entity);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return InternalServerError(new Exception(string.Format("Failed to update grocery {0}", entity.GroceryId), e));
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Grocery
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(GetGroceryDTO))]
        public IHttpActionResult PostGrocery(int clusterId, GroceryDTO grocery)
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

            Category category = null;
            if (grocery.Category != null)
            {
                category = UnitOfWork.CategoryRepository.Get(x => x.Name == grocery.Category).FirstOrDefault();
                if (category == null)
                    return (BadRequest("Category name is unknown"));
            }

            Grocery entity = new Grocery()
            {
                Name = grocery.Name,
                Description = grocery.Description,
                Category = category,
                Picture = grocery.Picture,
            };

            var clusterGrocery = new ClusterGrocery()
            {
                Cluster = cluster,
                Grocery = entity,
                Quantity = grocery.Quantity,
                UpdateTime = DateTime.Now
            };

            UnitOfWork.ClusterGroceriesRepository.Insert(clusterGrocery);
            UnitOfWork.Save();

            return CreatedAtRoute("GetGrocery", new { clusterId = clusterId, id = entity.Id }, Mapper.Map<GetGroceryDTO>(clusterGrocery));
        }

        // POST: api/Grocery
        [HttpPost]
        [Route("{id:int}")]
        [ResponseType(typeof(GetGroceryDTO))]
        public IHttpActionResult PostGrocery(int clusterId, int id, ExistingGrocery existingGrocery)
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

            Grocery grocery = UnitOfWork.GroceryRepository.GetByID(id);
            if (cluster == null)
                return NotFound();

            var clusterGrocery = new ClusterGrocery()
            {
                Cluster = cluster,
                Grocery = grocery,
                Quantity = existingGrocery.Quantity,
                Name = existingGrocery.Name,
                UpdateTime = DateTime.Now
            };

            UnitOfWork.ClusterGroceriesRepository.Insert(clusterGrocery);
            UnitOfWork.Save();

            return CreatedAtRoute("GetGrocery", new { clusterId = clusterId, id = grocery.Id }, Mapper.Map<GetGroceryDTO>(clusterGrocery));
        }

        // DELETE: api/Grocery/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(GetGroceryDTO))]
        public IHttpActionResult DeleteGroceryDTO(int clusterId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.ClusterGroceries.FirstOrDefault(x => x.Grocery.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            UnitOfWork.ClusterGroceriesRepository.Delete(entity);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetGroceryDTO>(entity));
        }
    }
}