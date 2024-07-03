using System.Net;
using Library.Repository.Contract;
using Library.Service.Contract;
using Library.Shared.Models;
using Library.Shared.Validations;

namespace Library.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

		public BookService(IBookRepository bookRepository)
		{
            this.bookRepository = bookRepository;
		}

        public List<Book> GetBooks()
        {
            var books = this.bookRepository.GetBooks();
            return books;
        }

        public Book GetBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new Exception("Invalid Book Id");
            }

            var book = this.bookRepository.GetBook(bookId);
            return book;
        }
       
        public Book CreateBook(Book book)
        {
            if (book.BookId > 0)
            {
                throw new Exception("Invalid Book Id");
            }
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) ||
                string.IsNullOrEmpty(book.ISBN) || string.IsNullOrEmpty(book.Genre))
            {
                throw new Exception("Please provide values to all the required fields");
            }
            if (book.PublishedDate > DateTime.Now)
            {
                throw new Exception("Invalid Book Publish Date");
            }
            if (!ISBNValidator.IsValidISBN(book.ISBN))
            {
                throw new Exception("Invalid Book ISBN");
            }

            var newBook = this.bookRepository.CreateBook(book);
            return newBook;
        }

        public Book UpdateBook(Book book)
        {
            if (book.BookId <= 0)
            {
                throw new Exception("Invalid Book Id");
            }
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) ||
                string.IsNullOrEmpty(book.ISBN) || string.IsNullOrEmpty(book.Genre))
            {
                throw new Exception("Please provide values to all the required fields");
            }
            if (book.PublishedDate > DateTime.Now)
            {
                throw new Exception("Invalid Book Publish Date");
            }
            if (!ISBNValidator.IsValidISBN(book.ISBN))
            {
                throw new Exception("Invalid Book ISBN");
            }

            var updatedBook = this.bookRepository.UpdateBook(book);
            return updatedBook;
        }     

        public bool DeleteBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new Exception("Invalid Book Id");
            }

            var isDeleted = this.bookRepository.DeleteBook(bookId);
            return isDeleted;
        }
    }
}