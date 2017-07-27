﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Groger.Entity
{
    public class Cluster
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Description { get; set; }

        public virtual ICollection<Grocery> Groceries { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
} 
