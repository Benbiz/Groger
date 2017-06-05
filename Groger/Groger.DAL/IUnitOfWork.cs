using Groger.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Cluster> ClusterRepository { get; }

        IGenericRepository<Grocery> GroceryRepository { get; }

        void Save();
    }
}
