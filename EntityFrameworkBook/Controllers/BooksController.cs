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
using EntityFrameworkBook.Models.ViewModel;

namespace EntityFrameworkBook.Controllers
{
    public class BooksController : Controller
    {

        private readonly BookStoreDbContext Context;
        private UnitOfWork unitOfWork;
        public BooksController(BookStoreDbContext context)
        {
            Context = context;
            unitOfWork = new UnitOfWork(Context);
        }

        // GET: Books
        public IActionResult Index(string value)
        {
            var books = unitOfWork.BookStore.GetAllBooks();
            foreach (var book in books)
            {
                book.Author = unitOfWork.BookStore.GetAuthor(book.AuthorId);
            }

            if (!String.IsNullOrEmpty(value))
            {
                books = unitOfWork.BookStore.SearchForBook(value);
                
                return View(books);
            }
           
            return View(books);
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookStore.GetBook(id);
            book.Author = unitOfWork.BookStore.GetAuthor(book.AuthorId);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(unitOfWork.BookStore.GetAllAuthors(), "Id", "Id");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Count,Description,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookStore.AddBook(book);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(unitOfWork.BookStore.GetAllAuthors(), "Id", "Id", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookStore.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(unitOfWork.BookStore.GetAllAuthors(), "Id", "Id", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Count,Description,AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.BookStore.UpdateBook(book);
                    unitOfWork.Complete();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList(unitOfWork.BookStore.GetAllAuthors(), "Id", "Id", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookStore.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            unitOfWork.BookStore.DeleteBook(id);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return unitOfWork.BookStore.GetAllBooks().Any(e => e.Id == id);
        }
    }
}
