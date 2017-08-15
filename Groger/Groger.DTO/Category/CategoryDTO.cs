using System.ComponentModel.DataAnnotations;

namespace Groger.DTO
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
