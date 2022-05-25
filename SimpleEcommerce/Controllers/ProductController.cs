using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleEcommerce.Business.Abstract;
using SimpleEcommerce.Entity;
using SimpleEcommerce.Helpers;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ILogger<ProductController> logger,ICategoryService categoryService)
        {
            _productService = productService;
            _logger = logger;
            _categoryService = categoryService;
        }


        public IActionResult Index()
        {
            //join işlemi ile kategori isimlerini getirmek istiyorum
            List<Product> products = _productService.GetAllIncludeCategories();

            if (products.Count == 0)
            {
                TempData["message"] = "Hiç kategori bulunamadı.";
            }

            var model = new ProductViewModel
            {
                products = products,
                Count = products.Count()
            };
            return View(model);
        }


        public IActionResult List(string cName, ProductViewModel? outmodel)
        {
            //modelin içi dolu geldi ise demek ki filterden geliyor.
            if(outmodel.araliklow != null)
            {

                //null durumunda fiyatları min max ayarlıyor
                if (outmodel.araliklow == null)
                {
                    outmodel.araliklow = "0";
                }

                if (outmodel.aralikhigh == null)
                {
                    outmodel.aralikhigh = "10000000";
                }


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
        public IActionResult Filter(string cName,string? araliklow,string? aralikhigh)
        {
            if (ModelState.IsValid)
            {
                //navigasyonda a tagından gelen kategori name ile veritabanına ürün sorgusu atıcam seo açısından önemli olduğu için yapıyı kategori ismi üzerinden kurucam 
                if (cName == null)
                {
                    return NotFound();
                }
                //null durumunda fiyatları min max ayarlıyor
                if (araliklow == null)
                {
                    araliklow = "0";
                }

                if (aralikhigh == null)
                {
                    aralikhigh = "10000000";
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

            return RedirectToAction("List",cName);
           
        }



        public IActionResult Add()
        {
            //üst kategorileri selectlistle seçtireceğim
            var categories = _categoryService.GetAll();

            ProductModel model = new ProductModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                
                Product product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Tax = model.Tax,
                    CategoryId = model.CategoryID
                };
           
                _productService.Create(product);

                return Redirect("/Product/Index");
            }

            //valid olmama durumunda modelstateyi geri gönderiyorum.
            var categories = _categoryService.GetAll();
            ProductModel newmodel = new ProductModel
            {
                Categories = categories
            };

            return View(newmodel);
        }





        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                TempData["message"] = "Kategori bulunamadı.";
                return Redirect("/Category/Index");
            }

            ProductModel productModel = new ProductModel
            {
               
                Name = product.Name,
                Price = product.Price,
                Tax= product.Tax,
                ProductId = product.ProductId,
                CategoryID = (int)product.CategoryId

            };

            //üst kategorileri selectlistle seçtireceğim
            var categories = _categoryService.GetAll();

            productModel.Categories = categories;

            return View(productModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetById((int)model.ProductId);
                if (product == null)
                {
                    TempData["message"] = "Kategori bulunamadı.";
                    return Redirect("/Product/Index");
                }


                product.Name = model.Name;
                product.Price = model.Price;
                product.Tax = model.Tax;
                product.CategoryId = model.CategoryID;
                _productService.Update(product);



                return Redirect("/Product/Index");
            }


            ProductModel productModel = new ProductModel
            {

                Name = model.Name,
                Price = model.Price,
                Tax = model.Tax,
                ProductId = model.ProductId,
                CategoryID = (int)model.CategoryID

            };


           

            //üst kategorileri selectlistle seçtireceğim
            var categories = _categoryService.GetAll();

            productModel.Categories = categories;


            return View(productModel);
        }
    }
}
