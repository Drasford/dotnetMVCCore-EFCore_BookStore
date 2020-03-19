using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkBook.Database;
using EntityFrameworkBook.Models;
using EntityFrameworkBook.Database.UnitOfWork;

namespace EntityFrameworkBook.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookStoreDbContext Context;

        private readonly UnitOfWork unitOfWork;

        public AuthorsController(BookStoreDbContext context)
        {
            Context = context;
            unitOfWork = new UnitOfWork(Context);
        }

        // GET: Authors
        public IActionResult Index()
        {
            var authors = unitOfWork.BookStore.GetAllAuthors();
            return View(authors);
        }

        // GET: Authors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = unitOfWork.BookStore.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Author author)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookStore.AddAuthor(author);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = unitOfWork.BookStore.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.BookStore.UpdateAuthor(author);
                    unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = unitOfWork.BookStore.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            unitOfWork.BookStore.DeleteAuthor(id);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return unitOfWork.BookStore.GetAllAuthors().Any(e => e.Id == id);
        }
    }
}
