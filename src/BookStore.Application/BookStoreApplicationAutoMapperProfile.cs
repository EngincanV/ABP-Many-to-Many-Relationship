using AutoMapper;
using BookStore.Authors;
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
        }
    }
}
