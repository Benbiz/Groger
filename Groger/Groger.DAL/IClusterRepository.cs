using Groger.Entity;
using System;
using System.Collections.Generic;

namespace Groger.DAL
{
    public interface IClusterRepository : IDisposable
    {
        IEnumerable<Cluster> GetClusters();
        Cluster GetClusterById(int clusterId);
        void InsertCluster(Cluster cluster);
        void DeleteCluster(int clusterId);
        void UpdateCluster(Cluster cluster);
        void Save();
    }
}
