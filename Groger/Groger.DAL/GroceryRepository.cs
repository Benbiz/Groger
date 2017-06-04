using Groger.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Groger.DAL
{
    public class GroceryRepository : IGroceryRepository, IDisposable
    {
        private GrogerContext context;

        public GroceryRepository(GrogerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Grocery> GetGroceries()
        {
            return context.Groceries.ToList();
        }

        public Grocery GetGroceryById(int groceryId)
        {
            return context.Groceries.Find(groceryId);
        }

        public void InsertGrocery(Grocery grocery)
        {
            context.Groceries.Add(grocery);
            context.Entry(grocery).Reference(x => x.Cluster).Load();
        }

        public void UpdateGrocery(Grocery grocery)
        {
            context.Entry(grocery).State = EntityState.Modified;
        }

        public void DeleteGrocery(int groceryId)
        {
            Grocery grocery = context.Groceries.Find(groceryId);
            context.Groceries.Remove(grocery);
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
