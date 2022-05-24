using SimpleEcommerce.Business.Abstract;
using SimpleEcommerce.Data.Abstract;
using SimpleEcommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEcommerce.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Product entity)
        {
            _unitOfWork.Products.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Product entity)
        {
            _unitOfWork.Products.Delete(entity);
            _unitOfWork.Save();
        }

        public List<Product> GetAll()
        {
           return _unitOfWork.Products.GetAll();    
        }

        public List<Product> GetAllbyCategoryName(string cname)
        {
            return _unitOfWork.Products.GetAllbyCategoryName(cname);
        }

        public Product GetById(int id)
        {
            return _unitOfWork.Products.GetById(id);
        }

        public List<Product> GetFilteredProducts(string cName, string araliklow,string aralikhigh)
        {
            return _unitOfWork.Products.GetFilteredProducts(cName, araliklow,aralikhigh);
        }

        public void Update(Product entity)
        {
            _unitOfWork.Products.Update(entity);
            _unitOfWork.Save();
        }
    }
}
