using LibraryMVC.Models;
using LibraryMVC.Repositories;
using LibraryMVC.UserTestManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Services
{
    public class LibraryUserService
    {
        private LibraryUserRepository libraryUserRepository = new LibraryUserRepository();

        public List<Models.LibraryUser> GetAllLibraryUsersFromDbDirectly()
        {
            return libraryUserRepository.GetAllLibraryUsers();
        }

        public List<UserTestManagement.LibraryUser> GetAllLibraryUsersFromWcfService()
        {
            UserClient client = new UserClient();
            return client.GetAllUsers().ToList();
        }
    }
}