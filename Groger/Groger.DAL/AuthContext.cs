using Groger.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DAL
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("GrogerContext")
        {

        }

        public DbSet<Grocery> Groceries { get; set; }

        public DbSet<Cluster> Clusters { get; set; }
    }
}
