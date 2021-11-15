using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Books;
using BookStore.Categories;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Pages.Books
{
    public class CreateModal : BookStorePageModel
    {
        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }
           
        [BindProperty]
        public List<CategoryViewModel> Categories { get; set; }
        
        public List<SelectListItem> AuthorList { get; set; }

        private readonly IBookAppService _bookAppService;

        public CreateModal(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new CreateUpdateBookDto();
            
            //Get all authors and fill the select list
            var authorLookup = await _bookAppService.GetAuthorLookupAsync();
            AuthorList = authorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            //Get all categories
            var categoryLookupDto = await _bookAppService.GetCategoryLookupAsync();
            Categories = ObjectMapper.Map<List<CategoryLookupDto>, List<CategoryViewModel>>(categoryLookupDto.Items.ToList());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();
            
            var selectedCategories = Categories.Where(x => x.IsSelected).ToList();
            if (selectedCategories.Any())
            {
                var categoryNames = selectedCategories.Select(x => x.Name).ToArray();
                Book.CategoryNames = categoryNames;
            }
            
            await _bookAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}