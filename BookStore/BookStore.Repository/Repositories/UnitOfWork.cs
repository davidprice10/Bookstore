using BookStore.Domain.Interfaces;

namespace BookStore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BookStoreDbContext _context;

        public IBookRepository Books { get; }

        public UnitOfWork(BookStoreDbContext bookStoreDbContext, IBookRepository bookRepository)
        {
            this._context = bookStoreDbContext;
            this.Books = bookRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
