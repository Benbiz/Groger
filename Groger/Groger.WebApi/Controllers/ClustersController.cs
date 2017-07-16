using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers
{
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
        [Authorize]
        public IQueryable<GetClusterDTO> GetClusters()
        {
            var clustersDTO = Mapper.Map<IEnumerable<GetClusterDTO>>(UserRecord.Clusters);

            return clustersDTO.AsQueryable();
        }

        // GET: api/Clusters/5
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
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCluster(int id, ClusterDTO cluster)
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
        [ResponseType(typeof(GetClusterDTO))]
        public IHttpActionResult PostCluster(ClusterDTO cluster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Cluster()
            {
                Name = cluster.Name,
                Description = cluster.Description
            };

            UnitOfWork.ClusterRepository.Insert(entity);
            UnitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = entity.Id }, Mapper.Map<GetClusterDTO>(entity));
        }

        // DELETE: api/Clusters/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}