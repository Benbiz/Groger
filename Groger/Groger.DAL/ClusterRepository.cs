using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groger.Entity;
using System.Data.Entity;

namespace Groger.DAL
{
    public class ClusterRepository : IClusterRepository
    {
        private GrogerContext context;

        public ClusterRepository(GrogerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Cluster> GetClusters()
        {
            return context.Clusters.ToList();
        }

        public Cluster GetClusterById(int clusterId)
        {
            return context.Clusters.Find(clusterId);
        }

        public void DeleteCluster(int clusterId)
        {
            Cluster cluster = context.Clusters.Find(clusterId);
            context.Clusters.Remove(cluster);
        }

        public void InsertCluster(Cluster cluster)
        {
            context.Clusters.Add(cluster);
            context.Entry(cluster).Reference(x => x.Groceries).Load();
        }

        public void UpdateCluster(Cluster cluster)
        {
            context.Entry(cluster).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
