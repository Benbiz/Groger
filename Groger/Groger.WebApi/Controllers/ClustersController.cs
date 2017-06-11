using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers
{
    public class ClustersController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public ClustersController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ClustersController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        // GET: api/Clusters
        [Authorize]
        public IQueryable<ClusterDTO> GetClusters()
        {
            var clustersDTO = Mapper.Map<IEnumerable<ClusterDTO>>(unitOfWork.ClusterRepository.Get());

            return clustersDTO.AsQueryable();
        }

        // GET: api/Clusters/5
        [ResponseType(typeof(ClusterDTO))]
        public IHttpActionResult GetCluster(int id)
        {
            var entity = unitOfWork.ClusterRepository.GetByID(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ClusterDTO>(entity));
        }

        // PUT: api/Clusters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCluster(int id, Cluster cluster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cluster.Id)
            {
                return BadRequest();
            }

            unitOfWork.ClusterRepository.Update(cluster);

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.ClusterRepository.GetByID(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clusters
        [ResponseType(typeof(Cluster))]
        public IHttpActionResult PostCluster(Cluster cluster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.ClusterRepository.Insert(cluster);
            unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = cluster.Id }, cluster);
        }

        // DELETE: api/Clusters/5
        [ResponseType(typeof(Cluster))]
        public IHttpActionResult DeleteCluster(int id)
        {
            Cluster cluster = unitOfWork.ClusterRepository.GetByID(id);
            if (cluster == null)
            {
                return NotFound();
            }

            unitOfWork.ClusterRepository.Delete(id);
            unitOfWork.Save();

            return Ok(cluster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}