using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;

public class User : IUser
{

    private LibraryUserRepository libraryUserRepository = new LibraryUserRepository();

    public List<LibraryUser> GetAllUsers()
    {
        return libraryUserRepository.GetAllLibraryUsers();
    }

    public void SomeBigTask()
    {
        Thread.Sleep(5000);
    }

    public void TaskProcess()
    {
        for (int i = 1; i <= 100; i++)
        {
            Thread.Sleep(50);
            OperationContext.Current.GetCallbackChannel<IProcessCallback>().TaskProgress(i);
        }
    }
}