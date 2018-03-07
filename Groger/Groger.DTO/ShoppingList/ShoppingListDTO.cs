using System.ComponentModel.DataAnnotations;

namespace Groger.DTO.ShoppingList
{
    public class ShoppingListDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool Validated { get; set; }
    }
}
