using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Models
{
    public class LibraryContext : IdentityDbContext<User>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BooksCheckedOut>().HasKey(B => new {B.CheckOutId,B.BookId});
            base.OnModelCreating(builder);
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }

        public DbSet<Return> Returns { get; set; }

        public DbSet<BooksCheckedOut> BooksCheckedOuts { get; set; }

        public DbSet<Profile> Profile { get; set; }

        public DbSet<Return> returns { get; set; }
    }
}
