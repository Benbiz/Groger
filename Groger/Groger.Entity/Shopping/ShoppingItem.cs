using System;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity.Shopping
{
    public class ShoppingItem
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
        [Required]
        [Range(0, int.MaxValue)]
        public int Brought { get; set; }
        [Required]
        public bool Validated { get; set; }
        [Required]
        public DateTime AddDate { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        public DateTime? ValidatedDate { get; set; }
    }
}
