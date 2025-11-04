namespace ProjectHK3.Domain.ValueObjects.Base
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;
            return GetEqualityComponents().SequenceEqual(((ValueObject)obj).GetEqualityComponents());
        }

        public bool Equals(ValueObject? other)
            => other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

        public override int GetHashCode()
            => GetEqualityComponents().Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return (current * 23) + (obj?.GetHashCode() ?? 0);
                }
            });

        public static bool operator ==(ValueObject? a, ValueObject? b)
            => a is null && b is null || a is not null && a.Equals(b);

        public static bool operator !=(ValueObject? a, ValueObject? b) 
            => !(a == b);
    }
}
