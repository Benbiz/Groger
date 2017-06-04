using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity
{
    public class Cluster
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ICollection<Grocery> Groceries { get; set; }
    }
}
