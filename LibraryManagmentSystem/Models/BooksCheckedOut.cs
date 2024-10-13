namespace LibraryManagmentSystem.Models
{
    public class BooksCheckedOut
    {

        public int Id { get; set; }
        public  int CheckOutId { get; set; }

        public  CheckOut? CheckOut { get; set; }
        public  int BookId { get; set; }
        public  Book? Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
