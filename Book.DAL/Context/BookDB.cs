using Book.DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace Book.DAL.Context
{
    public class BookDB : DbContext
    {
        public DbSet<BookElem> BooksElems { get; set; }  
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Seller> Sellers { get; set; }


        public BookDB(DbContextOptions<BookDB> options) : base(options) 
        {
                
        }

         
    }
}
