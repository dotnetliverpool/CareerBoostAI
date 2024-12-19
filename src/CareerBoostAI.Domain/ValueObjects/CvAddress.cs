namespace CareerBoostAI.Domain.ValueObjects;

public class CvAddress
{
    public string HouseAddress { get; }
    public string City { get; }
    public string Postcode { get; }

    public CvAddress(string houseAddress, string city, string postcode)
    {
        if (string.IsNullOrWhiteSpace(houseAddress))
            throw new ArgumentException("House address cannot be empty or whitespace.", nameof(houseAddress));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty or whitespace.", nameof(city));

        if (string.IsNullOrWhiteSpace(postcode))
            throw new ArgumentException("Postcode cannot be empty or whitespace.", nameof(postcode));

        if (postcode.Length > 10) // Example validation for postcode length
            throw new ArgumentException("Postcode cannot exceed 10 characters.", nameof(postcode));

        HouseAddress = houseAddress;
        City = city;
        Postcode = postcode;
    }

    public override bool Equals(object? obj) =>
        obj is CvAddress other &&
        HouseAddress.Equals(other.HouseAddress, StringComparison.OrdinalIgnoreCase) &&
        City.Equals(other.City, StringComparison.OrdinalIgnoreCase) &&
        Postcode.Equals(other.Postcode, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        HashCode.Combine(
            HouseAddress.ToLowerInvariant(),
            City.ToLowerInvariant(),
            Postcode.ToLowerInvariant());

    public override string ToString() => $"{HouseAddress}, {City}, {Postcode}";
}
