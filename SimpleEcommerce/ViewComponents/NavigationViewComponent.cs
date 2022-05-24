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
            // one to many yerine self references tercih ettim databaseyi iki tabloyu join etme yükünden kurtardım
            // fakat view kısmında iç içe iki döngü kullanmak zorunda kaldım şu an çok mantıklı gelmese de çok yüksek sayıda kategori olmayacagını varsayarak devam ediyorum.
            var categories = _categoryService.GetAll();
            var layoutModel = new LayoutModel()
            {
                Categories = categories,
            };

            return View(layoutModel);
        }

    }
}
