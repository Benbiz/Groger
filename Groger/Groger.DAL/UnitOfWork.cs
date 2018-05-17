using Groger.DAL.Repositories;
using Groger.Entity;
using Groger.Entity.Shopping;
using System;

namespace Groger.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private GrogerContext context = new GrogerContext();
        private GenericRepository<Cluster> clusterRepository;
        private GenericRepository<Grocery> groceryRepository;
        private GenericRepository<ClusterGrocery> clusterGroceriesyRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<ShoppingList> shoppingListRepository;
        private GenericRepository<ShoppingItem> shoppingItemRepository;
        private GenericRepository<ShoppingModelList> shoppingModelListRepository;
        private GenericRepository<ShoppingModelItem> shoppingModelItemRepository;
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

        public IGenericRepository<ClusterGrocery> ClusterGroceriesRepository
        {
            get
            {
                if (this.clusterGroceriesyRepository == null)
                {
                    this.clusterGroceriesyRepository = new GenericRepository<ClusterGrocery>(context);
                }
                return clusterGroceriesyRepository;
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

        public IGenericRepository<ShoppingList> ShoppingListRepository
        {
            get
            {
                if (this.shoppingListRepository == null)
                {
                    this.shoppingListRepository = new GenericRepository<ShoppingList>(context);
                }
                return shoppingListRepository;
            }
        }

        public IGenericRepository<ShoppingModelList> ShoppingModelListRepository
        {
            get
            {
                if (this.shoppingModelListRepository == null)
                {
                    this.shoppingModelListRepository = new GenericRepository<ShoppingModelList>(context);
                }
                return shoppingModelListRepository;
            }
        }

        public IGenericRepository<ShoppingItem> ShoppingItemRepository
        {
            get
            {
                if (this.shoppingItemRepository == null)
                {
                    this.shoppingItemRepository = new GenericRepository<ShoppingItem>(context);
                }
                return shoppingItemRepository;
            }
        }

        public IGenericRepository<ShoppingModelItem> ShoppingModelItemRepository
        {
            get
            {
                if (this.shoppingModelItemRepository == null)
                {
                    this.shoppingModelItemRepository = new GenericRepository<ShoppingModelItem>(context);
                }
                return shoppingModelItemRepository;
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
