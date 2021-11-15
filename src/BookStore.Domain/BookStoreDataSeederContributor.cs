using System;
using System.Threading.Tasks;
using BookStore.Authors;
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
        
        public BookStoreDataSeederContributor(
            IGuidGenerator guidGenerator,
            IRepository<Category, Guid> categoryRepository, 
            IRepository<Author, Guid> authorRepository
        )
        {
            _guidGenerator = guidGenerator;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedCategoriesAsync();
            await SeedAuthorsAsync();
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
        
        private async Task SeedAuthorsAsync()
        {
            if (await _authorRepository.GetCountAsync() <= 0)
            {
                await _authorRepository.InsertAsync(
                    new Author(
                        _guidGenerator.Create(), 
                        "George Orwell", 
                        new DateTime(1903, 06, 25),
                  "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
                    )
                );
                
                await _authorRepository.InsertAsync(
                    new Author(
                        _guidGenerator.Create(), 
                        "Dan Brown", 
                        new DateTime(1964, 06, 22),
                        "Daniel Gerhard Brown (born June 22, 1964) is an American author best known for his thriller novels"
                    )
                );
            }
        }
    }
}