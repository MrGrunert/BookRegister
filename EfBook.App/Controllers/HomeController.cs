using System.Collections.Generic;
using System.Linq;
using EfBook.Data;
using Microsoft.AspNetCore.Mvc;
using EfBook;
using EfBook.Domain;

namespace EfBook.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBookView()
        {
            return View();
        }

        public IActionResult AddBook(Domain.Book book)
        {
            return Ok("Add book here...");
        }

       public IActionResult ListBooksView()
        {
            return View();
        }

        public IActionResult UpdateBookView()
        {
            return View();
        }

        public IActionResult DeleteBookView()
        {
            return View();
        }


        public IActionResult SearchForBookByAuthor()
        {

            var books = GetAllBooks().OrderBy(s => s.Title);
            return Ok("Bajs");

        }


        public List<EfBook.Domain.Book> GetAllBooks()
        {
            var context = new BookContext();
            return new List<EfBook.Domain.Book>(context.Books);
        }
    }
}
