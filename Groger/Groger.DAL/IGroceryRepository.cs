using Groger.Entity;
using System;
using System.Collections.Generic;

namespace Groger.DAL
{
    public interface IGroceryRepository : IDisposable
    {
        IEnumerable<Grocery> GetGroceries();
        Grocery GetGroceryById(int groceryId);
        void InsertGrocery(Grocery grocery);
        void DeleteGrocery(int groceryId);
        void UpdateGrocery(Grocery grocery);
        void Save();
    }
}
