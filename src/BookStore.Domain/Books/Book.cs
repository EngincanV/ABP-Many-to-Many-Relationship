using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        public Guid AuthorId { get; set; }

        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        private Book()
        {
        }

        public Book(Guid id, Guid authorId, string name, BookType type, DateTime publishDate, float price) 
            : base(id)
        {
            AuthorId = authorId;
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
        }
    }
}