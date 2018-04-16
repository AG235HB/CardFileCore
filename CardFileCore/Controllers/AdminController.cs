using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardFileCore.Models;
using CardFileCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardFileCore.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        ApplicationContext db;

        public AdminController(ApplicationContext context)
        { db = context; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QueryConfirm()
        {
            if(db.BookQueries.Count()>0)
                ViewBag.RequestedBooks = db.BookQueries;

            return View();
        }

        [HttpPost]
        public IActionResult QueryConfirm(List<int> user_id)
        {
            foreach(var id in user_id)
            {
                var record = from bookQuery in db.BookQueries
                             where bookQuery.Id == id
                             select bookQuery;

                var newRecord = record.ToList();

                db.GivenBooks.Add(new GivenBook { BookName = newRecord[0].BookName, BookNumber = newRecord[0].BookNumber, UserName = newRecord[0].UserName, Validity = newRecord[0].Validity });
                db.BookQueries.Remove(newRecord[0]);

                var givenBook = from book in db.Books
                                where book.Name == newRecord[0].BookName
                                select book;
                var Book = givenBook.ToList();
                Book[0].Availible = false;

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Clear()
        {
            db.BookQueries.RemoveRange(db.BookQueries);
            db.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult ViewAvailibleBooks()
        {
            var books = from book in db.Books
                        where book.Availible
                        select book;

            if(books.Count()>0)
                ViewBag.AvailibleBooks = books;

            return View();
        }

        [HttpGet]
        public IActionResult ViewGivenBooks()
        {
            if (db.GivenBooks.Count() > 0)
                ViewBag.GivenBooks = db.GivenBooks;

            return View();
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View(new AddBookViewModel { });
        }

        [HttpGet]
        public IActionResult AddBookError()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook(AddBookViewModel model)
        {
            if(ModelState.IsValid)
            {
                var number = from book in db.Books
                             where book.Number == model.Number
                             select book.Number;

                if (number.Count() == 0)
                {
                    db.Books.Add(new Book { Name = model.Name, Number = model.Number, Availible = true });
                    db.SaveChanges();
                }
                else
                    return RedirectToAction("AddBookError", "Admin");
            }

            return RedirectToAction("Index","Admin");
        }
    }
}