using LibraryMVC.Models;
using LibraryMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class LoanController : Controller
    {

        private LibraryUserService LibraryUserService = new LibraryUserService();
        private BookService bookService = new BookService();
        private LoanService loanService = new LoanService();

        public ActionResult Index()
        {
            var loans = loanService.FindUnreturnedLoans();
            return View(loans);
        }


        public ActionResult Create(int bookId)
        {
            var book = bookService.GetById(bookId);
            if (book == null)
            {
                return HttpNotFound();
            }

            //class CreateBookViewModel
            ViewBag.usersList = LibraryUserService.GetAllLibraryUsersFromDbDirectly();
            ViewBag.book = book;
            
            return View(new Loan());
        }

        [HttpPost]
        public ActionResult Create(Loan loan)
        {
            loanService.LoanBook(loan);
            return RedirectToAction(nameof(Index));
        }

        // GET: Loan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Loan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Return(int id)
        {
            var loan = loanService.FindById(id);
            loanService.ReturnBook(loan);
            return RedirectToAction("Index");
        }
    }
}
