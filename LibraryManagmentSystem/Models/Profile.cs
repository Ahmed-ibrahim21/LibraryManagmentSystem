using Microsoft.AspNetCore.Identity;

namespace LibraryManagmentSystem.Models
{
    public class Profile
    {
       public int Id { get; set; }

       public string UserId { get; set; }

       public User? Member { get; set; }
        public byte[]? ProfilePic { get; set; }
       public List<Book>? BorrowedBooks { get; set; }

        public int BorrowingLimit { get; set; }
    }
}
