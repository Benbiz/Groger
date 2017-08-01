using Groger.DAL.Repositories;
using Groger.Entity;
using System;

namespace Groger.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private GrogerContext context = new GrogerContext();
        private GenericRepository<Cluster> clusterRepository;
        private GenericRepository<Grocery> groceryRepository;
        private GenericRepository<Category> categoryRepository;
        private AuthRepository authRepository;

        public IGenericRepository<Cluster> ClusterRepository
        {
            get
            {
                if (this.clusterRepository == null)
                {
                    this.clusterRepository = new GenericRepository<Cluster>(context);
                }
                return clusterRepository;
            }
        }
        public IGenericRepository<Grocery> GroceryRepository
        {
            get
            {
                if (this.groceryRepository == null)
                {
                    this.groceryRepository = new GenericRepository<Grocery>(context);
                }
                return groceryRepository;
            }
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.groceryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public AuthRepository AuthRepository
        {
            get
            {
                if (this.groceryRepository == null)
                {
                    this.authRepository = new AuthRepository(context);
                }
                return authRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
