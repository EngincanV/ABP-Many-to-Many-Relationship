using System;
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
    public class EditModal : BookStorePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBookDto EditingBook { get; set; }
        
        [BindProperty]
        public List<CategoryViewModel> Categories { get; set; }
        
        public List<SelectListItem> AuthorList { get; set; }

        private readonly IBookAppService _bookAppService;

        public EditModal(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            EditingBook = ObjectMapper.Map<BookDto, CreateUpdateBookDto>(bookDto);
            
            var authorLookup = await _bookAppService.GetAuthorLookupAsync();
            AuthorList = authorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            var categoryLookupDto = await _bookAppService.GetCategoryLookupAsync();
            Categories = ObjectMapper.Map<List<CategoryLookupDto>, List<CategoryViewModel>>(categoryLookupDto.Items.ToList());

            if (EditingBook.CategoryNames != null && EditingBook.CategoryNames.Any())
            {
                Categories
                    .Where(x => EditingBook.CategoryNames.Contains(x.Name))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();
            
            var selectedCategories = Categories.Where(x => x.IsSelected).ToList();
            if (selectedCategories.Any())
            {
                var categoryNames = selectedCategories.Select(x => x.Name).ToArray();
                EditingBook.CategoryNames = categoryNames;
            }
            
            await _bookAppService.UpdateAsync(Id, EditingBook);
            return NoContent();
        }
    }
}