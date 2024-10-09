using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Models
{
    public class Return
    {
        public int Id { get; set; }
        public int Penalty { get; set; }
        [ForeignKey("User")]
        public string? LibrarianId { get; set; }

        public User? Librarian { get; set; }
        [ForeignKey("User")]
        public string? MemberId { get; set; }
        public User? Member { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime BorrowDate { get; set; }
        List<Book>? ReturnedBooks { get; set; }
    }
}
