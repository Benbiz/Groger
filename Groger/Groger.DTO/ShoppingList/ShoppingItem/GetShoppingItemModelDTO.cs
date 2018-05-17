using System;

namespace Groger.DTO.ShoppingList.ShoppingItem
{
    public class GetShoppingItemModelDTO
    {
        public int Id { get; set; }
        public int GroceryId { get; set; }
        public string Comment { get; set; }
        public int ToBuy { get; set; }
    }
}
