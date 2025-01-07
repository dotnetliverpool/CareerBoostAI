using System.Diagnostics.Contracts;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Enums;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvFile : ValueObject
{
    public string Name { get; private set; }
    public CvStorageMedium StorageMedium { get; private set; }  
    public string StorageAddress { get; private set; } 
    

    
    private CvFile(string name, CvStorageMedium storageMedium, string storageAddress)
    {
        Name = name;
        StorageMedium = storageMedium;
        StorageAddress = storageAddress;
    }

    
    public static CvFile Create(string name,  CvStorageMedium storageMedium, string storageAddress)
    {
        Validate(name, storageAddress);
        return new CvFile(name, storageMedium, storageAddress);
    }

    private static void Validate(string name, string storageAddress)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new EmptyArgumentException("File Name");
        }

        if (string.IsNullOrEmpty(storageAddress))
        {
            throw new EmptyArgumentException("File Storage Address");
        }
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return StorageMedium;
        yield return StorageAddress;
    }
}
