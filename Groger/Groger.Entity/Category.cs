using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.Entity
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Grocery> Groceries { get; set; }
    }
}
