using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Enums;
using CareerBoostAI.Domain.UserUpload.ValueObjects;

namespace CareerBoostAI.Domain.UserUpload;

public class User : AggregateRoot<UserId>
{
    public string Address { get; }
    public StorageMedium Medium { get;  }
    public string FileName { get;  }
    public string Extension { get;  }
    
    public User(string address, StorageMedium medium, string fileName, string extension)
    {
        Address = address;
        Medium = medium;
        FileName = fileName;
        Extension = extension;
    }

    
}