using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Groger.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Cluster> Clusters { get; set; }
    }
}
