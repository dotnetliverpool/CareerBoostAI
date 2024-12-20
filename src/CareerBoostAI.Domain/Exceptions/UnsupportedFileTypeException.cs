namespace CareerBoostAI.Domain.Exceptions;

public class UnsupportedFileTypeException : CareerBoostAIDomainException
{
    public UnsupportedFileTypeException(string fileName) : base($"{fileName} is an unsupported file type.")
    {
    }
}