using BookStore.Domain;
using BookStore.Domain.Interfaces;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI
{
    public class DatabaseSeed : IDatabaseSeed
    {
        private readonly BookStoreDbContext _context;

        public DatabaseSeed(BookStoreDbContext context)
        {
            this._context = context;
        }

        // https://exceptionnotfound.net/ef-core-inmemory-asp-net-core-store-database/

        public async Task AddDataToDB(/*IServiceProvider serviceProvider*/)
        {
            if (!_context.Books.Any())
            {
                await _context.Books.AddRangeAsync(
                    new Book
                    {
                        Id = 1,
                        Author = "A. A. Milne",
                        Title = "Winnie-the-Pooh",
                        Price = 19.25m
                    },
                    new Book
                    {
                        Id = 2,
                        Author = "Jane Austen",
                        Title = "Pride and Prejudice",
                        Price = 5.49m
                    },
                    new Book
                    {
                        Id = 3,
                        Author = "William Shakespeare",
                        Title = "Romeo and Juliet",
                        Price = 6.95m
                    });
            }

            await _context.SaveChangesAsync();
        }
    }
}
