using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DTO
{
    public class ClusterGroceryDTO
    {
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [Range(0, uint.MaxValue)]
        public uint Quantity { get; set; }
    }
}
