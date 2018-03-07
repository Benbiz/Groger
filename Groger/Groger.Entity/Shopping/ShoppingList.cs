using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity.Shopping
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public virtual ICollection<ShoppingItem> ShoppingItems { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public virtual Cluster Cluster { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool Validated { get; set; }
        public DateTime? ValidatedDate { get; set; }
    }
}
