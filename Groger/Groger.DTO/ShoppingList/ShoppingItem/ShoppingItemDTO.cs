using System.ComponentModel.DataAnnotations;

namespace Groger.DTO.ShoppingList.ShoppingItem
{
    public class ShoppingItemDTO
    {
        [Required]
        public int GroceryId { get; set; }
        public string Comment { get; set; }
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
