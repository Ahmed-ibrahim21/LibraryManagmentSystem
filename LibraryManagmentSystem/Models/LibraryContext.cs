using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LibraryManagmentSystem.Models
{
    public class LibraryContext : IdentityDbContext<User>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }

        public DbSet<Return> Returns { get; set; }

        public DbSet<BooksCheckedOut> BooksCheckedOuts { get; set; }

        public DbSet<Profile> Profile { get; set; }

        public DbSet<Return> returns { get; set; }

        public DbSet<BooksReturned> BooksReturned { get; set; }
    }
}
