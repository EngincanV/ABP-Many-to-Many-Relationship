using System;
using BookStore.Categories;
using Volo.Abp.Application.Services;

namespace BookStore.Authors
{
    public interface IAuthorAppService : ICrudAppService<AuthorDto, Guid, AuthorGetListInput, CreateUpdateAuthorDto, CreateUpdateAuthorDto>
    {
        
    }
}