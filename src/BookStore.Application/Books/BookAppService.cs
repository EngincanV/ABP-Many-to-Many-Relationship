using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Books
{
    public class BookAppService : BookStoreAppService, IBookAppService
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookManager _bookManager;

        public BookAppService(IBookRepository bookRepository, BookManager bookManager)
        {
            _bookRepository = bookRepository;
            _bookManager = bookManager;
        }
        
        public async Task<PagedResultDto<BookDto>> GetListAsync(BookGetListInput input)
        {
            var books = await _bookRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount);
            var totalCount = await _bookRepository.CountAsync();

            return new PagedResultDto<BookDto>(totalCount, ObjectMapper.Map<List<BookWithDetails>, List<BookDto>>(books));
        }

        public async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);

            return ObjectMapper.Map<BookWithDetails, BookDto>(book);
        }

        public async Task CreateAsync(CreateUpdateBookDto input)
        {
            await _bookManager.CreateAsync(
                input.AuthorId, 
                input.Name, 
                input.PublishDate, 
                input.Price,
                input.CategoryNames
            );
        }

        public async Task UpdateAsync(Guid id, CreateUpdateBookDto input)
        {
            var book = await _bookRepository.GetAsync(id, includeDetails: true);
            
            await _bookManager.UpdateAsync(
                book, 
                input.AuthorId, 
                input.Name, 
                input.PublishDate, 
                input.Price, 
                input.CategoryNames
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}