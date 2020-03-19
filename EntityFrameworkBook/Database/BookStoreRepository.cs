using EntityFrameworkBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkBook.Database
{
    public class BookStoreRepository : IBookStoreRepository
    {

        internal protected readonly BookStoreDbContext Context;

        public BookStoreRepository(BookStoreDbContext Context)
        {
            this.Context = Context;
        }

        public void AddAuthor(Author a)
        {
            Context.Authors.Add(a);
        }

        public void AddBook(Book b)
        {
            Context.Books.Add(b);
        }

        public void DeleteAuthor(int? id)
        {
            var authorToDelete = Context.Authors.Where(a => a.Id.Equals(id)).FirstOrDefault();
            Context.Authors.Remove(authorToDelete);     
        }

        public void DeleteBook(int? id)
        {
            var bookToDelete = Context.Books.Where(b => b.Id.Equals(id)).FirstOrDefault();
            Context.Books.Remove(bookToDelete);
        }

        public List<Author> GetAllAuthors()
        {
            return Context.Authors.ToList();
        }

        public List<Book> GetAllBooks()
        {
            return Context.Books.ToList();
        }

        public Author GetAuthor(int? id)
        {
            return Context.Authors.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public Book GetBook(int? id)
        {
            return Context.Books.Where(b => b.Id.Equals(id)).FirstOrDefault();
        }

        public List<Book> SearchForBook(string value)
        {
            int? authorId = Context.Authors.Where(a => a.Name.Contains(value)).FirstOrDefault()?.Id;
            if(authorId != null)
            {
                return Context.Books.Where(b => b.AuthorId.Equals(authorId)).ToList();
            }
            int? bookId = Context.Books.Where(b => b.Name.Contains(value)).FirstOrDefault()?.Id;
            if (bookId != null)
            {
                return Context.Books.Where(b => b.Id.Equals(bookId)).ToList();
            }
            return GetAllBooks();
        }

        public void UpdateAuthor(Author a)
        {
            var authorToUpdate = Context.Authors.Attach(a);
            authorToUpdate.State = EntityState.Modified;
        }

        public void UpdateBook(Book b)
        {
            var bookToUpdate = Context.Books.Attach(b);
            bookToUpdate.State = EntityState.Modified;
        }
       
    }
}

