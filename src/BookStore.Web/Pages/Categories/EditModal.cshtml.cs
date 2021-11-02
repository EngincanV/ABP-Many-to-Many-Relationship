using System;
using System.Threading.Tasks;
using BookStore.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Web.Pages.Categories
{
    public class EditModal : BookStorePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateCategoryDto EditingCategory { get; set; }

        private readonly ICategoryAppService _categoryAppService;

        public EditModal(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task OnGetAsync()
        {
            var category = await _categoryAppService.GetAsync(Id);
            EditingCategory = ObjectMapper.Map<CategoryDto, CreateUpdateCategoryDto>(category);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _categoryAppService.UpdateAsync(Id, EditingCategory);
            return NoContent();
        }
    }
}