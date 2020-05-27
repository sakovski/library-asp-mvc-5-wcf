using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class LibraryUser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CardIdentifier { get; set; }
        public string Representation 
        { 
            get
            {
                return Firstname + " " + Lastname + " " + CardIdentifier;
            }
        }
    }
}