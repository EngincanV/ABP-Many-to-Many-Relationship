using System;
using System.Threading.Tasks;
using BookStore.Authors;
using BookStore.Books;
using BookStore.Categories;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace BookStore
{
    public class BookStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Author, Guid> _authorRepository;
        private readonly BookManager _bookManager;
        
        public BookStoreDataSeederContributor(
            IGuidGenerator guidGenerator,
            IRepository<Category, Guid> categoryRepository, 
            IRepository<Author, Guid> authorRepository,
            BookManager bookManager
        )
        {
            _guidGenerator = guidGenerator;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _bookManager = bookManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var author1Id = _guidGenerator.Create();
            var author2Id = _guidGenerator.Create();
            
            await SeedCategoriesAsync();
            await SeedAuthorsAsync(author1Id, author2Id);
            await SeedBooksAsync(author1Id, author2Id);
        }

        private async Task SeedCategoriesAsync()
        {
            if (await _categoryRepository.GetCountAsync() <= 0)
            {
                await _categoryRepository.InsertAsync(
                    new Category(_guidGenerator.Create(), "History")
                );

                await _categoryRepository.InsertAsync(
                    new Category(_guidGenerator.Create(), "Unknown")
                );

                await _categoryRepository.InsertAsync(
                    new Category(_guidGenerator.Create(), "Adventure")
                );

                await _categoryRepository.InsertAsync(
                new Category(_guidGenerator.Create(), "Action")
                );

                await _categoryRepository.InsertAsync(
                    new Category(_guidGenerator.Create(), "Crime")
                );
                
                await _categoryRepository.InsertAsync(
                    new Category(_guidGenerator.Create(), "Dystopia")
                );
            }
        }
        
        private async Task SeedAuthorsAsync(Guid author1Id, Guid author2Id)
        {
            if (await _authorRepository.GetCountAsync() <= 0)
            {
                await _authorRepository.InsertAsync(
                    new Author(
                        author1Id, 
                        "George Orwell", 
                        new DateTime(1903, 06, 25),
                  "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
                    )
                );
                
                await _authorRepository.InsertAsync(
                    new Author(
                        author2Id, 
                        "Dan Brown", 
                        new DateTime(1964, 06, 22),
                        "Daniel Gerhard Brown (born June 22, 1964) is an American author best known for his thriller novels"
                    )
                );
            }
        }

        private async Task SeedBooksAsync(Guid author1Id, Guid author2Id)
        {
            if (await _authorRepository.GetCountAsync() <= 0)
            {
                await _bookManager.CreateAsync(
                    author1Id,
                    "1984",
                    new DateTime(1949, 6, 8),
                    19.85f,
                    categoryNames: new string[] {"Dystopia"}
                );

                await _bookManager.CreateAsync(
                    author2Id,
                    "Origin",
                    new DateTime(2017, 10, 03),
                    23.50f,
                    categoryNames: new string[] {"Adventure", "Action"}
                );
            }
        }
    }
}