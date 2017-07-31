using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
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
    public class GroceriesController : BaseApiController
    {
        public GroceriesController()
        {
        }

        public GroceriesController(IUnitOfWork unit)
            : base(unit)
        {
        }

        // GET: api/Grocery
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Grocery list", typeof(IEnumerable<GetGroceryDTO>))]
        public IHttpActionResult GetGroceries(int clusterId)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            return Ok(Mapper.Map<IEnumerable<GetGroceryDTO>>(cluster.Groceries));
        }

        // GET: api/Grocery/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(GetGroceryDTO))]
        public IHttpActionResult GetGrocery(int clusterId, int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.Groceries.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            
            return Ok(Mapper.Map<GetGroceryDTO>(entity));
        }

        // PUT: api/Grocery/5
        [HttpPut]
        [Route("{id:int}", Name = "GetGrocery")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrocery(int clusterId, int id, GroceryDTO grocery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(clusterId);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var entity = cluster.Groceries.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = grocery.Name;
            entity.Description = grocery.Description;
            entity.Quantity = grocery.Quantity;
            entity.Picture = grocery.Picture;

            UnitOfWork.GroceryRepository.Update(entity);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return InternalServerError(new Exception(string.Format("Failed to update grocery {0}", entity.Id), e));
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

            Grocery entity = new Grocery()
            {
                Name = grocery.Name,
                Cluster = cluster,
                ClusterId = cluster.Id,
                Quantity = grocery.Quantity,
                Description = grocery.Description,
                Picture = grocery.Picture
            };

            UnitOfWork.GroceryRepository.Insert(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("GetGrocery", new { clusterId = clusterId, id = entity.Id }, Mapper.Map<GetGroceryDTO>(entity));
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

            var entity = cluster.Groceries.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            UnitOfWork.GroceryRepository.Delete(id);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetGroceryDTO>(entity));
        }
    }
}