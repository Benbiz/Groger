using System;

namespace Groger.DTO.ShoppingList.ShoppingItem
{
    public class GetShoppingItemDTO
    {
        public int Id { get; set; }
        public int GroceryId { get; set; }
        public string Comment { get; set; }
        public int ToBuy { get; set; }
        public int Brought { get; set; }
        public bool Validated { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime? ValidatedDate { get; set; }
    }
}
