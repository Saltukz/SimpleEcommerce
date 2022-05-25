using SimpleEcommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetAllWithSubs();
       
    }
}
