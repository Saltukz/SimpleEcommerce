using Microsoft.AspNetCore.Mvc;
using SimpleEcommerce.Business.Abstract;
using SimpleEcommerce.Entity;
using SimpleEcommerce.Helpers;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<ProductController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            if (categories.Count == 0)
            {
                TempData["message"] = "Hiç kategori bulunamadı.";
            }

            var model = new CategoryViewModel
            {
                categories = categories,
                Count = categories.Count()
            };
            return View(model);
        }
    
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                TempData["message"] = "Kategori bulunamadı.";
                return Redirect("/Category/Index");
            }

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                CategoryID = category.CategoryId,
                Name = category.Name,
                Description = category.Description,

            };

            if(category.UpperCategoryId != null)
            {
                categoryViewModel.CategoryUpperID = (int)category.UpperCategoryId;
            }

            //üst kategorileri selectlistle seçtireceğim
            var categories = _categoryService.GetAll().Where(x => x.UpperCategory == null).ToList();

            categoryViewModel.categories = categories;

            return View(categoryViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            var category = _categoryService.GetById((int)model.CategoryID);
            if(category == null)
            {
                TempData["message"] = "Kategori bulunamadı.";
                return Redirect("/Category/Index");
            }

            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                CategoryID = category.CategoryId,
                Name = category.Name,
                Description = category.Description,

            };
          
            return View("/Category/Index");
        }


        public IActionResult Add()
        {
            //üst kategorileri selectlistle seçtireceğim
            var categories = _categoryService.GetAll().Where(x => x.UpperCategory == null).ToList();

            CategoryViewModel model = new CategoryViewModel
            {
                categories = categories
            };
    
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                //FriendlyUrl bende önceden bulunan bir method stringleri asciye uygun dönüştürüyor bu sayede linklerde kullanabiliyorum.
                Category category = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    SeoCode = UrlHelper.FriendlyUrl(model.Name)
                };
                if(model.CategoryUpperID != 0)
                {
                    category.UpperCategoryId = model.CategoryUpperID;
                }
                _categoryService.Create(category);

                return Redirect("/Category/Index");
            }

            //üst kategorileri selectlistle seçtireceğim
            var categories = _categoryService.GetAll().Where(x => x.UpperCategory == null).ToList();
            CategoryViewModel newModel = new CategoryViewModel { categories = categories };
            
            return View(newModel);
        }
    }
}
