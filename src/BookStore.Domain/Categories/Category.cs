using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Categories
{
    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        /* This constructor is for deserialization / ORM purpose */
        private Category()
        {
        }

        public Category(Guid id, string name) : base(id)
        {
            SetName(name);
        }

        public Category SetName(string name)
        { 
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength);
            return this;
        }
    }
}