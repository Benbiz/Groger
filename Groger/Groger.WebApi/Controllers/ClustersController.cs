using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using AutoMapper;
using System.Collections.Generic;

namespace Groger.WebApi.Controllers
{
    public class ClustersController : ApiController
    {
        private IClusterRepository repository = new ClusterRepository(new GrogerContext());

        // GET: api/Clusters
        public IQueryable<ClusterDTO> GetClusters()
        {
            var clustersDTO = Mapper.Map<IEnumerable<ClusterDTO>>(repository.GetClusters());

            return clustersDTO.AsQueryable();
        }

        // GET: api/Clusters/5
        [ResponseType(typeof(ClusterDTO))]
        public IHttpActionResult GetCluster(int id)
        {
            var entity = repository.GetClusterById(id);
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

            repository.UpdateCluster(cluster);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (repository.GetClusterById(id) == null)
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

            repository.InsertCluster(cluster);
            repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = cluster.Id }, cluster);
        }

        // DELETE: api/Clusters/5
        [ResponseType(typeof(Cluster))]
        public IHttpActionResult DeleteCluster(int id)
        {
            Cluster cluster = repository.GetClusterById(id);
            if (cluster == null)
            {
                return NotFound();
            }

            repository.DeleteCluster(id);
            repository.Save();

            return Ok(cluster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}