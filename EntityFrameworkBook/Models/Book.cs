
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public int  AuthorId { get; set; }
       
    }
}
