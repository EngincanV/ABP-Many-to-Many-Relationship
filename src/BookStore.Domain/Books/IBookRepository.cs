using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        Task<List<BookWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        );

        Task<BookWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}