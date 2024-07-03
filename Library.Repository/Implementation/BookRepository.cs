using System;
using Library.Repository.Contract;
using Library.Shared.Models;

namespace Library.Repository.Implementation
{
	public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext dbContext;

		public BookRepository(ApplicationDbContext dbContext)
		{
            this.dbContext = dbContext;
		}


        public List<Book> GetBooks()
        {
            var books = this.dbContext.Books.ToList();
            return books;
        }

        public Book GetBook(int bookId)
        {
            var book = this.dbContext.Books.Find(bookId);
            return book;
        }

        public Book CreateBook(Book book)
        {
            var entity = this.dbContext.Books.Add(book);
            this.dbContext.SaveChanges();
            return entity.Entity;
        }

        public Book UpdateBook(Book book)
        {
            var existingBook = GetBook(book.BookId);
            if (existingBook == null)
            {
                throw new Exception("Book not found");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.Genre = book.Genre;
            var entity = this.dbContext.Books.Update(existingBook);
            this.dbContext.SaveChanges();

            return entity.Entity;
        }

        public bool DeleteBook(int bookId)
        {
            var existingBook = GetBook(bookId);
            if (existingBook == null)
            {
                throw new Exception("Book not found");
            }

            this.dbContext.Books.Remove(existingBook);
            var changeStates = this.dbContext.SaveChanges();
            return changeStates > 0;
        }
    }
}

