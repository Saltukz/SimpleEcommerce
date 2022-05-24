using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Business.Abstract;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILogger<NavigationViewComponent> _logger;
        private ICategoryService _categoryService;

        public NavigationViewComponent(ILogger<NavigationViewComponent> logger, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            
            var categories = _categoryService.GetAll();
         
            var layoutModel = new LayoutModel()
            {
                Categories = categories,
            };

            return View(layoutModel);
        }

    }
}
