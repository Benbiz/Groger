using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.Entity.Shopping
{
    public class ShoppingList
    {
        public virtual IEnumerable<ShoppingItem> ShoppingItems { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public virtual Cluster Cluster { get; set; }
    }
}
