using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groger.DTO
{
    public class GetClusterDTO : ClusterDTO
    {
        public int Id { get; set; }
        public int GroceriesQuantity { get; set; }
        public string FirstGroceryName { get; set; }
    }
}
