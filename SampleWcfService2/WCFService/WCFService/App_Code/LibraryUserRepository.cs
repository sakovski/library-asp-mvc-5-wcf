using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class LibraryUserRepository
{

    private const string GetAllLibraryUsersQuery = "SELECT * FROM dbo.LibraryUser";

    public List<LibraryUser> GetAllLibraryUsers()
    {
        var libraryUsers = new List<LibraryUser>();
        using (var connection = new SqlConnection("Data Source=DESKTOP-GI8MCVU\\SQLSERVEREXPRESS;Initial Catalog=LibraryMVC;Integrated Security=True"))
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