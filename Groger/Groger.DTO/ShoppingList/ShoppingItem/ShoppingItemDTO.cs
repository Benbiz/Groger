using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DTO.ShoppingList.ShoppingItem
{
    public class ShoppingItemDTO
    {
        [Required]
        public int GroceryId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ToBuy { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Brought { get; set; }
        [Required]
        public bool Validated { get; set; }
    }
}
