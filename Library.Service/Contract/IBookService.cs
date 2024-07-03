using Library.Shared.Models;

namespace Library.Service.Contract
{
	public interface IBookService
	{
        List<Book> GetBooks();

        Book GetBook(int bookId);

        Book CreateBook(Book book);

        Book UpdateBook(Book book);

        bool DeleteBook(int bookId);
    }
}