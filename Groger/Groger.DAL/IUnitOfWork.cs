using Groger.DAL.Repositories;
using Groger.Entity;
using Groger.Entity.Shopping;
using System;

namespace Groger.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Cluster> ClusterRepository { get; }

        IGenericRepository<Grocery> GroceryRepository { get; }

        IGenericRepository<ClusterGrocery> ClusterGroceriesRepository { get; }

        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<ShoppingList> ShoppingListRepository { get; }

        IGenericRepository<ShoppingModelList> ShoppingModelListRepository { get; }

        IGenericRepository<ShoppingItem> ShoppingItemRepository { get; }

        IGenericRepository<ShoppingModelItem> ShoppingModelItemRepository { get; }

        AuthRepository  AuthRepository { get; }
        void Save();
    }
}
