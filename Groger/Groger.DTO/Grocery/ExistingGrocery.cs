using Groger.Entity;
using System.ComponentModel.DataAnnotations;

namespace Groger.DTO.Grocery
{
    public class ExistingGrocery
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public GroceryUnits Unit { get; set; }
        public string Name { get; set; }
    }
}
