using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class EducationalGrade : ValueObject
{
    public string Program { get;  }
    public string Grade { get;}
    
    private EducationalGrade(string program, string grade)
    {
        Program = program;
        Grade = grade;
    }

    public static EducationalGrade Create(string program, string grade)
    {
        program.ThrowIfNullOrEmpty("EducationalGrade.Program");
        grade.ThrowIfNullOrEmpty("EducationalGrade.Grade");
        return new(program, grade);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Program;
        yield return Grade;
    }
}