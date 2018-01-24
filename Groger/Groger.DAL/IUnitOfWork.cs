using Groger.DAL.Repositories;
using Groger.Entity;
using System;

namespace Groger.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Cluster> ClusterRepository { get; }

        IGenericRepository<Grocery> GroceryRepository { get; }

        IGenericRepository<ClusterGrocery> ClusterGroceriesRepository { get; }

        IGenericRepository<Category> CategoryRepository { get; }

        AuthRepository  AuthRepository { get; }
        void Save();
    }
}
