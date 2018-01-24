using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DTO.Grocery
{
    public class ExistingGrocery
    {
        [Required]
        public uint Quantity { get; set; }
        public string Name { get; set; }
    }
}
