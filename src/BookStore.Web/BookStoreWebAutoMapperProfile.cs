using AutoMapper;
using BookStore.Books;
using BookStore.Categories;
using BookStore.Web.Models;
using BookStore.Web.Pages.Books;
using Volo.Abp.AutoMapper;

namespace BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            CreateMap<CategoryLookupDto, CategoryViewModel>()
                .Ignore(x => x.IsSelected);

            CreateMap<BookDto, CreateUpdateBookDto>();
        }
    }
}
