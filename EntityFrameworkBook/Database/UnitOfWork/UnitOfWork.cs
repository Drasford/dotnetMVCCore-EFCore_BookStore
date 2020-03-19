using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkBook.Database.UnitOfWork
{
    public class UnitOfWork 
    {

        private readonly BookStoreDbContext Context;
        public UnitOfWork(BookStoreDbContext dbContext)
        {
            this.Context = dbContext;
            BookStore = new BookStoreRepository(Context);
        }
        public BookStoreRepository BookStore { get; private set; }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
