using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Models
{
    public class Return
    {
        public int Id { get; set; }
        public int Penalty { get; set; }
        [ForeignKey(nameof(Librarian))]
        public string? LibrarianId { get; set; }

        public User? Librarian { get; set; }
        [ForeignKey(nameof(Member))]
        public string? MemberId { get; set; }
        public User? Member { get; set; }
        List<BooksReturned>? ReturnedBooks { get; set; }
    }
}
