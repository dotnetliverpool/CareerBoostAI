namespace CareerBoostAI.Domain.Common.Abstractions.SpecificationPattern;

public abstract class Specification<T>
{
    public abstract bool IsSatisfiedBy(T candidate);

    public Specification<T> And(Specification<T> other)
    {
        return new AndSpecification<T>(this, other);
    }

    public Specification<T> Or(Specification<T> other)
    {
        return new OrSpecification<T>(this, other);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}
