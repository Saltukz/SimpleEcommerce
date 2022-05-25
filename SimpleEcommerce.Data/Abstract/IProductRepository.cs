using SimpleEcommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetAllbyCategoryName(string cname);
        List<Product> GetFilteredProducts(string cName, string araliklow,string aralikhigh);
        List<Product> GetAllIncludeCategories();
    }
}
