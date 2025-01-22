using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class Location : ValueObject
{
    public string City { get; private set; }
    public string Country { get; private set; }

    private Location(string city, string country)
    {
        City = city;
        Country = country;
    }

    public static Location Create(string city, string country)
    {
        city.ThrowIfNullOrEmpty("Location.City");
        country.ThrowIfNullOrEmpty("Location.Country");
        return new Location(city, country);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return City;
        yield return Country;
    }
}