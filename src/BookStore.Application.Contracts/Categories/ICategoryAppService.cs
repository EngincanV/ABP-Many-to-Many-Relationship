using System;
using Volo.Abp.Application.Services;

namespace BookStore.Categories
{
    public interface ICategoryAppService : ICrudAppService<CategoryDto, Guid, CategoryGetListInput, CreateUpdateCategoryDto, CreateUpdateCategoryDto>
    {
        
    }
}