using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Authors
{
    public interface IAuthorAppService : 
        ICrudAppService<AuthorDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateAuthorDto, CreateUpdateAuthorDto>
    {
        
    }
}