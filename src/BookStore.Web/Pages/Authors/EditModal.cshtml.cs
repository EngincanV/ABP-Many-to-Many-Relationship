using System;
using System.Threading.Tasks;
using BookStore.Authors;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Pages.Authors
{
    public class EditModal : BookStorePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        
        [BindProperty]
        public CreateUpdateAuthorDto EditingAuthor { get; set; }

        private readonly IAuthorAppService _authorAppService;

        public EditModal(IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }

        public async Task OnGetAsync()
        {
            var authorDto = await _authorAppService.GetAsync(Id);
            EditingAuthor = ObjectMapper.Map<AuthorDto, CreateUpdateAuthorDto>(authorDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _authorAppService.UpdateAsync(Id, EditingAuthor);
            return NoContent();
        }
    }
}