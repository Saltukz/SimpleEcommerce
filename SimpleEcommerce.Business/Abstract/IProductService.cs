using SimpleEcommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetAll();
        void Create(Product entity);

        void Update(Product entity);

        void Delete(Product entity);
        List<Product> GetAllbyCategoryName(string cname);
        List<Product> GetFilteredProducts(string cName, string araliklow,string aralikhigh);
    }
}
