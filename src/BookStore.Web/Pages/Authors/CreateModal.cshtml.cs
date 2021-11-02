using System.Threading.Tasks;
using BookStore.Authors;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Pages.Authors
{
    public class CreateModal : BookStorePageModel
    {
        [BindProperty] 
        public CreateUpdateAuthorDto Author { get; set; }

        private readonly IAuthorAppService _authorAppService;

        public CreateModal(IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }

        public void OnGet()
        {
            Author = new CreateUpdateAuthorDto();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _authorAppService.CreateAsync(Author);
            return NoContent();
        }
    }
}