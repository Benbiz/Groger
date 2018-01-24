using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity
{
    public class Cluster
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Description { get; set; }

        public virtual ICollection<ClusterGrocery> ClusterGroceries { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
} 
