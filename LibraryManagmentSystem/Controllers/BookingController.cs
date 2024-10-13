using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        IBookRepository bookRepository;
        ICheckOutRepository checkOutRepository;
        IBooksCheckedOutRepository bookCheckedOutRepository;
        IUsersBooks usersBooks;
        public BookingController(IUsersBooks usersBooks,IBookRepository bookRepository,ICheckOutRepository checkOutRepository,IBooksCheckedOutRepository booksCheckedOutRepository) 
        {
            this.bookRepository = bookRepository;
            this.checkOutRepository = checkOutRepository;
            this.usersBooks = usersBooks;
            bookCheckedOutRepository = booksCheckedOutRepository;
        }

        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult MakeCheckOut(Book book) 
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var checkout = checkOutRepository.GetUserCheckOut(userId);
            checkout.MemberId = userId;
            checkOutRepository.Update(checkout);
            checkOutRepository.Save();
            if (!usersBooks.GetBook(userId, book.Id))
            {
                var bookcheckedout = bookCheckedOutRepository.GetById(book.Id,checkout.Id);
                if (bookcheckedout == null)
                {
                    bookcheckedout = new BooksCheckedOut
                    {
                        CheckOutId = checkout.Id,
                        BookId = book.Id,
                    };
                    bookCheckedOutRepository.Add(bookcheckedout);
                    bookCheckedOutRepository.Save();
                }
            }
            return RedirectToAction("Index","Book");
        }
    }
}
