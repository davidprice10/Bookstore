using BookStore.Domain;
using BookStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context) : base(context)
        {
        }

        public void AddBook(Book book)
        {
            Add(book);
        }

        public void DeleteBook(Book book)
        {
            Delete(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await FindAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Book> GetBookByIDAsync(int id)
        {
            return await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void UpdateBook(Book book)
        {
            Update(book);
        }

        public void SaveChanges()
        {
            Save();
        }
    }
}
