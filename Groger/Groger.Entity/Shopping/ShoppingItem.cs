﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Groger.Entity.Shopping
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        [Required]
        public int ClusterGroceryId { get; set; }
        [Required]
        public virtual ClusterGrocery Grocery { get; set; }
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
