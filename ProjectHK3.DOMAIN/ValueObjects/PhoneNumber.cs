using ProjectHK3.Domain.ValueObjects.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ProjectHK3.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        [Column(TypeName ="VARCHAR(50)")]
        public string Value { get; }

        private static readonly Regex E164Regex =
            new Regex(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

        private PhoneNumber()
        {
            Value = string.Empty;
        }

        public PhoneNumber(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), "Phone number is required");
            }

            var cleaned = value
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Trim();

            if (!cleaned.StartsWith("+"))
            {
                throw new ArgumentException("Phone number must include country code (+...)");
            }

            if (!E164Regex.IsMatch(cleaned))
            {
                throw new ArgumentException("Invalid international phone number format (E.164).");
            }

            Value = cleaned;
        }
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string? ToString() => Value;
    }
}
