using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Authors;
using BookStore.Categories;
using BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BookStore.Books
{
    public class EfCoreBookRepository : EfCoreRepository<BookStoreDbContext, Book, Guid>, IBookRepository
    {
        public EfCoreBookRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<BookWithDetails>> GetListAsync(
            string sorting, 
            int skipCount, 
            int maxResultCount, 
            CancellationToken cancellationToken = default
        )
        {
            var query = await ApplyFilterAsync();
            
            return await query
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Book.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<BookWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();
            
            return await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        private async Task<IQueryable<BookWithDetails>> ApplyFilterAsync()
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync())
                .Include(x => x.Categories)
                .Join(dbContext.Set<Author>(), book => book.AuthorId, author => author.Id,
                    (book, author) => new {book, author})
                .Select(x => new BookWithDetails
                {
                    Id = x.book.Id,
                    Name = x.book.Name,
                    Price = x.book.Price,
                    PublishDate = x.book.PublishDate,
                    CreationTime = x.book.CreationTime,
                    AuthorName = x.author.Name,
                    CategoryNames = (from bookCategories in x.book.Categories
                        join category in dbContext.Set<Category>() on bookCategories.CategoryId equals category.Id
                        select category.Name).ToArray()
                });
        }
    }
}