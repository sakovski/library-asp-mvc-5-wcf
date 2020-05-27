using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace LibraryMVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public Author Author { get; set; }

        public int LoanId { get; set; }
    }
}