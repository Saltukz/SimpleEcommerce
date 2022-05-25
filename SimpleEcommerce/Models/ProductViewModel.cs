using SimpleEcommerce.Entity;

namespace SimpleEcommerce.Models
{
    public class ProductViewModel
    {
        public string cname { get; set; }

        public string araliklow { get; set; } = null!;

        public string aralikhigh{ get; set; } = null!;

        public int Count { get; set; }
        public List<Product> products { get; set; } = null!;

      
    }
}
