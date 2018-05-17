using System;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity.Shopping
{
    public class ShoppingModelItem
    {
        public int Id { get; set; }
        [Required]
        public int GroceryId { get; set; }
        [Required]
        public virtual Grocery Grocery { get; set; }
        public string Comment { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ToBuy { get; set; }
    }
}
