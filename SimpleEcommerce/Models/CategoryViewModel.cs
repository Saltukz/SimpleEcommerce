using SimpleEcommerce.Entity;
using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Models
{
    public class CategoryViewModel
    {
        public int? CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryUpperID { get; set; }

        public List<Category>? categories { get; set; } 

        public int Count { get; set; } 
    }
}
