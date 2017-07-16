using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Cluster> Clusters { get; set; }
    }
}
