using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity
{
    public class Grocery
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Description { get; set; }
        [Url]
        public string Picture { get; set; }
        
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ClusterGrocery> ClusterGroceries { get; set; }
    }
}
