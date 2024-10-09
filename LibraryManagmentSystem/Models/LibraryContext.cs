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


        DbSet<Book> Books { get; set; }
        DbSet<CheckOut> CheckOuts { get; set; }

        DbSet<Return> Returns { get; set; }

        DbSet<BooksCheckedOut> BooksCheckedOuts { get; set; }

        DbSet<Profile> Profile { get; set; }

        DbSet<Return> returns { get; set; }
    }
}
