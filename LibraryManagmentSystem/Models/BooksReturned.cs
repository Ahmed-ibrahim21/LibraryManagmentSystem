using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentSystem.Models
{
    public class BooksReturned
    {
        public int Id { get; set; }
        [ForeignKey("Return")]
        public int ReturnId { get; set; }

        public int BooksCheckedOutId { get; set; }
        public Return? Return { get; set; }
        public BooksCheckedOut? BooksCheckedOut { get; set; }
    }
}
