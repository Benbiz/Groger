using System.ComponentModel.DataAnnotations;

namespace Groger.DTO.ShoppingList.ShoppingItem
{
    public class ShoppingItemModelDTO
    {
        [Required]
        public int GroceryId { get; set; }
        public string Comment { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ToBuy { get; set; }
    }
}
