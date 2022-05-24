using SimpleEcommerce.Entity;

namespace SimpleEcommerce.Models
{
    public class ProductViewModel
    {
        public string cname { get; set; }

        public string araliklow { get; set; }

        public string aralikhigh{ get; set; }

        public int Count { get; set; }
        public List<Product> products { get; set; } = null!;

      
    }
}
