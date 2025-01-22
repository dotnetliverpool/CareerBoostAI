using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Cv.ValueObjects;

public class ProgramResult : ValueObject
{
    public string Program { get;  }
    public string Grade { get;}
    
    private ProgramResult(string program, string grade)
    {
        Program = program;
        Grade = grade;
    }

    public static ProgramResult Create(string program, string grade)
    {
        program.ThrowIfNullOrEmpty("Education.Program");
        grade.ThrowIfNullOrEmpty("Education.Grade");
        return new(program, grade);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Program;
        yield return Grade;
    }
}