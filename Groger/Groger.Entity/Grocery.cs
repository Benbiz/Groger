﻿using System.ComponentModel.DataAnnotations;

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
        [Required]
        [Range(0, uint.MaxValue)]
        public int Quantity { get; set; }
        [Url]
        public string Picture { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public int ClusterId { get; set; }

        public virtual Cluster  Cluster { get; set; }
    }
}
