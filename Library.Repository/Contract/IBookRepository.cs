using Library.Shared.Models;

namespace Library.Repository.Contract
{
	public interface IBookRepository
	{
        List<Book> GetBooks();

        Book GetBook(int bookId);

        Book CreateBook(Book book);

        Book UpdateBook(Book book);

        bool DeleteBook(int bookId);
    }
}