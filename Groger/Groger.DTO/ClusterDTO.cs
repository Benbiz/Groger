using System.ComponentModel.DataAnnotations;

namespace Groger.DTO
{
    public class ClusterDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "The name must be at least 6 characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The description must be at least 6 characters long.", MinimumLength = 6)]
        public string Description { get; set; }
    }
}
