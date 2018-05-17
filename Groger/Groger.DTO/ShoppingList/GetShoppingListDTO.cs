using System;

namespace Groger.DTO.ShoppingList
{
    /// <summary>
    /// A shopping list of a cluter
    /// </summary>
    public class GetShoppingListDTO
    {
        /// <summary>
        /// Id of the list
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the shopping list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optionnal description associate to the shopping list
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number of products in the shopping list
        /// </summary>
        public int Products { get; set; }
    }
}
