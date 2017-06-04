using System.ComponentModel.DataAnnotations;

namespace Groger.Entity
{
    public class Grocery
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int  ClusterId { get; set; }
        public virtual Cluster  Cluster { get; set; }
    }
}
