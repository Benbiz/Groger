using System.ComponentModel.DataAnnotations;

namespace Groger.DTO
{
    public class GroceryDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "The name must be at least 6 characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The description must be at least 6 characters long.", MinimumLength = 6)]
        public string Description { get; set; }
        [Required]
        [Range(0, uint.MaxValue)]
        public int Quantity { get; set; }
        [Url]
        public string Picture { get; set; }
    }
}
