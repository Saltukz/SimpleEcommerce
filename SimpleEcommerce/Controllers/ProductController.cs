using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Business.Abstract;

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

       
        public IActionResult List(string cName)
        {
            //navigasyonda a tagından gelen kategori name ile veritabanına ürün sorgusu atıcam seo açısından önemli olduğu için yapıyı kategori ismi üzerinden kurucam 
            if(cName == null)
            {
                return RedirectPermanent("/");
            }


            return View();
        }
    }
}
