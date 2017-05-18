using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfBook.Data;
using EfBook.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfBook.App.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        public ActionResult Index()
        {
            return View();
        }

        // GET: Genre/Details/5
        public ActionResult Details(int id)
        {
            var context = new BookContext();
            
            return View(context.Genres.Select(g => g).ToList());
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre collection)
        {
            try
            {
                //var context = new BookContext();
                //var genres =  context.Genres.Select(g => g).ToList();
                //ViewBag.GenreList = new SelectList(genres, "Type");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Genre/Edit/5
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

        // GET: Genre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Genre/Delete/5
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