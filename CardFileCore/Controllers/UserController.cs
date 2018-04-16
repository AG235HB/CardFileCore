using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardFileCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardFileCore.Controllers
{
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {
        ApplicationContext db;

        public UserController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            //var username = User.Identity.Name;

            var givenBooks = from givenBook in db.GivenBooks
                             where givenBook.UserName == User.Identity.Name
                             select givenBook;

            //foreach (GivenBook gb in givenBooks)
            //    Console.WriteLine(gb.BookName);

            if (givenBooks.Count() != 0)
                ViewBag.GivenBooks = givenBooks;

            return View();
        }

        [HttpGet]
        public IActionResult BookQuery()
        {
            var availibleBooks = from book in db.Books
                                 where book.Availible
                                 select book;

            ViewBag.AvailibleBooks = availibleBooks;

            return View();
        }

        [HttpPost]
        public IActionResult BookQuery(/*BookQuery bookQuery, */List<int> book_id, string requestedDate)
        {
            //BookQuery bookQuery

            foreach (var id in book_id)
            {
                var Book = from book in db.Books
                              where book.Id == id
                              select book;

                var newBook = Book.ToList();

                db.BookQueries.Add(new BookQuery { BookName = newBook[0].Name, BookNumber = newBook[0].Number, UserName = User.Identity.Name, Validity = DateTime.Parse(requestedDate) });

                //этим занимается админ
                //newBook[0].Availible = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult ReturnBook(int id)
        {
            GivenBook returnableBook = new GivenBook();
            IEnumerable<GivenBook> givenBooks = db.GivenBooks;
            foreach (var book in givenBooks)
                if (book.Id == id)
                    returnableBook = book;

            var rBook = from book in db.Books
                        where book.Number == returnableBook.BookNumber
                        select book;
            var retBook = rBook.ToList();
            retBook[0].Availible = true;
            db.GivenBooks.Remove(returnableBook);
            db.SaveChanges();

            return RedirectToAction("Index", "User");
        }
    }
}