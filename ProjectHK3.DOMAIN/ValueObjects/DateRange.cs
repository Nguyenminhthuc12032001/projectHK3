using ProjectHK3.Domain.ValueObjects.Base;

namespace ProjectHK3.Domain.ValueObjects
{
    internal sealed class DateRange : ValueObject
    {
        public DateOnly Start { get; }

        public DateOnly End { get; }

        public DateRange(DateOnly start, DateOnly end)
        {
            if (end < start) throw new ArgumentException("End must be >= start.");
            Start = start;
            End = end;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }

        public bool Overlaps(DateRange other)
            => Start <= other.End && other.Start  <= End;
    }
}
