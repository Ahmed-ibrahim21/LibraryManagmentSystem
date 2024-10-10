﻿using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repositories;
using LibraryManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Controllers
{
    public class BookController : Controller
    {
        IBookRepository bookRepo;
        public BookController(IBookRepository bookRepository)
        {
            bookRepo = bookRepository;
        }

        public IActionResult Index()
        {
            List<Book> books = bookRepo.GetAll();
            return View("Index", books);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View("AddBook");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBook(BookViewModel bookviewmodel)
        {
            if (ModelState.IsValid) 
            {
                var book = new Book
                {
                    Title = bookviewmodel.Title,
                    quantity = bookviewmodel.Quantity
                };
                if (bookviewmodel.Img != null && bookviewmodel.Img.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await bookviewmodel.Img.CopyToAsync(memoryStream);
                        book.img = memoryStream.ToArray();
                    }
                }
                bookRepo.Add(book);
                bookRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddBook",bookviewmodel);
            }
        }

        public IActionResult GetImage(int id)
        {
            var book = bookRepo.GetById(id);
            if (book?.img != null)
            {
                return File(book.img, "image/jpeg"); // Adjust MIME type as needed (e.g., "image/png")
            }

            return NotFound(); // Return a 404 if no image exists
        }

    }
}
