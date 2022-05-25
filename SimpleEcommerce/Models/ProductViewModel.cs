using SimpleEcommerce.Entity;
using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerce.Models
{
    public class ProductViewModel
    {
        public string cname { get; set; }

        public string? araliklow { get; set; }

        public string? aralikhigh{ get; set; } 

        public int Count { get; set; }
        public List<Product>? products { get; set; }

      
    }



    public class ProductModel 
    {

        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 100)]
        public decimal Tax { get; set; }
        [Required]
        public int CategoryID { get; set; }


        public List<Category>? Categories { get; set; }  



    }
}
