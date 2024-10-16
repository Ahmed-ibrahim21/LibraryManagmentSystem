using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSystem.Controllers
{
    public class ReturnController : Controller
    {
        IBookRepository bookRepository;
        ICheckOutRepository checkOutRepository;
        IBooksCheckedOutRepository bookCheckedOutRepository;
        IProfileRepository profileRepository;
        IReturnRepository returnRepository;
        public ReturnController(IReturnRepository returnRepository, IBookRepository bookRepository, ICheckOutRepository checkOutRepository, IBooksCheckedOutRepository booksCheckedOutRepository, IProfileRepository profileRepository)
        {
            this.bookRepository = bookRepository;
            this.checkOutRepository = checkOutRepository;
            bookCheckedOutRepository = booksCheckedOutRepository;
            this.profileRepository = profileRepository;
            this.returnRepository = returnRepository;
        }


        [Authorize(Roles = "Member")]
        public IActionResult Index()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var returns = returnRepository.GetAllreturnsForUser(userId);
            return View("myreturns", returns);
        }

        [Authorize(Roles = "Member")]
        public IActionResult MakeReturn(int id)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var returnreq = returnRepository.GetUserActiveReturns(userId);
            returnreq.MemberId = userId;
            returnRepository.Update(returnreq);
            returnRepository.Save(); // Save the Return entity first

            var bookcheckedout = bookCheckedOutRepository.Get(id);
            bookcheckedout.ReturnId = returnreq.Id; // Use the saved Return entity's Id
            bookCheckedOutRepository.Update(bookcheckedout); // Update the BooksCheckedOuts entity
            bookCheckedOutRepository.Save(); // Save the BooksCheckedOuts entity

            return RedirectToAction("Index", "Return");
        }

        public IActionResult ConfirmReturn(int id)
        {
            var returnreq = returnRepository.GetById(id);
            if (returnreq.status == 0)
            {
                returnreq.status = 1;
                returnRepository.Update(returnreq);
                returnRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult CancelReturn(int id)
        {
            var returnreq = returnRepository.GetById(id);
            if (returnreq.status == 0 || returnreq.status == 1)
            {
                List<BooksCheckedOut> booksCheckedOuts = bookCheckedOutRepository.GetBooksCheckedOutsByReturnId(returnreq.Id);
                foreach (var book in booksCheckedOuts)
                {
                    book.ReturnId = null;
                }
                bookCheckedOutRepository.Save();
                returnRepository.Delete(returnreq);
                returnRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
