using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DTO.ShoppingList.ShoppingItem
{
    public class GetShoppingItemDTO
    {
        public int Id { get; set; }
        public int ClusterGroceryId { get; set; }
        public int ToBuy { get; set; }
        public int Brought { get; set; }
        public bool Validated { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime? ValidatedDate { get; set; }
    }
}
