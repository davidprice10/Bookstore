namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> GetBookByIDAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        void Add(Book book);
        void Delete(Book book);
        void Update(Book book);
        void SaveChanges();
    }
}
