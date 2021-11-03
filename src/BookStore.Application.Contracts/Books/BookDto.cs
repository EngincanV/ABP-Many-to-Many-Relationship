using System;
using Volo.Abp.Application.Dtos;

namespace BookStore.Books
{
    public class BookDto : EntityDto<Guid>
    {
        public string AuthorName { get; set; }

        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public string[] CategoryNames { get; set; }
    }
}