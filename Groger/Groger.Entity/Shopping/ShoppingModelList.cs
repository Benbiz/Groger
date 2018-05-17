using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity.Shopping
{
    public class ShoppingModelList
    {
        public int Id { get; set; }
        public virtual ICollection<ShoppingModelItem> ShoppingModelItems { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}
