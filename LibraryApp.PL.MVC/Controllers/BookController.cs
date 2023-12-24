using LibraryApp.BLL.DTOs;
using LibraryApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LibraryApp.PL.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public ActionResult Index() => View();

        [HttpGet]
        public async Task<ActionResult> GetBooks()
        {
            var books = await this.bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        public ActionResult AddBook() => View();

        [HttpPost]
        public async Task<ActionResult> AddBook(string title, string author, int year)
        {
            var bookDto = new BookDTO() { Title = title, Author = author, IsAvailable = true, Year = year };
            await this.bookService.AddBookAsync(bookDto);

            return RedirectToAction("GetBooks");
        }

        public async Task<ActionResult> DeleteBook(int id)
        {
            await this.bookService.DeleteBookAsync(id);
            return RedirectToAction("GetBooks");
        }
    }
}
