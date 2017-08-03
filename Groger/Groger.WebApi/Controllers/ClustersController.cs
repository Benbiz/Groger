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
    [RoutePrefix("api/clusters")]
    public class ClustersController : BaseApiController
    {

        public ClustersController()
        {
        }

        public ClustersController(IUnitOfWork uow)
            : base(uow)
        {
        }

        // GET: api/Clusters
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Cluster list", typeof(IEnumerable<GetClusterDTO>))]
        public IHttpActionResult GetClusters()
        {
            var clustersDTO = Mapper.Map<IEnumerable<GetClusterDTO>>(UserRecord.Clusters);

            return Ok(clustersDTO);
        }

        // GET: api/Clusters/5
        [HttpGet]
        [Route("{id:int}", Name = "GetCluster")]
        [ResponseType(typeof(GetClusterDTO))]
        public IHttpActionResult GetCluster(int id)
        {
            Cluster entity = UnitOfWork.ClusterRepository.GetByID(id);

            if (entity == null)
                return NotFound();
            else if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            return Ok(Mapper.Map<GetClusterDTO>(entity));
        }

        // PUT: api/Clusters/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCluster(int id, [FromBody] ClusterDTO cluster)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Cluster entity = UnitOfWork.ClusterRepository.GetByID(id);

            if (entity == null)
                return NotFound();
            else if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            entity.Name = cluster.Name;
            entity.Description = cluster.Description;

            UnitOfWork.ClusterRepository.Update(entity);

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

        // POST: api/Clusters
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(GetClusterDTO))]
        public IHttpActionResult PostCluster([FromBody] ClusterDTO cluster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Cluster()
            {
                Name = cluster.Name,
                Description = cluster.Description,
                ApplicationUsers = new List<ApplicationUser>() { UserRecord }
            };

            UnitOfWork.ClusterRepository.Insert(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("GetCluster", new { id = entity.Id }, Mapper.Map<GetClusterDTO>(entity));
        }

        // DELETE: api/Clusters/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(GetClusterDTO))]
        public IHttpActionResult DeleteCluster(int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(id);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null) 
                return Unauthorized();

            UnitOfWork.ClusterRepository.Delete(id);
            UnitOfWork.Save();

            return Ok(Mapper.Map<GetClusterDTO>(cluster));
        }

        [HttpGet]
        [Route("{id:int}/categories")]
        public IHttpActionResult GetCategories(int id)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(id);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var cat = cluster.Groceries.Select(x => x.Category).GroupBy(x => x);

            List<GetCategoryDTO> cats = new List<GetCategoryDTO>();

            foreach (IGrouping<Category, Category> group in cat)
            {
                var dto = Mapper.Map<GetCategoryDTO>(group.Key);
                dto.Quantity = group.Count();
                cats.Add(dto);
            }

            return Ok(cats);
        }

        [HttpGet]
        [Route("{id:int}/categories/{categoryId}", Name = "GetCategory")]
        public IHttpActionResult GetCategory(int id, int categoryId)
        {
            Cluster cluster = UnitOfWork.ClusterRepository.GetByID(id);

            if (cluster == null)
                return NotFound();
            else if (cluster.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            var cats = cluster.Groceries.Select(x => x.Category).GroupBy(x => x);

            var cat = cats.FirstOrDefault(x => x.Key.Id == categoryId);
            if (cat == null)
                return NotFound();

            var dto = Mapper.Map<GetCategoryDTO>(cat.Key);
            dto.Quantity = cat.Count();

            return Ok(dto);
        }
    }
}