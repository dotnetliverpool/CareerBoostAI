using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class SectionItemLocation : ValueObject
{
    public string City { get; private set; }
    public string Country { get; private set; }

    public SectionItemLocation(string city, string country)
    {
        City = city;
        Country = country;
    }

    public SectionItemLocation Create(string city, string country)
    {
        if (string.IsNullOrEmpty(city))
        {
            throw new EmptyArgumentException("SectionItemLocationCity");
        }

        if (string.IsNullOrEmpty(country))
        {
            throw new EmptyArgumentException("SectionItemLocationCountry");
        }
        return new SectionItemLocation(city, country);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return City;
        yield return Country;
    }
}