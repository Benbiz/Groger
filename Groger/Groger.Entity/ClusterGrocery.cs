﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Groger.Entity
{
    public class ClusterGrocery
    {
        [Required]
        [Key, Column(Order = 0)]
        public int ClusterId { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public int GroceryId { get; set; }

        [Required]
        public virtual Cluster Cluster { get; set; }
        [Required]
        public virtual Grocery Grocery { get; set; }

        /// <summary>
        /// Nom optionnel
        /// </summary>
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        /// <summary>
        /// Quantité actuellement en stock
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
