using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Authors
{
    public class AuthorAppService : 
        CrudAppService<Author, AuthorDto, Guid, AuthorGetListInput, CreateUpdateAuthorDto, CreateUpdateAuthorDto>, 
        IAuthorAppService
    {
        public AuthorAppService(IRepository<Author, Guid> repository) : base(repository)
        {
        }
    }
}