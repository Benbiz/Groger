using System.ComponentModel.DataAnnotations;

namespace Groger.DTO
{
    public class ClusterGroceryDTO
    {
        [StringLength(100, MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [Range(0, uint.MaxValue)]
        public int Quantity { get; set; }
    }
}
