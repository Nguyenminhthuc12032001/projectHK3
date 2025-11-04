using ProjectHK3.Domain.ValueObjects.Base;

namespace ProjectHK3.Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public string? Currency { get; }
        public decimal Amount { get; }

        public Money(string? currency, decimal amount)
        {
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Amount = amount >= 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative");
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Currency!.ToUpperInvariant();
            yield return Amount;
        }

        public Money Add(Money other)
        {
            if (!Currency!.Equals(other.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Currency mismatch");
            return new Money(Currency, Amount + other.Amount);
        }
    }
}
