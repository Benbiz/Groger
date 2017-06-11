using Groger.DAL.Repositories;
using Groger.Entity;
using System;

namespace Groger.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Cluster> ClusterRepository { get; }

        IGenericRepository<Grocery> GroceryRepository { get; }

        void Save();
    }
}
