using ProjectHK3.Domain.ValueObjects.Base;

namespace ProjectHK3.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string? Street { get; }
        public string? City { get; }
        public string? State { get; }
        public string? PostalCode { get; }

        public Address(string? street, string? city, string? state, string? postalCode)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street?.ToLowerInvariant();
            yield return City?.ToLowerInvariant();
            yield return State?.ToLowerInvariant();
            yield return PostalCode?.ToLowerInvariant();
        }

        public override string ToString()
            => $"{Street}, {City}, {State}, {PostalCode}".TrimEnd(',', ' ');
    }
}
