using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;
using System.Web;

[ServiceContract]
public interface IUser
{

    [OperationContract(IsOneWay = true)]
    void SomeBigTask();

    [OperationContract]
    List<LibraryUser> GetAllUsers();
}


[ServiceContract(CallbackContract = typeof(IProcessCallback))]
public interface IProcess
{
    [OperationContract]
    void TaskProcess();
}

public interface IProcessCallback
{
    [OperationContract]
    void TaskProgress(int percentDone);
}

[DataContract]
public class LibraryUser
{
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string Firstname { get; set; }
    [DataMember]
    public string Lastname { get; set; }
    [DataMember]
    public string CardIdentifier { get; set; }
}

