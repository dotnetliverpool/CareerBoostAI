namespace CareerBoostAI.Domain.Common.Exceptions;

public class UnsupportedFileTypeException : CareerBoostAIDomainException
{
    public UnsupportedFileTypeException(string fileName) : base($"{fileName} is an unsupported file type.")
    {
    }
}