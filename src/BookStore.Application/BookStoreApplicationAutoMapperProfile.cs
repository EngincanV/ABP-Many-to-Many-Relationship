using AutoMapper;
using BookStore.Authors;
using BookStore.Books;
using BookStore.Categories;

namespace BookStore
{
    public class BookStoreApplicationAutoMapperProfile : Profile
    {
        public BookStoreApplicationAutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto, Category>();

            CreateMap<Author, AuthorDto>();
            CreateMap<CreateUpdateAuthorDto, Author>();

            CreateMap<BookWithDetails, BookDto>();
        }
    }
}
