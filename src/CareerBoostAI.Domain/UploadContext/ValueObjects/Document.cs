using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.UploadContext.ValueObjects;

public class Document : ValueObject
{
    private Document(string address, string medium, string fileName, string extension)
    {
        Address = address;
        Medium = medium;
        FileName = fileName;
        Extension = extension;
    }

    public string Address { get; }
    public string Medium { get;  }
    public string FileName { get;  }
    public string Extension { get;  }

    public static Document Create(string address, string medium, string fileName, string extension)
    {
        
        return new (address, medium, fileName, extension);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Address;
        yield return Medium;
        yield return FileName;
        yield return Extension;
    }
}