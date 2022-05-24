using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleEcommerce.Business.Abstract;
using SimpleEcommerce.Entity;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

      
        public IActionResult List(string cName, ProductViewModel outmodel)
        {
            //modelin içi dolu geldi ise demek ki filterden geliyor.
            if(outmodel.araliklow != null)
            {

                List<Product> productsnew = _productService.GetFilteredProducts(cName, outmodel.araliklow, outmodel.aralikhigh);


                if (productsnew.Count == 0)
                {

                    TempData["message"] = "Hiç ürün bulunamadı.";


                }
                ProductViewModel newmodel = new ProductViewModel
                {
                    Count = productsnew.Count,
                    products = productsnew,
                    cname = cName
                };

                return View(newmodel);
            }
            //modelik aralık kısmı boş demek ki navigasyondan geliyor
            //navigasyonda a tagından gelen kategori name ile veritabanına ürün sorgusu atıcam seo açısından önemli olduğu için yapıyı kategori ismi üzerinden kurucam 
            if(cName == null)
            {
                return NotFound();
            }

            List<Product> products = _productService.GetAllbyCategoryName(cName);

           
            if (products.Count == 0)
            {
              
                TempData["message"] = "Hiç ürün bulunamadı.";
              

            }

            ProductViewModel model = new ProductViewModel
            {
                Count = products.Count,
                products = products,
                cname = cName
            };



            return View(model);
        }



        [HttpPost]
        public IActionResult Filter(string cName,string araliklow,string aralikhigh)
        {
            if (ModelState.IsValid)
            {
                //navigasyonda a tagından gelen kategori name ile veritabanına ürün sorgusu atıcam seo açısından önemli olduğu için yapıyı kategori ismi üzerinden kurucam 
                if (cName == null)
                {
                    return NotFound();
                }

               

                List<Product> products = _productService.GetFilteredProducts(cName,araliklow,aralikhigh);


                if (products.Count == 0)
                {

                    TempData["message"] = "Hiç ürün bulunamadı.";


                }

                ProductViewModel model = new ProductViewModel
                {
                    Count = products.Count,
                    products = products,
                    cname = cName,
                    araliklow = araliklow,
                    aralikhigh = aralikhigh
                };



                return View("List",model);

            }

            return RedirectToAction("List",ModelState);
           
        }
    }
}
