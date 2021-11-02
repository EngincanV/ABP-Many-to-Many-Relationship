using AutoMapper;
using BookStore.Categories;
using BookStore.Web.Pages.Books;
using Volo.Abp.AutoMapper;

namespace BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            CreateMap<CategoryLookupDto, CreateModal.CategoryViewModel>()
                .Ignore(x => x.IsSelected);
        }
    }
}
