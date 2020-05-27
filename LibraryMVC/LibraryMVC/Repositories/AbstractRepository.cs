using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Repositories
{
    public abstract class AbstractRepository
    {
        //could be loaded from config file https://docs.microsoft.com/pl-pl/dotnet/framework/data/adonet/connection-strings-and-configuration-files
        protected const string ConnectionString = "Data Source=DESKTOP-GI8MCVU\\SQLSERVEREXPRESS;Initial Catalog=LibraryMVC;Integrated Security=True";
    }
}