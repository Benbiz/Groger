using Groger.Entity;
using System.Data.Entity;

namespace Groger.DAL
{
    public class GrogerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public GrogerContext() : base("name=GrogerContext")
        {
        }

        public DbSet<Grocery> Groceries { get; set; }

        public DbSet<Cluster> Clusters { get; set; }
    }
}
