using System.ComponentModel.DataAnnotations;

namespace Groger.DTO.Grocery
{
    public class SearchGroceryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Categorie { get; set; }
    }
}
