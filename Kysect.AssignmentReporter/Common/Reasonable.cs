namespace Kysect.AssignmentReporter.Common
{
    public static class Reasonable
    {
        public static Reasonable<T> Create<T>(T value) => new Reasonable<T>(value, null);
        public static Reasonable<T> Create<T>(T value, string reason) => new Reasonable<T>(value, reason);
    }
    public class Reasonable<T>
    {
        public T Value { get; set; }
        public string Reason { get; set; }

        public Reasonable(T value, string reason)
        {
            Value = value;
            Reason = reason;
        }

        public static implicit operator T(Reasonable<T> value)
        {
            return value.Value;
        }

        public string Format()
        {
            if (string.IsNullOrWhiteSpace(Reason))
                return Value.ToString();

            return $"{Value} ({Reason})";
        }
    }
}