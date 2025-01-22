using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Cv.ValueObjects;

public class EducationGrade : ValueObject
{
    public string Program { get;  }
    public string Grade { get;}
    
    private EducationGrade(string program, string grade)
    {
        Program = program;
        Grade = grade;
    }

    public static EducationGrade Create(string program, string grade)
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