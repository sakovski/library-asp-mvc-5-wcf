using LibraryMVC.Models;
using LibraryMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LibraryMVC.Services
{
    public class BookService
    {
        private BookRepository bookRepository = new BookRepository();

        public List<Book> GetAllBooks()
        {
            return bookRepository.GetAllBooks();
        }

        public Book GetById(int bookId)
        {
            return bookRepository.GetById(bookId);
        }
    }
}