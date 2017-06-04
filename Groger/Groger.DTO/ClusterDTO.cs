using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DTO
{
    public class ClusterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GroceriesQuantity { get; set; }
        public string FirstGroceryName { get; set; }
    }
}
