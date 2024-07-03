using Library.Service.Contract;
using Library.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet, Authorize]
        public IActionResult Index()
        {
            var books = this.bookService.GetBooks();
            return View(books);
        }

        [HttpGet, Authorize]
        public ActionResult Details(int id)
        {
            Book book = this.bookService.GetBook(id);
            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("BookId,Title,Author,ISBN,Genre,PublishedDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                var result = this.bookService.CreateBook(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            Book book = this.bookService.GetBook(id);
            return View(book);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind("BookId,Title,Author,ISBN,Genre,PublishedDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                this.bookService.UpdateBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Book book = this.bookService.GetBook(id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }
}