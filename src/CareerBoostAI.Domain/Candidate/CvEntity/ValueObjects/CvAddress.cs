using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvAddress : ValueObject
{
    public string? HouseAddress { get; }
    public string? City { get; }
    public string? Postcode { get; }
    public string? Country { get; }

    private CvAddress(string? houseAddress = null, string? city = null, 
        string? country = null,
        string? postcode = null)
    {
       
        City = city;
        Postcode = postcode;
        Country = country;
        HouseAddress = houseAddress;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return HouseAddress;
        yield return City;
        yield return Postcode;
        yield return Country;
    }

    public static CvAddress Create(
        string? houseAddress, string? city, 
        string? country, string? postcode)
    {
        var addressString = city == null 
                            && postcode == null 
                            && country == null 
                            && houseAddress == null 
            ? CvEntity.Constants.NoAddressFound : houseAddress;
        
        return new CvAddress(addressString, city, country, postcode);
    }
}
