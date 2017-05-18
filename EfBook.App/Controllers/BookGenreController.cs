using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfBook.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfBook.App.Controllers
{
    public class BookGenreController : Controller
    {
        // GET: BookGenre
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookGenre/Details/5
        public ActionResult Details(int id)
        {
            var context = new BookContext();
            //return View(context.Books.Select(s => s).ToList());

            return View();
        }

        // GET: BookGenre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookGenre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookGenre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookGenre/Edit/5
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

        // GET: BookGenre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookGenre/Delete/5
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