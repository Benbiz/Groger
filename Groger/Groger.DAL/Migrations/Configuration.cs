using Groger.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Groger.DAL.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<GrogerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GrogerContext context)
        {
            var manager = new UserManager<ApplicationUser>(
                              new UserStore<ApplicationUser>(
                              context));

            var user = new ApplicationUser()
            {
                UserName = "User1",
                Email = "Email1@Example.com",
            };
            var result = manager.Create(user, "Password1");

            var users = new List<ApplicationUser>() { user };

           context.Groceries.AddOrUpdate(x => x.Id,
                new Grocery() { Id = 1, Name = "Sel de mer", Description = "Sel la balaine", CategoryId = 1 },
                new Grocery() { Id = 2, Name = "Poivre", Description = "Poivre 5 baies", CategoryId = 1 },
                new Grocery() { Id = 3, Name = "Côtes de porc", Description = "Côtes de porc", CategoryId = 1 },
                new Grocery() { Id = 4, Name = "Cuisse de poulet", Description = "Poulet de louet", CategoryId = 1 }
                );

            context.Clusters.AddOrUpdate(x => x.Id,
                new Cluster() { Id = 1, Name = "Cuisine1", Description = "Ma cuisine 1", ApplicationUsers = users },
                new Cluster() { Id = 2, Name = "Cuisine2", Description = "Ma cuisine 2", ApplicationUsers = users },
                new Cluster() { Id = 3, Name = "Cuisine3", Description = "Ma cuisine 3", ApplicationUsers = users },
                new Cluster() { Id = 4, Name = "Cuisine4", Description = "Ma cuisine 4", ApplicationUsers = users }
                );

            var clusterGroceries = new ClusterGrocery
            {
                ClusterId = 1,
                GroceryId = 1,
                Quantity = 2,
                UpdateTime = DateTime.Now
            };

            context.ClusterGroceries.Add(clusterGroceries);

            context.Categories.AddOrUpdate(x => x.Id,
                new Category() { Id = 1, Name = "Tooout", Description = "Tout est fini entre noooous" }
                );
        }
    }
}
