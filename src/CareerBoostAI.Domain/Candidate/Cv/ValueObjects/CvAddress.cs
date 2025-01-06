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
       // TODO: Use a Constant
        HouseAddress = houseAddress ?? "No House Address";
        City = city;
        Postcode = postcode;
        Country = country;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return HouseAddress;
        yield return City;
        yield return Postcode;
        yield return Country;
    }
}
