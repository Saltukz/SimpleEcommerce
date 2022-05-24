using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Data.Abstract;
using SimpleEcommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Data.Concrete
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product>, IProductRepository
    {
        public EfCoreProductRepository(DbContext context) : base(context)
        {
        }

        public ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public List<Product> GetAllbyCategoryName(string cname)
        {
            //ürünün kategorisinin seocodu veya kategorisinin üst kategorisinin seocodu aranan değer ise listeye dahil ediyorum.
            return ShopContext.Products.Include(c=>c.Category).Where(c => c.Category.SeoCode == cname || c.Category.UpperCategory.SeoCode == cname).ToList();
        }

        public List<Product> GetFilteredProducts(string cName, string araliklow,string aralikhigh)
        {
            var aralik1 = Decimal.Parse(araliklow);
            var aralik2 = Decimal.Parse(aralikhigh);
            return ShopContext.Products.Include(c => c.Category)
                .Where(c => c.Category.SeoCode == cName || c.Category.UpperCategory.SeoCode == cName)
                .Where(x=> x.Price >= aralik1 && x.Price <= aralik2).ToList();
        }
    }
}
