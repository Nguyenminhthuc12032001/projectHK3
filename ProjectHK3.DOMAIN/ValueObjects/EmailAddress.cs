
using ProjectHK3.Domain.ValueObjects.Base;

namespace ProjectHK3.Domain.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {
        public string? Value { get; }

        public EmailAddress(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("Email is required");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value!.ToLowerInvariant();
        }

        public override string ToString() => Value!;
    }
}
