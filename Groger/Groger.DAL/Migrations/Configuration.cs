namespace Groger.DAL.Migrations
{
    using Groger.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GrogerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GrogerContext context)
        {
            /*context.Groceries.AddOrUpdate(x => x.Id,
                new Grocery() { Id = 1, Name = "Sel", Description = "Sel la balaine", Quantity = 2, ClusterId = 1 },
                new Grocery() { Id = 2, Name = "Poivre", Description = "Poivre 5 baies", Quantity = 1, ClusterId = 1 },
                new Grocery() { Id = 3, Name = "Côtes de porc", Description = "Côtes de porc", Quantity = 4, ClusterId = 1 },
                new Grocery() { Id = 4, Name = "Poulet", Description = "Poulet de louet", Quantity = 1, ClusterId = 1 }
                );

            context.Clusters.AddOrUpdate(x => x.Id,
                new Cluster() { Id = 1, Name = "Cuisine1", Description = "Ma cuisine 1" },
                new Cluster() { Id = 2, Name = "Cuisine2", Description = "Ma cuisine 2" },
                new Cluster() { Id = 3, Name = "Cuisine3", Description = "Ma cuisine 3" },
                new Cluster() { Id = 4, Name = "Cuisine4", Description = "Ma cuisine 4" }
                );*/
        }
    }
}
