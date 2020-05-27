using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryMVC.Repositories
{
    public class BookRepository : AbstractRepository
    {
        private const string GetAllBooksQuery = "SELECT B.id, B.Title, B.Isbn, A.id, A.Firstname, A.Lastname, L.id FROM dbo.Book B " +
            "JOIN dbo.Author A on B.AuthorId = A.id " +
            "LEFT JOIN dbo.Loan L on B.LoanId = L.id";
        private const string GetBookByIdQuery = "SELECT TOP 1 id, Title, Isbn FROM dbo.Book WHERE id = @id";

        public Book GetById(int bookId)
        {
            Book book = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var sqlCommand = new SqlCommand(GetBookByIdQuery, connection);
                sqlCommand.Parameters.AddWithValue("@id", bookId);
                var dataReader = sqlCommand.ExecuteReader();

                if(dataReader.Read())
                {
                    book = new Book();
                    book.Id = dataReader.GetInt32(0);
                    book.Title = dataReader.GetString(1);
                    book.Isbn = dataReader.GetString(2);
                }
            }

            return book;
        }

        public List<Book> GetAllBooks()
        {
            return GetBooksByQuery(GetAllBooksQuery);
        }

        private List<Book> GetBooksByQuery(string query)
        {
            var books = new List<Book>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var dataReader = new SqlCommand(GetAllBooksQuery, connection).ExecuteReader();

                while (dataReader.Read())
                {
                    int loanId;
                    if (dataReader.IsDBNull(6))
                    {
                        loanId = 0;
                    } 
                    else
                    {
                        loanId = dataReader.GetInt32(6);
                    }

                    var book = new Book()
                    {
                        Id = dataReader.GetInt32(0),
                        Title = dataReader.GetString(1),
                        Isbn = dataReader.GetString(2),
                        LoanId = loanId,
                        Author = new Author()
                        {
                            Id = dataReader.GetInt32(3),
                            Firstname = dataReader.GetString(4),
                            Lastname = dataReader.GetString(5)
                        }
                    };
                    books.Add(book);
                }

                dataReader.Close();
            }

            return books;
        }
    }
}