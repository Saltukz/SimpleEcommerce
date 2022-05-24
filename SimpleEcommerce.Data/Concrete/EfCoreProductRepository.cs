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
    }
}
