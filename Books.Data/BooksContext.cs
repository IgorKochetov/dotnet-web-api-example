using Books.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Books.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
