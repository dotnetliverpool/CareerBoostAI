using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvAddress : ValueObject
{
    public string? HouseAddress { get; }
    public string? City { get; }
    public string? Postcode { get; }
    public string? Country { get; }

    public CvAddress(string? houseAddress = null, string? city = null, 
        string? postcode = null, string? country = null)
    {
       
        City = city;
        Postcode = postcode;
        Country = country;
        HouseAddress = city == null 
                       && postcode == null 
                       && country == null 
                       && houseAddress == null 
            ? Constants.NoAddressFound : houseAddress;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return HouseAddress;
        yield return City;
        yield return Postcode;
        yield return Country;
    }
}
