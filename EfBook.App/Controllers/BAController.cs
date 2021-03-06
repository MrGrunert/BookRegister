using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfBook.App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EfBook.Data;
using EfBook.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EfBook.App.Controllers
{
    public class BAController : Controller
    {
        // GET: BA
        public ActionResult Index()
        {
            return View();
        }

        // GET: BA/Details/5
        public ActionResult Details(int id)
        {
            var context = new BookContext();

            var books = context.Books.Include(b => b.Author).Include(g => g.BookGenres).ThenInclude(bg => bg.Genre)
                .Select(s => s).ToList();
            List<BookVM> bookVms = new List<BookVM>();

            foreach (var book in books)
            {
                bookVms.Add(new BookVM(book));
            }

            return View(bookVms);
        }

        // GET: BA/CreateAuthor
        public ActionResult CreateAuthor()
        {

            return View();
        }

        // POST: BA/CreateAuthor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var context = new BookContext();
                    context.Add(author);
                    context.SaveChanges();
                    int aId = context.Authors.Select(a => a.Id).Max();

                    return RedirectToAction("CreateBook", new { authorId = aId });
                }

                return View(author);
            }
            catch
            {
                return View();
            }
        }


        // GET: BA/CreateBook
        public ActionResult CreateBook(int? authorId)
        {
            var context = new BookContext();
            var genres = context.Genres.Select(g => g).ToList();
            CreateBookVM createBook = new CreateBookVM
            {
                AuthorId = authorId.Value
            };

            foreach (var genre in genres)
            {
                createBook.Genres.Add(new GenreTypeModel(genre));
            }

            return View(createBook);
        }

        // POST: BA/CreateBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(CreateBookVM createBook)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createBook.Genres.FindAll(g => g.IsChecked).Count == 0)
                    {
                        return View(createBook);
                    }

                    var context = new BookContext();
                    var newBook = new Domain.Book
                    {
                        Title = createBook.Title,
                        ReleaseYear = createBook.ReleaseYear,
                        NumberOfPages = createBook.NumberOfPages,
                        BookLanguage = createBook.BookLanguage,
                        Resume = createBook.Resume,
                        AuthorId = createBook.AuthorId
                    };
                    context.Add(newBook);
                    context.SaveChanges();
                    var newBookId = context.Books.Last().Id;

                    foreach (var item in createBook.Genres)
                    {
                        if (item.IsChecked)
                        {
                            context.BookGenres.Add(new BookGenre(newBookId, item.PoopId));
                        }
                    }
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(createBook);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }

        public ActionResult CreateGenre()
        {
            return View();
        }

        // POST: BA/CreateBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGenre(Genre genre)
        {
            try
            {

                var context = new BookContext();
                context.Add(genre);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: BA/Edit/5
        public ActionResult Edit(int id)
        {
            var context = new BookContext();
            var book =  context.Books.Include(bg => bg.BookGenres).First(b => b.Id == id);
            var genres = context.Genres.Select(g => g).ToList();
            var bookVM = new CreateBookVM
            {
                Id = book.Id,
                Title = book.Title,
                ReleaseYear = book.ReleaseYear,
                NumberOfPages = book.NumberOfPages,
                BookLanguage = book.BookLanguage,
                Resume = book.Resume,
                AuthorId = book.AuthorId
            };

            foreach (var genre in genres)
            {
                var genreTypeModel = new GenreTypeModel(genre);
                
                if (!(book.BookGenres.FirstOrDefault(bg => bg.GenreId == genre.Id) is null))
                {
                    genreTypeModel.IsChecked = true;
                }
                bookVM.Genres.Add(genreTypeModel);
            }


            return View(bookVM);
        }

        // POST: BA/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateBookVM editBook)
        {
            try
            {
                if (editBook.Genres.FindAll(g => g.IsChecked).Count == 0)
                {
                    return View(editBook);
                }


                var context = new BookContext();
                var newBook = new Domain.Book
                {
                    Id = editBook.Id,
                    Title = editBook.Title,
                    ReleaseYear = editBook.ReleaseYear,
                    NumberOfPages = editBook.NumberOfPages,
                    BookLanguage = editBook.BookLanguage,
                    Resume = editBook.Resume,
                    AuthorId = editBook.AuthorId
                };
                context.Update(newBook);
                context.SaveChanges();
                var newBookId = context.Books.LastOrDefault(nb => nb.Id == id).Id;
                var bookGenreToClear = context.BookGenres.Where(bg => bg.BookId == id);
                context.BookGenres.RemoveRange(bookGenreToClear);
                foreach (var item in editBook.Genres)
                {                    
                    if (item.IsChecked)
                    {
                        context.BookGenres.Add(new BookGenre(newBookId, item.PoopId));
                    }
                }
                
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditAuthor(int AuthorId)
        {
            var context = new BookContext();
            var author = context.Authors.First(a => a.Id == AuthorId);
            return View(author);
        }

        // POST: BA/EditAuthor/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(int AuthorId, Author author)
        {
            try
            {
                var context = new BookContext();
                context.Update(author);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BA/Delete/5
        public ActionResult Delete(int id)
        {
            var context = new BookContext();

            var book = context.Books.Include(b => b.Author).Include(g => g.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefault(s => s.Id == id);

            return View(book);


        }

        // POST: BA/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Domain.Book book)
        {
            try
            {
                var context = new BookContext();
                context.Books.Remove(book);
                context.SaveChanges();
                

                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}