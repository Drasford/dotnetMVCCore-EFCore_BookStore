using EntityFrameworkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkBook.Database
{
    public interface IBookStoreRepository 
    {

        Book GetBook(int? id);
        void AddBook(Book b);
        void UpdateBook(Book b);
        List<Book> GetAllBooks();
        void DeleteBook(int? id);

        Author GetAuthor(int? id);
        void AddAuthor(Author a);
        void UpdateAuthor(Author a);
        List<Author> GetAllAuthors();
        void DeleteAuthor(int? id);
        
        List<Book> SearchForBook(string value);
    }
}
