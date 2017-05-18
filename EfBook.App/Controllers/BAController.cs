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
using Book = EfBook.Domain.Book;

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


            return View(context.Books.Include(b => b.Author).Select(s => s).ToList());
        }

        // GET: BA/CreateAuthor
        public ActionResult CreateAuthor()
        {
            
            return View();
        }

        // POST: BA/CreateAuthor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(Author collection)
        {
            try
            {
                var context = new BookContext();
                context.Add(collection);
                context.SaveChanges();
                int aId = context.Authors.Select(a => a.Id).Max();

                return RedirectToAction("CreateBook",new{ authorId = aId});
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
        public ActionResult CreateBook(CreateBookVM collection)
        {
            try
            {
                var context = new BookContext();
                Domain.Book newBook = new Domain.Book
                {
                    Title = collection.Title,
                    ReleaseYear = collection.ReleaseYear,
                    NumberOfPages = collection.NumberOfPages,
                    BookLanguage = collection.BookLanguage,
                    Resume = collection.Resume,
                    AuthorId = collection.AuthorId
                };
                context.Add(newBook);
                context.SaveChanges();
                var newBookId = context.Books.Last().Id;

                foreach (var item in collection.Genres)
                {
                    if (item.IsChecked)
                    {
                        context.BookGenres.Add(new BookGenre(newBookId, item.PoopId));
                    }
                }
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception e)
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
        public ActionResult CreateGenre(Genre collection)
        {
            try
            {
                var context = new BookContext();
                context.Add(collection);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }



        // GET: BA/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // POST: BA/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: BA/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}