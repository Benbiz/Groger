using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// If the list is validated
        /// </summary>
        public bool Validated { get; set; }

        /// <summary>
        /// Validation  date
        /// </summary>
        public DateTime? ValidatedDate { get; set; }
    }
}
