using System;
using Volo.Abp.Application.Dtos;

namespace BookStore.Categories
{
    public class CategoryLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}