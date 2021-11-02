using System.Threading.Tasks;
using BookStore.Categories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Pages.Categories
{
    public class CreateModal : BookStorePageModel
    {
        [BindProperty]
        public CreateUpdateCategoryDto Category { get; set; }

        private readonly ICategoryAppService _categoryAppService;

        public CreateModal(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public void OnGet()
        {
            Category = new CreateUpdateCategoryDto();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _categoryAppService.CreateAsync(Category);
            return NoContent();
        }
    }
}