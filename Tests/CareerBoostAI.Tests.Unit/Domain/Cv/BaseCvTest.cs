using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public abstract class BaseCvTest
{
    protected ICvFactory GetCvFactory()
    {
        return new CvFactory();
    }
}