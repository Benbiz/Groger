using Groger.Entity.Shopping;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Groger.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Cluster> Clusters { get; set; }

        public virtual ICollection<ShoppingModelList> ShoppingListModels { get; set; }
    }
}
