using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int LibraryUserId { get; set; }
        public int BookId { get; set; }
        public bool IsReturned { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}