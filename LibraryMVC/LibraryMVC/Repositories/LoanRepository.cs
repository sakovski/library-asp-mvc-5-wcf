using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Web;

namespace LibraryMVC.Repositories
{
    public class LoanRepository : AbstractRepository
    {
        private readonly string LoanBookProcedure = "dbo.LoanBook";
        private readonly string FindByIdQuery = "SELECT id, LoanDate, ReturnDate, IsReturned, LibraryUserId, BookId FROM dbo.Loan WHERE id = @Id";
        private readonly string ReturnLoanQuery = "UPDATE dbo.Loan SET IsReturned = 1 WHERE id = @id";
        private readonly string FreeBookQuery = "UPDATE dbo.Book SET LoanId = NULL WHERE id = @id";
        private readonly string GetAllUnreturnedLoansQuery = "SELECT id, LoanDate, ReturnDate, IsReturned, LibraryUserId, BookId FROM dbo.Loan WHERE IsReturned = 0";

        public Loan FindById(int id)
        {
            Loan loan = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(FindByIdQuery, connection);
                var idParam = new SqlParameter("@Id", id);
                cmd.Parameters.Add(idParam);
                var dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {               
                    loan = new Loan()
                    {
                        Id = dataReader.GetInt32(0),
                        LoanDate = dataReader.GetDateTime(1),
                        ReturnDate = dataReader.GetDateTime(2),
                        IsReturned = dataReader.GetBoolean(3),
                        LibraryUserId = dataReader.GetInt32(4),
                        BookId = dataReader.GetInt32(5)
                    };
                }

                return loan;
            }
        }

        public void CreateLoan(Loan loan)
        {
            using(var connection = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(LoanBookProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var bookIdParam = cmd.Parameters.Add("@BookId", SqlDbType.Int);
                bookIdParam.Value = loan.BookId;
                var libraryUserIdParam = cmd.Parameters.Add("@LibraryUserId", SqlDbType.Int);
                libraryUserIdParam.Value = loan.LibraryUserId;
                var returnDateParam = cmd.Parameters.Add("@ReturnDate", SqlDbType.DateTime);
                returnDateParam.Value = loan.ReturnDate;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ReturnLoan(Loan loan)
        {
            using (var scope = new TransactionScope())
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        var updateLoanCommand = new SqlCommand(ReturnLoanQuery, connection);
                        updateLoanCommand.Parameters.AddWithValue("@id", loan.Id);
                        updateLoanCommand.ExecuteNonQuery();

                        var updateBookCommand = new SqlCommand(FreeBookQuery, connection);
                        updateBookCommand.Parameters.AddWithValue("@id", loan.BookId);
                        updateBookCommand.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {
                       //transaction should be rolled back
                    }
                }

                scope.Complete();
            }
        }

        public List<Loan> FindUnreturnedLoans()
        {
            var loans = new List<Loan>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var dataReader = new SqlCommand(GetAllUnreturnedLoansQuery, connection).ExecuteReader();

                while (dataReader.Read())
                { 
                    var loan = new Loan()
                    {
                        Id = dataReader.GetInt32(0),
                        LoanDate = dataReader.GetDateTime(1),
                        ReturnDate = dataReader.GetDateTime(2),
                        IsReturned = dataReader.GetBoolean(3),
                        LibraryUserId = dataReader.GetInt32(4),
                        BookId = dataReader.GetInt32(5)
                    };
                    loans.Add(loan);
                }

                dataReader.Close();
            }

            return loans;
        } 
    }
}