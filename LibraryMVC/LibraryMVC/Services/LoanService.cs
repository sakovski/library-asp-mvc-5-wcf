using LibraryMVC.Models;
using LibraryMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Services
{
    public class LoanService
    {
        private LoanRepository loanRepository = new LoanRepository();

        public void LoanBook(Loan loan)
        {
            loanRepository.CreateLoan(loan);
        }

        public void ReturnBook(Loan loan)
        {
            loanRepository.ReturnLoan(loan);
        }

        public List<Loan> FindUnreturnedLoans()
        {
            return loanRepository.FindUnreturnedLoans();
        }

        public Loan FindById(int id)
        {
            return loanRepository.FindById(id);
        }
    }
}