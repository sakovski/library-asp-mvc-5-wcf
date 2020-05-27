using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryMVC.Repositories
{
    public class LibraryUserRepository : AbstractRepository
    {

        private const string GetAllLibraryUsersQuery = "SELECT * FROM dbo.LibraryUser";

        public List<LibraryUser> GetAllLibraryUsers()
        {
            var libraryUsers = new List<LibraryUser>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var dataReader = new SqlCommand(GetAllLibraryUsersQuery, connection).ExecuteReader();

                while (dataReader.Read())
                {
                    var libraryUser = new LibraryUser()
                    {
                        Id = dataReader.GetInt32(0),
                        Firstname = dataReader.GetString(1),
                        Lastname = dataReader.GetString(2),
                        CardIdentifier = dataReader.GetString(3),
                    };
                    libraryUsers.Add(libraryUser);
                }

                dataReader.Close();
            }

            return libraryUsers;
        }
    }
}