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
        IProfileRepository profileRepository;
        public BookingController(IUsersBooks usersBooks,IBookRepository bookRepository, ICheckOutRepository checkOutRepository, IBooksCheckedOutRepository booksCheckedOutRepository, IProfileRepository profileRepository)
        {
            this.bookRepository = bookRepository;
            this.checkOutRepository = checkOutRepository;
            this.usersBooks = usersBooks;
            bookCheckedOutRepository = booksCheckedOutRepository;
            this.profileRepository = profileRepository;
        }

        public IActionResult Index() 
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var CheckOuts = checkOutRepository.GetAllCheckOutsForUser(userId);
            return View("mycheckouts",CheckOuts);
        }

        [Authorize(Roles = "Member")]
        public IActionResult MakeCheckOut(Book book) 
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var checkout = checkOutRepository.GetUserActiveCheckOut(userId);
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
                        ProfileId = profileRepository.GetById(userId).Id,
                        status = 0
                    };
                    bookCheckedOutRepository.Add(bookcheckedout);
                    bookCheckedOutRepository.Save();
                }
            }
            return RedirectToAction("Index","Book");
        }

        public IActionResult ConfirmCheckOut(int id)
        {
            var checkout = checkOutRepository.GetById(id);
            if(checkout.status == 0)
            {
                checkout.status = 1;
                checkOutRepository.Update(checkout);
                checkOutRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult CancelCheckOut(int id)
        {
            var checkout = checkOutRepository.GetById(id);
            if (checkout.status == 0 || checkout.status == 1)
            {
                List<BooksCheckedOut> booksCheckedOuts = bookCheckedOutRepository.GetBooksCheckedOutByCheckoutId(checkout.Id);
                foreach (var book in booksCheckedOuts)
                {
                    bookCheckedOutRepository.Delete(book);
                }
                checkOutRepository.Delete(checkout);
                checkOutRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Librarian")]
        public IActionResult ManangePendingCheckOuts()
        {
            var checkouts = checkOutRepository.GetAllPendingCheckOuts();
            return View("ManagePendingCheckOuts",checkouts);
        }
    }
}
